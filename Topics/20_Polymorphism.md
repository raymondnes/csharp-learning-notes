# Polymorphism

## Introduction

Polymorphism, meaning "many forms," is a core OOP principle that allows objects of different types to be treated as objects of a common base type. It enables you to write flexible, extensible code that can work with objects of various types through a unified interface.

## Types of Polymorphism

1. **Compile-time (Static)**: Method overloading
2. **Runtime (Dynamic)**: Method overriding with virtual/override

## Method Overriding (Runtime Polymorphism)

The most common form - derived classes provide specific implementations:

```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal makes a sound");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog barks: Woof!");
    }
}

class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Cat meows: Meow!");
    }
}

// Polymorphism in action
Animal myAnimal = new Dog();
myAnimal.Speak();  // "Dog barks: Woof!" - calls Dog's version

myAnimal = new Cat();
myAnimal.Speak();  // "Cat meows: Meow!" - calls Cat's version
```

## The virtual and override Keywords

- **virtual**: Declares a method can be overridden
- **override**: Provides a new implementation in derived class

```csharp
class Shape
{
    public virtual double GetArea()
    {
        return 0;
    }

    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override void Draw()
    {
        Console.WriteLine($"Drawing circle with radius {Radius}");
    }
}

class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double GetArea()
    {
        return Width * Height;
    }

    public override void Draw()
    {
        Console.WriteLine($"Drawing rectangle {Width}x{Height}");
    }
}
```

## Polymorphic Collections

Process different types uniformly:

```csharp
List<Shape> shapes = new List<Shape>
{
    new Circle { Radius = 5 },
    new Rectangle { Width = 4, Height = 3 },
    new Circle { Radius = 2 },
    new Rectangle { Width = 6, Height = 4 }
};

double totalArea = 0;
foreach (Shape shape in shapes)
{
    shape.Draw();           // Calls appropriate Draw()
    totalArea += shape.GetArea();  // Calls appropriate GetArea()
}
Console.WriteLine($"Total area: {totalArea}");
```

## Calling Base Implementation

Use `base` to call the parent's version:

```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("*breathing*");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        base.Speak();  // Call Animal's Speak first
        Console.WriteLine("Woof!");
    }
}

Dog dog = new Dog();
dog.Speak();
// Output:
// *breathing*
// Woof!
```

## The new Keyword (Method Hiding)

`new` hides (not overrides) a base method - generally avoided:

```csharp
class Animal
{
    public void Speak()  // Not virtual
    {
        Console.WriteLine("Animal sound");
    }
}

class Dog : Animal
{
    public new void Speak()  // Hides base method
    {
        Console.WriteLine("Woof!");
    }
}

Dog dog = new Dog();
dog.Speak();  // "Woof!"

Animal animal = dog;
animal.Speak();  // "Animal sound" - NOT polymorphic!
```

**Avoid `new`** - it breaks polymorphism. Use `virtual`/`override` instead.

## Sealed Override

Prevent further overriding:

```csharp
class Animal
{
    public virtual void Speak() { }
}

class Dog : Animal
{
    public sealed override void Speak()  // Cannot be overridden further
    {
        Console.WriteLine("Woof!");
    }
}

class Poodle : Dog
{
    // public override void Speak() { }  // ERROR: Speak is sealed
}
```

## Polymorphism with Properties

Properties can also be virtual:

```csharp
class Vehicle
{
    public virtual int MaxSpeed => 100;
    public virtual string Description => "A vehicle";
}

class SportsCar : Vehicle
{
    public override int MaxSpeed => 250;
    public override string Description => $"Sports car (max {MaxSpeed} mph)";
}

class Truck : Vehicle
{
    public override int MaxSpeed => 80;
    public override string Description => $"Heavy truck (max {MaxSpeed} mph)";
}
```

## Type Checking and Casting

Work with polymorphic types:

```csharp
void ProcessAnimal(Animal animal)
{
    animal.Speak();  // Polymorphic call

    // Type checking
    if (animal is Dog dog)
    {
        dog.Fetch();  // Dog-specific method
    }
    else if (animal is Cat cat)
    {
        cat.Purr();  // Cat-specific method
    }
}

// Pattern matching
string GetAnimalInfo(Animal animal) => animal switch
{
    Dog d => $"Dog named {d.Name}",
    Cat c => $"Cat named {c.Name}",
    _ => "Unknown animal"
};
```

## Practical Examples

### Example 1: Payment Processing

```csharp
abstract class Payment
{
    public decimal Amount { get; set; }
    public abstract bool Process();
    public virtual string GetReceipt()
    {
        return $"Payment of ${Amount} processed.";
    }
}

class CreditCardPayment : Payment
{
    public string CardNumber { get; set; }

    public override bool Process()
    {
        Console.WriteLine($"Processing credit card {CardNumber}...");
        return true;
    }

    public override string GetReceipt()
    {
        return base.GetReceipt() + $" Card: ****{CardNumber[^4..]}";
    }
}

class PayPalPayment : Payment
{
    public string Email { get; set; }

    public override bool Process()
    {
        Console.WriteLine($"Processing PayPal payment for {Email}...");
        return true;
    }
}

// Polymorphic processing
void ProcessPayments(List<Payment> payments)
{
    foreach (var payment in payments)
    {
        if (payment.Process())
        {
            Console.WriteLine(payment.GetReceipt());
        }
    }
}
```

### Example 2: Employee Salary Calculation

```csharp
class Employee
{
    public string Name { get; set; }
    public decimal BaseSalary { get; set; }

    public virtual decimal CalculatePay()
    {
        return BaseSalary;
    }
}

class Manager : Employee
{
    public decimal Bonus { get; set; }

    public override decimal CalculatePay()
    {
        return BaseSalary + Bonus;
    }
}

class SalesEmployee : Employee
{
    public decimal Commission { get; set; }
    public decimal TotalSales { get; set; }

    public override decimal CalculatePay()
    {
        return BaseSalary + (TotalSales * Commission);
    }
}

class HourlyEmployee : Employee
{
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public override decimal CalculatePay()
    {
        decimal regularPay = Math.Min(HoursWorked, 40) * HourlyRate;
        decimal overtime = Math.Max(0, HoursWorked - 40) * HourlyRate * 1.5m;
        return regularPay + overtime;
    }
}

// Process all employees uniformly
decimal CalculatePayroll(List<Employee> employees)
{
    decimal total = 0;
    foreach (var emp in employees)
    {
        decimal pay = emp.CalculatePay();  // Polymorphic call
        Console.WriteLine($"{emp.Name}: ${pay:N2}");
        total += pay;
    }
    return total;
}
```

### Example 3: Game Characters

```csharp
abstract class GameCharacter
{
    public string Name { get; set; }
    public int Health { get; set; }

    public abstract int Attack();
    public abstract void SpecialAbility();

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} takes {damage} damage. HP: {Health}");
    }
}

class Warrior : GameCharacter
{
    public int Strength { get; set; }

    public override int Attack()
    {
        int damage = Strength * 2;
        Console.WriteLine($"{Name} swings sword for {damage} damage!");
        return damage;
    }

    public override void SpecialAbility()
    {
        Console.WriteLine($"{Name} uses Shield Block! Damage reduced.");
    }
}

class Mage : GameCharacter
{
    public int Intelligence { get; set; }
    public int Mana { get; set; }

    public override int Attack()
    {
        int damage = Intelligence * 3;
        Mana -= 10;
        Console.WriteLine($"{Name} casts Fireball for {damage} damage!");
        return damage;
    }

    public override void SpecialAbility()
    {
        Mana += 50;
        Console.WriteLine($"{Name} meditates. Mana restored!");
    }
}

// Battle system using polymorphism
void Battle(GameCharacter attacker, GameCharacter defender)
{
    int damage = attacker.Attack();  // Polymorphic
    defender.TakeDamage(damage);     // Polymorphic
}
```

## Benefits of Polymorphism

1. **Code Reusability**: Write code that works with base types
2. **Extensibility**: Add new types without changing existing code
3. **Flexibility**: Swap implementations at runtime
4. **Maintainability**: Changes to derived classes don't affect base code
5. **Testability**: Easy to mock/substitute for testing

## Summary

- Polymorphism allows objects to be treated as their base type
- Use `virtual` to allow overriding, `override` to provide new implementation
- Method calls are resolved at runtime based on actual object type
- Use `base` to call parent implementation
- Avoid `new` keyword - it hides rather than overrides
- `sealed override` prevents further overriding
- Polymorphism enables flexible, extensible code design

Polymorphism is essential for writing maintainable, object-oriented code.
