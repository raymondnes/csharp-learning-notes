# Project 19: Basic Menu System

## Difficulty: Beginner

## Concepts: Do-while loops, switch, program structure

## Requirements

Create a menu-driven application framework.

### Tasks:
1. Display menu with numbered options
2. Get user choice
3. Execute selected option
4. Loop until user exits
5. Handle invalid input

## Expected Output

```
═══════════════════════════════════════
        MENU SYSTEM DEMO
═══════════════════════════════════════

        MAIN MENU
───────────────────────────────────────
  1. Say Hello
  2. Show Date & Time
  3. Generate Random Number
  4. Calculator
  5. About
  0. Exit
───────────────────────────────────────

Select option: 1

Hello! Welcome to the menu system.

Press Enter to continue...

        MAIN MENU
───────────────────────────────────────
Select option: 2

Current Date: January 15, 2024
Current Time: 10:30:45 AM

Press Enter to continue...

        MAIN MENU
───────────────────────────────────────
Select option: 4

      CALCULATOR
  ─────────────────
  1. Add
  2. Subtract
  3. Multiply
  4. Divide
  0. Back to Main

Select: 1

Enter first number: 10
Enter second number: 5
Result: 10 + 5 = 15

Press Enter to continue...

Select option: 0

Thank you for using the Menu System!
Goodbye!
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
        Console.WriteLine("        MENU SYSTEM DEMO");
        Console.WriteLine("═══════════════════════════════════════\n");

        bool running = true;

        do
        {
            DisplayMainMenu();
            int choice = GetChoice();

            switch (choice)
            {
                case 1:
                    SayHello();
                    break;
                case 2:
                    ShowDateTime();
                    break;
                case 3:
                    GenerateRandom();
                    break;
                case 4:
                    CalculatorMenu();
                    break;
                case 5:
                    ShowAbout();
                    break;
                case 0:
                    running = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid option! Try again.");
                    break;
            }

            if (running && choice != 0)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

        } while (running);

        Console.WriteLine("\nThank you for using the Menu System!");
        Console.WriteLine("Goodbye!");
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("        MAIN MENU");
        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine("  1. Say Hello");
        Console.WriteLine("  2. Show Date & Time");
        Console.WriteLine("  3. Generate Random Number");
        Console.WriteLine("  4. Calculator");
        Console.WriteLine("  5. About");
        Console.WriteLine("  0. Exit");
        Console.WriteLine("───────────────────────────────────────\n");
    }

    static int GetChoice()
    {
        Console.Write("Select option: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
            return choice;
        return -1;
    }

    static void SayHello()
    {
        Console.WriteLine("\nHello! Welcome to the menu system.");
    }

    static void ShowDateTime()
    {
        Console.WriteLine($"\nCurrent Date: {DateTime.Now:MMMM dd, yyyy}");
        Console.WriteLine($"Current Time: {DateTime.Now:hh:mm:ss tt}");
    }

    static void GenerateRandom()
    {
        Random random = new Random();
        Console.WriteLine($"\nRandom Number: {random.Next(1, 101)}");
    }

    static void CalculatorMenu()
    {
        // Implement sub-menu for calculator
    }

    static void ShowAbout()
    {
        Console.WriteLine("\nMenu System Demo v1.0");
        Console.WriteLine("A simple menu-driven application.");
    }
}
```
