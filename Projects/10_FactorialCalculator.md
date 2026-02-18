# Project 10: Factorial Calculator

## Difficulty: Beginner

## Concepts: While loops, accumulator pattern

## Requirements

Create a program that calculates factorials.

### Tasks:
1. Get a number from user (0-20)
2. Calculate factorial using a while loop
3. Show step-by-step calculation
4. Handle edge cases (0! = 1, negative numbers)

### Formula:
n! = n × (n-1) × (n-2) × ... × 2 × 1

## Expected Output

```
═══════════════════════════════════════
      FACTORIAL CALCULATOR
═══════════════════════════════════════

Enter a number (0-20): 5

Calculating 5!

Step-by-step:
  5 × 4 = 20
  20 × 3 = 60
  60 × 2 = 120
  120 × 1 = 120

Result: 5! = 120
═══════════════════════════════════════

Try another? (y/n): y

Enter a number (0-20): 0

Result: 0! = 1
(By definition, 0 factorial equals 1)
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
        Console.WriteLine("      FACTORIAL CALCULATOR");
        Console.WriteLine("═══════════════════════════════════════\n");

        bool continueCalculating = true;

        while (continueCalculating)
        {
            Console.Write("Enter a number (0-20): ");
            int number = int.Parse(Console.ReadLine());

            if (number < 0)
            {
                Console.WriteLine("Factorial is not defined for negative numbers.\n");
                continue;
            }

            if (number == 0)
            {
                Console.WriteLine("\nResult: 0! = 1");
                Console.WriteLine("(By definition, 0 factorial equals 1)");
            }
            else
            {
                long result = 1;
                int current = number;

                Console.WriteLine($"\nCalculating {number}!\n");
                Console.WriteLine("Step-by-step:");

                while (current > 1)
                {
                    // Show calculation steps
                    // Update result
                    // Decrement current
                }

                Console.WriteLine($"\nResult: {number}! = {result}");
            }

            Console.Write("\nTry another? (y/n): ");
            continueCalculating = Console.ReadLine().ToLower() == "y";
        }
    }
}
```

## Hints

- Use `long` instead of `int` for larger factorials
- 20! is the max that fits in a long
