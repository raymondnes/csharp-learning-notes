# Topic 33: SOLID Principles

## Introduction

SOLID is an acronym for five design principles that make software more maintainable, flexible, and scalable. These principles are fundamental to professional software development.

## S - Single Responsibility Principle (SRP)

**A class should have only one reason to change.**

### Bad Example

```csharp
// This class does too much
public class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    // Responsibility 1: Employee data
    public void UpdateName(string name) => Name = name;

    // Responsibility 2: Salary calculation
    public decimal CalculateTax() => Salary * 0.3m;

    // Responsibility 3: Persistence
    public void SaveToDatabase()
    {
        // Database logic
    }

    // Responsibility 4: Reporting
    public string GenerateReport()
    {
        return $"Employee: {Name}, Salary: {Salary}";
    }
}
```

### Good Example

```csharp
// Employee data only
public class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
}

// Tax calculation
public class TaxCalculator
{
    public decimal CalculateTax(Employee employee)
    {
        return employee.Salary * 0.3m;
    }
}

// Persistence
public class EmployeeRepository
{
    public void Save(Employee employee)
    {
        // Database logic
    }
}

// Reporting
public class EmployeeReportGenerator
{
    public string Generate(Employee employee)
    {
        return $"Employee: {employee.Name}, Salary: {employee.Salary}";
    }
}
```

## O - Open/Closed Principle (OCP)

**Classes should be open for extension but closed for modification.**

### Bad Example

```csharp
public class DiscountCalculator
{
    public decimal CalculateDiscount(string customerType, decimal amount)
    {
        // Adding new customer type requires modifying this method
        if (customerType == "Regular")
            return amount * 0.1m;
        else if (customerType == "Premium")
            return amount * 0.2m;
        else if (customerType == "VIP")
            return amount * 0.3m;
        // New type? Modify this code...
        return 0;
    }
}
```

### Good Example

```csharp
public interface IDiscountStrategy
{
    decimal CalculateDiscount(decimal amount);
}

public class RegularDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal amount) => amount * 0.1m;
}

public class PremiumDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal amount) => amount * 0.2m;
}

public class VIPDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal amount) => amount * 0.3m;
}

// Adding new discount type doesn't modify existing code
public class GoldDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal amount) => amount * 0.25m;
}

public class DiscountCalculator
{
    private readonly IDiscountStrategy _strategy;

    public DiscountCalculator(IDiscountStrategy strategy)
    {
        _strategy = strategy;
    }

    public decimal CalculateDiscount(decimal amount)
    {
        return _strategy.CalculateDiscount(amount);
    }
}
```

## L - Liskov Substitution Principle (LSP)

**Subtypes must be substitutable for their base types.**

### Bad Example

```csharp
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying...");
    }
}

public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotSupportedException("Penguins can't fly!");  // Violates LSP
    }
}

// Code expecting Bird breaks with Penguin
public void MakeBirdFly(Bird bird)
{
    bird.Fly();  // Throws for Penguin
}
```

### Good Example

```csharp
public abstract class Bird
{
    public abstract void Move();
}

public interface IFlyable
{
    void Fly();
}

public class Sparrow : Bird, IFlyable
{
    public override void Move() => Fly();
    public void Fly() => Console.WriteLine("Flying...");
}

public class Penguin : Bird
{
    public override void Move() => Console.WriteLine("Swimming...");
}

// Now code works with any Bird
public void MakeBirdMove(Bird bird)
{
    bird.Move();  // Works for all birds
}
```

## I - Interface Segregation Principle (ISP)

**Clients should not be forced to depend on interfaces they don't use.**

### Bad Example

```csharp
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
    void TakeBreak();
}

// Robot doesn't eat or sleep
public class Robot : IWorker
{
    public void Work() => Console.WriteLine("Working...");
    public void Eat() => throw new NotSupportedException();  // Forced to implement
    public void Sleep() => throw new NotSupportedException();  // Forced to implement
    public void TakeBreak() { }
}
```

### Good Example

```csharp
public interface IWorkable
{
    void Work();
}

public interface IFeedable
{
    void Eat();
}

public interface IRestable
{
    void Sleep();
    void TakeBreak();
}

public class Human : IWorkable, IFeedable, IRestable
{
    public void Work() => Console.WriteLine("Working...");
    public void Eat() => Console.WriteLine("Eating...");
    public void Sleep() => Console.WriteLine("Sleeping...");
    public void TakeBreak() => Console.WriteLine("Taking break...");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Working...");
    // No need to implement Eat or Sleep
}
```

## D - Dependency Inversion Principle (DIP)

**High-level modules should not depend on low-level modules. Both should depend on abstractions.**

### Bad Example

```csharp
// High-level module depends on concrete low-level module
public class OrderService
{
    private readonly SqlDatabase _database;  // Concrete dependency
    private readonly SmtpEmailSender _emailSender;  // Concrete dependency

    public OrderService()
    {
        _database = new SqlDatabase();
        _emailSender = new SmtpEmailSender();
    }

    public void CreateOrder(Order order)
    {
        _database.Save(order);
        _emailSender.Send(order.CustomerEmail, "Order created");
    }
}
```

### Good Example

```csharp
// Abstractions
public interface IDatabase
{
    void Save<T>(T entity);
}

public interface IEmailSender
{
    void Send(string to, string message);
}

// High-level module depends on abstractions
public class OrderService
{
    private readonly IDatabase _database;
    private readonly IEmailSender _emailSender;

    public OrderService(IDatabase database, IEmailSender emailSender)
    {
        _database = database;
        _emailSender = emailSender;
    }

    public void CreateOrder(Order order)
    {
        _database.Save(order);
        _emailSender.Send(order.CustomerEmail, "Order created");
    }
}

// Low-level modules implement abstractions
public class SqlDatabase : IDatabase
{
    public void Save<T>(T entity) { /* SQL implementation */ }
}

public class MongoDatabase : IDatabase
{
    public void Save<T>(T entity) { /* MongoDB implementation */ }
}

public class SmtpEmailSender : IEmailSender
{
    public void Send(string to, string message) { /* SMTP implementation */ }
}

public class SendGridEmailSender : IEmailSender
{
    public void Send(string to, string message) { /* SendGrid implementation */ }
}
```

## Complete SOLID Example

```csharp
// Domain entities
public class Order
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus { Pending, Confirmed, Shipped, Delivered }

// Interfaces (DIP + ISP)
public interface IOrderRepository
{
    Order? GetById(int id);
    void Save(Order order);
}

public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount, string paymentMethod);
}

public interface INotificationService
{
    void NotifyCustomer(string customerId, string message);
}

public interface IInventoryService
{
    bool CheckAvailability(List<OrderItem> items);
    void ReserveItems(List<OrderItem> items);
}

// Single Responsibility - each class has one job
public class OrderValidator
{
    private readonly IInventoryService _inventory;

    public OrderValidator(IInventoryService inventory)
    {
        _inventory = inventory;
    }

    public ValidationResult Validate(Order order)
    {
        if (!order.Items.Any())
            return ValidationResult.Failure("Order must have items");

        if (!_inventory.CheckAvailability(order.Items))
            return ValidationResult.Failure("Items not available");

        return ValidationResult.Success();
    }
}

public class OrderPriceCalculator
{
    public decimal Calculate(Order order)
    {
        var subtotal = order.Items.Sum(i => i.Price * i.Quantity);
        var tax = subtotal * 0.08m;
        return subtotal + tax;
    }
}

// Open/Closed - payment strategies
public interface IPaymentStrategy
{
    bool Process(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public bool Process(decimal amount) { /* ... */ return true; }
}

public class PayPalPayment : IPaymentStrategy
{
    public bool Process(decimal amount) { /* ... */ return true; }
}

// Main service using all principles
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notifications;
    private readonly IInventoryService _inventory;
    private readonly OrderValidator _validator;
    private readonly OrderPriceCalculator _calculator;

    public OrderService(
        IOrderRepository repository,
        INotificationService notifications,
        IInventoryService inventory,
        OrderValidator validator,
        OrderPriceCalculator calculator)
    {
        _repository = repository;
        _notifications = notifications;
        _inventory = inventory;
        _validator = validator;
        _calculator = calculator;
    }

    public OrderResult CreateOrder(Order order, IPaymentStrategy paymentStrategy)
    {
        // Validate
        var validation = _validator.Validate(order);
        if (!validation.IsValid)
            return OrderResult.Failure(validation.Error);

        // Calculate total
        order.Total = _calculator.Calculate(order);

        // Process payment
        if (!paymentStrategy.Process(order.Total))
            return OrderResult.Failure("Payment failed");

        // Reserve inventory
        _inventory.ReserveItems(order.Items);

        // Save order
        order.Status = OrderStatus.Confirmed;
        _repository.Save(order);

        // Notify customer
        _notifications.NotifyCustomer(order.CustomerId, "Order confirmed!");

        return OrderResult.Success(order);
    }
}
```

## Benefits of SOLID

| Principle | Benefit |
|-----------|---------|
| SRP | Easier to understand, test, and maintain |
| OCP | Add features without breaking existing code |
| LSP | Reliable polymorphism, no surprises |
| ISP | Cleaner interfaces, less coupling |
| DIP | Testable, swappable dependencies |

## Summary

SOLID principles guide you toward code that is:
- **Maintainable**: Easy to change
- **Testable**: Dependencies can be mocked
- **Flexible**: New features don't break existing code
- **Scalable**: Components can be replaced or upgraded

These principles are the foundation of professional C# development.
