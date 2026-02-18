# Project 23: Rectangle Calculator

## Difficulty: Intermediate

## Concepts: Constructors, this keyword, method overloading

## Requirements

Create a Rectangle class with multiple constructors and methods.

### Tasks:
1. Multiple constructors (default, with dimensions, square)
2. Calculate area and perimeter
3. Scale rectangle
4. Compare rectangles
5. Method overloading for different inputs

## Expected Output

```
═══════════════════════════════════════
       RECTANGLE CALCULATOR
═══════════════════════════════════════

Creating rectangles...

Rectangle 1 (default):
  Width: 1, Height: 1
  Area: 1, Perimeter: 4

Rectangle 2 (5 x 3):
  Width: 5, Height: 3
  Area: 15, Perimeter: 16

Rectangle 3 (square, side 4):
  Width: 4, Height: 4
  Area: 16, Perimeter: 16
  Is Square: Yes

───────────────────────────────────────
Operations:
───────────────────────────────────────

Scaling Rectangle 2 by factor 2...
  New dimensions: 10 x 6
  New Area: 60

Comparing rectangles by area:
  Rect1 (1) < Rect2 (60) < Rect3 (16)
  Largest: Rectangle 2

Creating rectangle from points:
  Point1: (0, 0), Point2: (8, 5)
  Dimensions: 8 x 5
  Area: 40
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;

class Rectangle
{
    public double Width { get; private set; }
    public double Height { get; private set; }

    // Default constructor
    public Rectangle()
    {
        Width = 1;
        Height = 1;
    }

    // Constructor with dimensions
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    // Square constructor
    public Rectangle(double side) : this(side, side)
    {
    }

    // Constructor from two points
    public Rectangle(double x1, double y1, double x2, double y2)
    {
        Width = Math.Abs(x2 - x1);
        Height = Math.Abs(y2 - y1);
    }

    // Properties
    public double Area => Width * Height;
    public double Perimeter => 2 * (Width + Height);
    public bool IsSquare => Width == Height;

    // Methods
    public void Scale(double factor)
    {
        Width *= factor;
        Height *= factor;
    }

    public void Scale(double widthFactor, double heightFactor)
    {
        Width *= widthFactor;
        Height *= heightFactor;
    }

    public int CompareTo(Rectangle other)
    {
        return Area.CompareTo(other.Area);
    }

    public void Display(string name = "Rectangle")
    {
        Console.WriteLine($"{name}:");
        Console.WriteLine($"  Width: {Width}, Height: {Height}");
        Console.WriteLine($"  Area: {Area}, Perimeter: {Perimeter}");
        if (IsSquare) Console.WriteLine("  Is Square: Yes");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("       RECTANGLE CALCULATOR");
        Console.WriteLine("═══════════════════════════════════════\n");

        // Create rectangles with different constructors
        Rectangle rect1 = new Rectangle();
        Rectangle rect2 = new Rectangle(5, 3);
        Rectangle rect3 = new Rectangle(4); // Square

        Console.WriteLine("Creating rectangles...\n");

        rect1.Display("Rectangle 1 (default)");
        Console.WriteLine();
        rect2.Display("Rectangle 2 (5 x 3)");
        Console.WriteLine();
        rect3.Display("Rectangle 3 (square, side 4)");

        // Perform operations and comparisons
    }
}
```
