# Project 04: Temperature Converter

## Difficulty: Beginner

## Concepts: Type conversion, arithmetic, formulas

## Requirements

Create a temperature converter that converts between Celsius, Fahrenheit, and Kelvin.

### Tasks:
1. Display a menu with conversion options
2. Get temperature value from user
3. Convert to all other units
4. Display results with 2 decimal places

### Formulas:
- C to F: (C × 9/5) + 32
- F to C: (F - 32) × 5/9
- C to K: C + 273.15

## Expected Output

```
══════════════════════════════════════
     TEMPERATURE CONVERTER
══════════════════════════════════════

Enter temperature in Celsius: 25

Conversion Results:
──────────────────────────────────────
25.00 °C equals:
  → 77.00 °F (Fahrenheit)
  → 298.15 K (Kelvin)
──────────────────────────────────────
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("══════════════════════════════════════");
        Console.WriteLine("     TEMPERATURE CONVERTER");
        Console.WriteLine("══════════════════════════════════════\n");

        Console.Write("Enter temperature in Celsius: ");
        double celsius = double.Parse(Console.ReadLine());

        // Convert to Fahrenheit
        double fahrenheit = 0; // Apply formula

        // Convert to Kelvin
        double kelvin = 0; // Apply formula

        // Display results (use :F2 for 2 decimal places)
    }
}
```

## Bonus Challenge

Add reverse conversions (F to C, K to C) with a menu system.
