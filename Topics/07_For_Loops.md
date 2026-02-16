# For Loops

## Introduction

Loops are fundamental to programming - they allow you to repeat actions without writing the same code multiple times. The `for` loop is ideal when you know in advance how many times you want to repeat something. It's particularly useful for iterating a specific number of times or traversing arrays and collections by index.

## Basic For Loop Syntax

```csharp
for (initialization; condition; iteration)
{
    // Code to repeat
}
```

### Components

1. **Initialization**: Runs once before the loop starts (typically declares a counter)
2. **Condition**: Checked before each iteration (loop continues while true)
3. **Iteration**: Runs after each iteration (typically increments the counter)

### Execution Flow

```
1. Initialization executes (once)
2. Condition is checked
   - If true: Execute loop body, then iteration, go to step 2
   - If false: Exit loop
```

## Simple Examples

### Count from 1 to 5

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
// Output: 1, 2, 3, 4, 5 (each on new line)
```

### Count from 0 to 4

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
// Output: 0, 1, 2, 3, 4
```

### Count down from 10 to 1

```csharp
for (int i = 10; i >= 1; i--)
{
    Console.WriteLine(i);
}
// Output: 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
```

## Variable Scope

The loop variable declared in initialization is scoped to the loop:

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);  // i is accessible here
}
// Console.WriteLine(i);  // ERROR: i doesn't exist here

// If you need the value after the loop:
int j;
for (j = 0; j < 5; j++)
{
    Console.WriteLine(j);
}
Console.WriteLine($"Final value: {j}");  // j = 5
```

## Different Step Values

### Increment by 2

```csharp
for (int i = 0; i <= 10; i += 2)
{
    Console.WriteLine(i);
}
// Output: 0, 2, 4, 6, 8, 10
```

### Decrement by 3

```csharp
for (int i = 15; i > 0; i -= 3)
{
    Console.WriteLine(i);
}
// Output: 15, 12, 9, 6, 3
```

### Multiply

```csharp
for (int i = 1; i <= 1000; i *= 2)
{
    Console.WriteLine(i);
}
// Output: 1, 2, 4, 8, 16, 32, 64, 128, 256, 512
```

## Working with Arrays

For loops are essential for array manipulation:

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// Forward iteration
for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine($"Index {i}: {numbers[i]}");
}

// Backward iteration
for (int i = numbers.Length - 1; i >= 0; i--)
{
    Console.WriteLine($"Index {i}: {numbers[i]}");
}
```

### Modifying Array Elements

```csharp
int[] values = { 1, 2, 3, 4, 5 };

// Double each element
for (int i = 0; i < values.Length; i++)
{
    values[i] *= 2;
}
// values is now: { 2, 4, 6, 8, 10 }
```

## Nested Loops

Loops inside loops are common for multi-dimensional operations:

### Multiplication Table

```csharp
for (int i = 1; i <= 5; i++)
{
    for (int j = 1; j <= 5; j++)
    {
        Console.Write($"{i * j,4}");  // ,4 adds padding
    }
    Console.WriteLine();  // New line after each row
}
```

Output:
```
   1   2   3   4   5
   2   4   6   8  10
   3   6   9  12  15
   4   8  12  16  20
   5  10  15  20  25
```

### Pattern Printing

```csharp
// Right triangle
for (int i = 1; i <= 5; i++)
{
    for (int j = 1; j <= i; j++)
    {
        Console.Write("* ");
    }
    Console.WriteLine();
}
```

Output:
```
*
* *
* * *
* * * *
* * * * *
```

## Break and Continue

### Break: Exit Loop Early

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i == 6)
    {
        break;  // Exit loop when i reaches 6
    }
    Console.WriteLine(i);
}
// Output: 1, 2, 3, 4, 5
```

### Continue: Skip to Next Iteration

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i % 2 == 0)
    {
        continue;  // Skip even numbers
    }
    Console.WriteLine(i);
}
// Output: 1, 3, 5, 7, 9
```

### Breaking from Nested Loops

```csharp
bool found = false;

for (int i = 0; i < 10 && !found; i++)
{
    for (int j = 0; j < 10 && !found; j++)
    {
        if (i * j == 42)
        {
            Console.WriteLine($"Found: {i} * {j} = 42");
            found = true;  // Will exit both loops
        }
    }
}
```

Or use a labeled approach with a method:

```csharp
void SearchMatrix()
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if (i * j == 42)
            {
                Console.WriteLine($"Found: {i} * {j} = 42");
                return;  // Exit method (and both loops)
            }
        }
    }
}
```

## Infinite Loops

A for loop with no conditions runs forever:

```csharp
for (;;)  // Infinite loop
{
    Console.WriteLine("This runs forever!");
    // Must use break to exit
}

// More commonly seen with while:
// while (true) { ... }
```

## Multiple Variables

You can use multiple variables in a for loop:

```csharp
for (int i = 0, j = 10; i < j; i++, j--)
{
    Console.WriteLine($"i = {i}, j = {j}");
}
// Output:
// i = 0, j = 10
// i = 1, j = 9
// i = 2, j = 8
// i = 3, j = 7
// i = 4, j = 6
```

## Practical Examples

### Example 1: Sum of Numbers

```csharp
int sum = 0;
for (int i = 1; i <= 100; i++)
{
    sum += i;
}
Console.WriteLine($"Sum of 1 to 100: {sum}");  // 5050
```

### Example 2: Factorial

```csharp
int n = 5;
long factorial = 1;

for (int i = 1; i <= n; i++)
{
    factorial *= i;
}
Console.WriteLine($"{n}! = {factorial}");  // 5! = 120
```

### Example 3: Fibonacci Sequence

```csharp
int n = 10;
int a = 0, b = 1;

Console.Write($"{a} {b} ");
for (int i = 2; i < n; i++)
{
    int next = a + b;
    Console.Write($"{next} ");
    a = b;
    b = next;
}
// Output: 0 1 1 2 3 5 8 13 21 34
```

### Example 4: Finding Prime Numbers

```csharp
Console.WriteLine("Prime numbers from 2 to 50:");
for (int num = 2; num <= 50; num++)
{
    bool isPrime = true;

    for (int i = 2; i <= Math.Sqrt(num); i++)
    {
        if (num % i == 0)
        {
            isPrime = false;
            break;
        }
    }

    if (isPrime)
    {
        Console.Write($"{num} ");
    }
}
// Output: 2 3 5 7 11 13 17 19 23 29 31 37 41 43 47
```

### Example 5: Array Search

```csharp
int[] numbers = { 5, 12, 8, 130, 44, 23 };
int target = 130;
int foundIndex = -1;

for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] == target)
    {
        foundIndex = i;
        break;
    }
}

if (foundIndex >= 0)
{
    Console.WriteLine($"Found {target} at index {foundIndex}");
}
else
{
    Console.WriteLine($"{target} not found");
}
```

### Example 6: String Processing

```csharp
string text = "Hello, World!";

// Count vowels
int vowelCount = 0;
for (int i = 0; i < text.Length; i++)
{
    char c = char.ToLower(text[i]);
    if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
    {
        vowelCount++;
    }
}
Console.WriteLine($"Vowels: {vowelCount}");  // 3

// Reverse string
string reversed = "";
for (int i = text.Length - 1; i >= 0; i--)
{
    reversed += text[i];
}
Console.WriteLine($"Reversed: {reversed}");  // !dlroW ,olleH
```

## Common Mistakes

### 1. Off-by-One Errors

```csharp
int[] arr = { 1, 2, 3, 4, 5 };

// Wrong: Will cause IndexOutOfRangeException
// for (int i = 0; i <= arr.Length; i++)

// Correct
for (int i = 0; i < arr.Length; i++)
{
    Console.WriteLine(arr[i]);
}
```

### 2. Infinite Loops

```csharp
// Wrong: i never changes
// for (int i = 0; i < 10; )
// {
//     Console.WriteLine(i);
// }

// Correct
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

### 3. Modifying Loop Variable

```csharp
// Confusing and error-prone
for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        i = 7;  // Skips 6, 7 - unexpected behavior
    }
    Console.WriteLine(i);
}
```

### 4. Wrong Condition Direction

```csharp
// Wrong: Loop never executes
// for (int i = 10; i < 5; i++)

// Correct for counting down
for (int i = 10; i >= 5; i--)
{
    Console.WriteLine(i);
}
```

## Performance Considerations

### Cache Array Length

```csharp
// Less efficient: Length checked each iteration
for (int i = 0; i < array.Length; i++) { }

// More efficient: Length cached
int length = array.Length;
for (int i = 0; i < length; i++) { }

// Or inline
for (int i = 0, len = array.Length; i < len; i++) { }
```

### Use Pre-increment When Possible

```csharp
// Technically ++i is slightly more efficient than i++
// (Though compilers often optimize this)
for (int i = 0; i < 100; ++i) { }
```

## Summary

- For loops repeat code a specific number of times
- Three parts: initialization, condition, iteration
- The loop variable is scoped to the loop block
- Step values can be any amount (++, --, +=2, *=2, etc.)
- Nested loops handle multi-dimensional operations
- `break` exits the loop; `continue` skips to the next iteration
- Be careful of off-by-one errors with array indices
- Cache array length for better performance in tight loops

For loops are essential for any operation that requires repetition with a known count or index-based access.
