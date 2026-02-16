# Interfaces

## Introduction

An interface defines a contract that classes must follow. It specifies what methods, properties, and events a class must implement, without providing the implementation itself. Interfaces enable polymorphism across unrelated classes and are fundamental to flexible, testable code design.

## What is an Interface?

An interface is declared with the `interface` keyword:

```csharp
interface IAnimal
{
    void Speak();
    void Move();
}

class Dog : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Woof!");
    }

    public void Move()
    {
        Console.WriteLine("Dog runs on four legs");
    }
}

class Bird : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Chirp!");
    }

    public void Move()
    {
        Console.WriteLine("Bird flies through the air");
    }
}
```

## Interface Naming Convention

Interfaces in C# start with "I" by convention:

```csharp
interface IReadable { }
interface IWritable { }
interface IDisposable { }
interface IComparable { }
interface IEnumerable { }
```

## Implementing Interfaces

A class uses `:` to implement an interface and must provide all members:

```csharp
interface IShape
{
    double GetArea();
    double GetPerimeter();
    void Draw();
}

class Circle : IShape
{
    public double Radius { get; set; }

    public double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }

    public void Draw()
    {
        Console.WriteLine($"Drawing circle with radius {Radius}");
    }
}

class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public double GetArea() => Width * Height;

    public double GetPerimeter() => 2 * (Width + Height);

    public void Draw()
    {
        Console.WriteLine($"Drawing rectangle {Width}x{Height}");
    }
}
```

## Multiple Interface Implementation

A class can implement multiple interfaces:

```csharp
interface IReadable
{
    string Read();
}

interface IWritable
{
    void Write(string content);
}

interface IDeletable
{
    void Delete();
}

class TextFile : IReadable, IWritable, IDeletable
{
    private string _content = "";

    public string Read()
    {
        return _content;
    }

    public void Write(string content)
    {
        _content = content;
    }

    public void Delete()
    {
        _content = "";
        Console.WriteLine("File deleted");
    }
}
```

## Interface Properties

Interfaces can define properties:

```csharp
interface IIdentifiable
{
    int Id { get; }
    string Name { get; set; }
}

interface ITimestamped
{
    DateTime Created { get; }
    DateTime Modified { get; set; }
}

class Document : IIdentifiable, ITimestamped
{
    public int Id { get; }
    public string Name { get; set; }
    public DateTime Created { get; }
    public DateTime Modified { get; set; }

    public Document(int id, string name)
    {
        Id = id;
        Name = name;
        Created = DateTime.Now;
        Modified = DateTime.Now;
    }
}
```

## Interface Inheritance

Interfaces can inherit from other interfaces:

```csharp
interface IEntity
{
    int Id { get; }
}

interface IAuditable : IEntity
{
    DateTime CreatedAt { get; }
    string CreatedBy { get; }
}

interface ISoftDeletable : IEntity
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
}

// Must implement all inherited interface members
class User : IAuditable, ISoftDeletable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public string Username { get; set; }
}
```

## Default Interface Methods (C# 8+)

Interfaces can provide default implementations:

```csharp
interface ILogger
{
    void Log(string message);

    // Default implementation
    void LogError(string message)
    {
        Log($"ERROR: {message}");
    }

    // Default implementation
    void LogWarning(string message)
    {
        Log($"WARNING: {message}");
    }
}

class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
    // LogError and LogWarning use default implementations
}

class FileLogger : ILogger
{
    public void Log(string message)
    {
        File.AppendAllText("log.txt", $"{message}\n");
    }

    // Override default
    public void LogError(string message)
    {
        Log($"[ERROR] {DateTime.Now}: {message}");
    }
}
```

## Polymorphism with Interfaces

Use interfaces for polymorphic behavior:

```csharp
interface IPaymentMethod
{
    bool ProcessPayment(decimal amount);
    string GetPaymentInfo();
}

class CreditCard : IPaymentMethod
{
    public string CardNumber { get; set; }

    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Charging ${amount} to card {CardNumber[^4..]}");
        return true;
    }

    public string GetPaymentInfo() => $"Credit Card ****{CardNumber[^4..]}";
}

class PayPal : IPaymentMethod
{
    public string Email { get; set; }

    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Charging ${amount} to PayPal {Email}");
        return true;
    }

    public string GetPaymentInfo() => $"PayPal: {Email}";
}

class BankTransfer : IPaymentMethod
{
    public string AccountNumber { get; set; }

    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Transferring ${amount} from account {AccountNumber}");
        return true;
    }

    public string GetPaymentInfo() => $"Bank Account: {AccountNumber}";
}

// Use polymorphically
void ProcessCheckout(IPaymentMethod payment, decimal total)
{
    Console.WriteLine($"Processing with: {payment.GetPaymentInfo()}");
    if (payment.ProcessPayment(total))
    {
        Console.WriteLine("Payment successful!");
    }
}
```

## Explicit Interface Implementation

When a class implements multiple interfaces with same member names:

```csharp
interface IUSPrinter
{
    void Print();  // Prints in US format
}

interface IEUPrinter
{
    void Print();  // Prints in EU format
}

class InternationalPrinter : IUSPrinter, IEUPrinter
{
    // Explicit implementation
    void IUSPrinter.Print()
    {
        Console.WriteLine("Printing in US Letter format (8.5x11)");
    }

    void IEUPrinter.Print()
    {
        Console.WriteLine("Printing in A4 format (210x297mm)");
    }

    // Regular method
    public void Print()
    {
        Console.WriteLine("Printing with default settings");
    }
}

// Usage
InternationalPrinter printer = new InternationalPrinter();
printer.Print();  // Default

IUSPrinter usPrinter = printer;
usPrinter.Print();  // US format

IEUPrinter euPrinter = printer;
euPrinter.Print();  // EU format
```

## Interfaces vs Abstract Classes

| Feature | Interface | Abstract Class |
|---------|-----------|----------------|
| Multiple inheritance | Yes | No |
| Fields | No (until C# 11) | Yes |
| Constructors | No | Yes |
| Access modifiers | Public only (default) | Any |
| Default implementation | Yes (C# 8+) | Yes |
| Use case | Contract for unrelated types | Base for related types |

```csharp
// Interface - contract for any type
interface IMovable
{
    void Move();
}

// Can be implemented by unrelated types
class Car : IMovable
{
    public void Move() => Console.WriteLine("Car drives");
}

class Fish : IMovable
{
    public void Move() => Console.WriteLine("Fish swims");
}

class Robot : IMovable
{
    public void Move() => Console.WriteLine("Robot walks");
}

// Abstract class - base for related types
abstract class Animal
{
    public string Name { get; set; }
    public abstract void Speak();
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Woof!");
}
```

## Common .NET Interfaces

### IComparable

```csharp
class Person : IComparable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public int CompareTo(Person other)
    {
        return Age.CompareTo(other.Age);
    }
}

List<Person> people = new List<Person>
{
    new Person { Name = "Alice", Age = 30 },
    new Person { Name = "Bob", Age = 25 },
    new Person { Name = "Carol", Age = 35 }
};

people.Sort();  // Uses CompareTo
```

### IEquatable

```csharp
class Product : IEquatable<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Equals(Product other)
    {
        if (other == null) return false;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Product);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```

### IDisposable

```csharp
class DatabaseConnection : IDisposable
{
    private bool _disposed = false;

    public void Open()
    {
        Console.WriteLine("Connection opened");
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Console.WriteLine("Connection closed");
            _disposed = true;
        }
    }
}

// Use with 'using' statement
using (var connection = new DatabaseConnection())
{
    connection.Open();
    // Work with connection
}  // Automatically calls Dispose()
```

### IEnumerable

```csharp
class Playlist : IEnumerable<string>
{
    private List<string> _songs = new List<string>();

    public void Add(string song) => _songs.Add(song);

    public IEnumerator<string> GetEnumerator()
    {
        return _songs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

Playlist playlist = new Playlist();
playlist.Add("Song 1");
playlist.Add("Song 2");

foreach (string song in playlist)
{
    Console.WriteLine(song);
}
```

## Practical Example: Plugin System

```csharp
interface IPlugin
{
    string Name { get; }
    string Version { get; }
    void Initialize();
    void Execute();
    void Shutdown();
}

class LoggingPlugin : IPlugin
{
    public string Name => "Logging Plugin";
    public string Version => "1.0.0";

    public void Initialize()
    {
        Console.WriteLine("Logging plugin initialized");
    }

    public void Execute()
    {
        Console.WriteLine("Logging active...");
    }

    public void Shutdown()
    {
        Console.WriteLine("Logging plugin shutdown");
    }
}

class CachePlugin : IPlugin
{
    public string Name => "Cache Plugin";
    public string Version => "2.1.0";

    public void Initialize()
    {
        Console.WriteLine("Cache warming up...");
    }

    public void Execute()
    {
        Console.WriteLine("Cache operational");
    }

    public void Shutdown()
    {
        Console.WriteLine("Cache flushed and shutdown");
    }
}

class PluginManager
{
    private List<IPlugin> _plugins = new List<IPlugin>();

    public void RegisterPlugin(IPlugin plugin)
    {
        _plugins.Add(plugin);
        Console.WriteLine($"Registered: {plugin.Name} v{plugin.Version}");
    }

    public void InitializeAll()
    {
        foreach (var plugin in _plugins)
        {
            plugin.Initialize();
        }
    }

    public void ExecuteAll()
    {
        foreach (var plugin in _plugins)
        {
            plugin.Execute();
        }
    }

    public void ShutdownAll()
    {
        foreach (var plugin in _plugins)
        {
            plugin.Shutdown();
        }
    }
}
```

## Summary

- Interfaces define contracts (what, not how)
- Classes implement interfaces with `:`
- A class can implement multiple interfaces
- Interface names start with "I" by convention
- Interfaces enable polymorphism across unrelated types
- Use explicit implementation for name conflicts
- Default implementations available in C# 8+
- Common .NET interfaces: IComparable, IEquatable, IDisposable, IEnumerable
- Prefer interfaces over abstract classes for loose coupling

Interfaces are essential for writing flexible, testable, and maintainable code.
