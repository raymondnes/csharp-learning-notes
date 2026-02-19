# Topic 30: Unit Testing with xUnit

## Introduction

Unit testing is the practice of testing individual units of code (methods, classes) in isolation to verify they work correctly. Senior developers write tests as a standard part of development, not as an afterthought.

**Why Testing Matters:**
- Catches bugs early (cheaper to fix)
- Enables confident refactoring
- Documents expected behavior
- Reduces debugging time
- Required in professional environments

## What is xUnit?

xUnit is the most popular testing framework for .NET. It's used by Microsoft for testing .NET itself.

### Setting Up xUnit

```bash
# Create a test project
dotnet new xunit -n MyApp.Tests

# Add reference to the project being tested
dotnet add reference ../MyApp/MyApp.csproj

# Run tests
dotnet test
```

### Project Structure

```
Solution/
├── MyApp/                    # Main application
│   ├── Services/
│   │   └── Calculator.cs
│   └── MyApp.csproj
└── MyApp.Tests/              # Test project
    ├── Services/
    │   └── CalculatorTests.cs
    └── MyApp.Tests.csproj
```

## Writing Your First Test

### The Code to Test

```csharp
// MyApp/Services/Calculator.cs
namespace MyApp.Services;

public class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        return a / b;
    }

    public bool IsEven(int number) => number % 2 == 0;
}
```

### The Test Class

```csharp
// MyApp.Tests/Services/CalculatorTests.cs
using MyApp.Services;
using Xunit;

namespace MyApp.Tests.Services;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        // Runs before each test (fresh instance)
        _calculator = new Calculator();
    }

    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange (setup)
        int a = 5;
        int b = 3;

        // Act (execute)
        int result = _calculator.Add(a, b);

        // Assert (verify)
        Assert.Equal(8, result);
    }

    [Fact]
    public void Subtract_TwoNumbers_ReturnsDifference()
    {
        int result = _calculator.Subtract(10, 4);

        Assert.Equal(6, result);
    }
}
```

## The AAA Pattern

Every test follows **Arrange-Act-Assert**:

```csharp
[Fact]
public void MethodName_Scenario_ExpectedResult()
{
    // Arrange - Set up test data and objects
    var service = new MyService();
    var input = "test data";

    // Act - Execute the method being tested
    var result = service.DoSomething(input);

    // Assert - Verify the result
    Assert.Equal("expected", result);
}
```

## Test Naming Convention

Follow this pattern: `MethodName_Scenario_ExpectedBehavior`

```csharp
// Good names - clearly describe what's being tested
Add_TwoPositiveNumbers_ReturnsSum()
Add_NegativeNumbers_ReturnsCorrectSum()
Divide_ByZero_ThrowsException()
IsEven_WithEvenNumber_ReturnsTrue()
GetUser_WithInvalidId_ReturnsNull()

// Bad names - unclear what's being tested
Test1()
AddTest()
TestAdd()
```

## xUnit Attributes

### [Fact] - Single Test Case

```csharp
[Fact]
public void IsEven_WithEvenNumber_ReturnsTrue()
{
    var calc = new Calculator();

    bool result = calc.IsEven(4);

    Assert.True(result);
}
```

### [Theory] - Multiple Test Cases

```csharp
[Theory]
[InlineData(2, true)]
[InlineData(4, true)]
[InlineData(100, true)]
[InlineData(1, false)]
[InlineData(3, false)]
[InlineData(99, false)]
public void IsEven_WithVariousNumbers_ReturnsExpectedResult(int number, bool expected)
{
    var calc = new Calculator();

    bool result = calc.IsEven(number);

    Assert.Equal(expected, result);
}
```

### [Theory] with Complex Data

```csharp
[Theory]
[InlineData(10, 5, 2)]
[InlineData(20, 4, 5)]
[InlineData(100, 10, 10)]
[InlineData(-10, 2, -5)]
public void Divide_ValidNumbers_ReturnsQuotient(int a, int b, int expected)
{
    var calc = new Calculator();

    int result = calc.Divide(a, b);

    Assert.Equal(expected, result);
}
```

## Common Assertions

```csharp
// Equality
Assert.Equal(expected, actual);
Assert.NotEqual(unexpected, actual);

// Boolean
Assert.True(condition);
Assert.False(condition);

// Null checks
Assert.Null(obj);
Assert.NotNull(obj);

// Collections
Assert.Empty(collection);
Assert.NotEmpty(collection);
Assert.Contains(item, collection);
Assert.DoesNotContain(item, collection);
Assert.Single(collection);  // Exactly one item
Assert.Equal(3, collection.Count());

// Strings
Assert.Contains("substring", actualString);
Assert.StartsWith("prefix", actualString);
Assert.EndsWith("suffix", actualString);
Assert.Matches(@"\d+", actualString);  // Regex

// Type checks
Assert.IsType<ExpectedType>(obj);
Assert.IsAssignableFrom<BaseType>(obj);

// Ranges
Assert.InRange(actual, low, high);
```

## Testing Exceptions

```csharp
[Fact]
public void Divide_ByZero_ThrowsDivideByZeroException()
{
    var calc = new Calculator();

    // Assert that exception is thrown
    var exception = Assert.Throws<DivideByZeroException>(
        () => calc.Divide(10, 0)
    );

    // Optionally verify exception message
    Assert.Equal("Cannot divide by zero", exception.Message);
}

[Fact]
public void Divide_ByZero_ThrowsWithMessage()
{
    var calc = new Calculator();

    var ex = Assert.Throws<DivideByZeroException>(
        () => calc.Divide(10, 0)
    );

    Assert.Contains("zero", ex.Message.ToLower());
}
```

## Testing a Real Service

```csharp
// Service to test
public class UserService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public User Create(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        if (!email.Contains("@"))
            throw new ArgumentException("Invalid email format", nameof(email));

        var user = new User
        {
            Id = _nextId++,
            Name = name.Trim(),
            Email = email.ToLower().Trim()
        };

        _users.Add(user);
        return user;
    }

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public IEnumerable<User> GetAll() => _users.ToList();
}

// Tests
public class UserServiceTests
{
    private readonly UserService _service;

    public UserServiceTests()
    {
        _service = new UserService();
    }

    [Fact]
    public void Create_ValidInput_ReturnsUserWithId()
    {
        var user = _service.Create("John Doe", "john@example.com");

        Assert.NotNull(user);
        Assert.True(user.Id > 0);
        Assert.Equal("John Doe", user.Name);
        Assert.Equal("john@example.com", user.Email);
    }

    [Fact]
    public void Create_ValidInput_AddsUserToCollection()
    {
        _service.Create("John", "john@example.com");

        var users = _service.GetAll();

        Assert.Single(users);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_EmptyName_ThrowsArgumentException(string name)
    {
        var ex = Assert.Throws<ArgumentException>(
            () => _service.Create(name, "test@example.com")
        );

        Assert.Equal("name", ex.ParamName);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("no-at-sign.com")]
    public void Create_InvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.Throws<ArgumentException>(
            () => _service.Create("John", email)
        );
    }

    [Fact]
    public void Create_TrimsAndNormalizesInput()
    {
        var user = _service.Create("  John Doe  ", "  JOHN@EXAMPLE.COM  ");

        Assert.Equal("John Doe", user.Name);
        Assert.Equal("john@example.com", user.Email);
    }

    [Fact]
    public void GetById_ExistingUser_ReturnsUser()
    {
        var created = _service.Create("John", "john@example.com");

        var found = _service.GetById(created.Id);

        Assert.NotNull(found);
        Assert.Equal(created.Id, found.Id);
    }

    [Fact]
    public void GetById_NonExistingUser_ReturnsNull()
    {
        var found = _service.GetById(999);

        Assert.Null(found);
    }
}
```

## Test Organization with Regions

```csharp
public class OrderServiceTests
{
    #region CreateOrder Tests

    [Fact]
    public void CreateOrder_ValidInput_ReturnsOrder() { }

    [Fact]
    public void CreateOrder_EmptyCart_ThrowsException() { }

    #endregion

    #region GetOrder Tests

    [Fact]
    public void GetOrder_ExistingId_ReturnsOrder() { }

    [Fact]
    public void GetOrder_InvalidId_ReturnsNull() { }

    #endregion
}
```

## Shared Test Context (Fixtures)

For expensive setup that should be shared across tests:

```csharp
// Fixture class
public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
        // Expensive setup - runs once
        Connection = new SqlConnection("...");
        Connection.Open();
    }

    public SqlConnection Connection { get; }

    public void Dispose()
    {
        // Cleanup - runs once after all tests
        Connection.Close();
    }
}

// Test class using fixture
public class DatabaseTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public DatabaseTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void CanQueryDatabase()
    {
        // Use _fixture.Connection
    }
}
```

## Running Tests

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test class
dotnet test --filter "FullyQualifiedName~CalculatorTests"

# Run specific test method
dotnet test --filter "MethodName=Add_TwoPositiveNumbers_ReturnsSum"

# Run tests matching pattern
dotnet test --filter "Category=Unit"
```

## Code Coverage

```bash
# Install coverage tool
dotnet tool install -g dotnet-coverage

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Generate report
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:coverage.xml -targetdir:coverage-report
```

## Best Practices

### 1. One Assert Per Test (Generally)

```csharp
// Good - focused test
[Fact]
public void Create_ValidInput_SetsCorrectName()
{
    var user = _service.Create("John", "john@example.com");
    Assert.Equal("John", user.Name);
}

// Also acceptable - related asserts
[Fact]
public void Create_ValidInput_ReturnsCompleteUser()
{
    var user = _service.Create("John", "john@example.com");

    Assert.NotNull(user);
    Assert.Equal("John", user.Name);
    Assert.Equal("john@example.com", user.Email);
    Assert.True(user.Id > 0);
}
```

### 2. Test Behavior, Not Implementation

```csharp
// Good - tests behavior
[Fact]
public void Add_TwoNumbers_ReturnsSum()
{
    Assert.Equal(5, calc.Add(2, 3));
}

// Bad - tests implementation details
[Fact]
public void Add_UsesInternalAdder()
{
    // Don't test private methods or internal state
}
```

### 3. Tests Should Be Independent

Each test should be able to run alone, in any order:

```csharp
public class UserServiceTests
{
    private readonly UserService _service;

    public UserServiceTests()
    {
        // Fresh instance for each test
        _service = new UserService();
    }
}
```

### 4. Use Meaningful Test Data

```csharp
// Good - meaningful data
var user = _service.Create("John Doe", "john.doe@company.com");

// Avoid - meaningless data
var user = _service.Create("aaa", "a@a.a");
```

## Summary

| Concept | Description |
|---------|-------------|
| `[Fact]` | Single test case |
| `[Theory]` | Parameterized test |
| `[InlineData]` | Data for theory |
| AAA Pattern | Arrange, Act, Assert |
| Assert | Verification methods |
| Fixture | Shared test context |

Unit testing is non-negotiable for senior developers. Write tests for every feature you build.
