# Foreach Loops

## Introduction

The `foreach` loop provides an elegant way to iterate over collections without managing indices. It's cleaner, less error-prone, and the preferred choice when you need to read through every element in a collection.

## Basic Syntax

```csharp
foreach (type variable in collection)
{
    // Use variable
}
```

## Simple Examples

### Iterating an Array

```csharp
string[] fruits = { "Apple", "Banana", "Cherry" };

foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}
// Output:
// Apple
// Banana
// Cherry
```

### Iterating a List

```csharp
List<int> numbers = new List<int> { 10, 20, 30, 40, 50 };

foreach (int num in numbers)
{
    Console.WriteLine(num);
}
```

## Foreach vs. For Loop

```csharp
string[] colors = { "Red", "Green", "Blue" };

// For loop
for (int i = 0; i < colors.Length; i++)
{
    Console.WriteLine(colors[i]);
}

// Foreach loop (cleaner)
foreach (string color in colors)
{
    Console.WriteLine(color);
}
```

| Aspect | for Loop | foreach Loop |
|--------|----------|--------------|
| Index access | Yes | No |
| Modify elements | Yes | No |
| Error-prone | More | Less |
| Readability | Lower | Higher |
| Performance | Slightly faster | Slightly slower |

## The var Keyword

Use `var` when the type is obvious:

```csharp
List<string> names = new List<string> { "Alice", "Bob", "Charlie" };

foreach (var name in names)  // var infers string
{
    Console.WriteLine(name);
}
```

## Working with Different Collections

### Strings (Character by Character)

```csharp
string message = "Hello";

foreach (char c in message)
{
    Console.Write($"{c} ");
}
// Output: H e l l o
```

### Dictionaries

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>
{
    { "Alice", 25 },
    { "Bob", 30 },
    { "Carol", 35 }
};

foreach (KeyValuePair<string, int> pair in ages)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// Or using var and deconstruction
foreach (var (name, age) in ages)
{
    Console.WriteLine($"{name}: {age}");
}
```

### Multi-dimensional Arrays

```csharp
int[,] matrix = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

// Foreach iterates through ALL elements
foreach (int value in matrix)
{
    Console.Write($"{value} ");
}
// Output: 1 2 3 4 5 6 7 8 9
```

### Jagged Arrays

```csharp
int[][] jagged = {
    new[] { 1, 2 },
    new[] { 3, 4, 5 },
    new[] { 6, 7, 8, 9 }
};

foreach (int[] row in jagged)
{
    foreach (int value in row)
    {
        Console.Write($"{value} ");
    }
    Console.WriteLine();
}
```

## Read-Only Nature

Foreach variables are read-only - you cannot modify them:

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

foreach (int num in numbers)
{
    // num = num * 2;  // ERROR: Cannot assign to 'num'
    Console.WriteLine(num);
}

// To modify, use a for loop or create a new list
for (int i = 0; i < numbers.Count; i++)
{
    numbers[i] *= 2;  // This works
}
```

## Cannot Modify Collection

You cannot add or remove elements during foreach iteration:

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// This throws InvalidOperationException!
foreach (int num in numbers)
{
    if (num == 3)
    {
        numbers.Remove(num);  // ERROR at runtime!
    }
}

// Solutions:

// 1. Use RemoveAll
numbers.RemoveAll(n => n == 3);

// 2. Iterate backwards with for loop
for (int i = numbers.Count - 1; i >= 0; i--)
{
    if (numbers[i] == 3)
        numbers.RemoveAt(i);
}

// 3. Create a copy to iterate
foreach (int num in numbers.ToList())  // .ToList() creates a copy
{
    if (num == 3)
        numbers.Remove(num);  // Safe now
}
```

## Using Index with Foreach

If you need the index, you have several options:

### Manual Counter

```csharp
List<string> items = new List<string> { "A", "B", "C" };
int index = 0;

foreach (string item in items)
{
    Console.WriteLine($"Index {index}: {item}");
    index++;
}
```

### LINQ Select with Index

```csharp
foreach (var (item, index) in items.Select((value, i) => (value, i)))
{
    Console.WriteLine($"Index {index}: {item}");
}
```

### Extension Method

```csharp
// Define extension
public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
{
    int index = 0;
    foreach (T item in source)
    {
        yield return (item, index++);
    }
}

// Use it
foreach (var (item, index) in items.WithIndex())
{
    Console.WriteLine($"Index {index}: {item}");
}
```

## Break and Continue

Both work in foreach loops:

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Continue - skip even numbers
foreach (int num in numbers)
{
    if (num % 2 == 0)
        continue;
    Console.Write($"{num} ");
}
// Output: 1 3 5 7 9

// Break - stop at first number > 5
foreach (int num in numbers)
{
    if (num > 5)
        break;
    Console.Write($"{num} ");
}
// Output: 1 2 3 4 5
```

## Practical Examples

### Example 1: Calculating Statistics

```csharp
List<double> temperatures = new List<double> { 72.5, 75.0, 68.3, 80.1, 77.9 };

double sum = 0;
double max = double.MinValue;
double min = double.MaxValue;

foreach (double temp in temperatures)
{
    sum += temp;
    if (temp > max) max = temp;
    if (temp < min) min = temp;
}

double average = sum / temperatures.Count;

Console.WriteLine($"Average: {average:F1}°F");
Console.WriteLine($"High: {max}°F");
Console.WriteLine($"Low: {min}°F");
```

### Example 2: Searching in Objects

```csharp
class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }
}

List<Student> students = new List<Student>
{
    new Student { Name = "Alice", Grade = 92 },
    new Student { Name = "Bob", Grade = 85 },
    new Student { Name = "Carol", Grade = 95 },
    new Student { Name = "David", Grade = 78 }
};

Console.WriteLine("Honor Roll (90+):");
foreach (var student in students)
{
    if (student.Grade >= 90)
    {
        Console.WriteLine($"  {student.Name}: {student.Grade}");
    }
}
```

### Example 3: Building a Result

```csharp
List<string> words = new List<string> { "Hello", "World", "from", "C#" };

string sentence = "";
foreach (string word in words)
{
    if (sentence.Length > 0)
        sentence += " ";
    sentence += word;
}

Console.WriteLine(sentence);  // Hello World from C#

// Better: Use string.Join
string better = string.Join(" ", words);
```

### Example 4: Nested Foreach

```csharp
List<List<int>> matrix = new List<List<int>>
{
    new List<int> { 1, 2, 3 },
    new List<int> { 4, 5, 6 },
    new List<int> { 7, 8, 9 }
};

int sum = 0;
foreach (var row in matrix)
{
    foreach (var cell in row)
    {
        sum += cell;
        Console.Write($"{cell,3}");
    }
    Console.WriteLine();
}
Console.WriteLine($"Sum: {sum}");
```

## When to Use Foreach vs. For

### Use foreach when:
- You need to read all elements
- You don't need the index
- You want cleaner, more readable code
- Working with IEnumerable sources

### Use for when:
- You need to modify elements
- You need the index for logic
- You need to iterate partially
- You need to iterate backwards
- Performance is critical

## IEnumerable Interface

Foreach works with any type implementing `IEnumerable`:

```csharp
// Arrays implement IEnumerable
int[] array = { 1, 2, 3 };

// Lists implement IEnumerable
List<int> list = new List<int> { 1, 2, 3 };

// Strings implement IEnumerable<char>
string text = "Hello";

// Dictionaries implement IEnumerable<KeyValuePair>
Dictionary<string, int> dict = new Dictionary<string, int>();

// Custom classes can implement IEnumerable too
```

## Summary

- Foreach provides clean iteration over collections
- Syntax: `foreach (type item in collection)`
- Read-only - cannot modify the iteration variable
- Cannot add/remove items during iteration
- Use `var` when type is obvious
- Works with arrays, lists, strings, dictionaries, and any IEnumerable
- Use `break` to exit early, `continue` to skip items
- Prefer foreach for reading; use for when modification needed

Foreach is the most idiomatic way to iterate collections in C# when you don't need index access.
