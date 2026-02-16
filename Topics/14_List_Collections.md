# List<T> Collections

## Introduction

While arrays are fixed-size, the `List<T>` class provides a dynamic, resizable collection. Lists grow and shrink automatically as you add and remove elements, making them more flexible for most scenarios.

## Creating Lists

```csharp
// Empty list
List<int> numbers = new List<int>();

// List with initial values
List<string> names = new List<string> { "Alice", "Bob", "Charlie" };

// Using target-typed new (C# 9+)
List<int> values = new() { 1, 2, 3, 4, 5 };

// Specifying initial capacity (performance optimization)
List<double> prices = new List<double>(100);  // Can hold 100 before resizing
```

## Adding Elements

```csharp
List<string> fruits = new List<string>();

// Add single element
fruits.Add("Apple");
fruits.Add("Banana");

// Add multiple elements
fruits.AddRange(new[] { "Cherry", "Date", "Elderberry" });

// Insert at specific index
fruits.Insert(1, "Avocado");  // Insert at index 1

// List: ["Apple", "Avocado", "Banana", "Cherry", "Date", "Elderberry"]
```

## Accessing Elements

```csharp
List<int> numbers = new List<int> { 10, 20, 30, 40, 50 };

// Access by index
int first = numbers[0];     // 10
int third = numbers[2];     // 30

// Get count
int count = numbers.Count;  // 5

// First and last
int firstItem = numbers[0];
int lastItem = numbers[numbers.Count - 1];

// Or using LINQ
int firstLinq = numbers.First();
int lastLinq = numbers.Last();
```

## Removing Elements

```csharp
List<string> items = new List<string> { "A", "B", "C", "D", "E" };

// Remove by value (first occurrence)
items.Remove("C");  // ["A", "B", "D", "E"]

// Remove at index
items.RemoveAt(0);  // ["B", "D", "E"]

// Remove range
items.RemoveRange(0, 2);  // ["E"]

// Remove all matching condition
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
numbers.RemoveAll(n => n % 2 == 0);  // [1, 3, 5, 7] - removed evens

// Clear all elements
items.Clear();  // []
```

## Checking for Elements

```csharp
List<string> colors = new List<string> { "Red", "Green", "Blue" };

// Check if contains
bool hasRed = colors.Contains("Red");    // true
bool hasYellow = colors.Contains("Yellow");  // false

// Find index
int index = colors.IndexOf("Green");     // 1
int notFound = colors.IndexOf("Yellow"); // -1

// Check if any element matches condition
bool hasLongName = colors.Exists(c => c.Length > 4);  // true ("Green")
```

## Sorting

```csharp
List<int> numbers = new List<int> { 5, 2, 8, 1, 9 };

// Sort ascending
numbers.Sort();  // [1, 2, 5, 8, 9]

// Sort descending
numbers.Sort();
numbers.Reverse();  // [9, 8, 5, 2, 1]

// Custom sort for complex types
List<string> names = new List<string> { "Charlie", "Alice", "Bob" };
names.Sort();  // ["Alice", "Bob", "Charlie"]

// Sort by custom criteria
names.Sort((a, b) => b.Length.CompareTo(a.Length));  // By length, descending
```

## Searching

```csharp
List<int> numbers = new List<int> { 1, 5, 3, 8, 2, 9, 4 };

// Find first match
int firstOver5 = numbers.Find(n => n > 5);  // 8

// Find last match
int lastOver5 = numbers.FindLast(n => n > 5);  // 9

// Find all matches
List<int> allOver5 = numbers.FindAll(n => n > 5);  // [8, 9]

// Find index of match
int indexOver5 = numbers.FindIndex(n => n > 5);  // 3
```

## Converting Between Arrays and Lists

```csharp
// Array to List
int[] array = { 1, 2, 3, 4, 5 };
List<int> list = new List<int>(array);
// Or
List<int> list2 = array.ToList();

// List to Array
List<string> names = new List<string> { "A", "B", "C" };
string[] nameArray = names.ToArray();
```

## Iterating Lists

```csharp
List<string> items = new List<string> { "Apple", "Banana", "Cherry" };

// foreach (most common)
foreach (string item in items)
{
    Console.WriteLine(item);
}

// for loop (when index needed)
for (int i = 0; i < items.Count; i++)
{
    Console.WriteLine($"{i}: {items[i]}");
}

// ForEach method
items.ForEach(item => Console.WriteLine(item));
```

## List vs Array Comparison

| Feature | Array | List<T> |
|---------|-------|---------|
| Size | Fixed | Dynamic |
| Adding/Removing | Not supported | Easy |
| Memory | More efficient | Slightly more overhead |
| Performance | Faster access | Slightly slower |
| Syntax | `int[]` | `List<int>` |

**When to use Array:**
- Fixed-size data
- Performance-critical code
- Multi-dimensional data

**When to use List:**
- Unknown or changing size
- Frequent add/remove operations
- More flexible operations needed

## Practical Examples

### Example 1: Shopping Cart

```csharp
class CartItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

List<CartItem> cart = new List<CartItem>();

// Add items
cart.Add(new CartItem { Name = "Apple", Price = 0.50m, Quantity = 5 });
cart.Add(new CartItem { Name = "Bread", Price = 2.99m, Quantity = 2 });
cart.Add(new CartItem { Name = "Milk", Price = 3.49m, Quantity = 1 });

// Calculate total
decimal total = 0;
foreach (var item in cart)
{
    total += item.Price * item.Quantity;
}
Console.WriteLine($"Total: ${total}");  // $11.96
```

### Example 2: Managing a Task List

```csharp
List<string> tasks = new List<string>();

// Add tasks
tasks.Add("Complete homework");
tasks.Add("Go grocery shopping");
tasks.Add("Call mom");

// Display tasks
Console.WriteLine("Tasks:");
for (int i = 0; i < tasks.Count; i++)
{
    Console.WriteLine($"{i + 1}. {tasks[i]}");
}

// Mark task complete (remove it)
tasks.RemoveAt(0);

// Insert priority task
tasks.Insert(0, "URGENT: Fix bug");
```

### Example 3: Student Grade Tracker

```csharp
List<int> grades = new List<int>();

// Simulate adding grades
grades.AddRange(new[] { 85, 92, 78, 90, 88, 95 });

// Calculate statistics
grades.Sort();
int min = grades[0];
int max = grades[grades.Count - 1];
double average = grades.Average();

// Find grades above average
List<int> aboveAverage = grades.FindAll(g => g > average);

Console.WriteLine($"Grades: {string.Join(", ", grades)}");
Console.WriteLine($"Min: {min}, Max: {max}");
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Above average: {string.Join(", ", aboveAverage)}");
```

## Advanced List Operations

### Using LINQ with Lists

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Filter
var evens = numbers.Where(n => n % 2 == 0).ToList();

// Transform
var doubled = numbers.Select(n => n * 2).ToList();

// Aggregate
int sum = numbers.Sum();
int max = numbers.Max();
double avg = numbers.Average();

// Check conditions
bool allPositive = numbers.All(n => n > 0);
bool anyOver5 = numbers.Any(n => n > 5);
```

### Capacity Management

```csharp
List<int> list = new List<int>();

Console.WriteLine($"Count: {list.Count}");       // 0
Console.WriteLine($"Capacity: {list.Capacity}"); // 0 or 4

// Add elements
for (int i = 0; i < 100; i++)
{
    list.Add(i);
}

// Capacity grows automatically
Console.WriteLine($"Count: {list.Count}");       // 100
Console.WriteLine($"Capacity: {list.Capacity}"); // 128 (or similar)

// Trim excess capacity
list.TrimExcess();
Console.WriteLine($"Capacity after trim: {list.Capacity}");  // Close to 100
```

## Common Mistakes

### 1. Modifying While Iterating

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// WRONG - throws InvalidOperationException
foreach (int n in numbers)
{
    if (n % 2 == 0)
        numbers.Remove(n);  // ERROR!
}

// CORRECT - use RemoveAll or iterate backwards
numbers.RemoveAll(n => n % 2 == 0);

// Or iterate backwards
for (int i = numbers.Count - 1; i >= 0; i--)
{
    if (numbers[i] % 2 == 0)
        numbers.RemoveAt(i);
}
```

### 2. Index Out of Range

```csharp
List<int> list = new List<int> { 1, 2, 3 };

// WRONG
int item = list[5];  // ArgumentOutOfRangeException!

// CORRECT
if (list.Count > 5)
{
    int item = list[5];
}
```

## Summary

- `List<T>` is a dynamic, resizable collection
- Add elements with `Add()`, `AddRange()`, `Insert()`
- Remove with `Remove()`, `RemoveAt()`, `RemoveAll()`, `Clear()`
- Access elements by index like arrays: `list[0]`
- Use `Count` property (not `Length` like arrays)
- `Contains()`, `IndexOf()`, `Find()` for searching
- `Sort()`, `Reverse()` for ordering
- Prefer `List<T>` when size may change
- Don't modify a list while iterating with foreach

Lists are the most commonly used collection type in C# due to their flexibility and ease of use.
