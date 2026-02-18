# Project 09: Multiplication Table

## Difficulty: Beginner

## Concepts: Nested loops, formatting

## Requirements

Create a program that displays multiplication tables.

### Tasks:
1. Get a number from the user
2. Display its multiplication table (1-12)
3. Format output in a neat table
4. Add option to show full 12x12 grid

## Expected Output

```
═══════════════════════════════════════
      MULTIPLICATION TABLE
═══════════════════════════════════════

Enter a number: 7

Multiplication Table for 7:
───────────────────────────
  7 ×  1 =   7
  7 ×  2 =  14
  7 ×  3 =  21
  7 ×  4 =  28
  7 ×  5 =  35
  7 ×  6 =  42
  7 ×  7 =  49
  7 ×  8 =  56
  7 ×  9 =  63
  7 × 10 =  70
  7 × 11 =  77
  7 × 12 =  84
───────────────────────────

Show full 12x12 grid? (y/n): y

    │  1   2   3   4   5   6   7   8   9  10  11  12
────┼────────────────────────────────────────────────
  1 │  1   2   3   4   5   6   7   8   9  10  11  12
  2 │  2   4   6   8  10  12  14  16  18  20  22  24
  3 │  3   6   9  12  15  18  21  24  27  30  33  36
... (continues to 12)
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("      MULTIPLICATION TABLE");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.Write("Enter a number: ");
        int number = int.Parse(Console.ReadLine());

        Console.WriteLine($"\nMultiplication Table for {number}:");
        Console.WriteLine("───────────────────────────");

        // Single table using for loop
        for (int i = 1; i <= 12; i++)
        {
            // Use formatting: {number,3} for right-aligned width 3
        }

        // Full grid using nested loops
    }
}
```

## Hints

- Use `{value,4}` for right-aligned output with width 4
- Nested loops: outer for rows, inner for columns
