# Classes and Objects

## Introduction

Object-Oriented Programming (OOP) is a programming paradigm that organizes code around "objects" rather than functions and procedures. A **class** is a blueprint for creating objects, and an **object** is an instance of a class. Understanding classes and objects is fundamental to writing professional C# code.

## What is a Class?

A class is a template that defines:
- **Fields**: Data/variables the object holds
- **Methods**: Actions the object can perform
- **Properties**: Controlled access to fields
- **Constructors**: How objects are created

Think of a class like a cookie cutter - it defines the shape, but doesn't create cookies itself.

## Defining a Class

```csharp
class Dog
{
    // Fields - data the object stores
    public string Name;
    public int Age;
    public string Breed;

    // Method - action the object can perform
    public void Bark()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Breed: {Breed}");
    }
}
```

## Creating Objects (Instantiation)

Objects are created using the `new` keyword:

```csharp
// Create objects from the Dog class
Dog myDog = new Dog();
myDog.Name = "Buddy";
myDog.Age = 3;
myDog.Breed = "Golden Retriever";

Dog anotherDog = new Dog();
anotherDog.Name = "Max";
anotherDog.Age = 5;
anotherDog.Breed = "German Shepherd";

// Use the objects
myDog.Bark();           // Buddy says: Woof!
anotherDog.Bark();      // Max says: Woof!
myDog.DisplayInfo();    // Name: Buddy, Age: 3, Breed: Golden Retriever
```

Each object has its own copy of the fields.

## Class vs. Object

| Class | Object |
|-------|--------|
| Blueprint/Template | Instance of the blueprint |
| Defines structure | Contains actual data |
| Created once | Can create many |
| `class Dog { }` | `new Dog()` |

```csharp
// One class definition
class Car { public string Model; }

// Multiple objects from same class
Car car1 = new Car { Model = "Tesla" };
Car car2 = new Car { Model = "BMW" };
Car car3 = new Car { Model = "Honda" };
```

## Fields

Fields are variables declared inside a class:

```csharp
class Student
{
    // Fields
    public string Name;
    public int StudentId;
    public double GPA;
    private string socialSecurityNumber;  // Private - hidden from outside
}
```

### Field Default Values

Uninitialized fields get default values:

| Type | Default |
|------|---------|
| `int`, `double`, etc. | `0` |
| `bool` | `false` |
| `string`, reference types | `null` |

```csharp
class Example
{
    public int Number;      // Default: 0
    public string Text;     // Default: null
    public bool Flag;       // Default: false
}

Example ex = new Example();
Console.WriteLine(ex.Number);  // 0
Console.WriteLine(ex.Text);    // (null)
Console.WriteLine(ex.Flag);    // False
```

### Field Initialization

You can initialize fields at declaration:

```csharp
class Counter
{
    public int Count = 0;
    public int MaxValue = 100;
    public string Status = "Active";
}
```

## Methods in Classes

Methods define behavior for objects:

```csharp
class BankAccount
{
    public string AccountNumber;
    public decimal Balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${Balance}");
        }
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");
            return true;
        }
        Console.WriteLine("Insufficient funds!");
        return false;
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Account {AccountNumber}: ${Balance}");
    }
}

// Using the class
BankAccount account = new BankAccount();
account.AccountNumber = "12345";
account.Balance = 1000;

account.Deposit(500);     // Deposited $500. New balance: $1500
account.Withdraw(200);    // Withdrew $200. New balance: $1300
account.DisplayBalance(); // Account 12345: $1300
```

## Access Modifiers

Access modifiers control visibility:

| Modifier | Accessible From |
|----------|----------------|
| `public` | Anywhere |
| `private` | Same class only |
| `protected` | Same class and derived classes |
| `internal` | Same assembly |
| `protected internal` | Same assembly or derived classes |

```csharp
class Person
{
    public string Name;           // Anyone can access
    private int secretCode;       // Only this class can access
    protected string familyName;  // This class and children
}

Person p = new Person();
p.Name = "Alice";        // OK - public
// p.secretCode = 123;   // ERROR - private
```

**Best Practice:** Keep fields private and expose them through properties.

## Object Initializer Syntax

Create and initialize objects in one statement:

```csharp
// Without initializer
Dog dog1 = new Dog();
dog1.Name = "Buddy";
dog1.Age = 3;
dog1.Breed = "Labrador";

// With initializer (cleaner)
Dog dog2 = new Dog
{
    Name = "Max",
    Age = 5,
    Breed = "Beagle"
};

// Target-typed new (C# 9+)
Dog dog3 = new()
{
    Name = "Rocky",
    Age = 2,
    Breed = "Bulldog"
};
```

## Multiple Classes in a Project

Classes can be organized across multiple files:

```csharp
// File: Person.cs
class Person
{
    public string Name;
    public int Age;
}

// File: Address.cs
class Address
{
    public string Street;
    public string City;
    public string ZipCode;
}

// File: Program.cs
Person person = new Person { Name = "Alice", Age = 30 };
Address address = new Address { Street = "123 Main St", City = "NYC" };
```

## Classes with Multiple Objects Example

```csharp
class Employee
{
    public string Name;
    public string Department;
    public decimal Salary;

    public void DisplayInfo()
    {
        Console.WriteLine($"{Name} - {Department}: ${Salary:N2}");
    }
}

// Create multiple employees
Employee[] employees = new Employee[]
{
    new Employee { Name = "Alice", Department = "Engineering", Salary = 75000 },
    new Employee { Name = "Bob", Department = "Marketing", Salary = 65000 },
    new Employee { Name = "Charlie", Department = "Engineering", Salary = 80000 }
};

// Process all employees
foreach (Employee emp in employees)
{
    emp.DisplayInfo();
}
```

## Reference Type Behavior

Classes are reference types - variables hold references, not the actual object:

```csharp
class Point
{
    public int X;
    public int Y;
}

Point p1 = new Point { X = 10, Y = 20 };
Point p2 = p1;  // p2 references the SAME object as p1

p2.X = 100;     // Changes the shared object

Console.WriteLine(p1.X);  // 100 - p1 also sees the change!
```

### Comparing Objects

```csharp
Point a = new Point { X = 10, Y = 20 };
Point b = new Point { X = 10, Y = 20 };
Point c = a;

Console.WriteLine(a == b);  // False - different objects
Console.WriteLine(a == c);  // True - same reference
```

## Null References

Reference types can be `null`:

```csharp
Dog dog = null;  // No object

// Calling method on null causes NullReferenceException
// dog.Bark();  // Crashes!

// Always check for null
if (dog != null)
{
    dog.Bark();
}

// Or use null-conditional operator
dog?.Bark();  // Does nothing if dog is null
```

## Practical Examples

### Example 1: Rectangle Class

```csharp
class Rectangle
{
    public double Width;
    public double Height;

    public double CalculateArea()
    {
        return Width * Height;
    }

    public double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }

    public void Display()
    {
        Console.WriteLine($"Rectangle: {Width} x {Height}");
        Console.WriteLine($"Area: {CalculateArea()}");
        Console.WriteLine($"Perimeter: {CalculatePerimeter()}");
    }
}

Rectangle rect = new Rectangle { Width = 5, Height = 3 };
rect.Display();
// Rectangle: 5 x 3
// Area: 15
// Perimeter: 16
```

### Example 2: Player Class for a Game

```csharp
class Player
{
    public string Name;
    public int Health = 100;
    public int MaxHealth = 100;
    public int AttackPower = 10;
    public int Level = 1;
    public int Experience = 0;

    public void Attack(Player target)
    {
        Console.WriteLine($"{Name} attacks {target.Name} for {AttackPower} damage!");
        target.TakeDamage(AttackPower);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        Console.WriteLine($"{Name} has {Health}/{MaxHealth} HP remaining.");
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
        Console.WriteLine($"{Name} healed! Now has {Health}/{MaxHealth} HP.");
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}

Player hero = new Player { Name = "Hero", AttackPower = 15 };
Player enemy = new Player { Name = "Goblin", Health = 30, MaxHealth = 30 };

hero.Attack(enemy);
hero.Attack(enemy);
Console.WriteLine($"{enemy.Name} is alive: {enemy.IsAlive()}");
```

### Example 3: Shopping Cart

```csharp
class Product
{
    public string Name;
    public decimal Price;
    public int Quantity;

    public decimal GetTotal()
    {
        return Price * Quantity;
    }
}

class ShoppingCart
{
    public Product[] Items = new Product[10];
    public int ItemCount = 0;

    public void AddItem(Product product)
    {
        if (ItemCount < Items.Length)
        {
            Items[ItemCount] = product;
            ItemCount++;
            Console.WriteLine($"Added {product.Name} to cart.");
        }
    }

    public decimal GetTotal()
    {
        decimal total = 0;
        for (int i = 0; i < ItemCount; i++)
        {
            total += Items[i].GetTotal();
        }
        return total;
    }

    public void DisplayCart()
    {
        Console.WriteLine("=== Shopping Cart ===");
        for (int i = 0; i < ItemCount; i++)
        {
            var item = Items[i];
            Console.WriteLine($"{item.Name} x{item.Quantity} @ ${item.Price} = ${item.GetTotal()}");
        }
        Console.WriteLine($"Total: ${GetTotal()}");
    }
}

// Usage
ShoppingCart cart = new ShoppingCart();
cart.AddItem(new Product { Name = "Apple", Price = 0.50m, Quantity = 5 });
cart.AddItem(new Product { Name = "Bread", Price = 2.99m, Quantity = 2 });
cart.DisplayCart();
```

## Common Mistakes

### 1. Forgetting to Create an Object

```csharp
Dog dog;      // Declared but not created
// dog.Bark(); // ERROR: dog is null!

Dog dog2 = new Dog();  // Correct: created with new
dog2.Bark();  // Works
```

### 2. Confusing Class and Object

```csharp
// Wrong: trying to use class directly
// Dog.Name = "Buddy";  // ERROR

// Right: create an instance first
Dog myDog = new Dog();
myDog.Name = "Buddy";
```

### 3. Not Understanding References

```csharp
Dog dog1 = new Dog { Name = "Buddy" };
Dog dog2 = dog1;
dog2.Name = "Max";
Console.WriteLine(dog1.Name);  // "Max" - they're the same object!
```

## Summary

- A **class** is a blueprint defining fields, methods, and other members
- An **object** is an instance of a class created with `new`
- **Fields** store data; **methods** define behavior
- **Access modifiers** control visibility (public, private, etc.)
- Classes are **reference types** - variables hold references
- Use **object initializers** for cleaner code
- Always check for **null** before using reference types

Classes and objects are the foundation of object-oriented programming in C#. Master these concepts to build well-organized, maintainable applications.
