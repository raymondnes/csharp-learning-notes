# Project 21: Student Class

## Difficulty: Intermediate

## Concepts: Classes, properties, constructors, methods

## Requirements

Create a Student class with proper OOP principles.

### Tasks:
1. Create Student class with properties
2. Add constructor(s)
3. Add methods for GPA calculation
4. Create multiple student instances
5. Display student information

## Expected Output

```
═══════════════════════════════════════
         STUDENT MANAGEMENT
═══════════════════════════════════════

Creating students...

Student 1:
───────────────────────────────────────
ID:       S001
Name:     Alice Johnson
Age:      20
Major:    Computer Science
Grades:   [95, 88, 92, 87, 90]
GPA:      3.70
Status:   Dean's List ⭐
───────────────────────────────────────

Student 2:
───────────────────────────────────────
ID:       S002
Name:     Bob Smith
Age:      22
Major:    Mathematics
Grades:   [75, 82, 78, 80, 85]
GPA:      3.00
Status:   Good Standing
───────────────────────────────────────

Adding grade to Alice: 100
New GPA: 3.77

Class Statistics:
  Total Students: 2
  Average GPA: 3.38
  Highest GPA: Alice (3.77)
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;
using System.Collections.Generic;

class Student
{
    // Properties
    public string Id { get; private set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    private List<int> _grades;

    // Static counter for auto ID
    private static int _nextId = 1;

    // Constructor
    public Student(string name, int age, string major)
    {
        Id = $"S{_nextId++:D3}";
        Name = name;
        Age = age;
        Major = major;
        _grades = new List<int>();
    }

    // Methods
    public void AddGrade(int grade)
    {
        if (grade >= 0 && grade <= 100)
        {
            _grades.Add(grade);
        }
    }

    public double CalculateGPA()
    {
        if (_grades.Count == 0) return 0;

        double average = 0;
        foreach (int grade in _grades)
        {
            average += grade;
        }
        average /= _grades.Count;

        // Convert to 4.0 scale
        return Math.Round(average / 25, 2);
    }

    public string GetStatus()
    {
        double gpa = CalculateGPA();
        if (gpa >= 3.5) return "Dean's List ⭐";
        if (gpa >= 2.0) return "Good Standing";
        return "Academic Probation";
    }

    public string GetGrades()
    {
        return "[" + string.Join(", ", _grades) + "]";
    }

    public void DisplayInfo()
    {
        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine($"ID:       {Id}");
        Console.WriteLine($"Name:     {Name}");
        Console.WriteLine($"Age:      {Age}");
        Console.WriteLine($"Major:    {Major}");
        Console.WriteLine($"Grades:   {GetGrades()}");
        Console.WriteLine($"GPA:      {CalculateGPA():F2}");
        Console.WriteLine($"Status:   {GetStatus()}");
        Console.WriteLine("───────────────────────────────────────");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("         STUDENT MANAGEMENT");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("Creating students...\n");

        // Create students
        Student alice = new Student("Alice Johnson", 20, "Computer Science");
        alice.AddGrade(95);
        alice.AddGrade(88);
        alice.AddGrade(92);
        alice.AddGrade(87);
        alice.AddGrade(90);

        Student bob = new Student("Bob Smith", 22, "Mathematics");
        bob.AddGrade(75);
        bob.AddGrade(82);
        bob.AddGrade(78);
        bob.AddGrade(80);
        bob.AddGrade(85);

        // Display info
        Console.WriteLine("Student 1:");
        alice.DisplayInfo();

        Console.WriteLine("\nStudent 2:");
        bob.DisplayInfo();

        // Add more functionality...
    }
}
```
