# Method Parameters, Arguments, and Return Values

## Introduction

Understanding how data flows into and out of methods is crucial for writing effective C# code. This topic explores the different ways parameters can be passed, how to return values (including multiple values), and advanced parameter techniques that make your methods more flexible and powerful.

## Parameters vs. Arguments

- **Parameters**: Variables declared in the method signature (formal parameters)
- **Arguments**: Actual values passed when calling the method (actual parameters)

```csharp
//              parameters
//                 ↓  ↓
void Calculate(int x, int y)
{
    Console.WriteLine(x + y);
}

//      arguments
//         ↓  ↓
Calculate(10, 20);
```

## Pass by Value (Default)

By default, C# passes arguments **by value**, meaning a copy of the value is passed:

```csharp
void TryToModify(int number)
{
    number = 999;  // Modifies the copy, not the original
    Console.WriteLine($"Inside method: {number}");
}

int original = 10;
TryToModify(original);
Console.WriteLine($"After method: {original}");

// Output:
// Inside method: 999
// After method: 10  (unchanged!)
```

### Reference Types are Different

For reference types, the reference is copied, not the object:

```csharp
void ModifyArray(int[] arr)
{
    arr[0] = 999;  // Modifies the actual array
}

int[] numbers = { 1, 2, 3 };
ModifyArray(numbers);
Console.WriteLine(numbers[0]);  // 999 (changed!)

// But reassigning doesn't affect original
void TryReassign(int[] arr)
{
    arr = new int[] { 100, 200, 300 };  // New reference, doesn't affect original
}

int[] data = { 1, 2, 3 };
TryReassign(data);
Console.WriteLine(data[0]);  // 1 (unchanged!)
```

## The ref Keyword

The `ref` keyword passes a variable by reference, allowing the method to modify the original:

```csharp
void AddTen(ref int number)
{
    number += 10;
}

int value = 5;
AddTen(ref value);
Console.WriteLine(value);  // 15 (modified!)
```

**Rules for ref:**
- Must use `ref` in both declaration AND call
- Variable must be initialized before passing

```csharp
void Swap(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}

int x = 10, y = 20;
Console.WriteLine($"Before: x={x}, y={y}");
Swap(ref x, ref y);
Console.WriteLine($"After: x={x}, y={y}");
// Before: x=10, y=20
// After: x=20, y=10
```

## The out Keyword

The `out` keyword is similar to `ref`, but:
- The variable doesn't need to be initialized
- The method MUST assign a value before returning

```csharp
void GetValues(out int x, out int y)
{
    x = 10;  // Must assign
    y = 20;  // Must assign
}

// No initialization needed
GetValues(out int a, out int b);
Console.WriteLine($"a={a}, b={b}");  // a=10, b=20
```

### Common Use: TryParse Pattern

```csharp
bool TryParseAge(string input, out int age)
{
    if (int.TryParse(input, out age) && age >= 0 && age <= 150)
    {
        return true;
    }
    age = 0;  // Must assign even on failure
    return false;
}

if (TryParseAge("25", out int parsedAge))
{
    Console.WriteLine($"Valid age: {parsedAge}");
}
```

### Inline out Variables (C# 7.0+)

```csharp
// Old way
int result;
if (int.TryParse("42", out result))
{
    Console.WriteLine(result);
}

// New way - declare inline
if (int.TryParse("42", out int number))
{
    Console.WriteLine(number);
}

// Can use var
if (int.TryParse("42", out var n))
{
    Console.WriteLine(n);
}
```

## The in Keyword

The `in` keyword passes by reference but prevents modification (read-only reference):

```csharp
void PrintPoint(in Point p)
{
    // p.X = 10;  // ERROR: Cannot modify - it's readonly
    Console.WriteLine($"Point: ({p.X}, {p.Y})");
}

struct Point { public int X; public int Y; }

Point point = new Point { X = 10, Y = 20 };
PrintPoint(in point);
```

`in` is useful for large structs where copying is expensive but modification shouldn't occur.

## ref vs. out vs. in Summary

| Keyword | Must Initialize | Method Must Assign | Can Read | Can Write |
|---------|-----------------|-------------------|----------|-----------|
| (none)  | Yes             | No                | Yes      | Copy only |
| `ref`   | Yes             | No                | Yes      | Yes       |
| `out`   | No              | Yes               | After assign | Yes  |
| `in`    | Yes             | No                | Yes      | No        |

## Returning Multiple Values

### Using out Parameters

```csharp
void GetMinMax(int[] array, out int min, out int max)
{
    min = array[0];
    max = array[0];
    foreach (int n in array)
    {
        if (n < min) min = n;
        if (n > max) max = n;
    }
}

int[] numbers = { 5, 2, 8, 1, 9 };
GetMinMax(numbers, out int minimum, out int maximum);
Console.WriteLine($"Min: {minimum}, Max: {maximum}");  // Min: 1, Max: 9
```

### Using Tuples (Preferred in Modern C#)

```csharp
(int min, int max) GetMinMax(int[] array)
{
    int min = array[0], max = array[0];
    foreach (int n in array)
    {
        if (n < min) min = n;
        if (n > max) max = n;
    }
    return (min, max);
}

var result = GetMinMax(new[] { 5, 2, 8, 1, 9 });
Console.WriteLine($"Min: {result.min}, Max: {result.max}");

// Or deconstruct
var (minimum, maximum) = GetMinMax(new[] { 5, 2, 8, 1, 9 });
Console.WriteLine($"Min: {minimum}, Max: {maximum}");
```

### Named Tuple Elements

```csharp
(string Name, int Age, string City) GetPersonInfo()
{
    return ("Alice", 25, "New York");
}

var person = GetPersonInfo();
Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
```

## params Keyword (Variable Arguments)

The `params` keyword allows passing a variable number of arguments:

```csharp
int Sum(params int[] numbers)
{
    int total = 0;
    foreach (int n in numbers)
    {
        total += n;
    }
    return total;
}

Console.WriteLine(Sum(1, 2, 3));          // 6
Console.WriteLine(Sum(1, 2, 3, 4, 5));    // 15
Console.WriteLine(Sum());                  // 0

// Can also pass an array directly
int[] arr = { 10, 20, 30 };
Console.WriteLine(Sum(arr));               // 60
```

**Rules for params:**
- Must be the last parameter
- Only one params parameter per method
- Type must be a single-dimensional array

```csharp
void Log(string format, params object[] args)
{
    Console.WriteLine(format, args);
}

Log("Name: {0}, Age: {1}", "Alice", 25);
// Output: Name: Alice, Age: 25
```

## Optional Parameters (Default Values)

```csharp
void CreateUser(string name, int age = 18, string role = "User")
{
    Console.WriteLine($"{name}, {age}, {role}");
}

CreateUser("Alice");                    // Alice, 18, User
CreateUser("Bob", 25);                  // Bob, 25, User
CreateUser("Charlie", 30, "Admin");     // Charlie, 30, Admin
CreateUser("Dave", role: "Guest");      // Dave, 18, Guest
```

## Named Arguments

Named arguments improve readability and allow any order:

```csharp
void SendEmail(string to, string subject, string body, bool urgent = false)
{
    Console.WriteLine($"To: {to}");
    Console.WriteLine($"Subject: {subject}");
    Console.WriteLine($"Body: {body}");
    Console.WriteLine($"Urgent: {urgent}");
}

// Positional
SendEmail("user@example.com", "Hello", "Message body");

// Named - any order
SendEmail(
    subject: "Hello",
    to: "user@example.com",
    urgent: true,
    body: "Message body"
);

// Mixed - positional first, then named
SendEmail("user@example.com", body: "Message", subject: "Hi");
```

## ref Returns and ref Locals

Methods can return references (C# 7.0+):

```csharp
ref int FindLargest(int[] numbers)
{
    int maxIndex = 0;
    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] > numbers[maxIndex])
        {
            maxIndex = i;
        }
    }
    return ref numbers[maxIndex];  // Return reference to element
}

int[] values = { 1, 5, 3, 9, 2 };
ref int largest = ref FindLargest(values);
Console.WriteLine(largest);  // 9

largest = 100;  // Modifies the array!
Console.WriteLine(values[3]);  // 100
```

## Practical Examples

### Example 1: Divide with Remainder

```csharp
(int quotient, int remainder) DivideWithRemainder(int dividend, int divisor)
{
    return (dividend / divisor, dividend % divisor);
}

var (q, r) = DivideWithRemainder(17, 5);
Console.WriteLine($"17 ÷ 5 = {q} remainder {r}");  // 3 remainder 2
```

### Example 2: Parsing with Validation

```csharp
bool TryParseDate(string input, out int year, out int month, out int day)
{
    year = 0;
    month = 0;
    day = 0;

    string[] parts = input.Split('-');
    if (parts.Length != 3) return false;

    if (!int.TryParse(parts[0], out year)) return false;
    if (!int.TryParse(parts[1], out month)) return false;
    if (!int.TryParse(parts[2], out day)) return false;

    return month >= 1 && month <= 12 && day >= 1 && day <= 31;
}

if (TryParseDate("2024-12-25", out int y, out int m, out int d))
{
    Console.WriteLine($"Date: {y}/{m}/{d}");
}
```

### Example 3: Statistics Calculator

```csharp
(double average, double min, double max, double sum) CalculateStats(params double[] values)
{
    if (values.Length == 0)
        throw new ArgumentException("Need at least one value");

    double sum = 0;
    double min = values[0];
    double max = values[0];

    foreach (double v in values)
    {
        sum += v;
        if (v < min) min = v;
        if (v > max) max = v;
    }

    return (sum / values.Length, min, max, sum);
}

var stats = CalculateStats(10, 20, 30, 40, 50);
Console.WriteLine($"Average: {stats.average}");  // 30
Console.WriteLine($"Min: {stats.min}");          // 10
Console.WriteLine($"Max: {stats.max}");          // 50
Console.WriteLine($"Sum: {stats.sum}");          // 150
```

### Example 4: Configuration Builder

```csharp
string BuildConnectionString(
    string server,
    string database,
    string user = null,
    string password = null,
    int timeout = 30,
    bool encrypt = true)
{
    var builder = new System.Text.StringBuilder();
    builder.Append($"Server={server};Database={database};");

    if (!string.IsNullOrEmpty(user))
        builder.Append($"User={user};Password={password};");
    else
        builder.Append("Integrated Security=true;");

    builder.Append($"Timeout={timeout};Encrypt={encrypt};");
    return builder.ToString();
}

// Various ways to call
Console.WriteLine(BuildConnectionString("localhost", "MyDB"));
Console.WriteLine(BuildConnectionString("localhost", "MyDB", timeout: 60));
Console.WriteLine(BuildConnectionString("server", "db", "admin", "pass123"));
```

## Summary

- **Value types** are copied by default; **reference types** pass a copy of the reference
- **ref**: Pass by reference, allows modification, must be initialized
- **out**: Pass by reference, method must assign, no initialization needed
- **in**: Pass by readonly reference, prevents modification
- **Tuples** are the modern way to return multiple values
- **params** enables variable-length argument lists
- **Named arguments** improve readability and allow flexible ordering
- **Default parameters** make arguments optional

Understanding these parameter mechanisms gives you full control over data flow in your methods.
