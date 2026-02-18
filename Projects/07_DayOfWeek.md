# Project 07: Day of Week

## Difficulty: Beginner

## Concepts: Switch statements, enums

## Requirements

Create a program that provides information about any day of the week.

### Tasks:
1. Get day number from user (1-7)
2. Use switch to determine the day
3. Display if it's a weekday or weekend
4. Show typical activities for that day
5. Handle invalid input

## Expected Output

```
═══════════════════════════════════════
       DAY OF THE WEEK
═══════════════════════════════════════

Enter day number (1=Monday, 7=Sunday): 5

═══════════════════════════════════════

Day: Friday
Type: Weekday
Days until weekend: 0

Typical activities:
  → Finish weekly tasks
  → Casual dress day
  → Team meetings
  → Plan weekend activities

Mood: 🎉 TGIF!
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
        Console.WriteLine("       DAY OF THE WEEK");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.Write("Enter day number (1=Monday, 7=Sunday): ");
        int dayNumber = int.Parse(Console.ReadLine());

        string dayName;
        string dayType;
        string mood;

        switch (dayNumber)
        {
            case 1:
                dayName = "Monday";
                dayType = "Weekday";
                mood = "😫 Start of the week";
                break;
            case 2:
                // Tuesday
                break;
            // Continue for all days...
            default:
                Console.WriteLine("Invalid day! Enter 1-7.");
                return;
        }

        // Display results
    }
}
```

## Bonus Challenge

Use `DayOfWeek` enum and allow text input ("Monday", "Mon")
