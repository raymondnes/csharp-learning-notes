# Project 13: FizzBuzz

## Difficulty: Beginner

## Concepts: Control flow, modulus operator, conditionals

## Requirements

Classic programming challenge with variations.

### Rules:
- Print numbers 1 to N
- For multiples of 3, print "Fizz"
- For multiples of 5, print "Buzz"
- For multiples of both, print "FizzBuzz"

### Tasks:
1. Implement classic FizzBuzz
2. Add statistics (counts of each)
3. Allow custom range and divisors

## Expected Output

```
═══════════════════════════════════════
           FIZZBUZZ
═══════════════════════════════════════

Enter range (1 to N): 20

1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz,
11, Fizz, 13, 14, FizzBuzz, 16, 17, Fizz, 19, Buzz

═══════════════════════════════════════
           STATISTICS
═══════════════════════════════════════
Numbers:   11
Fizz:       4
Buzz:       2
FizzBuzz:   1
Total:     20
═══════════════════════════════════════

Play custom version? (y/n): y

Enter first divisor: 3
Enter word for first: Fizz
Enter second divisor: 7
Enter word for second: Jazz

1, 2, Fizz, 4, 5, Fizz, Jazz, 8, Fizz, 10,
11, Fizz, 13, Jazz, Fizz, 16, 17, Fizz, 19, 20,
FizzJazz, 22, 23, Fizz, 25...
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
        Console.WriteLine("           FIZZBUZZ");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.Write("Enter range (1 to N): ");
        int n = int.Parse(Console.ReadLine());

        int numberCount = 0;
        int fizzCount = 0;
        int buzzCount = 0;
        int fizzBuzzCount = 0;

        Console.WriteLine();

        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                Console.Write("FizzBuzz");
                fizzBuzzCount++;
            }
            else if (i % 3 == 0)
            {
                Console.Write("Fizz");
                fizzCount++;
            }
            else if (i % 5 == 0)
            {
                Console.Write("Buzz");
                buzzCount++;
            }
            else
            {
                Console.Write(i);
                numberCount++;
            }

            // Add comma and newline formatting
            if (i < n) Console.Write(", ");
            if (i % 10 == 0) Console.WriteLine();
        }

        // Display statistics
    }
}
```
