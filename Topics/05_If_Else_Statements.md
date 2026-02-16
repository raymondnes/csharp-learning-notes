# If-Else Statements and Comparison Operators

## Introduction

Decision-making is at the heart of programming. The `if-else` statement allows your program to execute different code paths based on conditions. Combined with comparison operators, you can build programs that respond intelligently to different situations.

## Comparison Operators

Comparison operators compare two values and return a `bool` result (`true` or `false`).

| Operator | Name | Example | Result |
|----------|------|---------|--------|
| `==` | Equal to | `5 == 5` | `true` |
| `!=` | Not equal to | `5 != 3` | `true` |
| `>` | Greater than | `5 > 3` | `true` |
| `<` | Less than | `5 < 3` | `false` |
| `>=` | Greater than or equal | `5 >= 5` | `true` |
| `<=` | Less than or equal | `3 <= 5` | `true` |

### Examples

```csharp
int a = 10;
int b = 20;

bool isEqual = (a == b);        // false
bool isNotEqual = (a != b);     // true
bool isGreater = (a > b);       // false
bool isLess = (a < b);          // true
bool isGreaterOrEqual = (a >= 10);  // true
bool isLessOrEqual = (b <= 20);     // true
```

### Comparing Strings

```csharp
string name1 = "Alice";
string name2 = "alice";

bool exact = (name1 == name2);  // false (case-sensitive)
bool caseInsensitive = name1.Equals(name2, StringComparison.OrdinalIgnoreCase);  // true
```

## The Basic If Statement

The `if` statement executes a block of code only when its condition is `true`.

### Syntax

```csharp
if (condition)
{
    // Code to execute when condition is true
}
```

### Examples

```csharp
int age = 18;

if (age >= 18)
{
    Console.WriteLine("You are an adult.");
}

// Single statement (braces optional, but recommended)
if (age >= 18)
    Console.WriteLine("You can vote.");
```

## The If-Else Statement

Add an `else` block for code that runs when the condition is `false`.

### Syntax

```csharp
if (condition)
{
    // Code when condition is true
}
else
{
    // Code when condition is false
}
```

### Examples

```csharp
int temperature = 25;

if (temperature >= 30)
{
    Console.WriteLine("It's hot outside!");
}
else
{
    Console.WriteLine("It's not too hot.");
}
```

```csharp
int score = 75;

if (score >= 60)
{
    Console.WriteLine("You passed!");
}
else
{
    Console.WriteLine("You failed. Try again.");
}
```

## The Else-If Chain

For multiple conditions, use `else if`:

### Syntax

```csharp
if (condition1)
{
    // Code when condition1 is true
}
else if (condition2)
{
    // Code when condition2 is true
}
else if (condition3)
{
    // Code when condition3 is true
}
else
{
    // Code when all conditions are false
}
```

### Example: Grade Calculator

```csharp
int score = 85;
string grade;

if (score >= 90)
{
    grade = "A";
}
else if (score >= 80)
{
    grade = "B";
}
else if (score >= 70)
{
    grade = "C";
}
else if (score >= 60)
{
    grade = "D";
}
else
{
    grade = "F";
}

Console.WriteLine($"Your grade is: {grade}");  // Output: Your grade is: B
```

### Example: Age Categories

```csharp
int age = 35;

if (age < 13)
{
    Console.WriteLine("Child");
}
else if (age < 20)
{
    Console.WriteLine("Teenager");
}
else if (age < 60)
{
    Console.WriteLine("Adult");
}
else
{
    Console.WriteLine("Senior");
}
```

## Logical Operators

Combine multiple conditions using logical operators:

| Operator | Name | Description |
|----------|------|-------------|
| `&&` | AND | Both conditions must be true |
| `\|\|` | OR | At least one condition must be true |
| `!` | NOT | Inverts the condition |

### AND Operator (`&&`)

```csharp
int age = 25;
bool hasLicense = true;

if (age >= 18 && hasLicense)
{
    Console.WriteLine("You can drive.");
}
```

Both conditions must be `true` for the block to execute.

### OR Operator (`||`)

```csharp
bool isWeekend = true;
bool isHoliday = false;

if (isWeekend || isHoliday)
{
    Console.WriteLine("No work today!");
}
```

Only one condition needs to be `true`.

### NOT Operator (`!`)

```csharp
bool isRaining = false;

if (!isRaining)
{
    Console.WriteLine("Let's go outside!");
}
```

Inverts `false` to `true` and vice versa.

### Combining Operators

```csharp
int age = 25;
bool isStudent = true;
bool hasId = true;

// Complex condition with parentheses for clarity
if ((age >= 18 && age <= 30) && (isStudent || hasId))
{
    Console.WriteLine("You qualify for the discount.");
}
```

## Short-Circuit Evaluation

C# uses short-circuit evaluation for logical operators:

- `&&`: If the first condition is `false`, the second is not evaluated
- `||`: If the first condition is `true`, the second is not evaluated

```csharp
// Safe null check using short-circuit
string name = null;

if (name != null && name.Length > 0)
{
    Console.WriteLine(name);
}
// If name is null, name.Length is never evaluated (would cause exception)
```

## Nested If Statements

You can place `if` statements inside other `if` statements:

```csharp
int age = 25;
bool hasTicket = true;

if (age >= 18)
{
    if (hasTicket)
    {
        Console.WriteLine("Welcome to the show!");
    }
    else
    {
        Console.WriteLine("You need a ticket.");
    }
}
else
{
    Console.WriteLine("You must be 18 or older.");
}
```

**Note:** Deeply nested conditions can be hard to read. Consider using logical operators or early returns instead.

### Refactored Version

```csharp
int age = 25;
bool hasTicket = true;

if (age < 18)
{
    Console.WriteLine("You must be 18 or older.");
}
else if (!hasTicket)
{
    Console.WriteLine("You need a ticket.");
}
else
{
    Console.WriteLine("Welcome to the show!");
}
```

## The Ternary Operator

For simple if-else assignments, use the ternary operator `?:`:

### Syntax

```csharp
result = condition ? valueIfTrue : valueIfFalse;
```

### Examples

```csharp
int age = 20;
string status = age >= 18 ? "Adult" : "Minor";
Console.WriteLine(status);  // Adult

// Equivalent if-else:
string status2;
if (age >= 18)
{
    status2 = "Adult";
}
else
{
    status2 = "Minor";
}
```

```csharp
int score = 75;
string result = score >= 60 ? "Pass" : "Fail";

// Can be used inline
Console.WriteLine($"Result: {(score >= 60 ? "Pass" : "Fail")}");
```

### Nested Ternary (Use Sparingly)

```csharp
int num = 0;
string sign = num > 0 ? "Positive" : num < 0 ? "Negative" : "Zero";
```

**Warning:** Nested ternary operators reduce readability. Prefer if-else for complex conditions.

## Pattern Matching with Is

C# supports pattern matching in `if` statements:

```csharp
object value = 42;

if (value is int number)
{
    Console.WriteLine($"It's an integer: {number}");
}

// Null checking
string name = null;
if (name is null)
{
    Console.WriteLine("Name is null");
}

// Not null
if (name is not null)
{
    Console.WriteLine($"Name is: {name}");
}
```

## Null Conditional and Null Coalescing

### Null Conditional Operator (`?.`)

```csharp
string name = null;
int? length = name?.Length;  // null (doesn't throw exception)

// Equivalent to:
int? length2;
if (name != null)
{
    length2 = name.Length;
}
else
{
    length2 = null;
}
```

### Null Coalescing Operator (`??`)

```csharp
string name = null;
string displayName = name ?? "Unknown";  // "Unknown"

// Equivalent to:
string displayName2;
if (name != null)
{
    displayName2 = name;
}
else
{
    displayName2 = "Unknown";
}
```

### Combined Usage

```csharp
string name = null;
int length = name?.Length ?? 0;  // 0
```

## Practical Examples

### Example 1: Login Validation

```csharp
string username = "admin";
string password = "secret123";

if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
{
    Console.WriteLine("Username and password are required.");
}
else if (username == "admin" && password == "secret123")
{
    Console.WriteLine("Login successful!");
}
else
{
    Console.WriteLine("Invalid credentials.");
}
```

### Example 2: Number Classification

```csharp
Console.Write("Enter a number: ");
if (int.TryParse(Console.ReadLine(), out int number))
{
    if (number > 0)
    {
        Console.WriteLine($"{number} is positive.");

        if (number % 2 == 0)
            Console.WriteLine("It's also even.");
        else
            Console.WriteLine("It's also odd.");
    }
    else if (number < 0)
    {
        Console.WriteLine($"{number} is negative.");
    }
    else
    {
        Console.WriteLine("Zero is neither positive nor negative.");
    }
}
else
{
    Console.WriteLine("Invalid input.");
}
```

### Example 3: Ticket Pricing

```csharp
int age = 25;
bool isStudent = true;
bool isMember = false;
decimal basePrice = 20.00m;
decimal finalPrice;

if (age < 12)
{
    finalPrice = basePrice * 0.5m;  // 50% off for children
    Console.WriteLine("Child discount applied.");
}
else if (age >= 65)
{
    finalPrice = basePrice * 0.7m;  // 30% off for seniors
    Console.WriteLine("Senior discount applied.");
}
else if (isStudent || isMember)
{
    finalPrice = basePrice * 0.85m;  // 15% off for students/members
    Console.WriteLine("Special discount applied.");
}
else
{
    finalPrice = basePrice;
    Console.WriteLine("Regular price.");
}

Console.WriteLine($"Final price: ${finalPrice:F2}");
```

## Common Mistakes

### 1. Using = Instead of ==

```csharp
int x = 5;
// if (x = 5)  // ERROR: Assignment, not comparison!
if (x == 5)    // Correct: Comparison
{
    Console.WriteLine("x is 5");
}
```

### 2. Forgetting Braces

```csharp
if (condition)
    statement1;
    statement2;  // NOT inside the if! Always executes.

// Always use braces:
if (condition)
{
    statement1;
    statement2;
}
```

### 3. Unreachable Else

```csharp
int x = 10;
if (x > 5)
{
    Console.WriteLine("Greater than 5");
}
else if (x > 8)  // Never reached! Already caught by x > 5
{
    Console.WriteLine("Greater than 8");
}
```

### 4. Comparing Floating-Point Values

```csharp
double a = 0.1 + 0.2;
// if (a == 0.3)  // May be false due to precision!

// Better approach:
if (Math.Abs(a - 0.3) < 0.0001)
{
    Console.WriteLine("Approximately equal");
}
```

## Best Practices

1. **Always use braces**, even for single statements
2. **Keep conditions simple** - extract complex logic into well-named variables
3. **Order conditions logically** - most likely or most important first
4. **Avoid deep nesting** - refactor with early returns or logical operators
5. **Use meaningful variable names** for boolean values

```csharp
// Instead of:
if (age >= 18 && hasLicense && !suspended && yearsExperience > 2)

// Use:
bool isAdult = age >= 18;
bool canLegallyDrive = hasLicense && !suspended;
bool isExperienced = yearsExperience > 2;

if (isAdult && canLegallyDrive && isExperienced)
{
    // Clear and readable
}
```

## Summary

- Comparison operators (`==`, `!=`, `>`, `<`, `>=`, `<=`) return boolean results
- `if` statements execute code when conditions are true
- `else` provides an alternative when the condition is false
- `else if` chains handle multiple conditions
- Logical operators (`&&`, `||`, `!`) combine conditions
- The ternary operator `?:` provides concise conditional assignment
- Pattern matching with `is` enables type checking and null safety
- Always use braces and keep conditions readable

Mastering conditional statements enables you to build programs that respond intelligently to any situation.
