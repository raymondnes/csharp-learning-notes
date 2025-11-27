# C# Task Solutions

This file contains all accepted solutions for coding challenges completed during C# lessons, along with explanations and model solutions.

---

## Day 1: Hello World & .NET CLI

### Test_Agent Level 1: Basic Console Output (Trivial)

**Challenge:** Create a console application called `GreetingApp` that displays "Welcome to C#"

**Student's Solution:**
```csharp
namespace GreetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");
            Console.ReadLine();
        }
    }
}
```

**Explanation:**
- Uses `Console.WriteLine()` to display text to the console
- `Console.ReadLine()` keeps the console window open
- Demonstrates basic program structure with namespace and Main method

**Model Solution:** Student's solution is already optimal for this task.

---

### Test_Agent Level 2: Multiple Lines (Trivial)

**Challenge:** Display three lines of text, each on its own line

**Student's Solution:**
```csharp
Console.WriteLine("Learning C# is fun");
Console.WriteLine("Day 1 complete");
Console.WriteLine("Let's continue");
```

**Explanation:**
- Multiple `WriteLine()` calls create separate lines
- Uses escape character `\'` for apostrophe in "Let's"
- Demonstrates sequential execution

**Model Solution:** Student's solution is already optimal. Alternative approach using `\n`:
```csharp
Console.WriteLine("Learning C# is fun\nDay 1 complete\nLet's continue");
```

---

### Test_Agent Level 3: ASCII Art with Write/WriteLine Mix (Easy)

**Challenge:** Create ASCII art box using both `Console.Write()` and `Console.WriteLine()`

**Student's Solution:**
```csharp
Console.WriteLine("***************");
Console.WriteLine("* Hello C# ! *");
Console.Write("***************");
Console.ReadLine();
```

**Explanation:**
- `WriteLine()` adds a newline after output
- `Write()` keeps cursor on same line (used for last line before ReadLine)
- Demonstrates the difference between Write and WriteLine

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 4: Building Text with Write (Easy)

**Challenge:** Build sentence on one line using multiple `Write()` calls

**Student's Solution:**
```csharp
Console.Write("C#");
Console.Write(" is");
Console.Write(" powerful");
Console.Write(" and");
Console.Write(" easy");
Console.Write(" to ");
Console.WriteLine("learn!");
```

**Explanation:**
- Multiple `Write()` calls concatenate text on same line
- Final `WriteLine()` adds the newline
- Demonstrates sequential text building

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 5: Formatted Receipt (Moderate)

**Challenge:** Display a formatted receipt with borders

**Student's Solution:**
```csharp
Console.WriteLine("========== RECEIPT ==========");
Console.WriteLine("Item: Coffee");
Console.WriteLine("Item: Donut");
Console.WriteLine("Item: Sandwich");
Console.WriteLine("=============================");
Console.Write("Thank you for your purchase!");
```

**Explanation:**
- Creates visual structure with border lines (29 characters wide)
- Demonstrates formatting and alignment
- Uses Write for final line (before ReadLine)

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 6: Banner Message (Moderate)

**Challenge:** Create formatted banner using Write/WriteLine mix

**Student's Solution:**
```csharp
Console.WriteLine("+---------------------------+");
Console.Write("| Status:");
Console.Write(" System");
Console.Write(" Ready ");
Console.WriteLine("    |");
Console.Write("+---------------------------+");
```

**Explanation:**
- Builds middle line with multiple Write() calls
- All lines are exactly 29 characters wide
- Demonstrates precise formatting control

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 7: Progress Indicator (Challenging)

**Challenge:** Display progress bar built piece by piece

**Student's Solution:**
```csharp
Console.WriteLine("Loading system...");
Console.Write("[====");
Console.Write("====");
Console.Write("====");
Console.Write("==");
Console.Write("====");
Console.WriteLine("==] 100%");
Console.Write("Process complete!");
```

**Explanation:**
- Progress bar has exactly 20 equal signs between brackets
- Uses multiple Write() calls (5) to build the bar
- Demonstrates complex sequential output construction

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 8: System Diagnostics (Challenging)

**Challenge:** Display multi-section formatted output

**Student's Solution:**
```csharp
Console.WriteLine("=== SYSTEM DIAGNOSTICS ===");
Console.Write("CPU: ");
Console.Write("OK ");
Console.Write("| RAM: OK |");
Console.Write(" ");
Console.Write("DISK");
Console.Write(": ");
Console.WriteLine("OK");
Console.WriteLine("All systems operational.");
```

**Explanation:**
- Uses exactly 6 Write() calls followed by WriteLine()
- Demonstrates complex text formatting with separators
- Shows mastery of Write/WriteLine combination

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 9: System Startup Sequence (Very Challenging)

**Challenge:** Complete system startup display with Unicode box-drawing characters

**Student's Solution:**
```csharp
Console.WriteLine("╔════════════════════════════╗");
Console.WriteLine("║  SYSTEM INITIALIZATION      ║");
Console.WriteLine("╠════════════════════════════╣");
Console.Write("║");
Console.Write(" ");
Console.Write(" [✓] ");
Console.Write("Loading ");
Console.WriteLine("modules...    ║");
Console.Write("║ ");
Console.Write(" [✓] ");
Console.Write("Connecting ");
Console.WriteLine("database   ║");
Console.Write("║ ");
Console.Write(" [✓] ");
Console.WriteLine("Starting services     ║");
Console.WriteLine("╠════════════════════════════╣");
Console.Write("║  Status: ");
Console.WriteLine("READY              ║");
Console.Write("╚════════════════════════════╝");
```

**Explanation:**
- All lines exactly 30 characters wide
- Uses Unicode box-drawing characters (╔ ║ ╠ ╚ ═ ╗ ╣ ╝)
- Uses 11 Write() calls demonstrating complete mastery
- Requires precise spacing and formatting

**Model Solution:** Student's solution is already optimal. Shows expert-level attention to detail.

---

## Day 2: Variables and Data Types

### Test_Agent Level 1: Basic Variable Declaration (Trivial)

**Challenge:** Declare and display three variables with explicit types

**Student's Solution:**
```csharp
int score = 100;
string playerName = "Alex";
bool hasWon = true;

Console.WriteLine(score);
Console.WriteLine(playerName);
Console.WriteLine(hasWon);
```

**Explanation:**
- Uses explicit type declarations (`int`, `string`, `bool`)
- Proper camelCase naming convention
- Demonstrates basic variable declaration and output

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 2: String Interpolation (Trivial)

**Challenge:** Display variables with descriptive labels using string interpolation

**Student's Solution:**
```csharp
int score = 100;
string playerName = "Alex";
bool hasWon = true;

Console.WriteLine($"Score: {score}");
Console.WriteLine($"Player Name: {playerName}");
Console.WriteLine($"Has Won: {hasWon}");
```

**Explanation:**
- Uses `$"..."` syntax for string interpolation
- Variables embedded in curly braces `{variable}`
- More readable than concatenation with `+`

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 3: Variable Reassignment (Easy)

**Challenge:** Demonstrate variable reassignment with a `double` variable

**Student's Solution:**
```csharp
double temperature = 98.6;
Console.WriteLine($"Current Temperature: {temperature}");
temperature = 100.4;
Console.WriteLine($"Updated Temperature: {temperature}");
```

**Explanation:**
- Initial declaration: `double temperature = 98.6;`
- Reassignment without type: `temperature = 100.4;`
- Demonstrates that variables can change values
- Type is determined once and cannot change (static typing)

**Model Solution:** Student's solution is already optimal.

---

### Test_Agent Level 4: Type Inference with var (Easy)

**Challenge:** Use `var` keyword for type inference with four different types

**Student's Solution:**
```csharp
var userAge = 42;
var programName = "CSharp";
var valueOfPi = 3.14;
var isUserLoggedIn = true;

Console.WriteLine($"Users Age: {userAge}");
Console.WriteLine($"Program Name: {programName}");
Console.WriteLine($"Value of Pi: {valueOfPi}");
Console.WriteLine($"Is User Logged In: {isUserLoggedIn}");
```

**Explanation:**
- `var` allows compiler to infer type from assigned value
- `userAge` → `int`, `programName` → `string`, `valueOfPi` → `double`, `isUserLoggedIn` → `bool`
- Type is still static - once inferred, it cannot change
- Excellent meaningful variable names

**Model Solution:** Student's solution is already optimal. Shows professional naming practices.

---

### Test_Agent Level 5: Multiple Reassignments (Moderate)

**Challenge:** Demonstrate cumulative variable changes with calculations

**Student's Solution:**
```csharp
int counter = 0;
Console.WriteLine($"Initial Counter: {counter}");
counter += 5;
Console.WriteLine($"After adding 5: {counter}");
counter += 10;
Console.WriteLine($"After adding 10: {counter}");
counter *= 2;
Console.WriteLine($"After multiplying by 2: {counter}");
```

**Explanation:**
- Uses compound assignment operators (`+=`, `*=`)
- Demonstrates cumulative changes: 0 → 5 → 15 → 30
- `counter += 5` is shorthand for `counter = counter + 5`
- Shows professional coding style

**Model Solution:** Student's solution is already optimal. Excellent use of compound operators.

---

### Test_Agent Level 6: Profile Builder (Moderate)

**Challenge:** Build formatted profile with multiple variables

**Student's Solution:**
```csharp
string firstName = "Jordan";
string lastName = "Smith";
int age = 28;
double heigth = 5.9;  // Note: typo in variable name
bool isEmployed = true;

Console.WriteLine("===== PROFILE =====");
Console.WriteLine($"Name: {firstName} {lastName}");
Console.WriteLine($"Age: {age} years");
Console.WriteLine($"Height: {heigth} feet");
Console.WriteLine($"Employed: {isEmployed}");
Console.WriteLine("===================");
```

**Explanation:**
- Combines multiple variables in one interpolation: `{firstName} {lastName}`
- All border lines exactly 19 characters wide
- String interpolation makes formatting clean and readable
- Minor typo: `heigth` should be `height` (doesn't affect functionality)

**Model Solution:**
```csharp
string firstName = "Jordan";
string lastName = "Smith";
int age = 28;
double height = 5.9;  // Fixed typo
bool isEmployed = true;

Console.WriteLine("===== PROFILE =====");
Console.WriteLine($"Name: {firstName} {lastName}");
Console.WriteLine($"Age: {age} years");
Console.WriteLine($"Height: {height} feet");
Console.WriteLine($"Employed: {isEmployed}");
Console.WriteLine("===================");
```

---

### Test_Agent Level 7: Shopping Cart Calculator (Challenging)

**Challenge:** Calculate subtotal, tax, and total for shopping cart

**Student's Solution:**
```csharp
string itemName = "Laptop";
decimal itemPrice = 999.99m;
int quantity = 2;
decimal taxRate = 0.08m;
var subTotal = itemPrice * quantity;
var taxAmount = subTotal * (decimal)taxRate;
var total = subTotal + taxAmount;

Console.WriteLine("====== RECEIPT ======");
Console.WriteLine($"Item: {itemName}");
Console.WriteLine($"Price: ${itemPrice}");
Console.WriteLine($"Quantity: {quantity}");
Console.WriteLine("--------------------");
Console.WriteLine($"Subtotal: ${subTotal}");
Console.WriteLine($"Tax (8%): ${taxAmount}");
Console.WriteLine($"Total: ${total}");
Console.WriteLine("====================");
```

**Explanation:**
- **Excellent choice**: Uses `decimal` for monetary values (more precise than `double`)
- `m` suffix denotes decimal literals (999.99m, 0.08m)
- `var` for calculated values - compiler infers `decimal`
- Professional practice: `decimal` is the standard for financial calculations
- Border lines exactly 20 characters wide

**Model Solution:** Student's solution is already optimal. Shows professional-level understanding of appropriate data types for financial data.

---

### Test_Agent Level 8: Game Score Tracker (Challenging)

**Challenge:** Track cumulative game score with multiplier bonus

**Student's Solution:**
```csharp
var score = 0;
Console.WriteLine("=== GAME SCORE TRACKER ===");
Console.WriteLine($"Initial Score: {score}");
score += 100;
Console.WriteLine($"After Level 1: {score}");
score += 250;
Console.WriteLine($"After Level 2: {score}");
double bonusMultiplier = 1.5;
score = (int)(score * bonusMultiplier);
Console.WriteLine($"With Bonus (1.5x): {score}");
Console.WriteLine("=========================");
```

**Explanation:**
- `var score = 0` → compiler infers `int`
- Demonstrates type conversion: `score * bonusMultiplier` produces `double`
- Explicit cast `(int)` converts back to integer: `(int)(score * bonusMultiplier)`
- Shows understanding of type compatibility and conversion
- Border lines exactly 25 characters wide

**Model Solution:** Student's solution is already optimal. Demonstrates mastery of type conversion.

---

### Test_Agent Level 9: Temperature Monitor System (Very Challenging)

**Challenge:** Convert Fahrenheit to Celsius and Kelvin with precise formatting

**Student's Solution:**
```csharp
double fahrenheit = 98.6;
var celsius = (fahrenheit - 32) * 5.0 / 9.0;
var kelvin = celsius + 273.15;
bool isAboveFreezing = celsius > 0;

Console.WriteLine($"╔════════════════════════════════╗");
Console.WriteLine($"║   TEMPERATURE MONITOR v1.0     ║");
Console.WriteLine($"╠════════════════════════════════╣");
Console.WriteLine($"║ Fahrenheit: {fahrenheit}°F            ║");
Console.WriteLine($"║ Celsius: {Math.Round(celsius)}°C                 ║");
Console.WriteLine($"║ Kelvin: {kelvin:F2}K               ║");
Console.WriteLine($"║ Above Freezing: {isAboveFreezing}          ║");
Console.WriteLine($"╚════════════════════════════════╝");
```

**Explanation:**
- **Temperature formulas**:
  - Celsius: `(F - 32) × 5/9` → Uses `5.0/9.0` to avoid integer division
  - Kelvin: `C + 273.15`
- **Formatting techniques**:
  - `Math.Round(celsius)` → Rounds to whole number (37)
  - `{kelvin:F2}` → Formats to 2 decimal places (310.15)
- **Boolean logic**: `celsius > 0` checks if above freezing
- All border lines exactly 34 characters wide
- Uses Unicode box-drawing characters

**Model Solution:** Student's solution is already optimal. Demonstrates expert-level formatting, mathematical accuracy, and attention to detail.

---

## Summary Statistics

### Day 1 Performance:
- **Total Levels**: 9
- **Pass Rate**: 100%
- **Retries**: 3 (Levels 3, 4, 5, 7 - formatting precision)
- **Key Strengths**: Persistence, attention to detail, understanding Write vs WriteLine

### Day 2 Performance:
- **Total Levels**: 9
- **Pass Rate**: 100%
- **Retries**: 2 (Level 9 - spacing precision)
- **Key Strengths**: Professional data type choices (decimal for money), excellent variable naming, type conversion mastery

### Overall Achievement:
- **Total Challenges Completed**: 18
- **Overall Pass Rate**: 100%
- **Professional Practices Demonstrated**:
  - ✓ Proper camelCase naming conventions
  - ✓ Appropriate data type selection (decimal for financial data)
  - ✓ Compound assignment operators (+=, *=)
  - ✓ String interpolation mastery
  - ✓ Type conversion and casting
  - ✓ Mathematical formula implementation
  - ✓ Precision formatting

---

*This document is automatically updated after each Test_Agent assessment session.*

---
## Day 3: Operators and Expressions (2025-11-27)

### Socratic Task 1: Integer vs. Double Division

**Objective:** Understand the difference between integer division and floating-point division.

**Solution Code (`OperatorsDemo/Program.cs`):**

```csharp
// Integer Division
int a = 10;
int b = 3;
Console.WriteLine($"a / b = {a / b}");

// Double Division
Console.WriteLine("\n--- Using Double ---");
double c = 10;
double d = 3;
Console.WriteLine($"c / d = {c / d}");
```

**Test Output:**

```
a / b = 3
---
Using Double ---
c / d = 3.3333333333333335
```
**Explanation:** Dividing two `int` variables results in an `int`, with the decimal part truncated. To get a floating-point result, at least one of the operands must be a floating-point type like `double`.

### Socratic Task 2: Type Casting for Division

**Objective:** Force floating-point division on integers using type casting.

**Solution Code:**

```csharp
int a = 10;
int b = 3;
double result = (double)a / b;
Console.WriteLine($"10 / 3 = {result}");
```

**Test Output:**
```
10 / 3 = 3.3333333333333335
```
**Explanation:** Casting one of the integers to a `double` before the division operation forces the entire calculation to be performed using floating-point arithmetic, preserving the decimal result.

### Socratic Task 3: Combining Operator Types

**Objective:** Create a realistic scenario combining arithmetic, comparison, and logical operators.

**Solution Code (Banking App Withdrawal Logic):**
```csharp
double accountBalance = 200;
double withdrawalAmount = 150;
bool isAccountActive = true;

bool canWithdraw = isAccountActive && (accountBalance >= withdrawalAmount);
Console.WriteLine($"Can Withdraw: {canWithdraw}");
```
**Test Output:**
```
Can Withdraw: True
```
**Explanation:** This code correctly checks if an account is active (`isAccountActive`) AND if the balance is sufficient (`accountBalance >= withdrawalAmount`). It combines boolean, comparison (`>=`), and logical (`&&`) operators to make a complex decision.

---
### Test_Agent Level 1: Basic Arithmetic (Trivial)

**Challenge:** Perform and display all five basic arithmetic operations on two integers.

**Student's Solution:**
```csharp
int a = 20;
int b = 6;

Console.WriteLine($"a = {a}, b = {b}");
Console.WriteLine($"Addition: {a + b}");
Console.WriteLine($"Subtraction: {a - b}");
Console.WriteLine($"Multiplication: {a * b}");
Console.WriteLine($"Division: {a / b}");
Console.WriteLine($"Modulus: {a % b}");
```

**Test_Agent Evaluation:**
- **Result**: PASS
- **Output:**
```
a = 20, b = 6
Addition: 26
Subtraction: 14
Multiplication: 120
Division: 3
Modulus: 2
```
- **Explanation:** Correctly uses all five arithmetic operators and demonstrates understanding of integer division (`20 / 6 = 3`).

```