# Project 02: Personal Info Card

## Difficulty: Beginner

## Concepts: Variables, data types, string interpolation

## Requirements

Create a program that stores personal information in variables and displays a formatted info card.

### Tasks:
1. Create variables for: name (string), age (int), height (double), isStudent (bool)
2. Create variables for: city, country, hobby
3. Display all information in a nicely formatted card
4. Calculate and display birth year

## Expected Output

```
╔══════════════════════════════════════╗
║         PERSONAL INFO CARD           ║
╠══════════════════════════════════════╣
║ Name:      John Smith                ║
║ Age:       25                        ║
║ Born:      1999                      ║
║ Height:    5.9 ft                    ║
║ Student:   Yes                       ║
║ Location:  New York, USA             ║
║ Hobby:     Programming               ║
╚══════════════════════════════════════╝
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        // Declare and initialize variables
        string name = "Your Name";
        int age = 0;
        double height = 0.0;
        bool isStudent = true;
        string city = "";
        string country = "";
        string hobby = "";

        // Calculate birth year
        int currentYear = DateTime.Now.Year;
        int birthYear = 0; // Calculate this

        // Display the info card
        Console.WriteLine("╔══════════════════════════════════════╗");
        // Continue building the card...
    }
}
```

## Hints

- Birth year = current year - age
- Use `isStudent ? "Yes" : "No"` for boolean display
- Box drawing characters: ╔ ╗ ╚ ╝ ║ ═ ╠ ╣
