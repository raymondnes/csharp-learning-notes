# Arithmetic Operators

## Introduction

Arithmetic operators are the building blocks of mathematical computations in C#. They allow your programs to perform calculations, from simple addition to complex mathematical expressions. Understanding how these operators work, including their precedence and potential pitfalls, is essential for writing correct code.

## Basic Arithmetic Operators

C# provides five fundamental arithmetic operators:

| Operator | Name | Example | Result |
|----------|------|---------|--------|
| `+` | Addition | `5 + 3` | `8` |
| `-` | Subtraction | `5 - 3` | `2` |
| `*` | Multiplication | `5 * 3` | `15` |
| `/` | Division | `15 / 3` | `5` |
| `%` | Modulus (Remainder) | `17 % 5` | `2` |

### Addition (`+`)

```csharp
int sum = 10 + 5;           // 15
double total = 3.5 + 2.5;   // 6.0
string greeting = "Hello" + " " + "World";  // String concatenation
```

Note: The `+` operator also performs string concatenation when used with strings.

### Subtraction (`-`)

```csharp
int difference = 10 - 5;    // 5
double result = 7.5 - 2.3;  // 5.2
int negative = 5 - 10;      // -5
```

### Multiplication (`*`)

```csharp
int product = 6 * 7;        // 42
double area = 3.14 * 5 * 5; // 78.5 (area of circle)
```

### Division (`/`)

```csharp
int quotient = 15 / 3;      // 5
double precise = 15.0 / 4;  // 3.75
```

**Critical Concept: Integer Division**

When dividing two integers, C# performs **integer division**, which truncates the decimal part:

```csharp
int result = 7 / 2;         // 3 (not 3.5!)
int truncated = 15 / 4;     // 3 (not 3.75!)
```

To get decimal results, at least one operand must be a floating-point type:

```csharp
double result1 = 7.0 / 2;    // 3.5
double result2 = 7 / 2.0;    // 3.5
double result3 = (double)7 / 2;  // 3.5 (casting)
```

### Modulus (`%`)

The modulus operator returns the remainder after division:

```csharp
int remainder = 17 % 5;     // 2 (17 = 5*3 + 2)
int even = 10 % 2;          // 0 (10 is even)
int odd = 7 % 2;            // 1 (7 is odd)
```

**Common Uses:**
- Check if a number is even/odd: `number % 2 == 0`
- Wrap values around a range: `index % arrayLength`
- Extract digits: `number % 10` gives the last digit

```csharp
// Check even or odd
int number = 15;
if (number % 2 == 0)
    Console.WriteLine("Even");
else
    Console.WriteLine("Odd");

// Cycle through 0, 1, 2, 0, 1, 2...
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i % 3);
}
```

## Compound Assignment Operators

These operators combine arithmetic with assignment:

| Operator | Equivalent To | Example |
|----------|--------------|---------|
| `+=` | `a = a + b` | `x += 5;` |
| `-=` | `a = a - b` | `x -= 3;` |
| `*=` | `a = a * b` | `x *= 2;` |
| `/=` | `a = a / b` | `x /= 4;` |
| `%=` | `a = a % b` | `x %= 3;` |

```csharp
int score = 100;
score += 10;    // score = 110
score -= 25;    // score = 85
score *= 2;     // score = 170
score /= 10;    // score = 17
score %= 5;     // score = 2
```

## Increment and Decrement Operators

### Increment (`++`)

```csharp
int count = 5;
count++;        // count = 6 (post-increment)
++count;        // count = 7 (pre-increment)
```

### Decrement (`--`)

```csharp
int count = 5;
count--;        // count = 4 (post-decrement)
--count;        // count = 3 (pre-decrement)
```

### Pre vs. Post: The Crucial Difference

When used in expressions, the position matters:

```csharp
int a = 5;
int b = a++;    // b = 5, a = 6 (assign first, then increment)

int c = 5;
int d = ++c;    // d = 6, c = 6 (increment first, then assign)
```

**Demonstration:**

```csharp
int x = 10;
Console.WriteLine(x++);  // Prints 10, then x becomes 11
Console.WriteLine(x);    // Prints 11

int y = 10;
Console.WriteLine(++y);  // y becomes 11, then prints 11
Console.WriteLine(y);    // Prints 11
```

## Operator Precedence

Operators are evaluated in a specific order, similar to mathematical conventions:

| Precedence | Operators | Description |
|------------|-----------|-------------|
| Highest | `++`, `--` | Increment/Decrement |
| | `*`, `/`, `%` | Multiplication, Division, Modulus |
| Lowest | `+`, `-` | Addition, Subtraction |

**Left-to-right evaluation** occurs for operators of the same precedence.

```csharp
int result = 2 + 3 * 4;     // 14 (not 20!) - multiplication first
int result2 = (2 + 3) * 4;  // 20 - parentheses override precedence
int result3 = 10 / 2 * 5;   // 25 - left to right
int result4 = 10 - 5 - 2;   // 3 - left to right
```

### Using Parentheses for Clarity

Even when not required, parentheses improve readability:

```csharp
// Without parentheses (correct but harder to read)
double average = sum / count;

// With parentheses (clearer intent)
double average = (sum) / (count);

// Complex expression
double result = (a + b) * (c - d) / (e + f);
```

## Working with Different Types

### Numeric Type Coercion

When operators are used with different numeric types, C# follows promotion rules:

```csharp
int intVal = 10;
double doubleVal = 3.5;

// int is promoted to double
double result = intVal + doubleVal;  // 13.5

// Explicit casting needed for reverse
int truncated = (int)(intVal + doubleVal);  // 13
```

**Promotion hierarchy (lower to higher):**
`byte` → `short` → `int` → `long` → `float` → `double` → `decimal`

### Special Cases with Decimal

`decimal` does not automatically mix with `float` or `double`:

```csharp
decimal price = 19.99m;
double tax = 0.08;

// ERROR: Cannot mix decimal and double
// decimal total = price + tax;

// Must cast explicitly
decimal total = price + (decimal)tax;
```

## Common Pitfalls

### 1. Integer Division Truncation

```csharp
int percentage = 1 / 3 * 100;  // 0! (not 33)
// Fix:
double percentage = 1.0 / 3 * 100;  // 33.333...
```

### 2. Integer Overflow

```csharp
int max = int.MaxValue;  // 2,147,483,647
int overflow = max + 1;  // -2,147,483,648 (wraps around!)

// Use checked to detect overflow
checked
{
    int result = max + 1;  // Throws OverflowException
}
```

### 3. Division by Zero

```csharp
int result = 10 / 0;  // Throws DivideByZeroException

double result2 = 10.0 / 0;  // Returns Infinity (floating-point special case)
```

### 4. Floating-Point Precision

```csharp
double result = 0.1 + 0.2;
Console.WriteLine(result == 0.3);  // False!
Console.WriteLine(result);  // 0.30000000000000004

// For precise decimal arithmetic, use decimal
decimal precise = 0.1m + 0.2m;  // Exactly 0.3
```

## Practical Examples

### Example 1: Temperature Conversion

```csharp
double celsius = 25;
double fahrenheit = (celsius * 9 / 5) + 32;
Console.WriteLine($"{celsius}°C = {fahrenheit}°F");  // 25°C = 77°F
```

### Example 2: Calculate Average

```csharp
int score1 = 85, score2 = 92, score3 = 78;
double average = (score1 + score2 + score3) / 3.0;
Console.WriteLine($"Average: {average:F2}");  // Average: 85.00
```

### Example 3: Time Breakdown

```csharp
int totalSeconds = 3661;
int hours = totalSeconds / 3600;
int minutes = (totalSeconds % 3600) / 60;
int seconds = totalSeconds % 60;
Console.WriteLine($"{hours}h {minutes}m {seconds}s");  // 1h 1m 1s
```

### Example 4: Currency Calculation

```csharp
decimal unitPrice = 29.99m;
int quantity = 3;
decimal subtotal = unitPrice * quantity;
decimal taxRate = 0.08m;
decimal tax = subtotal * taxRate;
decimal total = subtotal + tax;

Console.WriteLine($"Subtotal: ${subtotal:F2}");  // $89.97
Console.WriteLine($"Tax: ${tax:F2}");             // $7.20
Console.WriteLine($"Total: ${total:F2}");         // $97.17
```

### Example 5: Circle Calculations

```csharp
const double PI = 3.14159265359;
double radius = 5;

double circumference = 2 * PI * radius;
double area = PI * radius * radius;
// Or using Math.Pow:
double areaAlt = PI * Math.Pow(radius, 2);

Console.WriteLine($"Circumference: {circumference:F2}");  // 31.42
Console.WriteLine($"Area: {area:F2}");  // 78.54
```

## The Math Class

For advanced mathematical operations, use the `System.Math` class:

```csharp
double power = Math.Pow(2, 8);      // 256 (2^8)
double squareRoot = Math.Sqrt(16);  // 4
double absolute = Math.Abs(-10);    // 10
double rounded = Math.Round(3.7);   // 4
double ceiling = Math.Ceiling(3.1); // 4
double floor = Math.Floor(3.9);     // 3
double max = Math.Max(10, 20);      // 20
double min = Math.Min(10, 20);      // 10
```

## Summary

- C# provides five basic arithmetic operators: `+`, `-`, `*`, `/`, `%`
- Integer division truncates decimal results
- Compound assignment operators (`+=`, `-=`, etc.) combine operation and assignment
- `++` and `--` have pre and post forms with different behaviors
- Operator precedence follows mathematical conventions (PEMDAS-like)
- Use parentheses for clarity and to override precedence
- Be aware of integer overflow and floating-point precision issues
- Use `decimal` for financial calculations
- The `Math` class provides advanced mathematical functions

Understanding arithmetic operators thoroughly prevents subtle bugs and enables you to write efficient mathematical code.
