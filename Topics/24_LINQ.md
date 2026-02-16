# LINQ (Language Integrated Query)

## Introduction

LINQ (Language Integrated Query) is a powerful feature in C# that provides a consistent way to query and manipulate data from various sources including collections, databases, XML, and more. It brings SQL-like querying capabilities directly into the C# language.

## What is LINQ?

LINQ allows you to write queries against collections using a declarative syntax:

```csharp
using System.Linq;

List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// LINQ query - find even numbers
var evenNumbers = numbers.Where(n => n % 2 == 0);

foreach (var num in evenNumbers)
{
    Console.WriteLine(num);  // 2, 4, 6, 8, 10
}
```

## Two LINQ Syntaxes

### Method Syntax (Fluent)

```csharp
var result = numbers
    .Where(n => n > 5)
    .OrderBy(n => n)
    .Select(n => n * 2);
```

### Query Syntax (SQL-like)

```csharp
var result = from n in numbers
             where n > 5
             orderby n
             select n * 2;
```

Both produce identical results. Method syntax is more common in practice.

## Essential LINQ Methods

### Where - Filtering

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var greaterThanFive = numbers.Where(n => n > 5);
// Result: 6, 7, 8, 9, 10

var evenNumbers = numbers.Where(n => n % 2 == 0);
// Result: 2, 4, 6, 8, 10
```

### Select - Projection/Transformation

```csharp
List<string> names = new List<string> { "alice", "bob", "carol" };

var upperNames = names.Select(n => n.ToUpper());
// Result: "ALICE", "BOB", "CAROL"

var nameLengths = names.Select(n => n.Length);
// Result: 5, 3, 5

// Anonymous type projection
var nameInfo = names.Select(n => new { Name = n, Length = n.Length });
```

### OrderBy / OrderByDescending - Sorting

```csharp
List<int> numbers = new List<int> { 5, 2, 8, 1, 9 };

var ascending = numbers.OrderBy(n => n);
// Result: 1, 2, 5, 8, 9

var descending = numbers.OrderByDescending(n => n);
// Result: 9, 8, 5, 2, 1

// Secondary sort with ThenBy
List<Person> people = new List<Person>
{
    new Person { Name = "Alice", Age = 30 },
    new Person { Name = "Bob", Age = 25 },
    new Person { Name = "Alice", Age = 25 }
};

var sorted = people
    .OrderBy(p => p.Name)
    .ThenBy(p => p.Age);
```

### First / FirstOrDefault

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

int first = numbers.First();  // 1
int firstEven = numbers.First(n => n % 2 == 0);  // 2

// FirstOrDefault returns default(T) if not found
int firstTen = numbers.FirstOrDefault(n => n > 10);  // 0 (default for int)

List<string> names = new List<string>();
string firstName = names.FirstOrDefault();  // null (default for string)
```

### Last / LastOrDefault

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

int last = numbers.Last();  // 5
int lastEven = numbers.Last(n => n % 2 == 0);  // 4
int lastOrDefault = numbers.LastOrDefault(n => n > 10);  // 0
```

### Single / SingleOrDefault

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };

// Single: expects exactly one match, throws if 0 or 2+
int two = numbers.Single(n => n == 2);  // 2

// SingleOrDefault: returns default if no match, throws if 2+
int ten = numbers.SingleOrDefault(n => n == 10);  // 0
```

### Count / Any / All

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

int count = numbers.Count();  // 5
int evenCount = numbers.Count(n => n % 2 == 0);  // 2

bool hasAny = numbers.Any();  // true (has elements)
bool hasEven = numbers.Any(n => n % 2 == 0);  // true

bool allPositive = numbers.All(n => n > 0);  // true
bool allEven = numbers.All(n => n % 2 == 0);  // false
```

### Sum / Average / Min / Max

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

int sum = numbers.Sum();  // 15
double average = numbers.Average();  // 3.0
int min = numbers.Min();  // 1
int max = numbers.Max();  // 5

// With selector
List<Product> products = new List<Product>
{
    new Product { Name = "A", Price = 10 },
    new Product { Name = "B", Price = 20 },
    new Product { Name = "C", Price = 15 }
};

decimal totalPrice = products.Sum(p => p.Price);  // 45
decimal avgPrice = products.Average(p => p.Price);  // 15
```

### Take / Skip

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var firstThree = numbers.Take(3);  // 1, 2, 3
var skipThree = numbers.Skip(3);  // 4, 5, 6, 7, 8, 9, 10
var page = numbers.Skip(3).Take(3);  // 4, 5, 6 (pagination)

// TakeWhile / SkipWhile
var takeWhile = numbers.TakeWhile(n => n < 5);  // 1, 2, 3, 4
var skipWhile = numbers.SkipWhile(n => n < 5);  // 5, 6, 7, 8, 9, 10
```

### Distinct

```csharp
List<int> numbers = new List<int> { 1, 2, 2, 3, 3, 3, 4 };

var unique = numbers.Distinct();  // 1, 2, 3, 4
```

### GroupBy

```csharp
List<Person> people = new List<Person>
{
    new Person { Name = "Alice", Department = "IT" },
    new Person { Name = "Bob", Department = "HR" },
    new Person { Name = "Carol", Department = "IT" },
    new Person { Name = "David", Department = "HR" }
};

var byDepartment = people.GroupBy(p => p.Department);

foreach (var group in byDepartment)
{
    Console.WriteLine($"Department: {group.Key}");
    foreach (var person in group)
    {
        Console.WriteLine($"  {person.Name}");
    }
}
// Output:
// Department: IT
//   Alice
//   Carol
// Department: HR
//   Bob
//   David
```

### Join

```csharp
List<Person> people = new List<Person>
{
    new Person { Id = 1, Name = "Alice" },
    new Person { Id = 2, Name = "Bob" }
};

List<Order> orders = new List<Order>
{
    new Order { PersonId = 1, Product = "Laptop" },
    new Order { PersonId = 1, Product = "Phone" },
    new Order { PersonId = 2, Product = "Tablet" }
};

var joined = people.Join(
    orders,
    person => person.Id,
    order => order.PersonId,
    (person, order) => new { person.Name, order.Product }
);

// Result: { Alice, Laptop }, { Alice, Phone }, { Bob, Tablet }
```

### SelectMany - Flattening

```csharp
List<List<int>> nestedLists = new List<List<int>>
{
    new List<int> { 1, 2, 3 },
    new List<int> { 4, 5, 6 },
    new List<int> { 7, 8, 9 }
};

var flattened = nestedLists.SelectMany(list => list);
// Result: 1, 2, 3, 4, 5, 6, 7, 8, 9
```

### ToList / ToArray / ToDictionary

```csharp
var query = numbers.Where(n => n > 5);

// Convert to List
List<int> list = query.ToList();

// Convert to Array
int[] array = query.ToArray();

// Convert to Dictionary
List<Person> people = GetPeople();
Dictionary<int, Person> dict = people.ToDictionary(p => p.Id);
Dictionary<int, string> nameDict = people.ToDictionary(p => p.Id, p => p.Name);
```

## Method Chaining

LINQ methods can be chained for complex queries:

```csharp
List<Product> products = GetProducts();

var result = products
    .Where(p => p.Price > 10)
    .Where(p => p.Category == "Electronics")
    .OrderByDescending(p => p.Price)
    .Take(5)
    .Select(p => new { p.Name, p.Price });
```

## Deferred Execution

LINQ queries are not executed until results are enumerated:

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(n => {
    Console.WriteLine($"Checking {n}");
    return n > 1;
});

Console.WriteLine("Query defined");

// Nothing printed yet

Console.WriteLine("Starting foreach");
foreach (var n in query)  // NOW the query executes
{
    Console.WriteLine($"Got {n}");
}

// Output:
// Query defined
// Starting foreach
// Checking 1
// Checking 2
// Got 2
// Checking 3
// Got 3
```

Force immediate execution with `ToList()`, `ToArray()`, `Count()`, etc.

## Practical Examples

### Filtering and Transforming

```csharp
List<Student> students = new List<Student>
{
    new Student { Name = "Alice", Grade = 95, Age = 20 },
    new Student { Name = "Bob", Grade = 78, Age = 22 },
    new Student { Name = "Carol", Grade = 88, Age = 19 },
    new Student { Name = "David", Grade = 92, Age = 21 }
};

// Get honor students (grade >= 90), sorted by grade
var honorStudents = students
    .Where(s => s.Grade >= 90)
    .OrderByDescending(s => s.Grade)
    .Select(s => new { s.Name, s.Grade });

foreach (var student in honorStudents)
{
    Console.WriteLine($"{student.Name}: {student.Grade}");
}
```

### Statistics

```csharp
List<int> scores = new List<int> { 78, 92, 88, 95, 67, 82 };

var stats = new
{
    Count = scores.Count(),
    Sum = scores.Sum(),
    Average = scores.Average(),
    Min = scores.Min(),
    Max = scores.Max(),
    PassCount = scores.Count(s => s >= 70),
    FailCount = scores.Count(s => s < 70)
};

Console.WriteLine($"Count: {stats.Count}");
Console.WriteLine($"Average: {stats.Average:F2}");
Console.WriteLine($"Passing: {stats.PassCount}");
```

### Grouping and Aggregation

```csharp
List<Sale> sales = new List<Sale>
{
    new Sale { Product = "Apple", Region = "North", Amount = 100 },
    new Sale { Product = "Banana", Region = "South", Amount = 150 },
    new Sale { Product = "Apple", Region = "South", Amount = 200 },
    new Sale { Product = "Banana", Region = "North", Amount = 120 }
};

var salesByProduct = sales
    .GroupBy(s => s.Product)
    .Select(g => new
    {
        Product = g.Key,
        TotalSales = g.Sum(s => s.Amount),
        AverageSale = g.Average(s => s.Amount)
    });

foreach (var item in salesByProduct)
{
    Console.WriteLine($"{item.Product}: Total=${item.TotalSales}, Avg=${item.AverageSale}");
}
```

### Pagination

```csharp
int pageSize = 10;
int pageNumber = 3;

var page = products
    .OrderBy(p => p.Name)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

## LINQ vs Traditional Loops

```csharp
// Traditional loop
List<int> evenNumbers = new List<int>();
foreach (int n in numbers)
{
    if (n % 2 == 0)
    {
        evenNumbers.Add(n);
    }
}

// LINQ
var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
```

LINQ is more:
- **Readable**: Declarative, describes what not how
- **Concise**: Less code
- **Composable**: Easy to chain operations
- **Consistent**: Same syntax for different data sources

## Summary

- LINQ provides query capabilities for collections
- Two syntaxes: Method (fluent) and Query (SQL-like)
- Essential methods: Where, Select, OrderBy, First, Count, Sum, GroupBy
- Deferred execution: Queries run when enumerated
- Method chaining enables complex queries
- ToList/ToArray force immediate execution
- LINQ makes code more readable and maintainable

LINQ is essential for working with data in modern C# applications.
