# Constructors and the this Keyword

## Introduction

Constructors are special methods that run when you create a new object. They ensure objects are properly initialized before use. The `this` keyword refers to the current instance of a class, helping distinguish between instance members and parameters.

## What is a Constructor?

A constructor is a special method that:
- Has the **same name as the class**
- Has **no return type** (not even void)
- Runs **automatically** when creating an object with `new`
- Initializes the object to a valid state

```csharp
class Dog
{
    public string Name;
    public int Age;

    // Constructor
    public Dog()
    {
        Name = "Unknown";
        Age = 0;
        Console.WriteLine("A new dog is created!");
    }
}

Dog myDog = new Dog();  // Constructor runs automatically
// Output: A new dog is created!
Console.WriteLine(myDog.Name);  // Unknown
```

## Default Constructor

If you don't define any constructor, C# provides a default (parameterless) constructor:

```csharp
class Simple
{
    public int Value;
    // Implicit default constructor: public Simple() { }
}

Simple s = new Simple();  // Uses default constructor
```

**Important:** Once you define any constructor, the default constructor is no longer provided automatically.

## Parameterized Constructors

Constructors can accept parameters to initialize objects with specific values:

```csharp
class Person
{
    public string Name;
    public int Age;

    // Parameterized constructor
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

Person alice = new Person("Alice", 30);
Console.WriteLine($"{alice.Name}, {alice.Age}");  // Alice, 30

// Person bob = new Person();  // ERROR: No parameterless constructor!
```

## The this Keyword

The `this` keyword refers to the current instance of the class. It's used to:

1. **Distinguish instance members from parameters**
2. **Pass the current object to other methods**
3. **Chain constructors**

### Distinguishing Members from Parameters

```csharp
class Employee
{
    private string name;
    private int salary;

    public Employee(string name, int salary)
    {
        // 'this.name' refers to the field
        // 'name' refers to the parameter
        this.name = name;
        this.salary = salary;
    }

    public void Display()
    {
        Console.WriteLine($"Name: {this.name}, Salary: {this.salary}");
        // 'this' is optional when there's no ambiguity
    }
}
```

### When this is Required

```csharp
class Point
{
    public int X;
    public int Y;

    // Without 'this' - parameter shadows the field
    public void SetBad(int X, int Y)
    {
        X = X;  // Does nothing! Assigns parameter to itself
        Y = Y;
    }

    // With 'this' - correctly assigns to fields
    public void SetGood(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }
}
```

## Constructor Overloading

Classes can have multiple constructors with different parameters:

```csharp
class Rectangle
{
    public double Width;
    public double Height;

    // Parameterless constructor - creates a unit square
    public Rectangle()
    {
        Width = 1;
        Height = 1;
    }

    // Single parameter - creates a square
    public Rectangle(double side)
    {
        Width = side;
        Height = side;
    }

    // Two parameters - creates a rectangle
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
}

Rectangle r1 = new Rectangle();         // 1x1
Rectangle r2 = new Rectangle(5);        // 5x5
Rectangle r3 = new Rectangle(4, 3);     // 4x3
```

## Constructor Chaining with this()

Constructors can call other constructors using `this()`:

```csharp
class Product
{
    public string Name;
    public decimal Price;
    public int Quantity;
    public string Category;

    // Primary constructor - does all the work
    public Product(string name, decimal price, int quantity, string category)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    // Chains to primary constructor with defaults
    public Product(string name, decimal price)
        : this(name, price, 0, "General")
    {
        // Additional code can go here
    }

    // Chains to above constructor
    public Product(string name)
        : this(name, 0.00m)
    {
    }

    // Default constructor
    public Product()
        : this("Unknown")
    {
    }
}

Product p1 = new Product();                              // Uses all defaults
Product p2 = new Product("Widget");                      // Name only
Product p3 = new Product("Gadget", 19.99m);             // Name and price
Product p4 = new Product("Tool", 29.99m, 10, "Hardware"); // All values
```

## Constructor Execution Order

When chaining constructors, the chained constructor runs first:

```csharp
class Demo
{
    public Demo() : this(0)
    {
        Console.WriteLine("Parameterless constructor");
    }

    public Demo(int value)
    {
        Console.WriteLine($"Parameterized constructor: {value}");
    }
}

Demo d = new Demo();
// Output:
// Parameterized constructor: 0
// Parameterless constructor
```

## Optional Parameters vs. Multiple Constructors

You can often simplify with optional parameters:

```csharp
// Multiple constructors approach
class ConfigA
{
    public string Server;
    public int Port;
    public bool Secure;

    public ConfigA() : this("localhost") { }
    public ConfigA(string server) : this(server, 8080) { }
    public ConfigA(string server, int port) : this(server, port, false) { }
    public ConfigA(string server, int port, bool secure)
    {
        Server = server;
        Port = port;
        Secure = secure;
    }
}

// Optional parameters approach (simpler)
class ConfigB
{
    public string Server;
    public int Port;
    public bool Secure;

    public ConfigB(string server = "localhost", int port = 8080, bool secure = false)
    {
        Server = server;
        Port = port;
        Secure = secure;
    }
}

ConfigB c1 = new ConfigB();                    // All defaults
ConfigB c2 = new ConfigB("example.com");       // Custom server
ConfigB c3 = new ConfigB(secure: true);        // Named argument
```

## Readonly Fields and Constructors

Fields marked `readonly` can only be assigned in constructors:

```csharp
class ImmutablePoint
{
    public readonly int X;
    public readonly int Y;

    public ImmutablePoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void TryModify()
    {
        // X = 100;  // ERROR: Cannot assign to readonly field
    }
}

ImmutablePoint p = new ImmutablePoint(10, 20);
// p.X = 30;  // ERROR: Cannot assign to readonly field
```

## Expression-Bodied Constructors

Simple constructors can use expression body syntax:

```csharp
class Wrapper
{
    public int Value { get; }

    // Expression-bodied constructor
    public Wrapper(int value) => Value = value;
}
```

## Primary Constructors (C# 12+)

C# 12 introduces primary constructors for cleaner syntax:

```csharp
// Traditional
class PersonTraditional
{
    public string Name { get; }
    public int Age { get; }

    public PersonTraditional(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

// Primary constructor (C# 12+)
class PersonModern(string name, int age)
{
    public string Name { get; } = name;
    public int Age { get; } = age;
}
```

## Practical Examples

### Example 1: BankAccount with Validation

```csharp
class BankAccount
{
    public string AccountNumber { get; }
    public string OwnerName { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountNumber, string ownerName, decimal initialDeposit = 0)
    {
        if (string.IsNullOrEmpty(accountNumber))
            throw new ArgumentException("Account number required");

        if (string.IsNullOrEmpty(ownerName))
            throw new ArgumentException("Owner name required");

        if (initialDeposit < 0)
            throw new ArgumentException("Initial deposit cannot be negative");

        AccountNumber = accountNumber;
        OwnerName = ownerName;
        Balance = initialDeposit;
    }
}

BankAccount account = new BankAccount("123456", "John Doe", 1000);
```

### Example 2: Game Character

```csharp
class Character
{
    public string Name { get; }
    public int Health { get; private set; }
    public int MaxHealth { get; }
    public int Attack { get; }
    public int Defense { get; }

    // Full customization
    public Character(string name, int health, int attack, int defense)
    {
        Name = name;
        Health = health;
        MaxHealth = health;
        Attack = attack;
        Defense = defense;
    }

    // Quick warrior
    public Character(string name)
        : this(name, 100, 15, 10)
    {
    }

    // Preset classes
    public static Character CreateWarrior(string name)
        => new Character(name, 150, 20, 15);

    public static Character CreateMage(string name)
        => new Character(name, 80, 25, 5);

    public static Character CreateRogue(string name)
        => new Character(name, 100, 18, 8);
}

Character warrior = Character.CreateWarrior("Conan");
Character mage = new Character("Gandalf", 80, 30, 5);
```

### Example 3: Configuration Builder

```csharp
class DatabaseConfig
{
    public string Host { get; }
    public int Port { get; }
    public string Database { get; }
    public string Username { get; }
    public string Password { get; }
    public int Timeout { get; }
    public bool UseSSL { get; }

    public DatabaseConfig(
        string host,
        string database,
        string username = null,
        string password = null,
        int port = 5432,
        int timeout = 30,
        bool useSSL = true)
    {
        Host = host ?? throw new ArgumentNullException(nameof(host));
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Port = port;
        Username = username;
        Password = password;
        Timeout = timeout;
        UseSSL = useSSL;
    }

    public string GetConnectionString()
    {
        var builder = new System.Text.StringBuilder();
        builder.Append($"Host={Host};Port={Port};Database={Database};");

        if (!string.IsNullOrEmpty(Username))
            builder.Append($"Username={Username};Password={Password};");

        builder.Append($"Timeout={Timeout};SSL={UseSSL};");
        return builder.ToString();
    }
}

var config = new DatabaseConfig("localhost", "myapp", useSSL: false);
Console.WriteLine(config.GetConnectionString());
```

## Common Mistakes

### 1. Forgetting Constructor is Not Inherited

```csharp
class Parent
{
    public Parent(int value) { }
}

class Child : Parent
{
    // ERROR: Must explicitly call base constructor
    // public Child() { }  // Won't compile

    public Child() : base(0) { }  // Correct
    public Child(int value) : base(value) { }  // Correct
}
```

### 2. Circular Constructor Calls

```csharp
class Bad
{
    public Bad() : this(0) { }
    public Bad(int x) : this() { }  // ERROR: Circular call
}
```

### 3. Using this Before Base Constructor

```csharp
class Example
{
    public int Value;

    // ERROR: Cannot use 'this' before base constructor completes
    // public Example() : base(this.Value) { }
}
```

## Summary

- **Constructors** initialize objects when created with `new`
- Same name as class, no return type
- **Parameterized constructors** accept initial values
- **this** keyword refers to current instance
- **Constructor overloading** provides multiple ways to create objects
- **this()** chains to other constructors in the same class
- **readonly** fields can only be set in constructors
- Use constructors to ensure objects start in a valid state

Constructors are essential for creating well-initialized objects that are ready to use immediately after creation.
