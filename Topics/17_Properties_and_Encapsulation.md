# Properties and Encapsulation

## Introduction

Encapsulation is one of the four pillars of object-oriented programming. It's the practice of hiding internal details and providing controlled access to an object's data. Properties in C# are the primary mechanism for implementing encapsulation, providing a clean way to expose data while maintaining control over how it's accessed and modified.

## The Problem with Public Fields

```csharp
class BadPerson
{
    public string Name;
    public int Age;  // Anyone can set this to -500 or 99999!
}

BadPerson p = new BadPerson();
p.Age = -500;  // No validation - bad data allowed!
```

## Properties to the Rescue

Properties look like fields but behave like methods:

```csharp
class Person
{
    private int _age;  // Private backing field

    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0 && value <= 150)
                _age = value;
            else
                throw new ArgumentException("Age must be between 0 and 150");
        }
    }
}

Person p = new Person();
p.Age = 25;       // OK - uses setter
int age = p.Age;  // OK - uses getter
p.Age = -500;     // Throws exception!
```

## Property Syntax

### Full Property

```csharp
class Product
{
    private decimal _price;  // Backing field

    public decimal Price
    {
        get { return _price; }
        set { _price = value; }
    }
}
```

### Auto-Implemented Property (Most Common)

When no special logic is needed, use auto-properties:

```csharp
class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
```

The compiler automatically creates the backing field.

### Expression-Bodied Properties (C# 6+)

```csharp
class Circle
{
    public double Radius { get; set; }

    // Read-only calculated property
    public double Area => Math.PI * Radius * Radius;
    public double Circumference => 2 * Math.PI * Radius;
}
```

## Get and Set Accessors

### Read-Only Property

```csharp
class Person
{
    public string Name { get; }  // No setter - read-only

    public Person(string name)
    {
        Name = name;  // Can only set in constructor
    }
}
```

### Write-Only Property (Rare)

```csharp
class Account
{
    private string _password;

    public string Password
    {
        set { _password = HashPassword(value); }
    }
}
```

### Read-Only Calculated Property

```csharp
class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }

    // Calculated - no setter
    public double Area
    {
        get { return Width * Height; }
    }

    // Or with expression body
    public double Perimeter => 2 * (Width + Height);
}
```

## Access Modifiers on Accessors

Control get/set access independently:

```csharp
class BankAccount
{
    // Anyone can read, only this class can set
    public decimal Balance { get; private set; }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            Balance += amount;
    }
}
```

```csharp
class Employee
{
    public string Name { get; set; }

    // Public read, protected set (derived classes can set)
    public int Level { get; protected set; }

    // Public read, internal set (same assembly can set)
    public string Department { get; internal set; }
}
```

## Property Validation

```csharp
class Person
{
    private string _name;
    private int _age;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty");
            _name = value.Trim();
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0)
                throw new ArgumentException("Age cannot be negative");
            if (value > 150)
                throw new ArgumentException("Age cannot exceed 150");
            _age = value;
        }
    }
}
```

## Init-Only Properties (C# 9+)

Properties that can only be set during initialization:

```csharp
class Person
{
    public string Name { get; init; }
    public int Age { get; init; }
}

// Can set during construction/initialization
Person p = new Person { Name = "Alice", Age = 30 };

// Cannot modify afterwards
// p.Name = "Bob";  // ERROR: init-only property
```

## Required Properties (C# 11+)

```csharp
class Person
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int Age { get; set; }  // Optional
}

// Must set required properties
Person p = new Person
{
    Name = "Alice",
    Email = "alice@example.com"
    // Age is optional
};
```

## Backing Fields

### When You Need a Backing Field

```csharp
class Temperature
{
    private double _celsius;

    public double Celsius
    {
        get => _celsius;
        set => _celsius = value;
    }

    // Different representation, same data
    public double Fahrenheit
    {
        get => (_celsius * 9 / 5) + 32;
        set => _celsius = (value - 32) * 5 / 9;
    }

    public double Kelvin
    {
        get => _celsius + 273.15;
        set => _celsius = value - 273.15;
    }
}

Temperature t = new Temperature();
t.Celsius = 100;
Console.WriteLine(t.Fahrenheit);  // 212
Console.WriteLine(t.Kelvin);      // 373.15
```

## Lazy Initialization

```csharp
class DataLoader
{
    private List<string> _data;

    public List<string> Data
    {
        get
        {
            if (_data == null)
            {
                Console.WriteLine("Loading data...");
                _data = LoadFromDatabase();  // Expensive operation
            }
            return _data;
        }
    }

    private List<string> LoadFromDatabase()
    {
        return new List<string> { "Item1", "Item2", "Item3" };
    }
}
```

Or using `Lazy<T>`:

```csharp
class DataLoader
{
    private readonly Lazy<List<string>> _data =
        new Lazy<List<string>>(() => LoadFromDatabase());

    public List<string> Data => _data.Value;
}
```

## Encapsulation Benefits

1. **Validation**: Ensure data integrity
2. **Computed Values**: Calculate on-the-fly
3. **Change Notification**: Trigger events on changes
4. **Lazy Loading**: Load data when needed
5. **Access Control**: Different read/write permissions
6. **Future Flexibility**: Change implementation without breaking code

## Practical Examples

### Example 1: Circle with Validation

```csharp
class Circle
{
    private double _radius;

    public double Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Radius must be positive");
            _radius = value;
        }
    }

    public double Diameter => Radius * 2;
    public double Area => Math.PI * Radius * Radius;
    public double Circumference => 2 * Math.PI * Radius;
}
```

### Example 2: Full Name Property

```csharp
class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Computed property
    public string FullName => $"{FirstName} {LastName}";

    // Split full name into parts
    public string FullNameReverse
    {
        set
        {
            var parts = value.Split(' ');
            if (parts.Length >= 2)
            {
                FirstName = parts[0];
                LastName = parts[parts.Length - 1];
            }
        }
    }
}

Person p = new Person();
p.FullNameReverse = "John Doe";
Console.WriteLine(p.FullName);  // John Doe
```

### Example 3: Observable Property

```csharp
class ObservableValue
{
    private int _value;

    public event Action<int, int> ValueChanged;

    public int Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                int oldValue = _value;
                _value = value;
                ValueChanged?.Invoke(oldValue, value);
            }
        }
    }
}

var observable = new ObservableValue();
observable.ValueChanged += (oldVal, newVal) =>
    Console.WriteLine($"Value changed from {oldVal} to {newVal}");

observable.Value = 10;  // Value changed from 0 to 10
observable.Value = 20;  // Value changed from 10 to 20
```

### Example 4: Configuration Class

```csharp
class AppConfig
{
    private string _connectionString;
    private int _maxRetries = 3;
    private TimeSpan _timeout = TimeSpan.FromSeconds(30);

    public string ConnectionString
    {
        get => _connectionString;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Connection string required");
            _connectionString = value;
        }
    }

    public int MaxRetries
    {
        get => _maxRetries;
        set => _maxRetries = Math.Max(0, Math.Min(10, value));  // Clamp 0-10
    }

    public TimeSpan Timeout
    {
        get => _timeout;
        set
        {
            if (value.TotalSeconds < 1 || value.TotalMinutes > 10)
                throw new ArgumentException("Timeout must be 1 second to 10 minutes");
            _timeout = value;
        }
    }

    public bool IsConfigured => !string.IsNullOrEmpty(_connectionString);
}
```

## Summary

- **Encapsulation** hides internal data and provides controlled access
- **Properties** provide field-like syntax with method-like control
- **Auto-properties** are concise when no validation needed
- Use **get** for reading, **set** for writing
- **Private set** allows public reading, private writing
- **Init-only** properties can only be set during initialization
- **Required properties** must be set when creating object
- Properties can be **calculated** from other data
- Use properties for **validation**, **notifications**, **lazy loading**

Properties are essential for writing well-encapsulated, maintainable C# code.
