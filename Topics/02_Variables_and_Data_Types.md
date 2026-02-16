# Variables and Data Types

## Introduction

Variables are the fundamental building blocks of any program. They are containers that store data your program needs to work with. Understanding how variables work and the different types of data you can store is essential for writing effective C# code.

## What is a Variable?

A variable is a named storage location in your computer's memory. Think of it as a labeled box where you can put information and retrieve it later.

### Anatomy of a Variable Declaration

```csharp
int age = 25;
```

Breaking this down:
- `int` - The **data type** (what kind of data can be stored)
- `age` - The **identifier** (the name you use to refer to this variable)
- `=` - The **assignment operator** (puts a value into the variable)
- `25` - The **value** (the actual data being stored)
- `;` - The **statement terminator** (marks the end of the statement)

## Built-In Data Types

C# is a **strongly-typed language**, meaning every variable must have a declared type, and you can only store compatible values in it.

### Numeric Types

#### Integer Types (Whole Numbers)

| Type | Size | Range | Use Case |
|------|------|-------|----------|
| `byte` | 1 byte | 0 to 255 | Small positive numbers |
| `sbyte` | 1 byte | -128 to 127 | Small signed numbers |
| `short` | 2 bytes | -32,768 to 32,767 | Small numbers |
| `ushort` | 2 bytes | 0 to 65,535 | Small positive numbers |
| `int` | 4 bytes | -2.1 billion to 2.1 billion | **Most common choice** |
| `uint` | 4 bytes | 0 to 4.2 billion | Large positive numbers |
| `long` | 8 bytes | -9.2 quintillion to 9.2 quintillion | Very large numbers |
| `ulong` | 8 bytes | 0 to 18.4 quintillion | Very large positive numbers |

```csharp
int population = 8000000;
long worldPopulation = 8000000000L;  // L suffix for long literals
byte percentage = 100;
```

#### Floating-Point Types (Decimal Numbers)

| Type | Size | Precision | Use Case |
|------|------|-----------|----------|
| `float` | 4 bytes | ~6-9 digits | Graphics, games |
| `double` | 8 bytes | ~15-17 digits | **Most common for decimals** |
| `decimal` | 16 bytes | 28-29 digits | Financial calculations |

```csharp
float temperature = 98.6f;     // f suffix required for float
double pi = 3.14159265359;     // Default for decimal literals
decimal price = 19.99m;        // m suffix required for decimal
```

**Why does `decimal` exist?**

Floating-point numbers (`float`, `double`) can have tiny precision errors:

```csharp
double a = 0.1 + 0.2;
Console.WriteLine(a);  // Output: 0.30000000000000004 (not exactly 0.3!)

decimal b = 0.1m + 0.2m;
Console.WriteLine(b);  // Output: 0.3 (exact)
```

For money and financial calculations, always use `decimal`.

### Boolean Type

The `bool` type stores `true` or `false` - nothing else.

```csharp
bool isLoggedIn = true;
bool hasPermission = false;
bool isValid = (5 > 3);  // true
```

Booleans are essential for decision-making in your programs.

### Character Type

The `char` type stores a single Unicode character, enclosed in single quotes.

```csharp
char grade = 'A';
char symbol = '@';
char newline = '\n';     // Escape sequence
char unicode = '\u0041'; // Unicode for 'A'
```

### String Type

The `string` type stores sequences of characters, enclosed in double quotes.

```csharp
string name = "John Doe";
string empty = "";
string message = "Hello, World!";
```

**Important:** Strings are **reference types**, while the others above are **value types**. This affects how they're stored in memory and how they behave when assigned or passed to methods.

## Variable Declaration and Initialization

### Declaration Without Initialization

```csharp
int count;           // Declared but not initialized
count = 10;          // Now initialized
```

**Warning:** Using an uninitialized variable causes a compile error:

```csharp
int number;
Console.WriteLine(number);  // ERROR: Use of unassigned local variable
```

### Declaration With Initialization

```csharp
int count = 10;      // Declared and initialized in one statement
```

### Multiple Declarations

```csharp
// Same type on one line
int x = 1, y = 2, z = 3;

// Or separately for clarity
int width = 100;
int height = 200;
```

## Type Inference with `var`

C# can infer the type from the assigned value using the `var` keyword:

```csharp
var name = "Alice";     // Compiler infers: string
var age = 30;           // Compiler infers: int
var price = 19.99;      // Compiler infers: double
var isActive = true;    // Compiler infers: bool
```

**Important rules for `var`:**
- Must be initialized at declaration
- Type is determined at compile time (not runtime)
- Once set, the type cannot change

```csharp
var x = 10;
x = "hello";  // ERROR: Cannot convert string to int
```

**When to use `var`:**
- When the type is obvious from the right side
- With complex generic types
- When following team conventions

**When NOT to use `var`:**
- When it reduces code clarity
- When the type isn't obvious

## Constants

Constants are variables whose values cannot change after initialization.

```csharp
const double PI = 3.14159265359;
const int MAX_USERS = 100;
const string APP_NAME = "MyApplication";
```

**Naming convention:** Constants typically use UPPER_SNAKE_CASE or PascalCase.

Attempting to change a constant results in a compile error:

```csharp
const int MAX = 100;
MAX = 200;  // ERROR: The left-hand side of an assignment must be a variable
```

## Default Values

When variables are fields in a class (not local variables), they have default values:

| Type | Default Value |
|------|---------------|
| `int`, `long`, etc. | `0` |
| `float`, `double` | `0.0` |
| `decimal` | `0.0m` |
| `bool` | `false` |
| `char` | `'\0'` (null character) |
| `string` | `null` |
| Reference types | `null` |

## Naming Conventions

C# has established naming conventions:

| Element | Convention | Example |
|---------|------------|---------|
| Local variables | camelCase | `firstName`, `totalAmount` |
| Constants | PascalCase or UPPER_SNAKE_CASE | `MaxValue`, `MAX_VALUE` |
| Private fields | _camelCase (underscore prefix) | `_count`, `_isValid` |

### Rules for Identifiers

1. Must start with a letter or underscore
2. Can contain letters, digits, and underscores
3. Cannot be a C# keyword (unless prefixed with `@`)
4. Case-sensitive (`age` ≠ `Age`)

```csharp
// Valid identifiers
int age;
int _count;
int firstName;
int person1;
int @class;  // Using keyword with @ prefix

// Invalid identifiers
// int 1person;    // Cannot start with digit
// int first-name; // Cannot contain hyphen
// int class;      // Cannot use keyword without @
```

## Nullable Value Types

By default, value types cannot be `null`. Adding `?` makes them nullable:

```csharp
int? nullableInt = null;
double? nullableDouble = null;
bool? nullableBool = null;

if (nullableInt.HasValue)
{
    Console.WriteLine(nullableInt.Value);
}
else
{
    Console.WriteLine("No value");
}
```

## Practical Examples

### Example 1: Personal Information

```csharp
string firstName = "John";
string lastName = "Doe";
int age = 28;
double height = 5.9;
bool isEmployed = true;

Console.WriteLine($"Name: {firstName} {lastName}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Height: {height} ft");
Console.WriteLine($"Employed: {isEmployed}");
```

### Example 2: Financial Calculation

```csharp
decimal principal = 1000.00m;
decimal interestRate = 0.05m;
int years = 5;

decimal interest = principal * interestRate * years;
decimal total = principal + interest;

Console.WriteLine($"Principal: ${principal}");
Console.WriteLine($"Interest: ${interest}");
Console.WriteLine($"Total: ${total}");
```

### Example 3: Game Statistics

```csharp
string playerName = "Hero";
int health = 100;
int maxHealth = 100;
double damageMultiplier = 1.5;
bool isAlive = true;
char rank = 'S';

Console.WriteLine($"Player: {playerName}");
Console.WriteLine($"Health: {health}/{maxHealth}");
Console.WriteLine($"Rank: {rank}");
```

## Common Mistakes

1. **Type mismatch:**
```csharp
int number = 3.14;  // ERROR: Cannot implicitly convert double to int
```

2. **Missing suffix for literals:**
```csharp
float f = 3.14;    // ERROR: Use 3.14f
decimal d = 19.99; // ERROR: Use 19.99m
```

3. **Using uninitialized variables:**
```csharp
int x;
Console.WriteLine(x);  // ERROR: Use of unassigned variable
```

4. **Overflow:**
```csharp
byte b = 256;  // ERROR: Constant value '256' cannot be converted to 'byte'
```

## Summary

- Variables store data with a specific type
- C# is strongly-typed - types must be declared and respected
- Use `int` for whole numbers, `double` for decimals, `decimal` for money
- Use `bool` for true/false values
- Use `string` for text
- Use `var` for type inference when the type is obvious
- Use `const` for values that never change
- Follow naming conventions for readable code
- Nullable types (`?`) allow value types to be `null`

Understanding data types is fundamental to writing correct and efficient C# code.
