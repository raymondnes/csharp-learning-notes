# Project 01: Hello World Variations

## Difficulty: Beginner

## Concepts: Console output, strings, escape characters

## Requirements

Create a program that displays various greetings using different output techniques.

### Tasks:
1. Print "Hello, World!" using `Console.WriteLine()`
2. Print "Hello, C#!" using `Console.Write()` (notice no newline)
3. Print a blank line
4. Print a greeting with your name using string concatenation
5. Print a greeting using string interpolation
6. Print a multi-line message using escape characters (\n)
7. Print a message with tabs (\t)
8. Print a message with quotes inside it

## Expected Output

```
Hello, World!
Hello, C#!

Welcome, [YourName]!
Hello, my name is [YourName]!
Line 1
Line 2
Line 3
	This is tabbed text
She said "Hello!" to me.
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        // Task 1: Print Hello, World!

        // Task 2: Print Hello, C#! (no newline)

        // Task 3: Print blank line

        // Task 4: String concatenation

        // Task 5: String interpolation

        // Task 6: Multi-line with \n

        // Task 7: Tabs with \t

        // Task 8: Quotes inside string
    }
}
```

## Hints

- `Console.WriteLine()` adds a newline at the end
- `Console.Write()` does not add a newline
- Use `+` for concatenation: `"Hello, " + name`
- Use `$""` for interpolation: `$"Hello, {name}"`
- Use `\"` to include quotes in a string
