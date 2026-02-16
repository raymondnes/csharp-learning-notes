# Inheritance

## Introduction

Inheritance is a fundamental OOP concept that allows a class to inherit properties and methods from another class. This enables code reuse, establishes "is-a" relationships, and creates hierarchical class structures.

## Base and Derived Classes

```csharp
// Base class (parent)
class Animal
{
    public string Name { get; set; }

    public void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }
}

// Derived class (child) - inherits from Animal
class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

// Usage
Dog dog = new Dog();
dog.Name = "Buddy";  // Inherited from Animal
dog.Eat();           // Inherited from Animal
dog.Bark();          // Defined in Dog
```

## The : Syntax

```csharp
class DerivedClass : BaseClass
{
    // DerivedClass inherits all members from BaseClass
}
```

## What Gets Inherited

| Member Type | Inherited? |
|-------------|-----------|
| Public fields | Yes |
| Public methods | Yes |
| Public properties | Yes |
| Protected members | Yes (accessible in derived) |
| Private members | No (but exist in object) |
| Constructors | No (but can be called) |
| Static members | Accessible but not inherited |

## The base Keyword

Call base class constructor or methods:

```csharp
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Employee : Person
{
    public string Department { get; set; }
    public decimal Salary { get; set; }

    // Call base constructor
    public Employee(string name, int age, string department, decimal salary)
        : base(name, age)  // Calls Person(name, age)
    {
        Department = department;
        Salary = salary;
    }
}

Employee emp = new Employee("Alice", 30, "Engineering", 75000);
Console.WriteLine($"{emp.Name}, {emp.Age}, {emp.Department}");
```

## Protected Access Modifier

`protected` members are accessible in the class and derived classes:

```csharp
class Animal
{
    public string Name { get; set; }
    protected int energy = 100;  // Accessible in derived classes

    protected void Rest()
    {
        energy += 10;
        Console.WriteLine("Resting...");
    }
}

class Dog : Animal
{
    public void Play()
    {
        energy -= 20;  // Can access protected member
        Console.WriteLine($"Playing! Energy: {energy}");

        if (energy < 30)
            Rest();  // Can call protected method
    }
}
```

## Method Overriding

Derived classes can override base class methods:

```csharp
class Animal
{
    public virtual void Speak()  // virtual allows overriding
    {
        Console.WriteLine("Some sound");
    }
}

class Dog : Animal
{
    public override void Speak()  // override replaces behavior
    {
        Console.WriteLine("Woof!");
    }
}

class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Meow!");
    }
}

Animal animal = new Animal();
Animal dog = new Dog();
Animal cat = new Cat();

animal.Speak();  // Some sound
dog.Speak();     // Woof!
cat.Speak();     // Meow!
```

## Calling Base Implementation

```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("*generic animal sound*");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        base.Speak();  // Call base implementation
        Console.WriteLine("Woof!");
    }
}

Dog dog = new Dog();
dog.Speak();
// Output:
// *generic animal sound*
// Woof!
```

## Sealed Classes and Methods

Prevent further inheritance:

```csharp
// Sealed class - cannot be inherited
sealed class FinalClass
{
    public void Method() { }
}

// class DerivedFromFinal : FinalClass { }  // ERROR!

// Sealed method - cannot be overridden further
class Animal
{
    public virtual void Move() { }
}

class Dog : Animal
{
    public sealed override void Move()  // Sealed override
    {
        Console.WriteLine("Dog runs");
    }
}

class Poodle : Dog
{
    // public override void Move() { }  // ERROR! Move is sealed
}
```

## Constructor Chaining

```csharp
class Vehicle
{
    public string Brand { get; set; }
    public int Year { get; set; }

    public Vehicle()
    {
        Brand = "Unknown";
        Year = 2000;
    }

    public Vehicle(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }
}

class Car : Vehicle
{
    public int Doors { get; set; }

    public Car() : base()  // Calls Vehicle()
    {
        Doors = 4;
    }

    public Car(string brand, int year, int doors) : base(brand, year)
    {
        Doors = doors;
    }
}
```

## Is-A Relationship

Inheritance represents "is-a" relationships:

```csharp
// Dog IS-A Animal
// Car IS-A Vehicle
// Employee IS-A Person

Dog dog = new Dog();
Animal animal = dog;  // OK - Dog is-a Animal

// Type checking
if (animal is Dog)
{
    Console.WriteLine("It's a dog!");
}
```

## Practical Examples

### Example 1: Employee Hierarchy

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
```

### Example 2: Shape Hierarchy

```csharp
class Shape
{
    public string Color { get; set; }

    public virtual double GetArea()
    {
        return 0;
    }

    public virtual void Draw()
    {
        Console.WriteLine($"Drawing a {Color} shape");
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
        Console.WriteLine($"Drawing a {Color} circle with radius {Radius}");
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
}
```

## Summary

- Inheritance creates "is-a" relationships between classes
- Use `:` to inherit from a base class
- `base` keyword accesses base class members
- `protected` makes members accessible to derived classes
- `virtual` allows methods to be overridden
- `override` replaces base method behavior
- `sealed` prevents further inheritance
- C# supports single inheritance only (one base class)

Inheritance promotes code reuse and establishes clear hierarchical relationships.
