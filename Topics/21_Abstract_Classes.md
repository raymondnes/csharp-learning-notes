# Abstract Classes

## Introduction

Abstract classes are partially implemented classes that cannot be instantiated directly. They serve as blueprints for derived classes, defining a contract that child classes must fulfill while optionally providing shared implementation.

## What is an Abstract Class?

An abstract class is declared with the `abstract` keyword and may contain:
- Abstract members (no implementation, must be overridden)
- Concrete members (full implementation, can be inherited)
- Constructors (called by derived classes)
- Fields and properties

```csharp
abstract class Animal
{
    // Concrete property
    public string Name { get; set; }

    // Concrete method
    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping.");
    }

    // Abstract method - no body, must be overridden
    public abstract void Speak();
}

// Cannot do this:
// Animal animal = new Animal();  // ERROR: Cannot create instance of abstract class

// Must create a derived class
class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

Dog dog = new Dog { Name = "Buddy" };
dog.Speak();  // Buddy says: Woof!
dog.Sleep();  // Buddy is sleeping.
```

## Abstract Methods

Abstract methods have no implementation - derived classes must provide one:

```csharp
abstract class Shape
{
    public string Color { get; set; }

    // Abstract methods - no body
    public abstract double GetArea();
    public abstract double GetPerimeter();

    // Concrete method
    public void DisplayInfo()
    {
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Area: {GetArea():F2}");
        Console.WriteLine($"Perimeter: {GetPerimeter():F2}");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
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

    public override double GetPerimeter()
    {
        return 2 * (Width + Height);
    }
}
```

## Abstract Properties

Properties can also be abstract:

```csharp
abstract class Employee
{
    public string Name { get; set; }

    // Abstract property
    public abstract decimal Salary { get; }

    // Abstract read-write property
    public abstract string Department { get; set; }
}

class Manager : Employee
{
    private decimal _baseSalary;
    private decimal _bonus;

    public override decimal Salary => _baseSalary + _bonus;

    public override string Department { get; set; }

    public Manager(string name, decimal baseSalary, decimal bonus)
    {
        Name = name;
        _baseSalary = baseSalary;
        _bonus = bonus;
    }
}
```

## Constructors in Abstract Classes

Abstract classes can have constructors called by derived classes:

```csharp
abstract class Vehicle
{
    public string Brand { get; }
    public string Model { get; }
    public int Year { get; }

    // Constructor
    protected Vehicle(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Console.WriteLine("Vehicle constructor called");
    }

    public abstract void Start();
}

class Car : Vehicle
{
    public int Doors { get; }

    public Car(string brand, string model, int year, int doors)
        : base(brand, model, year)  // Call base constructor
    {
        Doors = doors;
        Console.WriteLine("Car constructor called");
    }

    public override void Start()
    {
        Console.WriteLine($"{Brand} {Model} engine starting...");
    }
}

Car car = new Car("Toyota", "Camry", 2023, 4);
// Output:
// Vehicle constructor called
// Car constructor called
```

## Mixing Abstract and Concrete Members

```csharp
abstract class BankAccount
{
    public string AccountNumber { get; }
    public string Owner { get; set; }
    protected decimal Balance { get; set; }

    protected BankAccount(string accountNumber, string owner, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
    }

    // Concrete method
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited: ${amount:N2}");
        }
    }

    // Concrete method
    public decimal GetBalance() => Balance;

    // Abstract - each account type calculates differently
    public abstract decimal CalculateInterest();

    // Abstract - different withdrawal rules
    public abstract bool Withdraw(decimal amount);
}

class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingsAccount(string accountNumber, string owner, decimal initialBalance, decimal rate)
        : base(accountNumber, owner, initialBalance)
    {
        InterestRate = rate;
    }

    public override decimal CalculateInterest()
    {
        return Balance * InterestRate;
    }

    public override bool Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            return true;
        }
        return false;  // Cannot overdraw
    }
}

class CheckingAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    public CheckingAccount(string accountNumber, string owner, decimal initialBalance, decimal overdraftLimit)
        : base(accountNumber, owner, initialBalance)
    {
        OverdraftLimit = overdraftLimit;
    }

    public override decimal CalculateInterest()
    {
        return 0;  // No interest on checking
    }

    public override bool Withdraw(decimal amount)
    {
        if (amount <= Balance + OverdraftLimit)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}
```

## Abstract Class Inheritance Chain

Abstract classes can inherit from other abstract classes:

```csharp
abstract class Animal
{
    public string Name { get; set; }
    public abstract void Speak();
}

abstract class Mammal : Animal
{
    public int NumberOfLegs { get; set; }
    public abstract void Walk();

    // Can provide implementation for inherited abstract
    public override void Speak()
    {
        Console.WriteLine("*mammal sound*");
    }
}

class Dog : Mammal
{
    public override void Walk()
    {
        Console.WriteLine($"{Name} walks on {NumberOfLegs} legs");
    }

    public override void Speak()  // Can still override
    {
        Console.WriteLine("Woof!");
    }
}

class Human : Mammal
{
    public override void Walk()
    {
        Console.WriteLine($"{Name} walks upright on {NumberOfLegs} legs");
    }

    public override void Speak()
    {
        Console.WriteLine("Hello!");
    }
}
```

## When to Use Abstract Classes

### Use Abstract Classes When:
1. You want to share code among related classes
2. Classes share common state (fields)
3. You need non-public members
4. You need constructors
5. You want to provide a partial implementation

### Don't Use Abstract Classes When:
1. Classes are not related in an "is-a" relationship
2. You need multiple inheritance (use interfaces)
3. You only need method signatures (use interfaces)

## Abstract Classes vs Regular Classes

```csharp
// Regular base class
class RegularAnimal
{
    public virtual void Speak()
    {
        Console.WriteLine("Some sound");
    }
}

// Problem: Can instantiate directly
RegularAnimal animal = new RegularAnimal();  // Allowed but meaningless

// Abstract base class
abstract class AbstractAnimal
{
    public abstract void Speak();
}

// Cannot instantiate
// AbstractAnimal animal = new AbstractAnimal();  // ERROR

// Must use derived class
class Cat : AbstractAnimal
{
    public override void Speak()
    {
        Console.WriteLine("Meow!");
    }
}
```

## Practical Example: Document Processing

```csharp
abstract class Document
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime Created { get; }

    protected Document(string title, string author)
    {
        Title = title;
        Author = author;
        Created = DateTime.Now;
    }

    // Concrete method
    public void PrintMetadata()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Created: {Created}");
    }

    // Abstract methods
    public abstract void Render();
    public abstract void Save(string path);
    public abstract string GetFileExtension();
}

class PDFDocument : Document
{
    public bool IsCompressed { get; set; }

    public PDFDocument(string title, string author)
        : base(title, author) { }

    public override void Render()
    {
        Console.WriteLine($"Rendering PDF: {Title}");
    }

    public override void Save(string path)
    {
        string fullPath = $"{path}/{Title}{GetFileExtension()}";
        Console.WriteLine($"Saving PDF to {fullPath}");
    }

    public override string GetFileExtension() => ".pdf";
}

class WordDocument : Document
{
    public string Template { get; set; }

    public WordDocument(string title, string author)
        : base(title, author) { }

    public override void Render()
    {
        Console.WriteLine($"Rendering Word document: {Title}");
    }

    public override void Save(string path)
    {
        string fullPath = $"{path}/{Title}{GetFileExtension()}";
        Console.WriteLine($"Saving Word document to {fullPath}");
    }

    public override string GetFileExtension() => ".docx";
}

// Polymorphic processing
void ProcessDocuments(List<Document> documents)
{
    foreach (var doc in documents)
    {
        doc.PrintMetadata();
        doc.Render();
        doc.Save("/documents");
        Console.WriteLine();
    }
}
```

## Abstract Classes with Template Method Pattern

```csharp
abstract class DataProcessor
{
    // Template method - defines algorithm structure
    public void Process()
    {
        ReadData();
        ValidateData();
        TransformData();
        SaveData();
    }

    // Steps that must be implemented by derived classes
    protected abstract void ReadData();
    protected abstract void ValidateData();
    protected abstract void TransformData();

    // Common implementation
    protected virtual void SaveData()
    {
        Console.WriteLine("Saving processed data...");
    }
}

class CSVProcessor : DataProcessor
{
    protected override void ReadData()
    {
        Console.WriteLine("Reading CSV file...");
    }

    protected override void ValidateData()
    {
        Console.WriteLine("Validating CSV format...");
    }

    protected override void TransformData()
    {
        Console.WriteLine("Transforming CSV data...");
    }
}

class JSONProcessor : DataProcessor
{
    protected override void ReadData()
    {
        Console.WriteLine("Reading JSON file...");
    }

    protected override void ValidateData()
    {
        Console.WriteLine("Validating JSON schema...");
    }

    protected override void TransformData()
    {
        Console.WriteLine("Parsing JSON objects...");
    }

    protected override void SaveData()
    {
        Console.WriteLine("Saving to NoSQL database...");
    }
}
```

## Summary

- Abstract classes cannot be instantiated directly
- Use `abstract` keyword on class and members
- Abstract members have no implementation
- Derived classes must implement all abstract members
- Abstract classes can have constructors, fields, concrete methods
- Use for sharing code among related classes
- Good for defining a template with partial implementation
- Single inheritance only (unlike interfaces)

Abstract classes provide a powerful way to define contracts while sharing implementation code among related classes.
