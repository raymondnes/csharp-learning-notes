# Projects 27-40: Intermediate Projects

## Project 27: Dice Roller

**Concepts:** Random, classes, statistics

Create a dice rolling simulator with:
- Single die roll
- Multiple dice (2d6, 3d8, etc.)
- Roll statistics (average, distribution)
- Custom sided dice

```csharp
// Starter concept
class Die
{
    public int Sides { get; }
    private Random _random;

    public Die(int sides = 6) { Sides = sides; _random = new Random(); }
    public int Roll() => _random.Next(1, Sides + 1);
}
```

---

## Project 28: Library Book System

**Concepts:** Multiple classes, relationships

Create a library system with:
- Book class (title, author, ISBN, available)
- Library class with book collection
- Checkout/return functionality
- Search by title/author
- Due date tracking

---

## Project 29: Employee Payroll

**Concepts:** Class methods, calculations

Create payroll system with:
- Employee class (name, hourly rate, hours worked)
- Calculate gross pay, deductions, net pay
- Overtime calculation (1.5x over 40 hours)
- Generate pay stubs

---

## Project 30: Inventory System

**Concepts:** Lists, searching, CRUD

Create inventory manager:
- Product class (SKU, name, quantity, price)
- Add/remove stock
- Low stock alerts
- Search and filter
- Calculate inventory value

---

## Project 31: Grade Book

**Concepts:** Arrays, averaging, statistics

Create grade tracking system:
- Store student names and grades
- Calculate averages
- Find highest/lowest
- Letter grade conversion
- Class statistics

---

## Project 32: Password Validator

**Concepts:** String methods, validation rules

Create password checker:
- Minimum length check
- Uppercase/lowercase required
- Number required
- Special character required
- Strength meter (weak/medium/strong)

```csharp
static bool ValidatePassword(string password)
{
    bool hasLength = password.Length >= 8;
    bool hasUpper = password.Any(char.IsUpper);
    bool hasLower = password.Any(char.IsLower);
    bool hasDigit = password.Any(char.IsDigit);
    bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));

    return hasLength && hasUpper && hasLower && hasDigit && hasSpecial;
}
```

---

## Project 33: Number Statistics

**Concepts:** Array methods, math operations

Create statistics calculator:
- Input series of numbers
- Calculate: mean, median, mode
- Find range, variance, standard deviation
- Display histogram

---

## Project 34: Hangman Game

**Concepts:** Arrays, strings, game loop

Create hangman game:
- Word bank array
- Hidden word display (****)
- Letter guessing
- Wrong guess tracking
- ASCII art hangman

---

## Project 35: Tic-Tac-Toe

**Concepts:** 2D arrays, game logic

Create tic-tac-toe game:
- 3x3 board using 2D array
- Two player turns
- Win condition checking
- Draw detection
- Play again option

```csharp
char[,] board = new char[3, 3];

void DisplayBoard()
{
    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine($" {board[i,0]} | {board[i,1]} | {board[i,2]} ");
        if (i < 2) Console.WriteLine("---+---+---");
    }
}

bool CheckWin(char player)
{
    // Check rows, columns, diagonals
    for (int i = 0; i < 3; i++)
    {
        if (board[i,0] == player && board[i,1] == player && board[i,2] == player) return true;
        if (board[0,i] == player && board[1,i] == player && board[2,i] == player) return true;
    }
    if (board[0,0] == player && board[1,1] == player && board[2,2] == player) return true;
    if (board[0,2] == player && board[1,1] == player && board[2,0] == player) return true;
    return false;
}
```

---

## Project 36: File-Based Notes

**Concepts:** Try-catch, file I/O

Create note-taking app:
- Save notes to text file
- Load existing notes
- Handle file not found
- Append new notes
- Error handling

```csharp
using System.IO;

void SaveNote(string filename, string content)
{
    try
    {
        File.AppendAllText(filename, content + Environment.NewLine);
        Console.WriteLine("Note saved!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
```

---

## Project 37: Calculator with History

**Concepts:** Error handling, lists

Create calculator with:
- Basic operations
- Try-catch for invalid input
- Division by zero handling
- Calculation history
- Recall previous results

---

## Project 38: JSON Config Reader

**Concepts:** Try-catch, JSON parsing

Create config file reader:
- Read JSON configuration
- Parse settings
- Handle missing file
- Handle invalid JSON
- Default values fallback

```csharp
using System.Text.Json;

class Config
{
    public string AppName { get; set; }
    public int MaxUsers { get; set; }
    public bool DebugMode { get; set; }
}

Config LoadConfig(string path)
{
    try
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Config>(json);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Config not found, using defaults");
        return new Config { AppName = "Default", MaxUsers = 10, DebugMode = false };
    }
    catch (JsonException)
    {
        Console.WriteLine("Invalid config format");
        return null;
    }
}
```

---

## Project 39: Student Records

**Concepts:** Properties, validation

Create student record system with:
- Validated properties (age > 0, GPA 0-4)
- Auto-calculated properties
- Read-only properties
- Property change notification

```csharp
class Student
{
    private int _age;
    public int Age
    {
        get => _age;
        set
        {
            if (value < 0 || value > 120)
                throw new ArgumentException("Invalid age");
            _age = value;
        }
    }

    public string FullName => $"{FirstName} {LastName}";
    public bool IsHonorStudent => GPA >= 3.5;
}
```

---

## Project 40: Mini Game Engine

**Concepts:** Multiple classes, composition

Create simple game framework:
- GameObject class (position, name)
- Player class (health, score)
- Enemy class (damage)
- Game class (manages objects)
- Simple turn-based combat

```csharp
abstract class GameObject
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Name { get; set; }

    public abstract void Update();
}

class Player : GameObject
{
    public int Health { get; set; } = 100;
    public int Score { get; set; }

    public override void Update()
    {
        // Handle input
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) Console.WriteLine("Game Over!");
    }
}

class Enemy : GameObject
{
    public int Damage { get; set; } = 10;

    public override void Update()
    {
        // AI logic
    }

    public void Attack(Player player)
    {
        player.TakeDamage(Damage);
    }
}

class Game
{
    public Player Player { get; }
    public List<Enemy> Enemies { get; }

    public void Run()
    {
        while (Player.Health > 0)
        {
            Player.Update();
            foreach (var enemy in Enemies)
            {
                enemy.Update();
            }
        }
    }
}
```

---

Each project builds on previous concepts while introducing new challenges. Complete them in order to solidify your intermediate C# skills.
