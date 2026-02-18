# Projects 41-60: Advanced Projects

## Project 41: Shape Hierarchy

**Concepts:** Inheritance, virtual methods

Create shape class hierarchy:
- Base Shape class with Color, virtual Area/Perimeter
- Circle, Rectangle, Triangle subclasses
- Override methods in each
- Polymorphic array processing

```csharp
abstract class Shape
{
    public string Color { get; set; }
    public abstract double GetArea();
    public abstract double GetPerimeter();
    public virtual void Display()
    {
        Console.WriteLine($"{GetType().Name}: Area={GetArea():F2}");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }
    public override double GetArea() => Math.PI * Radius * Radius;
    public override double GetPerimeter() => 2 * Math.PI * Radius;
}
```

---

## Project 42: Vehicle System

**Concepts:** Virtual/override, base keyword

Create vehicle hierarchy:
- Vehicle base with virtual Start(), Stop()
- Car, Motorcycle, Truck subclasses
- Override behavior, call base methods
- FuelConsumption calculations

---

## Project 43: Employee Types

**Concepts:** Polymorphism, method overriding

Create employee system:
- Employee base with CalculatePay()
- HourlyEmployee, SalariedEmployee, CommissionEmployee
- Different pay calculations
- Process payroll polymorphically

---

## Project 44: Animal Kingdom

**Concepts:** Abstract classes, abstract methods

Create animal hierarchy:
- Abstract Animal with abstract Speak(), Move()
- Mammal, Bird, Fish intermediate classes
- Concrete animals: Dog, Eagle, Shark
- Demonstrate polymorphism

---

## Project 45: Payment Processors

**Concepts:** Interfaces, multiple implementations

Create payment system:
- IPaymentProcessor interface
- CreditCard, PayPal, Bitcoin implementations
- Process payments through interface
- Factory for processor creation

```csharp
interface IPaymentProcessor
{
    bool Validate();
    bool Process(decimal amount);
    string GetReceipt();
}

class CreditCardProcessor : IPaymentProcessor
{
    public string CardNumber { get; set; }
    public bool Validate() => CardNumber?.Length == 16;
    public bool Process(decimal amount) { /* ... */ return true; }
    public string GetReceipt() => $"CC Payment: {amount:C}";
}
```

---

## Project 46: Plugin System

**Concepts:** Multiple interfaces, composition

Create plugin architecture:
- IPlugin with Initialize(), Execute()
- IConfigurable for settings
- ILoggable for logging
- Plugin manager to load/run plugins

---

## Project 47: Word Frequency

**Concepts:** Dictionary, counting

Create word frequency analyzer:
- Read text input
- Count word occurrences
- Display sorted by frequency
- Filter common words

```csharp
Dictionary<string, int> CountWords(string text)
{
    var words = text.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var frequency = new Dictionary<string, int>();

    foreach (var word in words)
    {
        if (frequency.ContainsKey(word))
            frequency[word]++;
        else
            frequency[word] = 1;
    }
    return frequency;
}
```

---

## Project 48: Phone Directory

**Concepts:** Dictionary CRUD

Create phone book:
- Dictionary<string, string> for name->phone
- Add, update, delete contacts
- Reverse lookup (phone->name)
- Case-insensitive search

---

## Project 49: Student Grades Dictionary

**Concepts:** Dictionary of lists

Create grade tracker:
- Dictionary<string, List<int>> for student grades
- Add grades per student
- Calculate averages
- Find top performers

---

## Project 50: Product Filter

**Concepts:** LINQ basics (Where, Select, OrderBy)

Create product filter:
- Filter by price range
- Filter by category
- Sort by various fields
- Chain multiple filters

```csharp
var filtered = products
    .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
    .Where(p => string.IsNullOrEmpty(category) || p.Category == category)
    .OrderBy(p => p.Name)
    .Select(p => new { p.Name, p.Price });
```

---

## Project 51: Sales Report

**Concepts:** LINQ aggregations (Sum, Average, Count)

Create sales analyzer:
- Total sales
- Average sale amount
- Sales by category
- Top selling products

---

## Project 52: Data Analyzer

**Concepts:** LINQ GroupBy, complex queries

Create data analysis tool:
- Group data by category
- Aggregate per group
- Multiple grouping levels
- Generate reports

```csharp
var salesByCategory = sales
    .GroupBy(s => s.Category)
    .Select(g => new
    {
        Category = g.Key,
        TotalSales = g.Sum(s => s.Amount),
        AverageSale = g.Average(s => s.Amount),
        Count = g.Count()
    })
    .OrderByDescending(x => x.TotalSales);
```

---

## Project 53: Object Sorter

**Concepts:** IComparable implementation

Create sortable objects:
- Implement IComparable<T>
- Sort by multiple criteria
- Custom comparers
- Sort collections

```csharp
class Product : IComparable<Product>
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public int CompareTo(Product other)
    {
        int priceComparison = Price.CompareTo(other.Price);
        if (priceComparison != 0) return priceComparison;
        return Name.CompareTo(other.Name);
    }
}
```

---

## Project 54: Event Logger

**Concepts:** Static members, timestamps

Create logging system:
- Static Log method
- Static log file path
- Instance context (component name)
- Log levels (Info, Warning, Error)

---

## Project 55: Singleton Config

**Concepts:** Singleton design pattern

Create configuration singleton:
- Private constructor
- Static Instance property
- Thread-safe lazy initialization
- App-wide settings access

```csharp
class AppConfig
{
    private static AppConfig _instance;
    private static readonly object _lock = new object();

    private AppConfig() { LoadSettings(); }

    public static AppConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new AppConfig();
                }
            }
            return _instance;
        }
    }

    public string DatabaseConnection { get; private set; }
    private void LoadSettings() { /* Load from file */ }
}
```

---

## Project 56: Command Pattern

**Concepts:** Interfaces, command pattern

Create undo/redo system:
- ICommand with Execute(), Undo()
- Concrete commands (AddText, DeleteText)
- Command history stack
- Undo/Redo functionality

---

## Project 57: Expression Evaluator

**Concepts:** Stack, parsing

Create math expression evaluator:
- Parse infix expressions
- Convert to postfix (Shunting Yard)
- Evaluate using stack
- Handle operator precedence

---

## Project 58: File Organizer

**Concepts:** LINQ with files, Directory class

Create file organizer:
- Scan directory for files
- Group by extension
- Sort by size/date
- Move to categorized folders

---

## Project 59: CSV Processor

**Concepts:** File parsing, LINQ

Create CSV analyzer:
- Read CSV file
- Parse into objects
- Query with LINQ
- Generate statistics

```csharp
class CsvProcessor
{
    public List<Dictionary<string, string>> ReadCsv(string path)
    {
        var lines = File.ReadAllLines(path);
        var headers = lines[0].Split(',');

        return lines.Skip(1)
            .Select(line =>
            {
                var values = line.Split(',');
                return headers.Zip(values, (h, v) => new { h, v })
                    .ToDictionary(x => x.h, x => x.v);
            })
            .ToList();
    }
}
```

---

## Project 60: JSON Data Manager

**Concepts:** Serialization, file persistence

Create JSON data store:
- Save objects to JSON file
- Load objects from JSON
- CRUD operations with persistence
- Handle collections

```csharp
using System.Text.Json;

class JsonDataStore<T>
{
    private string _filePath;

    public JsonDataStore(string filePath) => _filePath = filePath;

    public List<T> LoadAll()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json);
    }

    public void SaveAll(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(_filePath, json);
    }
}
```

---

Complete these projects to master advanced C# concepts including inheritance, interfaces, LINQ, and design patterns.
