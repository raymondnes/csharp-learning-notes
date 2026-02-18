# Project 11: Prime Number Checker

## Difficulty: Beginner

## Concepts: Methods, loops, boolean logic

## Requirements

Create a program that checks if numbers are prime and finds primes in a range.

### Tasks:
1. Create an `IsPrime(int number)` method
2. Check single numbers
3. Find all primes in a range
4. Count primes found

## Expected Output

```
═══════════════════════════════════════
       PRIME NUMBER CHECKER
═══════════════════════════════════════

1. Check single number
2. Find primes in range
Choose option: 1

Enter a number: 17

17 is PRIME! ✓

Factors: 1, 17
═══════════════════════════════════════

Choose option: 2

Enter start of range: 1
Enter end of range: 50

Prime numbers from 1 to 50:
2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47

Total primes found: 15
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
        Console.WriteLine("       PRIME NUMBER CHECKER");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("1. Check single number");
        Console.WriteLine("2. Find primes in range");
        Console.Write("Choose option: ");
        int option = int.Parse(Console.ReadLine());

        if (option == 1)
        {
            Console.Write("\nEnter a number: ");
            int number = int.Parse(Console.ReadLine());

            if (IsPrime(number))
            {
                Console.WriteLine($"\n{number} is PRIME! ✓");
            }
            else
            {
                Console.WriteLine($"\n{number} is NOT prime.");
            }
        }
        else if (option == 2)
        {
            // Find primes in range
        }
    }

    static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        // Check odd divisors up to sqrt(number)
        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }
}
```

## Hints

- A prime number is only divisible by 1 and itself
- Only need to check up to square root of the number
