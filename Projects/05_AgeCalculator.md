# Project 05: Age Calculator

## Difficulty: Beginner

## Concepts: DateTime, calculations, user input

## Requirements

Create a program that calculates a person's exact age and provides interesting facts.

### Tasks:
1. Get birth date from user (year, month, day)
2. Calculate exact age in years
3. Calculate total months lived
4. Calculate total days lived
5. Calculate days until next birthday
6. Display the day of the week they were born

## Expected Output

```
══════════════════════════════════════════
          AGE CALCULATOR
══════════════════════════════════════════

Enter your birth year: 1998
Enter your birth month (1-12): 6
Enter your birth day (1-31): 15

══════════════════════════════════════════
          YOUR AGE ANALYSIS
══════════════════════════════════════════

Birth Date: June 15, 1998 (Monday)

You are:
  → 26 years old
  → 312 months old
  → 9,497 days old

Days until next birthday: 148

Fun fact: You've lived through approximately:
  → 227,928 hours
  → 13,675,680 minutes
══════════════════════════════════════════
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("══════════════════════════════════════════");
        Console.WriteLine("          AGE CALCULATOR");
        Console.WriteLine("══════════════════════════════════════════\n");

        // Get birth date components
        Console.Write("Enter your birth year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter your birth month (1-12): ");
        int month = int.Parse(Console.ReadLine());

        Console.Write("Enter your birth day (1-31): ");
        int day = int.Parse(Console.ReadLine());

        // Create DateTime object
        DateTime birthDate = new DateTime(year, month, day);
        DateTime today = DateTime.Today;

        // Calculate age
        // Hint: (today - birthDate).TotalDays

        // Calculate days until next birthday
        // Hint: Create next birthday date and subtract today
    }
}
```

## Hints

- Use `birthDate.DayOfWeek` to get day name
- Use `TimeSpan` for date differences
- Use `birthDate.ToString("MMMM dd, yyyy")` for formatting
