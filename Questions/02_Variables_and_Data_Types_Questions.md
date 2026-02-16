# Variables and Data Types - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Variables and Data Types concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Declare and initialize variables of four different basic types and display their values.

**Requirements:**
1. Declare an `int` variable with any whole number value
2. Declare a `double` variable with any decimal value
3. Declare a `bool` variable with either `true` or `false`
4. Declare a `string` variable with any text
5. Use `Console.WriteLine()` to display each value on a separate line

**Expected Output Example:**
```
42
3.14
True
Hello, World!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a program that stores and displays personal information using appropriate data types.

**Requirements:**
1. Create a `string` variable for a person's name
2. Create an `int` variable for their age
3. Create a `double` variable for their height in meters
4. Create a `char` variable for their first initial
5. Display all information using string interpolation

**Expected Output Example:**
```
Name: Alice
Age: 25
Height: 1.65 meters
Initial: A
```

---

## Level 3: Application (Easy)

**Challenge:** Demonstrate the difference between `float`, `double`, and `decimal` types.

**Requirements:**
1. Declare a `float` variable with value 1.123456789 (use appropriate suffix)
2. Declare a `double` variable with the same value
3. Declare a `decimal` variable with the same value (use appropriate suffix)
4. Print each value to show precision differences
5. Add a comment explaining which type to use for financial calculations and why

**Expected Output Example:**
```
Float:   1.1234568
Double:  1.123456789
Decimal: 1.123456789
```

---

## Level 4: Application (Easy)

**Challenge:** Create a program that demonstrates variable scope and the use of constants.

**Requirements:**
1. Declare a constant for Pi with value 3.14159
2. Declare a constant for the speed of light (299792458 meters per second)
3. Calculate the circumference of a circle with radius 5 using the Pi constant
4. Display both constants and the calculated circumference
5. Use appropriate data types for each value

**Expected Output:**
```
Pi: 3.14159
Speed of Light: 299792458 m/s
Circumference of circle (radius 5): 31.4159
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a simple product inventory item using multiple variables and calculations.

**Requirements:**
1. Store product name (string), price (decimal), quantity in stock (int)
2. Store whether the product is available (bool) - true if quantity > 0
3. Calculate the total inventory value (price * quantity)
4. Store a product SKU as a combination of letters and numbers (string)
5. Display all information in a formatted receipt-like output
6. Use proper decimal precision for currency (2 decimal places)

**Expected Output Example:**
```
=== PRODUCT DETAILS ===
SKU: PRD-12345
Name: Wireless Mouse
Price: $29.99
Quantity: 50
Available: True
Total Value: $1,499.50
=======================
```

---

## Level 6: Integration (Moderate)

**Challenge:** Demonstrate type inference with `var` and show when the compiler determines types.

**Requirements:**
1. Declare at least 6 variables using `var` with different inferred types
2. Include: integer, double, string, boolean, char, and decimal (with suffix)
3. Use `.GetType().Name` to print the actual type the compiler inferred
4. Format output as a two-column display showing value and type
5. Include a variable that could be ambiguous and explain the default inference

**Expected Output Example:**
```
Value                Type
-----                ----
42                   Int32
3.14                 Double
Hello                String
True                 Boolean
A                    Char
19.99                Decimal

Note: 3.14 without suffix defaults to Double, not Decimal
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a data type demonstration program showing overflow behavior and type limits.

**Requirements:**
1. Display the minimum and maximum values for: `byte`, `int`, `long`
2. Use the `.MinValue` and `.MaxValue` properties
3. Demonstrate what happens when you add 1 to `byte.MaxValue` using `unchecked`
4. Show the difference between `checked` and `unchecked` arithmetic
5. Create a formatted table showing all the information
6. Include comments explaining overflow behavior

**Expected Output Example:**
```
╔═══════════════════════════════════════════════════════════════╗
║                    DATA TYPE LIMITS                           ║
╠═══════════╦════════════════════╦══════════════════════════════╣
║ Type      ║ Min Value          ║ Max Value                    ║
╠═══════════╬════════════════════╬══════════════════════════════╣
║ byte      ║ 0                  ║ 255                          ║
║ int       ║ -2147483648        ║ 2147483647                   ║
║ long      ║ -9223372036854775808 ║ 9223372036854775807        ║
╚═══════════╩════════════════════╩══════════════════════════════╝

Overflow Demo (unchecked):
byte.MaxValue (255) + 1 = 0 (wraps around!)
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a type conversion demonstration showing implicit and explicit conversions.

**Requirements:**
1. Demonstrate implicit conversion (widening): byte → int → long → double
2. Demonstrate explicit conversion (narrowing) with casting: double → int → byte
3. Show potential data loss when converting from double to int
4. Demonstrate string-to-number conversion using `Parse` methods
5. Show number-to-string conversion using `.ToString()` and string interpolation
6. Handle a potential parse exception scenario
7. Display all conversions in a clear, formatted output

**Expected Output Example:**
```
=== IMPLICIT CONVERSIONS (Safe - No Data Loss) ===
byte (255) → int (255) → long (255) → double (255)

=== EXPLICIT CONVERSIONS (May Lose Data) ===
double 3.99 → int 3 (fractional part lost!)
int 300 → byte 44 (overflow - only lower 8 bits kept!)

=== STRING CONVERSIONS ===
String "42" → int 42
String "3.14" → double 3.14
int 100 → String "100"

=== ERROR HANDLING ===
Attempting to parse "hello" as int...
Parse failed: Input string was not in a correct format.
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive "Variable Inspector" utility that analyzes and displays detailed information about any value.

**Requirements:**
1. Create variables of at least 8 different types (including nullable types)
2. For each variable, display:
   - Variable name (use nameof() where possible)
   - Data type (full name and short name)
   - Value
   - Whether it's a value type or reference type
   - Whether it's nullable and if it has a value
   - Memory size in bytes (use sizeof() for value types or describe for reference types)
3. Create a helper method that formats this information
4. Handle nullable types correctly (e.g., `int?`, `double?`)
5. Include at least one null nullable value to show different behavior
6. Create a professional table output with proper alignment
7. Include a summary section showing total variables and type distribution

**Expected Output Example:**
```
╔══════════════════════════════════════════════════════════════════════════════╗
║                          VARIABLE INSPECTOR                                   ║
╠══════════════════════════════════════════════════════════════════════════════╣
║ Name         │ Type          │ Value        │ Category   │ Nullable │ Size   ║
╠══════════════╪═══════════════╪══════════════╪════════════╪══════════╪════════╣
║ age          │ Int32         │ 25           │ Value      │ No       │ 4 B    ║
║ price        │ Decimal       │ 19.99        │ Value      │ No       │ 16 B   ║
║ name         │ String        │ "Alice"      │ Reference  │ Yes      │ ~14 B  ║
║ isActive     │ Boolean       │ True         │ Value      │ No       │ 1 B    ║
║ rating       │ Double        │ 4.5          │ Value      │ No       │ 8 B    ║
║ grade        │ Char          │ 'A'          │ Value      │ No       │ 2 B    ║
║ nullableInt  │ Nullable<Int32>│ 42          │ Value      │ Yes (✓)  │ 8 B    ║
║ emptyNullable│ Nullable<Int32>│ (null)      │ Value      │ Yes (✗)  │ 8 B    ║
╚══════════════╧═══════════════╧══════════════╧════════════╧══════════╧════════╝

=== SUMMARY ===
Total Variables: 8
Value Types: 6 (75%)
Reference Types: 2 (25%)
Nullable Types: 3
Null Values: 1
Total Memory (approx): 61 bytes
```

**Additional Requirements:**
- Code must compile and run without errors
- Use proper type checking methods (IsValueType, etc.)
- Include meaningful comments explaining the inspection logic
- Handle edge cases (null values, empty strings)

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does it produce the expected output?
2. **Type Usage**: Are appropriate data types used?
3. **Syntax**: Is the code syntactically correct?
4. **Best Practices**: Does it follow C# conventions?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Variables and Data Types concepts.*
