# User Input and Type Casting

## Introduction

Interactive programs need to receive input from users. In console applications, we use `Console.ReadLine()` to capture user input. However, since all input comes as text (strings), we must convert it to appropriate data types before performing calculations. This process of converting between types is called **type casting** or **type conversion**.

## Reading User Input

### Console.ReadLine()

The `Console.ReadLine()` method reads a line of text from the user and returns it as a `string`.

```csharp
Console.Write("Enter your name: ");
string name = Console.ReadLine();
Console.WriteLine($"Hello, {name}!");
```

**Key behaviors:**
- Waits for the user to type and press Enter
- Returns everything typed as a single string
- Returns an empty string `""` if the user just presses Enter
- Can return `null` in certain scenarios (redirected input)

### Console.ReadKey()

For single character input without pressing Enter:

```csharp
Console.Write("Press any key to continue...");
ConsoleKeyInfo keyInfo = Console.ReadKey();
char keyPressed = keyInfo.KeyChar;
Console.WriteLine($"\nYou pressed: {keyPressed}");
```

### Console.Read()

Reads a single character and returns its ASCII value as an `int`:

```csharp
int asciiValue = Console.Read();
char character = (char)asciiValue;
```

## Type Conversion Overview

C# provides several ways to convert between types:

| Method | Use Case | Example |
|--------|----------|---------|
| Implicit Conversion | Safe, automatic conversions | `int` to `double` |
| Explicit Casting | Manual, may lose data | `double` to `int` |
| Parse Methods | String to numeric type | `int.Parse("42")` |
| TryParse Methods | Safe string parsing | `int.TryParse("42", out value)` |
| Convert Class | General conversions | `Convert.ToInt32("42")` |

## Implicit Conversion

Implicit conversions happen automatically when there's no risk of data loss:

```csharp
int intValue = 100;
long longValue = intValue;      // int to long - safe
double doubleValue = intValue;  // int to double - safe
float floatValue = intValue;    // int to float - safe
```

**Safe conversion paths:**
```
byte → short → int → long → float → double
                 ↘
                   decimal (requires explicit)
```

```csharp
byte b = 255;
short s = b;    // Implicit: byte to short
int i = s;      // Implicit: short to int
long l = i;     // Implicit: int to long
double d = l;   // Implicit: long to double
```

## Explicit Casting

When converting to a smaller or different type, you must use explicit casting. This acknowledges potential data loss:

```csharp
double price = 19.99;
int roundedPrice = (int)price;  // Explicit cast: 19 (truncates decimal)

long bigNumber = 1000;
int smallerNumber = (int)bigNumber;  // Explicit cast

float f = 3.14f;
int i = (int)f;  // Explicit cast: 3
```

### Casting Syntax

```csharp
targetType variable = (targetType)sourceValue;
```

### Data Loss Examples

```csharp
// Truncation (decimal part lost)
double d = 9.87;
int i = (int)d;         // i = 9 (not rounded, truncated!)

// Overflow (value too large)
int big = 300;
byte small = (byte)big;  // small = 44 (300 - 256)

// Precision loss
long precise = 123456789012345;
float f = precise;       // May lose precision
```

## Parsing Strings to Numbers

Since `Console.ReadLine()` returns strings, you need to parse them for calculations.

### Parse Methods

Each numeric type has a `Parse` method:

```csharp
string input = "42";
int number = int.Parse(input);       // 42
double d = double.Parse("3.14");     // 3.14
decimal m = decimal.Parse("19.99");  // 19.99
bool b = bool.Parse("true");         // true
```

**Exception danger:** Parse throws an exception if the string is invalid:

```csharp
int number = int.Parse("hello");  // Throws FormatException!
int number = int.Parse("");       // Throws FormatException!
int number = int.Parse(null);     // Throws ArgumentNullException!
```

### TryParse Methods (Recommended)

`TryParse` is safer because it doesn't throw exceptions:

```csharp
string input = "42";
bool success = int.TryParse(input, out int result);

if (success)
{
    Console.WriteLine($"Parsed successfully: {result}");
}
else
{
    Console.WriteLine("Invalid input!");
}
```

**How TryParse works:**
- Returns `true` if parsing succeeds, `false` if it fails
- The parsed value is stored in the `out` parameter
- If parsing fails, the `out` parameter is set to the type's default value (0 for int)

```csharp
// Compact form with out var
if (int.TryParse(Console.ReadLine(), out int age))
{
    Console.WriteLine($"Your age is {age}");
}
else
{
    Console.WriteLine("That's not a valid number!");
}
```

### Complete TryParse Examples

```csharp
// Integer
if (int.TryParse("123", out int intResult))
    Console.WriteLine($"Int: {intResult}");

// Double
if (double.TryParse("3.14", out double doubleResult))
    Console.WriteLine($"Double: {doubleResult}");

// Decimal
if (decimal.TryParse("19.99", out decimal decimalResult))
    Console.WriteLine($"Decimal: {decimalResult}");

// Boolean
if (bool.TryParse("true", out bool boolResult))
    Console.WriteLine($"Boolean: {boolResult}");
```

## The Convert Class

The `System.Convert` class provides methods for converting between types:

```csharp
// String to numeric
int i = Convert.ToInt32("42");
double d = Convert.ToDouble("3.14");
decimal m = Convert.ToDecimal("19.99");
bool b = Convert.ToBoolean("true");

// Numeric to string
string s = Convert.ToString(42);

// Between numeric types
int fromDouble = Convert.ToInt32(3.7);  // 4 (rounds, unlike casting!)
long fromInt = Convert.ToInt64(42);
```

### Convert vs. Parse vs. Casting

| Scenario | Best Choice | Reason |
|----------|-------------|--------|
| User input | `TryParse` | Handles invalid input gracefully |
| Trusted string data | `Parse` | Simpler, appropriate when you're sure input is valid |
| Numeric type change | Casting | Direct and efficient |
| Rounding required | `Convert` | Rounds instead of truncates |
| Null handling | `Convert` | Returns 0 for null instead of throwing |

```csharp
// Rounding difference
double d = 3.7;
int cast = (int)d;              // 3 (truncates)
int convert = Convert.ToInt32(d);  // 4 (rounds)

// Null handling
string nullStr = null;
// int.Parse(nullStr);           // Throws exception
int convert2 = Convert.ToInt32(nullStr);  // Returns 0
```

## Complete Input Examples

### Example 1: Simple Integer Input

```csharp
Console.Write("Enter your age: ");
string input = Console.ReadLine();
int age = int.Parse(input);
Console.WriteLine($"In 10 years, you'll be {age + 10}");
```

### Example 2: Validated Input

```csharp
Console.Write("Enter a number: ");
string input = Console.ReadLine();

if (int.TryParse(input, out int number))
{
    Console.WriteLine($"You entered: {number}");
    Console.WriteLine($"Doubled: {number * 2}");
}
else
{
    Console.WriteLine("That's not a valid number!");
}
```

### Example 3: Input Loop Until Valid

```csharp
int age;
bool validInput = false;

while (!validInput)
{
    Console.Write("Enter your age: ");
    validInput = int.TryParse(Console.ReadLine(), out age);

    if (!validInput)
    {
        Console.WriteLine("Invalid input. Please enter a number.");
    }
}

Console.WriteLine($"Your age is {age}");
```

### Example 4: Multiple Inputs

```csharp
Console.Write("Enter your first name: ");
string firstName = Console.ReadLine();

Console.Write("Enter your last name: ");
string lastName = Console.ReadLine();

Console.Write("Enter your birth year: ");
if (int.TryParse(Console.ReadLine(), out int birthYear))
{
    int age = DateTime.Now.Year - birthYear;
    Console.WriteLine($"Hello, {firstName} {lastName}! You are approximately {age} years old.");
}
else
{
    Console.WriteLine("Invalid birth year!");
}
```

### Example 5: Calculator with Validation

```csharp
Console.Write("Enter first number: ");
if (!double.TryParse(Console.ReadLine(), out double num1))
{
    Console.WriteLine("Invalid number!");
    return;
}

Console.Write("Enter second number: ");
if (!double.TryParse(Console.ReadLine(), out double num2))
{
    Console.WriteLine("Invalid number!");
    return;
}

Console.WriteLine($"Sum: {num1 + num2}");
Console.WriteLine($"Difference: {num1 - num2}");
Console.WriteLine($"Product: {num1 * num2}");

if (num2 != 0)
{
    Console.WriteLine($"Quotient: {num1 / num2}");
}
else
{
    Console.WriteLine("Cannot divide by zero!");
}
```

## ToString() Method

Every type in C# has a `ToString()` method for converting to string:

```csharp
int number = 42;
string s = number.ToString();  // "42"

double pi = 3.14159;
string piStr = pi.ToString();  // "3.14159"
string formatted = pi.ToString("F2");  // "3.14" (2 decimal places)

bool flag = true;
string boolStr = flag.ToString();  // "True"
```

### Formatting Options

```csharp
double value = 1234.5678;

Console.WriteLine(value.ToString("F2"));   // 1234.57 (fixed-point)
Console.WriteLine(value.ToString("N2"));   // 1,234.57 (with thousands separator)
Console.WriteLine(value.ToString("C"));    // $1,234.57 (currency)
Console.WriteLine(value.ToString("E2"));   // 1.23E+003 (scientific)
Console.WriteLine(value.ToString("P0"));   // 123,457% (percent)

int num = 42;
Console.WriteLine(num.ToString("D5"));     // 00042 (padded)
Console.WriteLine(num.ToString("X"));      // 2A (hexadecimal)
```

## Common Pitfalls

### 1. Forgetting to Parse

```csharp
Console.Write("Enter a number: ");
string input = Console.ReadLine();
// int doubled = input * 2;  // ERROR: Cannot multiply string!
int doubled = int.Parse(input) * 2;  // Correct
```

### 2. Culture-Sensitive Parsing

Decimal separators vary by culture:

```csharp
// In US: period is decimal separator
double.Parse("3.14");  // Works

// In Germany: comma is decimal separator
// double.Parse("3.14");  // May fail!

// Culture-invariant parsing
double.Parse("3.14", CultureInfo.InvariantCulture);
```

### 3. Overflow During Conversion

```csharp
string bigNumber = "999999999999";
// int small = int.Parse(bigNumber);  // Throws OverflowException!

// Check first or use TryParse
if (int.TryParse(bigNumber, out int result))
{
    Console.WriteLine(result);
}
else
{
    Console.WriteLine("Number too large for int!");
}
```

### 4. Null Input Handling

```csharp
string input = Console.ReadLine();  // Could be null!

// Safe approach
if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int value))
{
    // Use value
}
```

## Summary

- `Console.ReadLine()` reads user input as a string
- Use `TryParse` for safe string-to-number conversion
- Use `Parse` when you're certain the input is valid
- Implicit conversion happens automatically for safe conversions
- Explicit casting `(type)` is needed when data loss is possible
- `Convert` class methods round instead of truncating
- Always validate user input before processing
- Use `ToString()` with format specifiers for formatted output

Proper handling of user input and type conversion is essential for building robust, user-friendly applications.
