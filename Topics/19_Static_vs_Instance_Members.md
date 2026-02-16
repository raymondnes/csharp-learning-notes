# Static vs Instance Members

## Introduction

In C#, class members can be either **static** (belonging to the class itself) or **instance** (belonging to individual objects). Understanding this distinction is crucial for proper class design and resource management.

## Instance Members (Default)

Instance members belong to individual objects. Each object has its own copy.

```csharp
class Dog
{
    // Instance fields - each dog has its own
    public string Name;
    public int Age;

    // Instance method - operates on specific dog
    public void Bark()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

Dog dog1 = new Dog { Name = "Buddy", Age = 3 };
Dog dog2 = new Dog { Name = "Max", Age = 5 };

dog1.Bark();  // Buddy says: Woof!
dog2.Bark();  // Max says: Woof!
```

## Static Members

Static members belong to the class itself, not to any instance. There's only one copy shared by all.

```csharp
class Dog
{
    // Static field - shared by ALL dogs
    public static int TotalDogs = 0;

    public string Name;

    public Dog(string name)
    {
        Name = name;
        TotalDogs++;  // Increment shared counter
    }

    // Static method - belongs to class
    public static void ShowCount()
    {
        Console.WriteLine($"Total dogs: {TotalDogs}");
    }
}

Dog dog1 = new Dog("Buddy");
Dog dog2 = new Dog("Max");
Dog dog3 = new Dog("Rocky");

// Access static member through class name
Dog.ShowCount();  // Total dogs: 3
Console.WriteLine(Dog.TotalDogs);  // 3
```

## Key Differences

| Aspect | Instance | Static |
|--------|----------|--------|
| Belongs to | Individual object | The class |
| Access | Through object (`obj.Member`) | Through class (`Class.Member`) |
| Memory | One per instance | One per class |
| Can access | Instance + static members | Only static members |
| Requires object | Yes | No |

## Static Fields

```csharp
class Configuration
{
    // Static fields - shared configuration
    public static string AppName = "MyApp";
    public static string Version = "1.0.0";
    public static int MaxUsers = 100;
}

// Access without creating instance
Console.WriteLine(Configuration.AppName);  // MyApp
Configuration.MaxUsers = 200;  // Modify shared value
```

### Static Fields for Counting

```csharp
class Employee
{
    private static int nextId = 1;

    public int Id { get; }
    public string Name { get; set; }

    public Employee(string name)
    {
        Id = nextId++;  // Auto-increment ID
        Name = name;
    }
}

var emp1 = new Employee("Alice");  // Id = 1
var emp2 = new Employee("Bob");    // Id = 2
var emp3 = new Employee("Carol");  // Id = 3
```

## Static Methods

Static methods cannot access instance members:

```csharp
class Calculator
{
    // Static methods - no instance needed
    public static int Add(int a, int b) => a + b;
    public static int Subtract(int a, int b) => a - b;
    public static int Multiply(int a, int b) => a * b;
    public static double Divide(int a, int b) => (double)a / b;
}

// Use directly through class name
int sum = Calculator.Add(5, 3);       // 8
int diff = Calculator.Subtract(10, 4); // 6
```

### Static Methods Cannot Access Instance Members

```csharp
class Example
{
    public int InstanceValue = 10;
    public static int StaticValue = 20;

    public void InstanceMethod()
    {
        // Can access both
        Console.WriteLine(InstanceValue);  // OK
        Console.WriteLine(StaticValue);    // OK
    }

    public static void StaticMethod()
    {
        // Console.WriteLine(InstanceValue);  // ERROR!
        Console.WriteLine(StaticValue);       // OK
    }
}
```

## Static Properties

```csharp
class AppSettings
{
    private static string _connectionString;

    public static string ConnectionString
    {
        get => _connectionString;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Connection string required");
            _connectionString = value;
        }
    }

    public static bool IsConfigured => !string.IsNullOrEmpty(_connectionString);
}

AppSettings.ConnectionString = "Server=localhost;Database=MyDb";
Console.WriteLine(AppSettings.IsConfigured);  // True
```

## Static Constructors

Initialize static members when class is first used:

```csharp
class DatabaseConfig
{
    public static string ConnectionString { get; private set; }
    public static DateTime InitializedAt { get; private set; }

    // Static constructor - runs once, automatically
    static DatabaseConfig()
    {
        Console.WriteLine("Static constructor running...");
        ConnectionString = LoadFromConfig();
        InitializedAt = DateTime.Now;
    }

    private static string LoadFromConfig()
    {
        return "Server=localhost;Database=App";
    }
}

// First access triggers static constructor
Console.WriteLine(DatabaseConfig.ConnectionString);
```

**Static constructor rules:**
- No access modifier
- No parameters
- Runs automatically before first use
- Runs only once per application lifetime

## Static Classes

A class can be entirely static:

```csharp
static class MathHelper
{
    public static double Pi => 3.14159265359;

    public static double CircleArea(double radius)
    {
        return Pi * radius * radius;
    }

    public static double CircleCircumference(double radius)
    {
        return 2 * Pi * radius;
    }
}

// Cannot instantiate
// MathHelper helper = new MathHelper();  // ERROR!

// Use directly
double area = MathHelper.CircleArea(5);
```

**Static class rules:**
- Cannot be instantiated
- Cannot be inherited
- Can only contain static members
- Good for utility/helper methods

## Common Static Classes in .NET

```csharp
// Math class
double sqrt = Math.Sqrt(16);      // 4
double power = Math.Pow(2, 8);    // 256
int max = Math.Max(10, 20);       // 20

// Console class
Console.WriteLine("Hello");
Console.ReadLine();

// File class
string content = File.ReadAllText("file.txt");
File.WriteAllText("file.txt", "content");

// Environment class
string machine = Environment.MachineName;
string[] args = Environment.GetCommandLineArgs();
```

## When to Use Static

### Use Static For:
- Utility methods (Math.Sqrt, string.IsNullOrEmpty)
- Factory methods (File.ReadAllText)
- Shared state/configuration
- Constants and readonly values
- Extension methods
- Singleton pattern

### Use Instance For:
- Data that varies per object
- Methods that operate on object state
- When polymorphism is needed
- When testing/mocking is required
- Most business logic

## Mixing Static and Instance

```csharp
class BankAccount
{
    // Static - shared across all accounts
    private static decimal _interestRate = 0.05m;
    private static int _totalAccounts = 0;

    // Instance - unique to each account
    public string AccountNumber { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
        _totalAccounts++;
    }

    // Instance method using static field
    public void ApplyInterest()
    {
        Balance += Balance * _interestRate;
    }

    // Static method to change rate for all accounts
    public static void SetInterestRate(decimal rate)
    {
        _interestRate = rate;
    }

    public static int GetTotalAccounts() => _totalAccounts;
}
```

## Practical Examples

### Example 1: ID Generator

```csharp
class IdGenerator
{
    private static int _currentId = 0;
    private static readonly object _lock = new object();

    public static int GetNextId()
    {
        lock (_lock)  // Thread-safe
        {
            return ++_currentId;
        }
    }

    public static void Reset()
    {
        lock (_lock)
        {
            _currentId = 0;
        }
    }
}
```

### Example 2: Logger

```csharp
static class Logger
{
    private static readonly string LogFile = "app.log";

    public static void Info(string message)
    {
        Log("INFO", message);
    }

    public static void Error(string message)
    {
        Log("ERROR", message);
    }

    private static void Log(string level, string message)
    {
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
        Console.WriteLine(entry);
        File.AppendAllText(LogFile, entry + Environment.NewLine);
    }
}

Logger.Info("Application started");
Logger.Error("Something went wrong");
```

### Example 3: Singleton Pattern

```csharp
class GameManager
{
    private static GameManager _instance;
    private static readonly object _lock = new object();

    public int Score { get; set; }
    public int Level { get; set; }

    private GameManager() { }  // Private constructor

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameManager();
                    }
                }
            }
            return _instance;
        }
    }
}

// Same instance everywhere
GameManager.Instance.Score = 100;
Console.WriteLine(GameManager.Instance.Score);  // 100
```

## Summary

- **Instance members** belong to objects; each object has its own copy
- **Static members** belong to the class; shared by all instances
- Access static members through class name: `ClassName.Member`
- Static methods cannot access instance members
- Static constructors initialize static members once
- Static classes cannot be instantiated
- Use static for utilities, shared state, and constants
- Use instance for object-specific data and behavior

Understanding static vs instance is essential for proper C# class design.
