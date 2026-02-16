# Dictionary<TKey, TValue>

## Introduction

A Dictionary is a collection that stores key-value pairs, allowing fast lookup of values by their associated keys. It's one of the most commonly used collections in C# when you need to associate data with unique identifiers.

## What is a Dictionary?

A Dictionary maps keys to values with O(1) average lookup time:

```csharp
// Create a dictionary
Dictionary<string, int> ages = new Dictionary<string, int>();

// Add key-value pairs
ages["Alice"] = 25;
ages["Bob"] = 30;
ages["Carol"] = 28;

// Access by key
Console.WriteLine(ages["Alice"]);  // 25

// Keys must be unique
ages["Alice"] = 26;  // Updates existing value
```

## Creating Dictionaries

```csharp
// Empty dictionary
Dictionary<string, string> empty = new Dictionary<string, string>();

// With initial values (collection initializer)
Dictionary<string, int> scores = new Dictionary<string, int>
{
    { "Alice", 95 },
    { "Bob", 87 },
    { "Carol", 92 }
};

// Alternative syntax (C# 6+)
Dictionary<string, int> scores2 = new Dictionary<string, int>
{
    ["Alice"] = 95,
    ["Bob"] = 87,
    ["Carol"] = 92
};

// From arrays
string[] names = { "Alice", "Bob" };
int[] values = { 100, 200 };
Dictionary<string, int> fromArrays = new Dictionary<string, int>();
for (int i = 0; i < names.Length; i++)
{
    fromArrays[names[i]] = values[i];
}
```

## Adding and Updating Values

```csharp
Dictionary<string, string> capitals = new Dictionary<string, string>();

// Using Add method (throws if key exists)
capitals.Add("USA", "Washington D.C.");
capitals.Add("France", "Paris");
// capitals.Add("USA", "New York");  // ERROR: Key already exists

// Using indexer (adds or updates)
capitals["Germany"] = "Berlin";  // Adds
capitals["USA"] = "Washington";  // Updates

// TryAdd (returns false if key exists, doesn't throw)
bool added = capitals.TryAdd("Japan", "Tokyo");  // true
bool added2 = capitals.TryAdd("USA", "Chicago");  // false, not added
```

## Accessing Values

```csharp
Dictionary<string, double> prices = new Dictionary<string, double>
{
    ["Apple"] = 1.50,
    ["Banana"] = 0.75,
    ["Orange"] = 2.00
};

// Direct access (throws if key not found)
double applePrice = prices["Apple"];  // 1.50
// double grapePrice = prices["Grape"];  // KeyNotFoundException!

// Safe access with TryGetValue
if (prices.TryGetValue("Banana", out double bananaPrice))
{
    Console.WriteLine($"Banana costs ${bananaPrice}");
}
else
{
    Console.WriteLine("Banana not found");
}

// Check if key exists
if (prices.ContainsKey("Orange"))
{
    Console.WriteLine($"Orange: ${prices["Orange"]}");
}

// Check if value exists
if (prices.ContainsValue(0.75))
{
    Console.WriteLine("Found an item costing $0.75");
}

// GetValueOrDefault (C# 7.1+)
double price = prices.GetValueOrDefault("Grape", 0.00);  // Returns 0.00
```

## Removing Items

```csharp
Dictionary<int, string> items = new Dictionary<int, string>
{
    [1] = "One",
    [2] = "Two",
    [3] = "Three"
};

// Remove by key
bool removed = items.Remove(2);  // true
bool removed2 = items.Remove(99);  // false, key didn't exist

// Remove and get value
if (items.Remove(3, out string value))
{
    Console.WriteLine($"Removed: {value}");  // "Three"
}

// Clear all
items.Clear();
Console.WriteLine(items.Count);  // 0
```

## Iterating Over Dictionaries

```csharp
Dictionary<string, int> inventory = new Dictionary<string, int>
{
    ["Apples"] = 50,
    ["Bananas"] = 30,
    ["Oranges"] = 45
};

// Iterate key-value pairs
foreach (KeyValuePair<string, int> item in inventory)
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}

// Using var
foreach (var item in inventory)
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}

// Deconstruction (C# 7+)
foreach (var (fruit, count) in inventory)
{
    Console.WriteLine($"{fruit}: {count}");
}

// Iterate keys only
foreach (string key in inventory.Keys)
{
    Console.WriteLine($"Key: {key}");
}

// Iterate values only
foreach (int value in inventory.Values)
{
    Console.WriteLine($"Value: {value}");
}
```

## Dictionary Properties

```csharp
Dictionary<string, decimal> prices = new Dictionary<string, decimal>
{
    ["Item1"] = 10.99m,
    ["Item2"] = 25.50m,
    ["Item3"] = 5.00m
};

// Count of items
Console.WriteLine($"Items: {prices.Count}");  // 3

// Get all keys
ICollection<string> keys = prices.Keys;

// Get all values
ICollection<decimal> values = prices.Values;

// Convert to arrays
string[] keyArray = prices.Keys.ToArray();
decimal[] valueArray = prices.Values.ToArray();
```

## Complex Key and Value Types

```csharp
// Objects as values
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

Dictionary<int, Person> people = new Dictionary<int, Person>
{
    [1] = new Person { Name = "Alice", Age = 25 },
    [2] = new Person { Name = "Bob", Age = 30 }
};

Console.WriteLine(people[1].Name);  // Alice

// Tuple as value
Dictionary<string, (string City, string Country)> locations =
    new Dictionary<string, (string City, string Country)>
{
    ["Alice"] = ("New York", "USA"),
    ["Bob"] = ("Paris", "France")
};

Console.WriteLine(locations["Alice"].City);  // New York

// Custom object as key (must override GetHashCode and Equals)
class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override bool Equals(object obj) =>
        obj is Point p && X == p.X && Y == p.Y;
}

Dictionary<Point, string> grid = new Dictionary<Point, string>();
grid[new Point { X = 0, Y = 0 }] = "Origin";
```

## LINQ with Dictionaries

```csharp
Dictionary<string, int> scores = new Dictionary<string, int>
{
    ["Alice"] = 95,
    ["Bob"] = 87,
    ["Carol"] = 92,
    ["David"] = 78,
    ["Eve"] = 98
};

// Filter by value
var highScorers = scores.Where(s => s.Value >= 90);
foreach (var s in highScorers)
{
    Console.WriteLine($"{s.Key}: {s.Value}");
}

// Order by value
var orderedByScore = scores.OrderByDescending(s => s.Value);

// Order by key
var orderedByName = scores.OrderBy(s => s.Key);

// Find maximum
var topStudent = scores.MaxBy(s => s.Value);
Console.WriteLine($"Top: {topStudent.Key} with {topStudent.Value}");

// Convert to dictionary from other collection
List<string> words = new List<string> { "apple", "banana", "cherry" };
Dictionary<string, int> wordLengths = words.ToDictionary(
    w => w,         // Key selector
    w => w.Length   // Value selector
);

// Group and convert to dictionary
var grouped = scores
    .GroupBy(s => s.Value >= 90 ? "Pass" : "Fail")
    .ToDictionary(g => g.Key, g => g.ToList());
```

## Nested Dictionaries

```csharp
// Dictionary of dictionaries
Dictionary<string, Dictionary<string, int>> grades =
    new Dictionary<string, Dictionary<string, int>>
{
    ["Alice"] = new Dictionary<string, int>
    {
        ["Math"] = 95,
        ["English"] = 88,
        ["Science"] = 92
    },
    ["Bob"] = new Dictionary<string, int>
    {
        ["Math"] = 78,
        ["English"] = 85,
        ["Science"] = 80
    }
};

// Access nested value
int aliceMath = grades["Alice"]["Math"];  // 95

// Add to nested dictionary
grades["Carol"] = new Dictionary<string, int>();
grades["Carol"]["Math"] = 90;

// Iterate nested
foreach (var student in grades)
{
    Console.WriteLine($"\n{student.Key}'s Grades:");
    foreach (var subject in student.Value)
    {
        Console.WriteLine($"  {subject.Key}: {subject.Value}");
    }
}
```

## Practical Examples

### Word Counter

```csharp
string text = "the quick brown fox jumps over the lazy dog the fox";
Dictionary<string, int> wordCount = new Dictionary<string, int>();

foreach (string word in text.Split(' '))
{
    if (wordCount.ContainsKey(word))
    {
        wordCount[word]++;
    }
    else
    {
        wordCount[word] = 1;
    }
}

// Or using TryGetValue
foreach (string word in text.Split(' '))
{
    wordCount.TryGetValue(word, out int count);
    wordCount[word] = count + 1;
}

foreach (var item in wordCount.OrderByDescending(x => x.Value))
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}
```

### Phone Book

```csharp
Dictionary<string, string> phoneBook = new Dictionary<string, string>();

void AddContact(string name, string phone)
{
    if (phoneBook.TryAdd(name, phone))
    {
        Console.WriteLine($"Added {name}");
    }
    else
    {
        Console.WriteLine($"{name} already exists. Update? (y/n)");
        if (Console.ReadLine() == "y")
        {
            phoneBook[name] = phone;
        }
    }
}

string LookupPhone(string name)
{
    return phoneBook.GetValueOrDefault(name, "Not found");
}

void ListContacts()
{
    foreach (var (name, phone) in phoneBook.OrderBy(c => c.Key))
    {
        Console.WriteLine($"{name}: {phone}");
    }
}
```

### Caching

```csharp
class SimpleCache<TKey, TValue>
{
    private Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();

    public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
    {
        if (!_cache.TryGetValue(key, out TValue value))
        {
            value = factory(key);
            _cache[key] = value;
            Console.WriteLine($"Cache miss: Computing value for {key}");
        }
        else
        {
            Console.WriteLine($"Cache hit: {key}");
        }
        return value;
    }

    public void Clear() => _cache.Clear();
}

// Usage
var cache = new SimpleCache<int, int>();
int result1 = cache.GetOrAdd(5, n => n * n);  // Cache miss
int result2 = cache.GetOrAdd(5, n => n * n);  // Cache hit
```

## Dictionary vs Other Collections

| Feature | Dictionary | List | Array |
|---------|------------|------|-------|
| Access | By key O(1) | By index O(1) | By index O(1) |
| Search | O(1) by key | O(n) | O(n) |
| Order | Not guaranteed | Maintained | Maintained |
| Duplicates | No (keys) | Yes | Yes |
| Size | Dynamic | Dynamic | Fixed |

## Summary

- Dictionary stores key-value pairs with fast lookup
- Keys must be unique, values can be duplicated
- Use `TryGetValue` for safe access
- Use `ContainsKey` to check existence
- Iterate with `foreach` over KeyValuePair
- Works with LINQ for powerful queries
- Order is not guaranteed
- Good for caching, lookups, counting, mappings

Dictionary is essential for efficient key-based data access in C#.
