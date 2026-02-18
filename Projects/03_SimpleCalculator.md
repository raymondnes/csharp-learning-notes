# Project 03: Simple Calculator

## Difficulty: Beginner

## Concepts: Arithmetic operators, user input, type conversion

## Requirements

Create a calculator that performs basic arithmetic operations on two numbers.

### Tasks:
1. Prompt user for first number
2. Prompt user for second number
3. Display results of: addition, subtraction, multiplication, division
4. Display the remainder (modulus)
5. Handle division by zero gracefully

## Expected Output

```
═══════════════════════════════════
       SIMPLE CALCULATOR
═══════════════════════════════════

Enter first number: 15
Enter second number: 4

Results:
───────────────────────────────────
15 + 4 = 19
15 - 4 = 11
15 × 4 = 60
15 ÷ 4 = 3.75
15 % 4 = 3 (remainder)
───────────────────────────────────
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine("       SIMPLE CALCULATOR");
        Console.WriteLine("═══════════════════════════════════\n");

        // Get first number
        Console.Write("Enter first number: ");
        // Convert input to double

        // Get second number
        Console.Write("Enter second number: ");
        // Convert input to double

        // Perform calculations

        // Display results
    }
}
```

## Hints

- Use `double.Parse(Console.ReadLine())` to convert input
- Check if second number is 0 before dividing
- Use `%` for modulus (remainder)
