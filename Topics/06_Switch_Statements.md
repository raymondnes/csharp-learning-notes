# Switch Statements

## Introduction

The `switch` statement provides an elegant way to handle multiple conditions based on a single value. When you need to compare a variable against many possible values, `switch` is often cleaner and more readable than long `else-if` chains.

## Basic Switch Syntax

```csharp
switch (expression)
{
    case value1:
        // Code for value1
        break;
    case value2:
        // Code for value2
        break;
    case value3:
        // Code for value3
        break;
    default:
        // Code when no case matches
        break;
}
```

### Key Components

- **Expression**: The value being tested (must be a compatible type)
- **Case labels**: Each possible value to match against
- **Break**: Exits the switch after executing a case
- **Default**: Optional catch-all for unmatched values

## Simple Example

```csharp
int day = 3;
string dayName;

switch (day)
{
    case 1:
        dayName = "Monday";
        break;
    case 2:
        dayName = "Tuesday";
        break;
    case 3:
        dayName = "Wednesday";
        break;
    case 4:
        dayName = "Thursday";
        break;
    case 5:
        dayName = "Friday";
        break;
    case 6:
        dayName = "Saturday";
        break;
    case 7:
        dayName = "Sunday";
        break;
    default:
        dayName = "Invalid day";
        break;
}

Console.WriteLine(dayName);  // Output: Wednesday
```

## Switch with Strings

```csharp
string command = "start";

switch (command.ToLower())
{
    case "start":
        Console.WriteLine("Starting the engine...");
        break;
    case "stop":
        Console.WriteLine("Stopping the engine...");
        break;
    case "pause":
        Console.WriteLine("Pausing...");
        break;
    case "resume":
        Console.WriteLine("Resuming...");
        break;
    default:
        Console.WriteLine("Unknown command.");
        break;
}
```

## Case Grouping (Fall-Through)

Multiple cases can share the same code block:

```csharp
int month = 4;
string season;

switch (month)
{
    case 12:
    case 1:
    case 2:
        season = "Winter";
        break;
    case 3:
    case 4:
    case 5:
        season = "Spring";
        break;
    case 6:
    case 7:
    case 8:
        season = "Summer";
        break;
    case 9:
    case 10:
    case 11:
        season = "Fall";
        break;
    default:
        season = "Invalid month";
        break;
}

Console.WriteLine($"Month {month} is in {season}");  // Month 4 is in Spring
```

## The Break Statement

The `break` statement is **required** after each case block in C#. Unlike some other languages, C# does not allow implicit fall-through.

```csharp
// This will NOT compile in C#:
switch (value)
{
    case 1:
        Console.WriteLine("One");
        // Missing break - Compile Error!
    case 2:
        Console.WriteLine("Two");
        break;
}

// Correct version:
switch (value)
{
    case 1:
        Console.WriteLine("One");
        break;
    case 2:
        Console.WriteLine("Two");
        break;
}
```

**Exception:** Empty cases can fall through:

```csharp
switch (value)
{
    case 1:
    case 2:  // Empty case - falls through to case 3
    case 3:
        Console.WriteLine("One, Two, or Three");
        break;
}
```

## Switch Expressions (C# 8.0+)

Modern C# offers a more concise switch expression syntax:

```csharp
int day = 3;

string dayName = day switch
{
    1 => "Monday",
    2 => "Tuesday",
    3 => "Wednesday",
    4 => "Thursday",
    5 => "Friday",
    6 => "Saturday",
    7 => "Sunday",
    _ => "Invalid day"  // _ is the discard pattern (default)
};

Console.WriteLine(dayName);  // Wednesday
```

### Benefits of Switch Expressions

- More concise syntax
- Expression-based (returns a value)
- Uses `=>` instead of `:`
- Uses `_` for default case
- No `break` statements needed

### Multiple Cases in Switch Expressions

```csharp
int month = 4;

string season = month switch
{
    12 or 1 or 2 => "Winter",
    3 or 4 or 5 => "Spring",
    6 or 7 or 8 => "Summer",
    9 or 10 or 11 => "Fall",
    _ => "Invalid month"
};
```

## Pattern Matching in Switch

C# supports powerful pattern matching in switch statements:

### Type Patterns

```csharp
object obj = 42;

switch (obj)
{
    case int i:
        Console.WriteLine($"Integer: {i}");
        break;
    case string s:
        Console.WriteLine($"String: {s}");
        break;
    case double d:
        Console.WriteLine($"Double: {d}");
        break;
    case null:
        Console.WriteLine("Null value");
        break;
    default:
        Console.WriteLine($"Unknown type: {obj.GetType()}");
        break;
}
```

### When Guards

Add conditions to cases with `when`:

```csharp
int number = 15;

switch (number)
{
    case int n when n < 0:
        Console.WriteLine("Negative");
        break;
    case int n when n == 0:
        Console.WriteLine("Zero");
        break;
    case int n when n > 0 && n <= 10:
        Console.WriteLine("Small positive (1-10)");
        break;
    case int n when n > 10 && n <= 100:
        Console.WriteLine("Medium positive (11-100)");
        break;
    default:
        Console.WriteLine("Large positive (> 100)");
        break;
}
```

### Pattern Matching with Switch Expressions

```csharp
int score = 85;

string grade = score switch
{
    >= 90 => "A",
    >= 80 => "B",
    >= 70 => "C",
    >= 60 => "D",
    _ => "F"
};

Console.WriteLine($"Grade: {grade}");  // Grade: B
```

### Range Patterns

```csharp
int temperature = 75;

string description = temperature switch
{
    < 32 => "Freezing",
    >= 32 and < 50 => "Cold",
    >= 50 and < 70 => "Cool",
    >= 70 and < 85 => "Warm",
    >= 85 and < 100 => "Hot",
    >= 100 => "Extreme heat",
    _ => "Invalid temperature"
};

Console.WriteLine(description);  // Warm
```

## Switch vs. If-Else

### When to Use Switch

- Comparing a single value against multiple constants
- Menu selection systems
- State machines
- Enum handling
- Clear, discrete cases

### When to Use If-Else

- Complex boolean conditions
- Range comparisons (though patterns help)
- Non-constant comparisons
- Few conditions (2-3)

### Comparison Example

**If-Else Version:**
```csharp
string GetDayType(int day)
{
    if (day == 1 || day == 2 || day == 3 || day == 4 || day == 5)
        return "Weekday";
    else if (day == 6 || day == 7)
        return "Weekend";
    else
        return "Invalid";
}
```

**Switch Version:**
```csharp
string GetDayType(int day)
{
    switch (day)
    {
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
            return "Weekday";
        case 6:
        case 7:
            return "Weekend";
        default:
            return "Invalid";
    }
}
```

**Switch Expression Version:**
```csharp
string GetDayType(int day) => day switch
{
    1 or 2 or 3 or 4 or 5 => "Weekday",
    6 or 7 => "Weekend",
    _ => "Invalid"
};
```

## Switching on Enums

Switch is particularly useful with enums:

```csharp
enum TrafficLight
{
    Red,
    Yellow,
    Green
}

TrafficLight light = TrafficLight.Green;

switch (light)
{
    case TrafficLight.Red:
        Console.WriteLine("Stop!");
        break;
    case TrafficLight.Yellow:
        Console.WriteLine("Caution - prepare to stop");
        break;
    case TrafficLight.Green:
        Console.WriteLine("Go!");
        break;
}
```

Or with switch expression:

```csharp
string action = light switch
{
    TrafficLight.Red => "Stop!",
    TrafficLight.Yellow => "Caution",
    TrafficLight.Green => "Go!",
    _ => "Unknown signal"
};
```

## Practical Examples

### Example 1: Calculator Operation

```csharp
double Calculate(double a, double b, char operation)
{
    return operation switch
    {
        '+' => a + b,
        '-' => a - b,
        '*' => a * b,
        '/' => b != 0 ? a / b : throw new DivideByZeroException(),
        '%' => a % b,
        _ => throw new ArgumentException($"Unknown operation: {operation}")
    };
}

Console.WriteLine(Calculate(10, 3, '+'));  // 13
Console.WriteLine(Calculate(10, 3, '/'));  // 3.333...
```

### Example 2: Menu System

```csharp
void ShowMenu()
{
    Console.WriteLine("=== MAIN MENU ===");
    Console.WriteLine("1. New Game");
    Console.WriteLine("2. Load Game");
    Console.WriteLine("3. Settings");
    Console.WriteLine("4. Exit");
    Console.Write("Enter choice: ");
}

ShowMenu();
string choice = Console.ReadLine();

switch (choice)
{
    case "1":
        Console.WriteLine("Starting new game...");
        break;
    case "2":
        Console.WriteLine("Loading saved game...");
        break;
    case "3":
        Console.WriteLine("Opening settings...");
        break;
    case "4":
        Console.WriteLine("Goodbye!");
        break;
    default:
        Console.WriteLine("Invalid option. Please try again.");
        break;
}
```

### Example 3: HTTP Status Code Handler

```csharp
string GetStatusMessage(int statusCode) => statusCode switch
{
    200 => "OK - Request successful",
    201 => "Created - Resource created successfully",
    204 => "No Content - Request successful, no content to return",
    400 => "Bad Request - Invalid request syntax",
    401 => "Unauthorized - Authentication required",
    403 => "Forbidden - Access denied",
    404 => "Not Found - Resource does not exist",
    500 => "Internal Server Error - Server encountered an error",
    502 => "Bad Gateway - Invalid response from upstream server",
    503 => "Service Unavailable - Server temporarily unavailable",
    >= 200 and < 300 => "Success (other)",
    >= 400 and < 500 => "Client Error (other)",
    >= 500 and < 600 => "Server Error (other)",
    _ => "Unknown status code"
};

Console.WriteLine(GetStatusMessage(404));  // Not Found - Resource does not exist
```

### Example 4: Shape Area Calculator

```csharp
object shape = ("circle", 5.0);

double area = shape switch
{
    ("circle", double radius) => Math.PI * radius * radius,
    ("square", double side) => side * side,
    ("rectangle", double width, double height) => width * height,
    ("triangle", double baseLen, double height) => 0.5 * baseLen * height,
    _ => throw new ArgumentException("Unknown shape")
};

Console.WriteLine($"Area: {area:F2}");  // Area: 78.54
```

## Common Mistakes

### 1. Forgetting Break

```csharp
// Won't compile in C#
switch (x)
{
    case 1:
        DoSomething();
    case 2:  // Error: Control cannot fall through
        DoSomethingElse();
        break;
}
```

### 2. Duplicate Case Labels

```csharp
// Won't compile
switch (x)
{
    case 1:
        Console.WriteLine("One");
        break;
    case 1:  // Error: Duplicate case label
        Console.WriteLine("Also one");
        break;
}
```

### 3. Using Non-Constant Case Labels (Traditional Switch)

```csharp
int target = 5;
int value = 5;

// Won't compile with traditional switch
switch (value)
{
    case target:  // Error: Case label must be constant
        Console.WriteLine("Match");
        break;
}

// Use pattern matching instead
switch (value)
{
    case int n when n == target:
        Console.WriteLine("Match");
        break;
}
```

## Best Practices

1. **Always include a default case** to handle unexpected values
2. **Use switch expressions** for simple value-returning scenarios
3. **Group related cases** when they share the same logic
4. **Consider enums** when you have a fixed set of options
5. **Use pattern matching** for complex conditions
6. **Keep cases simple** - extract complex logic into methods

## Summary

- `switch` handles multiple conditions based on a single value
- Each case requires a `break` statement (unless empty)
- Case grouping allows multiple values to share code
- Switch expressions provide concise value-returning syntax
- Pattern matching enables type checking and conditions
- `when` guards add conditional logic to cases
- Use switch for discrete values, if-else for complex conditions
- Always include a default case for safety

Switch statements make code more readable when comparing a value against many possible matches.
