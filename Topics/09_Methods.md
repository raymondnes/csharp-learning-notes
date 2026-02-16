# Methods: Writing and Calling

## Introduction

Methods are the building blocks of organized code. They allow you to group related statements together, give that group a name, and call it whenever needed. Methods promote code reuse, improve readability, and make programs easier to maintain.

## What is a Method?

A method is a block of code that performs a specific task. Think of it as a recipe - you define the steps once and can follow them (call the method) whenever you need that result.

### Benefits of Using Methods

1. **Code Reuse**: Write once, use many times
2. **Organization**: Group related code together
3. **Readability**: Give meaningful names to operations
4. **Maintainability**: Change code in one place
5. **Testing**: Test individual components in isolation

## Method Syntax

```csharp
accessModifier returnType MethodName(parameters)
{
    // Method body - code that executes when called
    return value;  // If returnType is not void
}
```

### Components

- **Access Modifier**: Controls visibility (`public`, `private`, etc.)
- **Return Type**: The type of value returned (`int`, `string`, `void`, etc.)
- **Method Name**: Identifier used to call the method (PascalCase convention)
- **Parameters**: Input values the method accepts (optional)
- **Method Body**: The code that executes
- **Return Statement**: The value returned to the caller (if not void)

## Simple Method Examples

### Method with No Parameters, No Return Value

```csharp
void SayHello()
{
    Console.WriteLine("Hello, World!");
}

// Calling the method
SayHello();  // Output: Hello, World!
SayHello();  // Output: Hello, World!
```

### Method with Parameters

```csharp
void Greet(string name)
{
    Console.WriteLine($"Hello, {name}!");
}

// Calling with different arguments
Greet("Alice");  // Output: Hello, Alice!
Greet("Bob");    // Output: Hello, Bob!
```

### Method with Return Value

```csharp
int Add(int a, int b)
{
    return a + b;
}

// Using the returned value
int result = Add(5, 3);
Console.WriteLine(result);  // Output: 8

// Can use directly in expressions
Console.WriteLine(Add(10, 20));  // Output: 30
```

## The void Keyword

`void` indicates a method returns nothing:

```csharp
void PrintMessage(string message)
{
    Console.WriteLine(message);
    // No return statement needed
}

// void methods cannot be used in expressions
// int x = PrintMessage("test");  // ERROR!
```

You can use `return;` in void methods to exit early:

```csharp
void ProcessAge(int age)
{
    if (age < 0)
    {
        Console.WriteLine("Invalid age");
        return;  // Exit method early
    }

    Console.WriteLine($"Age is {age}");
}
```

## Return Statements

The `return` statement exits the method and provides a value:

```csharp
int GetMax(int a, int b)
{
    if (a > b)
    {
        return a;
    }
    return b;
}

// Or more concisely:
int GetMaxShort(int a, int b)
{
    return a > b ? a : b;
}
```

### Multiple Return Points

```csharp
string GetGrade(int score)
{
    if (score >= 90) return "A";
    if (score >= 80) return "B";
    if (score >= 70) return "C";
    if (score >= 60) return "D";
    return "F";
}
```

### All Code Paths Must Return

```csharp
// ERROR: Not all code paths return a value
int Bad(int x)
{
    if (x > 0)
    {
        return x;
    }
    // What about x <= 0? Missing return!
}

// CORRECT
int Good(int x)
{
    if (x > 0)
    {
        return x;
    }
    return 0;  // Handles all cases
}
```

## Parameters and Arguments

- **Parameters**: Variables declared in method signature
- **Arguments**: Actual values passed when calling the method

```csharp
//        parameters
//           ↓   ↓
void Display(int x, int y)
{
    Console.WriteLine($"x = {x}, y = {y}");
}

//    arguments
//       ↓   ↓
Display(10, 20);
```

### Multiple Parameters

```csharp
double CalculateVolume(double length, double width, double height)
{
    return length * width * height;
}

double volume = CalculateVolume(5.0, 3.0, 2.0);
Console.WriteLine($"Volume: {volume}");  // Volume: 30
```

### Parameter Order Matters

```csharp
void ShowInfo(string name, int age)
{
    Console.WriteLine($"{name} is {age} years old.");
}

ShowInfo("Alice", 25);    // Correct
// ShowInfo(25, "Alice"); // ERROR: Wrong types
```

## Named Arguments

You can specify arguments by name, allowing any order:

```csharp
void CreateUser(string name, int age, string city)
{
    Console.WriteLine($"{name}, {age}, from {city}");
}

// Positional (order matters)
CreateUser("Alice", 25, "NYC");

// Named (order doesn't matter)
CreateUser(age: 25, city: "NYC", name: "Alice");

// Mixed (positional first, then named)
CreateUser("Alice", city: "NYC", age: 25);
```

## Default Parameter Values

Parameters can have default values:

```csharp
void Greet(string name, string greeting = "Hello")
{
    Console.WriteLine($"{greeting}, {name}!");
}

Greet("Alice");           // Hello, Alice!
Greet("Bob", "Welcome");  // Welcome, Bob!
```

**Rules for default parameters:**
- Must appear after required parameters
- Are evaluated at compile time
- Must be constants or simple expressions

```csharp
void Configure(string host, int port = 8080, bool ssl = false)
{
    Console.WriteLine($"Connecting to {host}:{port}, SSL: {ssl}");
}

Configure("localhost");                    // localhost:8080, SSL: False
Configure("localhost", 443);               // localhost:443, SSL: False
Configure("localhost", 443, true);         // localhost:443, SSL: True
Configure("localhost", ssl: true);         // localhost:8080, SSL: True
```

## Method Overloading

Multiple methods can have the same name with different parameters:

```csharp
int Add(int a, int b)
{
    return a + b;
}

double Add(double a, double b)
{
    return a + b;
}

int Add(int a, int b, int c)
{
    return a + b + c;
}

// The compiler chooses the right version
Console.WriteLine(Add(1, 2));        // Calls int version: 3
Console.WriteLine(Add(1.5, 2.5));    // Calls double version: 4.0
Console.WriteLine(Add(1, 2, 3));     // Calls three-param version: 6
```

**Rules for overloading:**
- Methods must differ in parameter count or types
- Return type alone is not enough to distinguish

## Expression-Bodied Methods

For simple methods, use the `=>` syntax:

```csharp
// Traditional
int Square(int x)
{
    return x * x;
}

// Expression-bodied (C# 6+)
int Square(int x) => x * x;

// Works with void methods too
void PrintHello() => Console.WriteLine("Hello!");

// Multiple expressions? Use traditional body
int ComplexCalculation(int x)
{
    int temp = x * 2;
    return temp + 10;
}
```

## Local Functions

Methods can be defined inside other methods (C# 7+):

```csharp
void ProcessData()
{
    int[] numbers = { 1, 2, 3, 4, 5 };

    // Local function
    int Sum(int[] arr)
    {
        int total = 0;
        foreach (int n in arr)
        {
            total += n;
        }
        return total;
    }

    Console.WriteLine($"Sum: {Sum(numbers)}");
}
```

Local functions are useful for helper logic that's only needed in one place.

## Static Methods

Methods can be `static`, meaning they belong to the class, not instances:

```csharp
class Calculator
{
    public static int Add(int a, int b)
    {
        return a + b;
    }
}

// Called on the class, not an object
int result = Calculator.Add(5, 3);
```

In top-level programs, methods are implicitly static:

```csharp
// Program.cs
int Square(int x) => x * x;

Console.WriteLine(Square(5));  // 25
```

## Practical Examples

### Example 1: Temperature Converter

```csharp
double CelsiusToFahrenheit(double celsius)
{
    return (celsius * 9 / 5) + 32;
}

double FahrenheitToCelsius(double fahrenheit)
{
    return (fahrenheit - 32) * 5 / 9;
}

Console.WriteLine($"25°C = {CelsiusToFahrenheit(25)}°F");  // 77°F
Console.WriteLine($"98.6°F = {FahrenheitToCelsius(98.6):F1}°C");  // 37.0°C
```

### Example 2: String Utilities

```csharp
bool IsValidEmail(string email)
{
    return !string.IsNullOrEmpty(email) &&
           email.Contains("@") &&
           email.Contains(".");
}

string Truncate(string text, int maxLength)
{
    if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
        return text;
    return text.Substring(0, maxLength) + "...";
}

Console.WriteLine(IsValidEmail("test@example.com"));  // True
Console.WriteLine(Truncate("Hello World", 5));  // Hello...
```

### Example 3: Array Operations

```csharp
int FindMax(int[] array)
{
    if (array.Length == 0)
        throw new ArgumentException("Array cannot be empty");

    int max = array[0];
    for (int i = 1; i < array.Length; i++)
    {
        if (array[i] > max)
            max = array[i];
    }
    return max;
}

double CalculateAverage(int[] array)
{
    int sum = 0;
    for (int i = 0; i < array.Length; i++)
    {
        sum += array[i];
    }
    return (double)sum / array.Length;
}

int[] numbers = { 5, 2, 8, 1, 9 };
Console.WriteLine($"Max: {FindMax(numbers)}");  // 9
Console.WriteLine($"Average: {CalculateAverage(numbers)}");  // 5
```

### Example 4: Building a Simple Calculator

```csharp
double Calculate(double a, double b, char operation)
{
    return operation switch
    {
        '+' => a + b,
        '-' => a - b,
        '*' => a * b,
        '/' => b != 0 ? a / b : throw new DivideByZeroException(),
        _ => throw new ArgumentException($"Unknown operation: {operation}")
    };
}

void PrintResult(double a, double b, char op)
{
    try
    {
        double result = Calculate(a, b, op);
        Console.WriteLine($"{a} {op} {b} = {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

PrintResult(10, 5, '+');  // 10 + 5 = 15
PrintResult(10, 5, '-');  // 10 - 5 = 5
PrintResult(10, 0, '/');  // Error: Attempted to divide by zero.
```

## Common Mistakes

### 1. Forgetting to Return

```csharp
// ERROR: Missing return
int GetValue()
{
    int x = 5;
    // Forgot: return x;
}
```

### 2. Unreachable Code

```csharp
int Test()
{
    return 5;
    Console.WriteLine("Never reached");  // Warning: Unreachable code
}
```

### 3. Returning Wrong Type

```csharp
int GetNumber()
{
    return "hello";  // ERROR: Cannot convert string to int
}
```

## Summary

- Methods group related code into reusable units
- Use `void` when no value is returned
- The `return` statement exits and optionally provides a value
- Parameters receive values; arguments are passed values
- Named arguments improve readability
- Default parameters make arguments optional
- Method overloading allows same name with different signatures
- Expression-bodied methods provide concise syntax
- Static methods belong to the class, not instances

Methods are fundamental to writing organized, maintainable code. Master them and you'll write better programs.
