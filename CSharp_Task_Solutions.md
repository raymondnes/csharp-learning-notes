# C# Task Solutions

This file contains the accepted solutions for the coding tasks completed during the C# lessons.

---

## Week 1: C# Fundamentals & .NET Basics

### Task 1.1: Declare and Initialize

**Description:** Declare a `string` and an `int` variable, assign them values, and print them to the console.

**Your Solution:**
```csharp
string welcome = "Hello World!";
int num = 4;
Console.WriteLine(welcome);
Console.WriteLine(num);
```

**Optimal Code:** Your solution is already optimal for this task.

---

### Task 1.2: Arithmetic and Comparison Operators

**Description:** Perform and print the results of basic arithmetic (+, -, *, /) and comparison (==, !=, >, <=) operations on two `int` variables.

**Your Solution:**
```csharp
int a = 5;
int b = 10;

Console.WriteLine(a + b);
Console.WriteLine(a - b);
Console.WriteLine(a * b);
Console.WriteLine(a / b);
Console.WriteLine(a == b);
Console.WriteLine(a != b);
Console.WriteLine(a > b);
Console.WriteLine(a <= b);
```

**Optimal Code (with labels for clarity):**
```csharp
int a = 5;
int b = 10;

Console.WriteLine($"a + b = {a + b}");
Console.WriteLine($"a - b = {a - b}");
Console.WriteLine($"a * b = {a * b}");
Console.WriteLine($"a / b = {a / b}"); // Note: Integer division

Console.WriteLine($"a == b is {a == b}");
Console.WriteLine($"a != b is {a != b}");
Console.WriteLine($"a > b is {a > b}");
Console.WriteLine($"a <= b is {a <= b}");
```

---

### Task 1.3: Exploring Division

**Description:** Demonstrate the difference between integer division and floating-point division by casting an `int` to a `double`.

**Your Solution:**
```csharp
int a = 5;
int b = 10;

Console.WriteLine((double) a / b); // this gives 0.5
```

**Optimal Code:** Your solution is already optimal. It clearly demonstrates the concept.

---

### Task 1.5: Refined Conditional Logic (if-else if-else)

**Description:** Implement a greeting program that provides distinct messages for "Alice", "Bob", and a default case using an `if-else if-else` structure.

**Your Solution:**
```csharp
Console.Write("Please, Enter your name: ");
string userName = Console.ReadLine();

if (userName == "Alice")
    Console.WriteLine("Hello, Alice!");
else if (userName == "Bob")
    Console.WriteLine("Hi, Bob!");
else
    Console.WriteLine("Hello, stranger!");
```

**Optimal Code:** Your solution is already optimal.

---

### Task 1.6: Introduction to `for` loops

**Description:** Write a `for` loop to print numbers 1 to 5, and another to print "C# is fun!" three times.

**Your Solution:**
```csharp
// Print numbers 1 to 5
for (int p = 1; p <= 5; p++)
    Console.WriteLine(p);

// Print message three times
for (int q = 0; q < 3; q++)
    Console.WriteLine("C# is fun");
```

**Optimal Code:** Your solution is already optimal.

---

### Task 1.7: Introduction to `do-while` loops

**Description:** Write a `do-while` loop that always runs at least once and continues as long as the user enters 'yes' (case-insensitively).

**Your Solution:**
```csharp
string onGoingStatus;
string lower;
do
{
    Console.WriteLine("This will always run at least once");
    Console.Write("Enter \'yes\' to continue or \'no\' to stop: ");
    onGoingStatus = Console.ReadLine();
    lower = onGoingStatus.ToLower();
}
while (lower == "yes");
Console.WriteLine("The user has ended the loop");
```

**Optimal Code (more concise):** A slightly more concise version avoids the extra `lower` variable by performing the `ToLower()` call directly in the condition. Both are correct.
```csharp
string onGoingStatus;
do
{
    Console.WriteLine("This will always run at least once");
    Console.Write("Enter 'yes' to continue or 'no' to stop: ");
    onGoingStatus = Console.ReadLine();
}
while (onGoingStatus.ToLower() == "yes");

Console.WriteLine("The user has ended the loop");
```

---


### Task 1.8: Implementing a `switch` Statement

**Description:** Rewrite the greeting program from Task 1.5 using a `switch` statement.

**Your Solution:**
```csharp
Console.Write("Please, enter your name: ");
string userName = Console.ReadLine();
string greeting;

switch (userName)
{
    case "Alice":
        greeting = "Hello, Alice!";
        break;
    case "Bob":
        greeting = "Hi, Bob!";
        break;
    default:
        greeting = "Hello, stranger!";
        break;
}

Console.WriteLine(greeting);
```

**Optimal Code:** Your solution is already optimal.

---

