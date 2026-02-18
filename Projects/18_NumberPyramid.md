# Project 18: Number Pyramid

## Difficulty: Beginner

## Concepts: Nested loops, pattern printing

## Requirements

Create various number and star pyramids/patterns.

### Tasks:
1. Right-aligned number pyramid
2. Star pyramid (centered)
3. Inverted pyramid
4. Diamond shape
5. Number patterns

## Expected Output

```
═══════════════════════════════════════
         NUMBER PYRAMID
═══════════════════════════════════════

Enter number of rows: 5

Pattern 1: Right Triangle
1
12
123
1234
12345

Pattern 2: Number Pyramid
    1
   121
  12321
 1234321
123454321

Pattern 3: Star Pyramid
    *
   ***
  *****
 *******
*********

Pattern 4: Inverted Pyramid
*********
 *******
  *****
   ***
    *

Pattern 5: Diamond
    *
   ***
  *****
 *******
*********
 *******
  *****
   ***
    *

Pattern 6: Floyd's Triangle
1
2 3
4 5 6
7 8 9 10
11 12 13 14 15

═══════════════════════════════════════
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("         NUMBER PYRAMID");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.Write("Enter number of rows: ");
        int rows = int.Parse(Console.ReadLine());

        // Pattern 1: Right Triangle
        Console.WriteLine("\nPattern 1: Right Triangle");
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write(j);
            }
            Console.WriteLine();
        }

        // Pattern 2: Number Pyramid
        Console.WriteLine("\nPattern 2: Number Pyramid");
        for (int i = 1; i <= rows; i++)
        {
            // Print leading spaces
            for (int s = 1; s <= rows - i; s++)
            {
                Console.Write(" ");
            }
            // Print ascending numbers
            for (int j = 1; j <= i; j++)
            {
                Console.Write(j);
            }
            // Print descending numbers
            for (int j = i - 1; j >= 1; j--)
            {
                Console.Write(j);
            }
            Console.WriteLine();
        }

        // Pattern 3: Star Pyramid
        Console.WriteLine("\nPattern 3: Star Pyramid");
        // Implement using nested loops

        // Pattern 4: Inverted Pyramid
        Console.WriteLine("\nPattern 4: Inverted Pyramid");
        // Implement

        // Pattern 5: Diamond
        Console.WriteLine("\nPattern 5: Diamond");
        // Combine patterns 3 and 4

        // Pattern 6: Floyd's Triangle
        Console.WriteLine("\nPattern 6: Floyd's Triangle");
        int num = 1;
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write(num + " ");
                num++;
            }
            Console.WriteLine();
        }
    }
}
```
