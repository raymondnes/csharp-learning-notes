# C# Mastery: 3-Month Plan
## Date: 2025-11-30
### Month 1, Week 1, Day 4: User Input and Type Conversion

**Session Status:** Active

**Today's Goals:**
- Getting user input with `Console.ReadLine()`
- Understanding that `Console.ReadLine()` always returns a string
- Converting between types (type casting and parsing)
- Using `int.Parse()`, `double.Parse()`, `bool.Parse()`
- Understanding the difference between casting and parsing
- Handling basic type conversion scenarios
- Building interactive programs that respond to user input

---

## **Professor's Opening Dialogue**

Welcome to Day 4! Over the past three days, you've built a solid foundation:
- **Day 1**: You learned the .NET CLI and wrote your first program
- **Day 2**: You mastered variables and data types
- **Day 3**: You conquered operators and expressions

Today, we're going to make your programs **interactive**! So far, all the data in your programs has been hardcoded - you decided what values to use. But real programs need to interact with users, accept their input, and respond accordingly.

Before we dive into code, let me start with a foundational question:

**Professor's Socratic Question (Understanding Input/Output):**

Think about the programs you use every day - web browsers, calculators, games, messaging apps. What do they all have in common regarding how they interact with you?

In other words, why would a program need to accept input from a user instead of just having all its data built-in?

**Student's Response:**
So I input data in these programs based on my need and discretion, and data changes based on situations, times and seasons, hence, hard coding data for a program is not at all a good practice, because user experience is dead. No program has all data required by its users built in, not even craigslist who has refused to update the UI since inception, they still take in user input

**Professor's Response:**
Brilliant analysis! You've hit on several crucial insights:

1. ✓ **"Data changes based on situations, times and seasons"** - Exactly! Programs need to be flexible and adapt to different contexts.

2. ✓ **"Hard coding data... user experience is dead"** - Perfect! A calculator hardcoded to only compute 2 + 2 would be useless. Users need to provide their own data.

3. ✓ **The Craigslist example** - Love this! Even the most minimal, unchanged UI still needs user input for posts, searches, etc. The UI might be static, but the data is dynamic.

4. ✓ **"No program has all data required by its users built in"** - This is the key insight. Programs are **tools** that process user-provided data, not static displays of predetermined information.

You've perfectly captured why input/output is fundamental to programming. Without user input:
- A calculator couldn't calculate different numbers
- A search engine couldn't search for different queries
- A game couldn't respond to player actions
- Your banking app from yesterday couldn't check different account balances

**The Core Principle:**
Programs are **data processors**, not **data containers**. The code provides the logic, the user provides the data.

Now let's learn how to actually get input from users in C#!

---

## **Getting User Input with Console.ReadLine()**

In C#, the `Console.ReadLine()` method reads a line of text from the user. Let's see it in action.

**Professor's Instruction:**

Create a new console project:
```
dotnet new console -n UserInputDemo
cd UserInputDemo
```

Replace the contents of `Program.cs` with:

```csharp
Console.WriteLine("What is your name?");
string name = Console.ReadLine();
Console.WriteLine($"Hello, {name}!");

Console.ReadLine(); // Keep console open
```

Run the program, type your name when prompted, and press Enter. What happens?

**Student's Response:**
Output Hello and my string. Let me show you. Hello, Ray!

**Professor's Response:**
Perfect! Your program is now interactive! Let's break down what just happened:

1. **`Console.WriteLine("What is your name?");`** - Displays a prompt to the user
2. **`string name = Console.ReadLine();`** - Waits for the user to type something and press Enter, then stores that input in the `name` variable
3. **`Console.WriteLine($"Hello, {name}!");`** - Uses the user's input to create a personalized greeting

**Key Observation:**
Notice that `Console.ReadLine()` returns a `string`. Whatever the user types is captured as text.

Now here's the critical question:

**Professor's Socratic Question (The String Problem):**

Your program works great for names. But what if you wanted to ask the user for their age and then calculate their birth year?

You might try something like:
```csharp
Console.WriteLine("How old are you?");
string age = Console.ReadLine();
int currentYear = 2025;
int birthYear = currentYear - age;  // This won't work!
```

Why do you think this code would cause an error? What's the problem with trying to subtract `age` from `currentYear`?

**Student's Response:**
The age variable is a string type, and you cannot perform calculation on a string type variable, you can only concatenate them

**Professor's Response:**
Exactly right! This is a fundamental type mismatch issue:

- `currentYear` is an `int` (a number)
- `age` is a `string` (text)
- You can't subtract text from a number!

Even though the user might type "25" (which looks like a number to us), `Console.ReadLine()` treats it as the string `"25"` - just text characters.

Remember from Day 2 when you said "C# holds you to your word"? This is that principle in action. C# is **strongly typed** and won't let you mix incompatible types, even if it seems obvious to us what should happen.

**The Solution: Type Conversion**

We need to **convert** (or **parse**) the string into an integer before we can do math with it. C# provides methods specifically for this purpose.

---

## **Parsing Strings to Numbers**

C# gives us parsing methods for converting strings to different numeric types:

| Method | Converts To | Example |
|--------|-------------|---------|
| `int.Parse()` | `int` | `int.Parse("25")` → `25` |
| `double.Parse()` | `double` | `double.Parse("3.14")` → `3.14` |
| `decimal.Parse()` | `decimal` | `decimal.Parse("99.99")` → `99.99m` |
| `bool.Parse()` | `bool` | `bool.Parse("true")` → `true` |

**Professor's Socratic Question (Understanding Parsing):**

Look at this corrected code:
```csharp
Console.WriteLine("How old are you?");
string ageInput = Console.ReadLine();
int age = int.Parse(ageInput);
int currentYear = 2025;
int birthYear = currentYear - age;
Console.WriteLine($"You were born around {birthYear}");
```

Can you explain in your own words what `int.Parse(ageInput)` does? Why is it necessary?

**Student's Response:**
It converts the string value to an int value capable of performing arithmetic operations. It is necessary because we have to ensure the variable type is int before calculating with it

**Professor's Response:**
Perfect explanation! You've captured both the **what** and the **why**:

✓ **What it does**: Converts `"25"` (string) → `25` (int)
✓ **Why it's necessary**: Enables arithmetic operations that only work with numbers

**Important Terminology:**
- **Parsing** = Converting a string representation into another type
- `int.Parse("25")` reads the string `"25"` and produces the integer `25`

**Professor's Instruction:**

Now let's try it yourself. Modify your `UserInputDemo` program to:
1. Ask the user for their age
2. Convert the input to an integer using `int.Parse()`
3. Calculate their birth year (2025 - age)
4. Display the result

```csharp
Console.WriteLine("How old are you?");
string ageInput = Console.ReadLine();
int age = int.Parse(ageInput);
int currentYear = 2025;
int birthYear = currentYear - age;
Console.WriteLine($"You were born around {birthYear}");

Console.ReadLine();
```

Run the program and enter your age. What output do you get?

**Student's Response:**
```
How old are you?
23
You were born around 2002
```

**Professor's Response:**
Excellent! Your program is now interactive AND performing calculations with user input. Let's trace through what happened:

1. User typed `"23"` as text
2. `Console.ReadLine()` captured it as the string `"23"`
3. `int.Parse(ageInput)` converted `"23"` (string) → `23` (int)
4. The calculation `2025 - 23` produced `2002`
5. The result was displayed

**This is a huge milestone!** You've combined:
- User input (`Console.ReadLine()`)
- Type conversion (`int.Parse()`)
- Arithmetic operations (`-`)
- Output (`Console.WriteLine()`)

Now, let me show you a common shortcut that many C# developers use:

**Inline Parsing:**

Instead of:
```csharp
string ageInput = Console.ReadLine();
int age = int.Parse(ageInput);
```

You can combine it into one line:
```csharp
int age = int.Parse(Console.ReadLine());
```

This works because `Console.ReadLine()` returns a string, and that string is immediately passed to `int.Parse()`.

**Professor's Socratic Question (Parsing vs. Casting):**

You've now seen two ways to convert types:

1. **Casting** (from Day 3): `(double)10` or `(int)3.7`
2. **Parsing** (today): `int.Parse("25")`

What's the difference? When would you use casting vs. parsing?

Hint: Think about what kind of data you're starting with in each case.

**Student's Response:**
Casting is changing an already existing type to another type before performing a calculation, it involves two variables at least, while parsing is converting from string to any other type, and it can be done in a single line while creating the variable and getting the data input

**Professor's Response:**
Great thinking! You've identified some key differences, though let me refine one point:

**Casting:**
- ✓ Converts between **compatible numeric types** (int ↔ double, etc.)
- ✓ Works with variables that already exist in your code
- Example: `(double)10` converts an int to a double
- Note: Doesn't necessarily need two variables - `(double)5` is valid casting

**Parsing:**
- ✓ Converts **strings to other types** (string → int, string → double, etc.)
- ✓ Essential for user input (since `Console.ReadLine()` always returns a string)
- ✓ Can be done inline: `int age = int.Parse(Console.ReadLine());`

**The Key Distinction:**

| Aspect | Casting | Parsing |
|--------|---------|---------|
| **Input** | Already a numeric type | A string |
| **Use Case** | `(double)10` | `int.Parse("10")` |
| **Example** | Converting int to double for division | Converting user input to numbers |
| **Syntax** | `(type)value` | `type.Parse(string)` |

**Simple Rule:**
- **Casting**: Number → Number
- **Parsing**: String → Number (or other type)

---

## **Parsing Different Types**

You can parse strings into various types. Let's see them in action!

**Professor's Instruction:**

Create a small program that demonstrates parsing different types:

```csharp
Console.WriteLine("=== TYPE CONVERSION DEMO ===\n");

// Parsing to int
Console.Write("Enter an integer: ");
int myInt = int.Parse(Console.ReadLine());
Console.WriteLine($"You entered: {myInt} (type: int)\n");

// Parsing to double
Console.Write("Enter a decimal number: ");
double myDouble = double.Parse(Console.ReadLine());
Console.WriteLine($"You entered: {myDouble} (type: double)\n");

// Parsing to bool
Console.Write("Enter true or false: ");
bool myBool = bool.Parse(Console.ReadLine());
Console.WriteLine($"You entered: {myBool} (type: bool)\n");

Console.ReadLine();
```

Try running this and entering:
- An integer (like `42`)
- A decimal number (like `3.14`)
- The word `true` or `false`

What happens?

**Student's Response:**
```
It shows the types of each entered value
=== TYPE CONVERSION DEMO ===

Enter an integer: 45
You entered: 45 (type: int)

Enter a decimal number: 4.5
You entered: 4.5 (type: double)

Enter true or false: true
You entered: True (type: bool)
```

**Professor's Response:**
Perfect! Your program successfully parsed three different types from user input. Notice something interesting:

- You typed `"45"` (string) → converted to `45` (int)
- You typed `"4.5"` (string) → converted to `4.5` (double)
- You typed `"true"` (string) → converted to `True` (bool)

Each `Parse()` method knows how to interpret the string and convert it to the appropriate type.

**Important Observation:**
Notice that when you typed `true` (lowercase), C# displayed it as `True` (capital T). C# automatically handles the capitalization for boolean display, but `bool.Parse()` accepts both `"true"` and `"True"` as input.

Now let's build something practical that combines everything you've learned!

---

## **Hands-On: Building a Simple Calculator**

Let's create an interactive calculator that:
1. Asks the user for two numbers
2. Performs all arithmetic operations on them
3. Displays the results

**Professor's Instruction:**

Create this calculator program:

```csharp
Console.WriteLine("=== SIMPLE CALCULATOR ===\n");

Console.Write("Enter the first number: ");
double num1 = double.Parse(Console.ReadLine());

Console.Write("Enter the second number: ");
double num2 = double.Parse(Console.ReadLine());

Console.WriteLine($"\nResults:");
Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
Console.WriteLine($"{num1} * {num2} = {num1 * num2}");
Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
Console.WriteLine($"{num1} % {num2} = {num1 % num2}");

Console.ReadLine();
```

Try it with:
- First number: `10`
- Second number: `3`

What results do you get?

**Student's Response:**
```
=== SIMPLE CALCULATOR ===

Enter the first number: 10
Enter the second number: 3

Results:
10 + 3 = 13
10 - 3 = 7
10 * 3 = 30
10 / 3 = 3.33333333333333
10 % 3 = 1
```

**Professor's Response:**
Excellent! Your calculator is working perfectly! Notice something important here - let's compare this to what you learned on Day 3:

**On Day 3 (hardcoded values):**
```csharp
int a = 10;
int b = 3;
int result = a / b;  // Result: 3 (integer division)
```

**Today (user input with parsing):**
```csharp
double num1 = double.Parse(Console.ReadLine());  // User enters 10
double num2 = double.Parse(Console.ReadLine());  // User enters 3
double result = num1 / num2;  // Result: 3.3333... (floating-point division)
```

**Why the different results?**

Because you used `double.Parse()`, the user's input is converted to `double` instead of `int`. This means you get **floating-point division** (with decimals) instead of integer division!

**Professor's Socratic Question (Understanding Type Choice):**

Look at the modulus operation result: `10 % 3 = 1`

The modulus operator works with `double` types, but it's typically used with integers. If you wanted to create a calculator that only works with whole numbers (integers), what would you change in the code?

**Student's Response:**
```csharp
Console.Write("Enter the first number: ");
double num1 = int.Parse(Console.ReadLine());

Console.Write("Enter the second number: ");
double num2 = int.Parse(Console.ReadLine());
```
The parsing to int

**Professor's Response:**
You're on the right track with changing the parsing method! However, there's a subtle issue in your code. Look closely:

```csharp
double num1 = int.Parse(Console.ReadLine());
```

This says:
1. Parse the input as an `int` ✓
2. But store it in a `double` variable ✗

This creates an **implicit conversion** - C# automatically converts the `int` to `double` after parsing. While this works, it's not quite what we want for an integer-only calculator.

**The correct approach:**
```csharp
int num1 = int.Parse(Console.ReadLine());
int num2 = int.Parse(Console.ReadLine());
```

Now:
- Parse as `int` ✓
- Store as `int` ✓
- All operations use integer arithmetic ✓

**Professor's Socratic Question (Understanding the Impact):**

If you made this change and ran the calculator again with 10 and 3, how would the division result change?

Currently (with `double`): `10 / 3 = 3.33333333333333`

With `int`: `10 / 3 = ?`

What do you think it would be, and why?

**Student's Response:**
3, because the .33333 has been truncated since dividing int does not round up or down, it outputs the whole number

**Professor's Response:**
Perfect! You've remembered the integer division behavior from Day 3:

- `10 / 3` with `int` types = `3` (truncated, not rounded)
- `10 / 3` with `double` types = `3.33333...` (full decimal result)

Your understanding is spot-on: integer division **truncates** (chops off) the decimal part, it doesn't round.

This demonstrates an important principle: **The type you choose when parsing affects all subsequent operations!**

```csharp
// Integer calculator
int num1 = int.Parse(Console.ReadLine());  // Parses as int
int result = num1 / 3;  // Integer division = 3

// Floating-point calculator
double num1 = double.Parse(Console.ReadLine());  // Parses as double
double result = num1 / 3;  // Floating-point division = 3.333...
```

---

## **Day 4 Summary**

Excellent work today! You've learned how to make your programs interactive. Let me summarize what you've mastered:

**Core Concepts Learned:**

✓ **User Input with `Console.ReadLine()`**
  - Always returns a `string`, regardless of what the user types
  - Waits for user to press Enter

✓ **Parsing Strings to Other Types**
  - `int.Parse()` - string → int
  - `double.Parse()` - string → double
  - `bool.Parse()` - string → bool
  - Necessary because `Console.ReadLine()` always gives you text

✓ **Casting vs. Parsing**
  - **Casting**: Number → Number (e.g., `(double)10`)
  - **Parsing**: String → Number (e.g., `int.Parse("10")`)

✓ **Building Interactive Programs**
  - Combined input, parsing, operations, and output
  - Created a working calculator that responds to user data

**Key Insights You Discovered:**

1. **"Programs are data processors, not data containers"** - You articulated why dynamic input is essential
2. **Type choice matters** - Using `int.Parse()` vs `double.Parse()` affects division results
3. **Inline parsing** - `int.Parse(Console.ReadLine())` is concise and idiomatic

**Professor's Final Reflection:**

You've come a long way! Think about the progression:
- **Day 1**: Hello World with hardcoded text
- **Day 2**: Variables storing hardcoded values
- **Day 3**: Operations on hardcoded numbers
- **Day 4**: Interactive programs that respond to USER data!

Before we move to Test_Agent, let me ask you one final reflective question:

You've built an interactive calculator today. Can you think of one potential problem with using `Parse()` methods? What might happen if a user types something that can't be converted to a number (like typing "hello" when asked for an age)?

**Student's Response:**
There will be an unhandled exception error `System.FormatException: 'Input string was not in a correct format.'` which means the expected type was not provided, so we need to do a try parse to catch exceptions. I'm not familiar with this, but will be happy to learn

**Professor's Response:**
Absolutely brilliant! You've identified the exact problem AND you already know about potential solutions:

1. ✓ **The Problem**: `FormatException` when parsing invalid input
2. ✓ **The Cause**: User types "hello" when you're expecting "25"
3. ✓ **Potential Solutions**: Exception handling (`try-catch`) and `TryParse()`

You're thinking ahead like a professional developer! Here's what's coming:

**Where You'll Learn These:**
- **Exception handling** (`try-catch` blocks) - **Day 19** in Week 4
- **`TryParse()` method** - Often used as a safer alternative to `Parse()`

**Brief Preview** (since you asked):

`TryParse()` is a safer version of `Parse()` that returns `true` or `false` instead of throwing an exception:

```csharp
string input = Console.ReadLine();
int number;

if (int.TryParse(input, out number))
{
    Console.WriteLine($"Valid number: {number}");
}
else
{
    Console.WriteLine("Invalid input! Please enter a number.");
}
```

**For now**, your programs will assume users enter valid data. This is perfectly fine for learning - professional error handling comes later!

The fact that you:
- Tested your program with invalid input
- Saw the exception
- Understood what it meant
- Knew there are solutions

...shows **exceptional curiosity and testing mindset**. That's exactly how great developers think!

---

## **Test_Agent Invocation**

You've demonstrated deep understanding of user input and type conversion through our Socratic dialogue. You can:
- Explain why programs need user input (data processors vs containers)
- Use `Console.ReadLine()` to capture user input
- Understand that `Console.ReadLine()` always returns a string
- Parse strings to different types (`int.Parse()`, `double.Parse()`, `bool.Parse()`)
- Distinguish between casting (number→number) and parsing (string→number)
- Build interactive programs combining input, parsing, and operations
- Recognize potential issues (like `FormatException`) from invalid input

I'm now handing you over to Test_Agent for the Day 4 assessment. Test_Agent will verify your mastery through 9 progressive challenges covering:
- Getting user input with `Console.ReadLine()`
- Parsing strings to `int`, `double`, and `bool`
- Building interactive programs
- Combining input, parsing, and operations
- Understanding type conversion choices

Good luck!

---

## **Test_Agent Assessment - Day 4: User Input and Type Conversion**

**Concepts to be tested:**
- Getting user input with `Console.ReadLine()`
- Understanding that `Console.ReadLine()` always returns a string
- Parsing strings to int (`int.Parse()`)
- Parsing strings to double (`double.Parse()`)
- Parsing strings to bool (`bool.Parse()`)
- Building interactive programs
- Combining input, parsing, arithmetic operations, and output
- Understanding the difference between casting and parsing

---

## **Level 1: Foundation (Trivial)**

**Challenge:**
Create a program that asks the user for their name and greets them.

**Requirements:**
1. Display the prompt: `"What is your name?"`
2. Read the user's input using `Console.ReadLine()`
3. Store the input in a `string` variable called `name`
4. Display: `"Hello, [name]!"`
5. Use string interpolation for the output

**Expected Output Example:**
```
What is your name?
Ray
Hello, Ray!
```

Show me your code and run it with your name.

**Student's Code:**
```csharp
Console.WriteLine($"What is your name?");
string name = Console.ReadLine();
Console.WriteLine($"Hello, {name}!");

Console.ReadLine();
```

**Test_Agent Evaluation:**

✓ Correct prompt displayed
✓ Uses `Console.ReadLine()` to get user input
✓ Stores input in a `string` variable named `name`
✓ Displays greeting with user's name
✓ Uses string interpolation correctly
✓ Code structure is clean and correct

**LEVEL 1: PASS**

---

## **Level 2: Foundation (Trivial)**

**Challenge:**
Create a program that asks the user for their age (as an integer) and displays it back.

**Requirements:**
1. Display the prompt: `"How old are you?"`
2. Read the user's input
3. Parse the input to an `int` using `int.Parse()`
4. Store it in a variable called `age`
5. Display: `"You are [age] years old."`
6. Use string interpolation for output

**Expected Output Example:**
```
How old are you?
23
You are 23 years old.
```

Show me your code and run it.

**Student's Code:**
```csharp
Console.WriteLine($"How old are you?");
int age = int.Parse(Console.ReadLine());
Console.WriteLine($"You are {age} years old");
```

**Test_Agent Evaluation:**

✓ Correct prompt displayed
✓ Uses `Console.ReadLine()` to get input
✓ Parses input correctly with `int.Parse()`
✓ Stores result in an `int` variable named `age`
✓ Displays age back to user
✓ Uses string interpolation
✓ Excellent use of inline parsing: `int.Parse(Console.ReadLine())`
✗ Minor: Missing period at end of output (expected "years old." but got "years old")

**LEVEL 2: PASS** (minor formatting difference doesn't affect functionality)

---

## **Level 3: Application (Easy)**

**Challenge:**
Create a program that asks for the user's age and calculates their birth year.

**Requirements:**
1. Display prompt: `"Enter your age:"`
2. Read and parse the input to an `int`
3. Calculate birth year using: `2025 - age`
4. Display: `"You were born around [birthYear]"`
5. Use string interpolation

**Expected Output Example:**
```
Enter your age:
23
You were born around 2002
```

Show me your code and run it.

**Student's Code:**
```csharp
Console.Write($"Enter your age: ");
int age = int.Parse(Console.ReadLine());
int birthYear = 2025 - age;
Console.WriteLine($"You were born around {birthYear}");
Console.ReadLine();
```

**Test_Agent Evaluation:**

✓ Correct prompt displayed
✓ Uses `Console.ReadLine()` to get input
✓ Parses input correctly with `int.Parse()`
✓ Calculates birth year with correct formula: `2025 - age`
✓ Stores result in `birthYear` variable
✓ Displays result with correct format
✓ Uses string interpolation
✓ Good use of `Console.Write()` instead of `WriteLine()` for inline input
✓ Added extra `Console.ReadLine()` to keep console open - good practice!

**LEVEL 3: PASS**

---

## **Level 4: Application (Easy)**

**Challenge:**
Create a program that asks for a decimal number (temperature in Celsius) and converts it to Fahrenheit.

**Requirements:**
1. Display prompt: `"Enter temperature in Celsius:"`
2. Read and parse the input to a `double` using `double.Parse()`
3. Convert using the formula: `Fahrenheit = (Celsius * 9/5) + 32`
4. Display: `"[celsius]°C = [fahrenheit]°F"`
5. Use string interpolation

**Expected Output Example:**
```
Enter temperature in Celsius:
25
25°C = 77°F
```

Show me your code and run it.