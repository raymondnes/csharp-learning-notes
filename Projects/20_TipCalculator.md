# Project 20: Tip Calculator

## Difficulty: Beginner

## Concepts: Methods with parameters, return values, formatting

## Requirements

Create a restaurant tip calculator with bill splitting.

### Tasks:
1. Get bill amount
2. Select tip percentage
3. Calculate tip and total
4. Split bill among people
5. Show itemized breakdown

## Expected Output

```
═══════════════════════════════════════
          TIP CALCULATOR
═══════════════════════════════════════

Enter bill amount: $85.50

Select tip percentage:
  1. 10% (Okay service)
  2. 15% (Good service)
  3. 20% (Great service)
  4. 25% (Excellent service)
  5. Custom

Your choice: 3

Split bill? (y/n): y
How many people? 4

═══════════════════════════════════════
          BILL BREAKDOWN
═══════════════════════════════════════

Bill Amount:      $85.50
Tip (20%):        $17.10
───────────────────────────────────────
Total:            $102.60

Split 4 ways:     $25.65 each
───────────────────────────────────────

Tip per person:   $4.28
Bill per person:  $21.38
═══════════════════════════════════════

Calculate another? (y/n): n

Thank you! Have a great day!
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        bool calculating = true;

        while (calculating)
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("          TIP CALCULATOR");
            Console.WriteLine("═══════════════════════════════════════\n");

            // Get bill amount
            decimal billAmount = GetBillAmount();

            // Get tip percentage
            decimal tipPercentage = GetTipPercentage();

            // Calculate tip
            decimal tipAmount = CalculateTip(billAmount, tipPercentage);
            decimal total = billAmount + tipAmount;

            // Check for split
            Console.Write("\nSplit bill? (y/n): ");
            bool split = Console.ReadLine().ToLower() == "y";

            int people = 1;
            if (split)
            {
                Console.Write("How many people? ");
                people = int.Parse(Console.ReadLine());
            }

            // Display results
            DisplayResults(billAmount, tipAmount, total, tipPercentage, people);

            Console.Write("\nCalculate another? (y/n): ");
            calculating = Console.ReadLine().ToLower() == "y";
        }

        Console.WriteLine("\nThank you! Have a great day!");
    }

    static decimal GetBillAmount()
    {
        Console.Write("Enter bill amount: $");
        return decimal.Parse(Console.ReadLine());
    }

    static decimal GetTipPercentage()
    {
        Console.WriteLine("\nSelect tip percentage:");
        Console.WriteLine("  1. 10% (Okay service)");
        Console.WriteLine("  2. 15% (Good service)");
        Console.WriteLine("  3. 20% (Great service)");
        Console.WriteLine("  4. 25% (Excellent service)");
        Console.WriteLine("  5. Custom\n");

        Console.Write("Your choice: ");
        int choice = int.Parse(Console.ReadLine());

        return choice switch
        {
            1 => 0.10m,
            2 => 0.15m,
            3 => 0.20m,
            4 => 0.25m,
            5 => GetCustomTip(),
            _ => 0.15m
        };
    }

    static decimal GetCustomTip()
    {
        Console.Write("Enter custom tip %: ");
        return decimal.Parse(Console.ReadLine()) / 100;
    }

    static decimal CalculateTip(decimal bill, decimal percentage)
    {
        return bill * percentage;
    }

    static void DisplayResults(decimal bill, decimal tip, decimal total,
        decimal percentage, int people)
    {
        Console.WriteLine("\n═══════════════════════════════════════");
        Console.WriteLine("          BILL BREAKDOWN");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine($"Bill Amount:      {bill:C}");
        Console.WriteLine($"Tip ({percentage:P0}):        {tip:C}");
        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine($"Total:            {total:C}");

        if (people > 1)
        {
            decimal perPerson = total / people;
            decimal tipPerPerson = tip / people;
            decimal billPerPerson = bill / people;

            Console.WriteLine($"\nSplit {people} ways:     {perPerson:C} each");
            Console.WriteLine("───────────────────────────────────────");
            Console.WriteLine($"Tip per person:   {tipPerPerson:C}");
            Console.WriteLine($"Bill per person:  {billPerPerson:C}");
        }

        Console.WriteLine("═══════════════════════════════════════");
    }
}
```
