# Project 06: Grade Checker

## Difficulty: Beginner

## Concepts: If-else statements, comparison operators

## Requirements

Create a grading system that converts numerical scores to letter grades.

### Tasks:
1. Get score from user (0-100)
2. Validate the input is within range
3. Convert to letter grade using if-else
4. Display pass/fail status
5. Show encouraging message based on grade

### Grading Scale:
- A: 90-100
- B: 80-89
- C: 70-79
- D: 60-69
- F: Below 60

## Expected Output

```
═══════════════════════════════════════
         GRADE CHECKER
═══════════════════════════════════════

Enter your score (0-100): 85

═══════════════════════════════════════
           RESULTS
═══════════════════════════════════════

Score: 85/100
Grade: B
Status: PASSED ✓

Message: Great job! Keep up the good work!
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
        Console.WriteLine("         GRADE CHECKER");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.Write("Enter your score (0-100): ");
        int score = int.Parse(Console.ReadLine());

        string grade;
        string message;
        bool passed;

        // Validate input
        if (score < 0 || score > 100)
        {
            Console.WriteLine("Invalid score! Please enter 0-100.");
            return;
        }

        // Determine grade using if-else
        if (score >= 90)
        {
            grade = "A";
            // Set message and passed status
        }
        // Continue with other grades...

        // Display results
    }
}
```

## Bonus Challenge

Add +/- grades (A+, A, A-, B+, etc.)
