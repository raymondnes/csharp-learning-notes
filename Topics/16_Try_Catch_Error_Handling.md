# Error Handling with Try-Catch

## Introduction

Programs encounter errors. Files go missing, users enter invalid data, network connections fail. Exception handling allows your program to gracefully handle these situations instead of crashing. The `try-catch` block is the primary mechanism for handling exceptions in C#.

## What is an Exception?

An exception is an object that represents an error or unexpected condition that occurs during program execution. When an exception occurs, the normal flow of execution stops, and C# looks for code to handle the exception.

Common exceptions include:
- `NullReferenceException`: Accessing a null object
- `IndexOutOfRangeException`: Invalid array/list index
- `DivideByZeroException`: Dividing by zero
- `FormatException`: Invalid string format for parsing
- `FileNotFoundException`: File doesn't exist

## Basic Try-Catch Structure

```csharp
try
{
    // Code that might throw an exception
    int result = 10 / 0;  // This will throw DivideByZeroException
}
catch (Exception ex)
{
    // Code to handle the exception
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Multiple Catch Blocks

Handle different exception types differently:

```csharp
try
{
    Console.Write("Enter a number: ");
    int number = int.Parse(Console.ReadLine());
    int result = 100 / number;
    Console.WriteLine($"Result: {result}");
}
catch (FormatException)
{
    Console.WriteLine("That's not a valid number!");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Cannot divide by zero!");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

**Order matters:** Catch more specific exceptions first, then general ones.

## The Finally Block

The `finally` block always executes, whether an exception occurred or not:

```csharp
StreamReader reader = null;
try
{
    reader = new StreamReader("file.txt");
    string content = reader.ReadToEnd();
    Console.WriteLine(content);
}
catch (FileNotFoundException)
{
    Console.WriteLine("File not found!");
}
finally
{
    // Always executes - cleanup code
    reader?.Close();
    Console.WriteLine("Cleanup complete.");
}
```

**Common uses for finally:**
- Closing files/connections
- Releasing resources
- Logging
- Any cleanup that must happen

## Exception Properties

Exceptions contain useful information:

```csharp
try
{
    int[] arr = { 1, 2, 3 };
    Console.WriteLine(arr[10]);
}
catch (Exception ex)
{
    Console.WriteLine($"Message: {ex.Message}");
    Console.WriteLine($"Type: {ex.GetType().Name}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    Console.WriteLine($"Source: {ex.Source}");

    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner: {ex.InnerException.Message}");
    }
}
```

## Throwing Exceptions

You can throw exceptions explicitly:

```csharp
void SetAge(int age)
{
    if (age < 0)
    {
        throw new ArgumentException("Age cannot be negative");
    }
    if (age > 150)
    {
        throw new ArgumentOutOfRangeException(nameof(age), "Age cannot exceed 150");
    }

    Console.WriteLine($"Age set to {age}");
}

try
{
    SetAge(-5);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid argument: {ex.Message}");
}
```

### Re-throwing Exceptions

```csharp
try
{
    DoSomethingDangerous();
}
catch (Exception ex)
{
    // Log the error
    Console.WriteLine($"Logging error: {ex.Message}");

    // Re-throw to let caller handle it
    throw;  // Preserves stack trace

    // OR throw new exception with original as inner
    // throw new ApplicationException("Operation failed", ex);
}
```

**Important:** Use `throw;` not `throw ex;` to preserve the original stack trace.

## Common Exception Types

| Exception | Cause |
|-----------|-------|
| `ArgumentException` | Invalid argument value |
| `ArgumentNullException` | Null passed when not allowed |
| `ArgumentOutOfRangeException` | Argument outside valid range |
| `InvalidOperationException` | Operation invalid for current state |
| `NullReferenceException` | Accessing null reference |
| `IndexOutOfRangeException` | Invalid index |
| `FormatException` | Invalid format (parsing) |
| `IOException` | I/O operation failed |
| `FileNotFoundException` | File doesn't exist |
| `DivideByZeroException` | Integer division by zero |

## Exception Filters (C# 6+)

Add conditions to catch blocks:

```csharp
try
{
    DoHttpRequest();
}
catch (HttpException ex) when (ex.StatusCode == 404)
{
    Console.WriteLine("Page not found");
}
catch (HttpException ex) when (ex.StatusCode == 500)
{
    Console.WriteLine("Server error");
}
catch (HttpException ex)
{
    Console.WriteLine($"HTTP error: {ex.StatusCode}");
}
```

## Using Statement (Resource Management)

The `using` statement automatically disposes resources:

```csharp
// Old way with try-finally
StreamReader reader = null;
try
{
    reader = new StreamReader("file.txt");
    Console.WriteLine(reader.ReadToEnd());
}
finally
{
    reader?.Dispose();
}

// Modern way with using statement
using (StreamReader reader = new StreamReader("file.txt"))
{
    Console.WriteLine(reader.ReadToEnd());
}  // Automatically disposed here

// Even simpler (C# 8+)
using StreamReader reader = new StreamReader("file.txt");
Console.WriteLine(reader.ReadToEnd());
// Disposed at end of scope
```

## Best Practices

### 1. Be Specific

```csharp
// Bad - catches everything
catch (Exception) { }

// Good - catch specific exceptions
catch (FileNotFoundException) { }
catch (IOException) { }
```

### 2. Don't Swallow Exceptions

```csharp
// Bad - silent failure
catch (Exception) { }

// Good - at least log it
catch (Exception ex)
{
    Logger.Log(ex);
    throw;  // Re-throw if can't handle
}
```

### 3. Include Context

```csharp
// Bad
throw new Exception("Error");

// Good
throw new ArgumentException(
    $"Invalid user ID: {userId}. Must be positive.",
    nameof(userId));
```

### 4. Use Validation Instead of Exceptions for Expected Cases

```csharp
// Bad - using exceptions for flow control
try
{
    int num = int.Parse(userInput);
}
catch (FormatException)
{
    Console.WriteLine("Invalid number");
}

// Good - validate first
if (int.TryParse(userInput, out int num))
{
    Console.WriteLine($"Number: {num}");
}
else
{
    Console.WriteLine("Invalid number");
}
```

## Practical Examples

### Example 1: Safe Division

```csharp
double SafeDivide(double a, double b)
{
    if (b == 0)
    {
        throw new DivideByZeroException("Cannot divide by zero");
    }
    return a / b;
}

try
{
    double result = SafeDivide(10, 0);
    Console.WriteLine(result);
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

### Example 2: File Reading

```csharp
string ReadFileContent(string path)
{
    try
    {
        return File.ReadAllText(path);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"File not found: {path}");
        return string.Empty;
    }
    catch (UnauthorizedAccessException)
    {
        Console.WriteLine($"Access denied: {path}");
        return string.Empty;
    }
    catch (IOException ex)
    {
        Console.WriteLine($"IO Error: {ex.Message}");
        return string.Empty;
    }
}
```

### Example 3: User Input Validation

```csharp
int GetPositiveInteger(string prompt)
{
    while (true)
    {
        try
        {
            Console.Write(prompt);
            int value = int.Parse(Console.ReadLine());

            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "value", "Must be positive");
            }

            return value;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid number.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please enter a positive number.");
        }
    }
}
```

### Example 4: Custom Exception

```csharp
// Define custom exception
public class InsufficientFundsException : Exception
{
    public decimal Balance { get; }
    public decimal AttemptedAmount { get; }

    public InsufficientFundsException(decimal balance, decimal amount)
        : base($"Insufficient funds. Balance: {balance:C}, Attempted: {amount:C}")
    {
        Balance = balance;
        AttemptedAmount = amount;
    }
}

// Use it
class BankAccount
{
    public decimal Balance { get; private set; } = 100;

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientFundsException(Balance, amount);
        }
        Balance -= amount;
    }
}

try
{
    var account = new BankAccount();
    account.Withdraw(500);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine($"You can withdraw up to {ex.Balance:C}");
}
```

## Common Mistakes

### 1. Empty Catch Blocks

```csharp
// Terrible - hides errors
catch (Exception) { }

// At minimum, log something
catch (Exception ex)
{
    Debug.WriteLine(ex.Message);
}
```

### 2. Catching Too Broadly

```csharp
// Bad - catches OutOfMemoryException, StackOverflowException, etc.
catch (Exception) { }

// Better - catch what you can handle
catch (IOException) { }
catch (FormatException) { }
```

### 3. Losing Stack Trace

```csharp
// Bad - loses original stack trace
catch (Exception ex)
{
    throw ex;  // Don't do this!
}

// Good - preserves stack trace
catch (Exception)
{
    throw;  // Use throw; without the variable
}
```

## Summary

- `try-catch` handles exceptions gracefully
- Put risky code in `try`, handle errors in `catch`
- Use multiple `catch` blocks for different exception types
- `finally` always executes for cleanup
- Be specific with exception types
- Don't swallow exceptions silently
- Use `throw;` to re-throw while preserving stack trace
- Prefer validation over exception handling for expected cases
- Use `using` for automatic resource disposal

Exception handling makes your programs robust and user-friendly by gracefully handling errors instead of crashing.
