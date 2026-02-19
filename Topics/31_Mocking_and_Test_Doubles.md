# Topic 31: Mocking and Test Doubles

## Introduction

When unit testing, you want to test a class in **isolation**. But most classes depend on other classes (databases, APIs, file systems). Mocking allows you to replace those dependencies with controlled fake versions.

## Why Mock?

```csharp
// This class is hard to test - it depends on a real database
public class OrderService
{
    private readonly SqlConnection _db;

    public OrderService()
    {
        _db = new SqlConnection("connection-string");
    }

    public Order GetOrder(int id)
    {
        // Queries real database - slow, requires setup
    }
}
```

**Problems with testing real dependencies:**
- **Slow**: Database calls take time
- **Flaky**: Network issues cause random failures
- **Complex setup**: Need test data in database
- **Side effects**: Tests modify real data

**Solution: Replace dependencies with mocks**

## Test Doubles Overview

| Type | Description | Use Case |
|------|-------------|----------|
| **Dummy** | Placeholder that's never used | Fill required parameters |
| **Stub** | Returns predefined values | Provide test data |
| **Mock** | Verifies interactions | Check method was called |
| **Fake** | Working implementation (simplified) | In-memory database |
| **Spy** | Records calls for later verification | Audit method calls |

## Step 1: Design for Testability

Use **Dependency Injection** - accept dependencies through constructor:

```csharp
// Bad - hard to test (creates its own dependency)
public class OrderService
{
    private readonly OrderRepository _repo = new OrderRepository();
}

// Good - testable (accepts dependency)
public class OrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }
}
```

## Step 2: Define Interfaces

```csharp
public interface IOrderRepository
{
    Order? GetById(int id);
    IEnumerable<Order> GetAll();
    void Add(Order order);
    void Update(Order order);
    void Delete(int id);
}

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public interface IPaymentGateway
{
    PaymentResult ProcessPayment(decimal amount, string cardNumber);
}
```

## Step 3: Use Moq Framework

Moq is the most popular mocking framework for .NET.

```bash
# Add to test project
dotnet add package Moq
```

### Basic Mock Setup

```csharp
using Moq;
using Xunit;

public class OrderServiceTests
{
    [Fact]
    public void GetOrder_ExistingId_ReturnsOrder()
    {
        // Arrange - Create mock
        var mockRepo = new Mock<IOrderRepository>();

        // Setup mock behavior
        mockRepo.Setup(r => r.GetById(1))
                .Returns(new Order { Id = 1, Total = 100m });

        // Inject mock into service
        var service = new OrderService(mockRepo.Object);

        // Act
        var order = service.GetOrder(1);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(1, order.Id);
    }
}
```

## Mock Setup Patterns

### Return Specific Value

```csharp
var mock = new Mock<IUserRepository>();

// Return specific user for specific ID
mock.Setup(r => r.GetById(1))
    .Returns(new User { Id = 1, Name = "John" });

// Return null for non-existent user
mock.Setup(r => r.GetById(999))
    .Returns((User?)null);
```

### Match Any Argument

```csharp
// It.IsAny<T>() matches any value of type T
mock.Setup(r => r.GetById(It.IsAny<int>()))
    .Returns(new User { Id = 1, Name = "Default" });

// It.Is<T>() matches values that satisfy a condition
mock.Setup(r => r.GetById(It.Is<int>(id => id > 0)))
    .Returns(new User { Id = 1, Name = "Valid User" });
```

### Return Different Values

```csharp
// Return based on input
mock.Setup(r => r.GetById(It.IsAny<int>()))
    .Returns((int id) => new User { Id = id, Name = $"User {id}" });

// Return sequence of values
var sequence = new Queue<User>(new[]
{
    new User { Id = 1 },
    new User { Id = 2 }
});

mock.Setup(r => r.GetNext())
    .Returns(() => sequence.Dequeue());
```

### Throw Exceptions

```csharp
mock.Setup(r => r.GetById(-1))
    .Throws<ArgumentException>();

mock.Setup(r => r.GetById(999))
    .Throws(new KeyNotFoundException("User not found"));
```

### Async Methods

```csharp
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
}

// Setup async methods
mock.Setup(r => r.GetByIdAsync(1))
    .ReturnsAsync(new User { Id = 1 });

mock.Setup(r => r.GetAllAsync())
    .ReturnsAsync(new List<User> { user1, user2 });

// Async exception
mock.Setup(r => r.GetByIdAsync(-1))
    .ThrowsAsync(new ArgumentException());
```

## Verification

Verify that methods were called (or not called):

```csharp
[Fact]
public void CreateOrder_ValidOrder_SavesAndSendsEmail()
{
    var mockRepo = new Mock<IOrderRepository>();
    var mockEmail = new Mock<IEmailService>();

    var service = new OrderService(mockRepo.Object, mockEmail.Object);

    service.CreateOrder(new Order { CustomerEmail = "test@test.com" });

    // Verify Add was called exactly once
    mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.Once);

    // Verify email was sent
    mockEmail.Verify(e => e.SendEmail(
        "test@test.com",
        It.IsAny<string>(),
        It.IsAny<string>()
    ), Times.Once);
}
```

### Verification Options

```csharp
// Times options
mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.Once);
mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.Exactly(2));
mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.Never);
mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.AtLeastOnce);
mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.AtMost(3));
mockRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.Between(1, 5, Range.Inclusive));

// Verify no other calls were made
mockRepo.VerifyNoOtherCalls();
```

### Verify Argument Values

```csharp
[Fact]
public void SendWelcomeEmail_NewUser_SendsCorrectEmail()
{
    var mockEmail = new Mock<IEmailService>();
    var service = new UserService(mockEmail.Object);

    service.RegisterUser("john@example.com", "John");

    mockEmail.Verify(e => e.SendEmail(
        "john@example.com",           // Exact value
        It.Is<string>(s => s.Contains("Welcome")),  // Contains "Welcome"
        It.IsAny<string>()
    ));
}
```

## Complete Example

```csharp
// Service to test
public class OrderProcessor
{
    private readonly IOrderRepository _orderRepo;
    private readonly IInventoryService _inventory;
    private readonly IPaymentGateway _payment;
    private readonly IEmailService _email;

    public OrderProcessor(
        IOrderRepository orderRepo,
        IInventoryService inventory,
        IPaymentGateway payment,
        IEmailService email)
    {
        _orderRepo = orderRepo;
        _inventory = inventory;
        _payment = payment;
        _email = email;
    }

    public OrderResult ProcessOrder(Order order)
    {
        // Check inventory
        foreach (var item in order.Items)
        {
            if (!_inventory.IsInStock(item.ProductId, item.Quantity))
            {
                return new OrderResult(false, "Item out of stock");
            }
        }

        // Process payment
        var paymentResult = _payment.ProcessPayment(
            order.Total,
            order.PaymentCard
        );

        if (!paymentResult.Success)
        {
            return new OrderResult(false, "Payment failed");
        }

        // Reserve inventory
        foreach (var item in order.Items)
        {
            _inventory.Reserve(item.ProductId, item.Quantity);
        }

        // Save order
        order.Status = OrderStatus.Confirmed;
        order.PaymentReference = paymentResult.TransactionId;
        _orderRepo.Add(order);

        // Send confirmation email
        _email.SendEmail(
            order.CustomerEmail,
            "Order Confirmation",
            $"Your order {order.Id} has been confirmed."
        );

        return new OrderResult(true, "Order processed successfully");
    }
}

// Tests
public class OrderProcessorTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepo;
    private readonly Mock<IInventoryService> _mockInventory;
    private readonly Mock<IPaymentGateway> _mockPayment;
    private readonly Mock<IEmailService> _mockEmail;
    private readonly OrderProcessor _processor;

    public OrderProcessorTests()
    {
        _mockOrderRepo = new Mock<IOrderRepository>();
        _mockInventory = new Mock<IInventoryService>();
        _mockPayment = new Mock<IPaymentGateway>();
        _mockEmail = new Mock<IEmailService>();

        _processor = new OrderProcessor(
            _mockOrderRepo.Object,
            _mockInventory.Object,
            _mockPayment.Object,
            _mockEmail.Object
        );
    }

    private Order CreateValidOrder()
    {
        return new Order
        {
            Id = 1,
            CustomerEmail = "customer@test.com",
            PaymentCard = "4111111111111111",
            Total = 100m,
            Items = new List<OrderItem>
            {
                new() { ProductId = "PROD1", Quantity = 2 }
            }
        };
    }

    [Fact]
    public void ProcessOrder_AllValid_ReturnsSuccess()
    {
        // Arrange
        var order = CreateValidOrder();

        _mockInventory.Setup(i => i.IsInStock(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(true);

        _mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult { Success = true, TransactionId = "TXN123" });

        // Act
        var result = _processor.ProcessOrder(order);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void ProcessOrder_OutOfStock_ReturnsFailure()
    {
        var order = CreateValidOrder();

        _mockInventory.Setup(i => i.IsInStock("PROD1", 2))
            .Returns(false);

        var result = _processor.ProcessOrder(order);

        Assert.False(result.Success);
        Assert.Contains("out of stock", result.Message.ToLower());

        // Verify payment was never attempted
        _mockPayment.Verify(p => p.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void ProcessOrder_PaymentFails_ReturnsFailure()
    {
        var order = CreateValidOrder();

        _mockInventory.Setup(i => i.IsInStock(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(true);

        _mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult { Success = false });

        var result = _processor.ProcessOrder(order);

        Assert.False(result.Success);
        Assert.Contains("payment", result.Message.ToLower());

        // Verify inventory was not reserved
        _mockInventory.Verify(i => i.Reserve(It.IsAny<string>(), It.IsAny<int>()),
            Times.Never);
    }

    [Fact]
    public void ProcessOrder_Success_SavesOrderWithPaymentReference()
    {
        var order = CreateValidOrder();

        _mockInventory.Setup(i => i.IsInStock(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(true);

        _mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult { Success = true, TransactionId = "TXN123" });

        _processor.ProcessOrder(order);

        _mockOrderRepo.Verify(r => r.Add(It.Is<Order>(o =>
            o.Status == OrderStatus.Confirmed &&
            o.PaymentReference == "TXN123"
        )), Times.Once);
    }

    [Fact]
    public void ProcessOrder_Success_SendsConfirmationEmail()
    {
        var order = CreateValidOrder();

        _mockInventory.Setup(i => i.IsInStock(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(true);

        _mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult { Success = true, TransactionId = "TXN123" });

        _processor.ProcessOrder(order);

        _mockEmail.Verify(e => e.SendEmail(
            "customer@test.com",
            It.Is<string>(s => s.Contains("Confirmation")),
            It.Is<string>(s => s.Contains("confirmed"))
        ), Times.Once);
    }

    [Fact]
    public void ProcessOrder_Success_ReservesAllItems()
    {
        var order = CreateValidOrder();
        order.Items.Add(new OrderItem { ProductId = "PROD2", Quantity = 1 });

        _mockInventory.Setup(i => i.IsInStock(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(true);

        _mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult { Success = true, TransactionId = "TXN123" });

        _processor.ProcessOrder(order);

        _mockInventory.Verify(i => i.Reserve("PROD1", 2), Times.Once);
        _mockInventory.Verify(i => i.Reserve("PROD2", 1), Times.Once);
    }
}
```

## Mocking Properties

```csharp
public interface IConfiguration
{
    string ConnectionString { get; }
    int MaxRetries { get; set; }
}

var mockConfig = new Mock<IConfiguration>();

// Setup property getter
mockConfig.Setup(c => c.ConnectionString).Returns("test-connection");

// Setup property to track changes
mockConfig.SetupProperty(c => c.MaxRetries, 3);

// Now property works like a real property
mockConfig.Object.MaxRetries = 5;
Assert.Equal(5, mockConfig.Object.MaxRetries);
```

## Callbacks

Execute code when a method is called:

```csharp
var savedOrders = new List<Order>();

mockRepo.Setup(r => r.Add(It.IsAny<Order>()))
    .Callback<Order>(order => savedOrders.Add(order));

// Now when Add is called, the order is captured
service.CreateOrder(new Order { Id = 1 });

Assert.Single(savedOrders);
Assert.Equal(1, savedOrders[0].Id);
```

## Strict vs Loose Mocks

```csharp
// Loose (default) - undefined methods return default values
var loose = new Mock<IService>();
var result = loose.Object.GetValue();  // Returns default (null, 0, false)

// Strict - throws exception for undefined methods
var strict = new Mock<IService>(MockBehavior.Strict);
var result = strict.Object.GetValue();  // Throws MockException
```

## Best Practices

### 1. Don't Mock What You Don't Own

```csharp
// Bad - mocking third-party class
var mockHttpClient = new Mock<HttpClient>();  // Don't do this

// Good - wrap in your own interface
public interface IApiClient
{
    Task<string> GetAsync(string url);
}

public class HttpApiClient : IApiClient
{
    private readonly HttpClient _client;
    public async Task<string> GetAsync(string url) =>
        await _client.GetStringAsync(url);
}

// Now mock your interface
var mockApi = new Mock<IApiClient>();
```

### 2. Keep Mocks Simple

```csharp
// Bad - overly complex setup
mock.Setup(x => x.Method(It.Is<string>(s =>
    s.Length > 5 &&
    s.Contains("test") &&
    Regex.IsMatch(s, @"\d+")
))).Returns(/* complex logic */);

// Good - simple, focused setup
mock.Setup(x => x.Method("test-123")).Returns("result");
```

### 3. Verify Behavior, Not Implementation

```csharp
// Bad - testing implementation details
mockRepo.Verify(r => r.OpenConnection(), Times.Once);
mockRepo.Verify(r => r.ExecuteQuery(It.IsAny<string>()), Times.Once);
mockRepo.Verify(r => r.CloseConnection(), Times.Once);

// Good - testing observable behavior
mockRepo.Verify(r => r.Save(It.Is<User>(u => u.Name == "John")), Times.Once);
```

## Summary

| Concept | Purpose |
|---------|---------|
| Mock | Replace dependencies in tests |
| Setup | Define what mock returns |
| Verify | Check mock was called correctly |
| It.IsAny<T>() | Match any argument |
| Times | Specify call count expectations |
| Callback | Execute code on mock call |

Mocking is essential for writing fast, reliable unit tests that isolate the code under test.
