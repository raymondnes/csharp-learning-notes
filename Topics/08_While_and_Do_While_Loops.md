# While and Do-While Loops

## Introduction

While `for` loops are ideal when you know how many iterations you need, `while` and `do-while` loops excel when you want to repeat code based on a condition. These loops continue executing as long as a condition remains true, making them perfect for scenarios where the number of iterations isn't known in advance.

## The While Loop

The `while` loop checks its condition **before** each iteration.

### Syntax

```csharp
while (condition)
{
    // Code to repeat
    // Must eventually make condition false to avoid infinite loop
}
```

### Basic Example

```csharp
int count = 1;

while (count <= 5)
{
    Console.WriteLine(count);
    count++;
}
// Output: 1, 2, 3, 4, 5
```

### Execution Flow

```
1. Check condition
2. If true: Execute body, go to step 1
3. If false: Exit loop
```

**Important:** If the condition is initially false, the loop body never executes.

```csharp
int x = 10;

while (x < 5)  // false from the start
{
    Console.WriteLine("This never prints");
}
Console.WriteLine("Loop skipped entirely");
```

## The Do-While Loop

The `do-while` loop checks its condition **after** each iteration, guaranteeing at least one execution.

### Syntax

```csharp
do
{
    // Code to repeat (executes at least once)
} while (condition);
```

**Note:** The semicolon after `while (condition)` is required.

### Basic Example

```csharp
int count = 1;

do
{
    Console.WriteLine(count);
    count++;
} while (count <= 5);
// Output: 1, 2, 3, 4, 5
```

### Guaranteed First Execution

```csharp
int x = 10;

do
{
    Console.WriteLine("This prints once!");  // Executes before check
} while (x < 5);  // false, so loop ends

Console.WriteLine("Loop completed");
```

## While vs. Do-While Comparison

| Feature | While | Do-While |
|---------|-------|----------|
| Condition check | Before iteration | After iteration |
| Minimum executions | 0 | 1 |
| Use case | May not need to run | Must run at least once |

### Example Comparison

```csharp
int value = 0;

// While: might not execute
Console.WriteLine("While loop:");
while (value > 0)
{
    Console.WriteLine(value);
    value--;
}
Console.WriteLine("(nothing printed)");

// Do-while: always executes once
Console.WriteLine("\nDo-while loop:");
do
{
    Console.WriteLine(value);
    value--;
} while (value > 0);
Console.WriteLine("(printed 0 once)");
```

## Input Validation

A common use case for do-while is validating user input:

```csharp
int number;
bool validInput;

do
{
    Console.Write("Enter a positive number: ");
    validInput = int.TryParse(Console.ReadLine(), out number) && number > 0;

    if (!validInput)
    {
        Console.WriteLine("Invalid input. Try again.");
    }
} while (!validInput);

Console.WriteLine($"You entered: {number}");
```

## Menu Systems

Do-while is perfect for menu-driven programs:

```csharp
string choice;

do
{
    Console.WriteLine("\n=== MAIN MENU ===");
    Console.WriteLine("1. New Game");
    Console.WriteLine("2. Load Game");
    Console.WriteLine("3. Settings");
    Console.WriteLine("4. Exit");
    Console.Write("Enter choice: ");

    choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Starting new game...");
            break;
        case "2":
            Console.WriteLine("Loading game...");
            break;
        case "3":
            Console.WriteLine("Opening settings...");
            break;
        case "4":
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid choice!");
            break;
    }
} while (choice != "4");
```

## Infinite Loops

Sometimes you want a loop that runs forever (until explicitly broken):

```csharp
// Common infinite loop pattern
while (true)
{
    Console.Write("Enter command (quit to exit): ");
    string command = Console.ReadLine();

    if (command?.ToLower() == "quit")
    {
        break;
    }

    Console.WriteLine($"Processing: {command}");
}
```

## Sentinel Values

Use a sentinel (special value) to control loop termination:

```csharp
int sum = 0;
int number;

Console.WriteLine("Enter numbers to sum (enter -1 to stop):");

while (true)
{
    Console.Write("Number: ");
    if (int.TryParse(Console.ReadLine(), out number))
    {
        if (number == -1)  // Sentinel value
        {
            break;
        }
        sum += number;
    }
}

Console.WriteLine($"Total sum: {sum}");
```

## Counter-Controlled While Loops

While loops can work like for loops:

```csharp
// As a for loop
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}

// Equivalent while loop
int i = 0;
while (i < 5)
{
    Console.WriteLine(i);
    i++;
}
```

## Break and Continue

Both `break` and `continue` work in while loops:

### Break

```csharp
int i = 0;
while (i < 100)
{
    if (i == 5)
    {
        break;  // Exit loop when i reaches 5
    }
    Console.WriteLine(i);
    i++;
}
// Output: 0, 1, 2, 3, 4
```

### Continue

```csharp
int i = 0;
while (i < 10)
{
    i++;
    if (i % 2 == 0)
    {
        continue;  // Skip even numbers
    }
    Console.WriteLine(i);
}
// Output: 1, 3, 5, 7, 9
```

## Nested While Loops

```csharp
int outer = 1;

while (outer <= 3)
{
    int inner = 1;
    while (inner <= 3)
    {
        Console.WriteLine($"Outer: {outer}, Inner: {inner}");
        inner++;
    }
    outer++;
}
```

## Practical Examples

### Example 1: Number Guessing Game

```csharp
Random random = new Random();
int secretNumber = random.Next(1, 101);
int guess;
int attempts = 0;

Console.WriteLine("I'm thinking of a number between 1 and 100.");

do
{
    Console.Write("Your guess: ");
    if (int.TryParse(Console.ReadLine(), out guess))
    {
        attempts++;

        if (guess < secretNumber)
        {
            Console.WriteLine("Too low!");
        }
        else if (guess > secretNumber)
        {
            Console.WriteLine("Too high!");
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid number.");
    }
} while (guess != secretNumber);

Console.WriteLine($"Correct! You got it in {attempts} attempts!");
```

### Example 2: Reading Until Empty Line

```csharp
Console.WriteLine("Enter lines of text (empty line to stop):");

List<string> lines = new List<string>();
string line;

while ((line = Console.ReadLine()) != "")
{
    lines.Add(line);
}

Console.WriteLine($"\nYou entered {lines.Count} lines.");
```

### Example 3: Fibonacci Until Limit

```csharp
int limit = 1000;
int a = 0, b = 1;

Console.WriteLine($"Fibonacci numbers up to {limit}:");

while (a <= limit)
{
    Console.Write($"{a} ");
    int next = a + b;
    a = b;
    b = next;
}
// Output: 0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987
```

### Example 4: Password Attempts

```csharp
const string correctPassword = "secret123";
const int maxAttempts = 3;
int attemptsRemaining = maxAttempts;
bool authenticated = false;

while (attemptsRemaining > 0 && !authenticated)
{
    Console.Write($"Enter password ({attemptsRemaining} attempts left): ");
    string password = Console.ReadLine();

    if (password == correctPassword)
    {
        authenticated = true;
        Console.WriteLine("Access granted!");
    }
    else
    {
        attemptsRemaining--;
        if (attemptsRemaining > 0)
        {
            Console.WriteLine("Incorrect password. Try again.");
        }
    }
}

if (!authenticated)
{
    Console.WriteLine("Account locked. Too many failed attempts.");
}
```

### Example 5: Reading File Lines (Simulated)

```csharp
// Simulated file reading pattern
string[] fileLines = { "Line 1", "Line 2", "Line 3" };
int index = 0;

while (index < fileLines.Length)
{
    string currentLine = fileLines[index];
    Console.WriteLine($"Processing: {currentLine}");
    index++;
}
```

### Example 6: Dice Rolling Until Target

```csharp
Random dice = new Random();
int target = 6;
int rolls = 0;
int result;

Console.WriteLine($"Rolling dice until we get a {target}...");

do
{
    result = dice.Next(1, 7);
    rolls++;
    Console.WriteLine($"Roll {rolls}: {result}");
} while (result != target);

Console.WriteLine($"Got {target} after {rolls} rolls!");
```

## Common Mistakes

### 1. Infinite Loops

```csharp
// Wrong: count never changes
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    // Forgot count++;
}

// Correct
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    count++;
}
```

### 2. Off-by-One Errors

```csharp
// Wrong: prints 1-4 instead of 1-5
int i = 1;
while (i < 5)  // Should be i <= 5
{
    Console.WriteLine(i);
    i++;
}
```

### 3. Wrong Loop Type

```csharp
// Using while when do-while is better
Console.Write("Enter name: ");
string name = Console.ReadLine();

while (string.IsNullOrEmpty(name))
{
    Console.WriteLine("Name cannot be empty.");
    Console.Write("Enter name: ");
    name = Console.ReadLine();  // Duplicated code
}

// Better with do-while
string name;
do
{
    Console.Write("Enter name: ");
    name = Console.ReadLine();
    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Name cannot be empty.");
    }
} while (string.IsNullOrEmpty(name));
```

### 4. Modifying Condition Variable Incorrectly

```csharp
// Wrong: decrements at wrong place
int count = 5;
while (count > 0)
{
    count--;
    Console.WriteLine(count);  // Prints 4,3,2,1,0 instead of 5,4,3,2,1
}

// Correct order
int count = 5;
while (count > 0)
{
    Console.WriteLine(count);
    count--;
}
```

## Choosing the Right Loop

| Scenario | Best Loop |
|----------|-----------|
| Known number of iterations | `for` |
| Condition may be false initially | `while` |
| Must execute at least once | `do-while` |
| Iterating collections | `foreach` |
| Complex termination logic | `while` with `break` |

## Summary

- `while` checks condition before each iteration (may execute 0 times)
- `do-while` checks condition after each iteration (always executes at least once)
- Use `while` for pre-checked conditions
- Use `do-while` for input validation and menus
- Always ensure the loop can terminate
- Use `break` for early exit, `continue` to skip iterations
- Avoid infinite loops by ensuring the condition eventually becomes false

Understanding when to use while vs. do-while leads to cleaner, more intentional code.
