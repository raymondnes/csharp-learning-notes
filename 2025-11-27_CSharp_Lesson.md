# C# Mastery: 3-Month Plan
## Date: 2025-11-27
### Month 1, Week 1, Day 3: Operators and Expressions

**Session Status:** Active

**Today's Goals:**
- Understanding arithmetic operators (+, -, *, /, %)
- Understanding comparison operators (==, !=, <, >, <=, >=)
- Understanding logical operators (&&, ||, !)
- Operator precedence and evaluation order
- Compound assignment operators (+=, -=, *=, /=, %=)

---

## **Professor's Opening Dialogue**

Welcome to Day 3! Yesterday you mastered variables and data types - learning how to store and manipulate different kinds of data in C#. Today, we're going to explore how to actually **work with** that data using **operators and expressions**.

You've already seen some operators in action (like `=` for assignment and `+` for addition), but today we're going to dive deep into the full range of operations you can perform in C#.

Before we jump into code, let me start with a foundational question:

**Professor's Socratic Question (Understanding Operators):**

Think about mathematics for a moment. When you see an expression like `5 + 3 * 2`, you know the answer is `11`, not `16`. Why?

In other words, why do we multiply first (3 * 2 = 6, then 5 + 6 = 11) instead of adding first (5 + 3 = 8, then 8 * 2 = 16)?

**Student's Response:**
BODMAS, or order of operation in mathematics, where multiplication precedes addition

**Professor's Response:**
Exactly right! **BODMAS** (or PEMDAS in some regions) - **B**rackets, **O**rders, **D**ivision, **M**ultiplication, **A**ddition, **S**ubtraction. This mathematical rule ensures everyone evaluates expressions the same way.

Here's the great news: **C# follows the exact same precedence rules!**

Programming languages need this consistency just like mathematics does. Imagine if different compilers evaluated `5 + 3 * 2` differently - your program would behave unpredictably! C# enforces operator precedence so that:

```csharp
int result = 5 + 3 * 2;  // result is 11, not 16
```

The multiplication happens first, just like in math.

Now here's my follow-up question:

**Professor's Socratic Question (Operator Precedence in Code):**

If C# follows BODMAS, what do you think would happen with this expression:

```csharp
int result = (5 + 3) * 2;
```

Notice the parentheses around `5 + 3`. What would the result be, and why?

**Student's Response:**
The result is 16. The parenthesis has created a new order for the calculation mandating the addition to kickstart operation before the multiplication

**Professor's Response:**
Perfect! You've captured the essence of how parentheses work in programming. The parentheses **override** the default precedence, forcing the addition to happen first:

1. `(5 + 3)` = 8 (parentheses are evaluated first)
2. `8 * 2` = 16 (then multiplication)

You said "mandating the addition to kickstart operation" - I love that phrasing! Parentheses give you **explicit control** over evaluation order. This is crucial in programming because sometimes the natural precedence isn't what you need.

Now let's explore the arithmetic operators in C# and see them in action.

---

## **Arithmetic Operators in C#**

C# provides five basic arithmetic operators:

| Operator | Name           | Example  | Result |
|----------|----------------|----------|--------|
| `+`      | Addition       | `5 + 3`  | `8`    |
| `-`      | Subtraction    | `5 - 3`  | `2`    |
| `*`      | Multiplication | `5 * 3`  | `15`   |
| `/`      | Division       | `6 / 3`  | `2`    |
| `%`      | Modulus        | `5 % 3`  | `2`    |

You're familiar with the first four from mathematics. The last one, **modulus (`%`)**, might be new.

**Professor's Socratic Question (Understanding Modulus):**

The modulus operator (`%`) returns the **remainder** after division. For example:
- `5 % 3 = 2` (because 5 ÷ 3 = 1 remainder **2**)
- `10 % 4 = 2` (because 10 ÷ 4 = 2 remainder **2**)
- `7 % 2 = 1` (because 7 ÷ 2 = 3 remainder **1**)

Can you think of a practical situation in programming where you'd want to know the **remainder** of a division rather than the quotient?

**Student's Response:**
Odd number division

**Professor's Response:**
Good thinking! You're on the right track. Let me expand on that idea:

You can use modulus to **determine if a number is even or odd**:
- `number % 2 == 0` → the number is **even** (divides evenly by 2, no remainder)
- `number % 2 == 1` → the number is **odd** (remainder of 1 when divided by 2)

For example:
```csharp
int num = 7;
int remainder = num % 2;  // remainder = 1, so 7 is odd

int num2 = 8;
int remainder2 = num2 % 2;  // remainder2 = 0, so 8 is even
```

But there are many other practical uses for modulus:

1. **Checking divisibility**: Is a number divisible by 5? `number % 5 == 0`
2. **Cycling through values**: Imagine a carousel with 10 items. If you're at position 27, which item are you at? `27 % 10 = 7`
3. **Alternating patterns**: Display every 3rd item differently? `index % 3 == 0`
4. **Time calculations**: Converting 135 minutes to hours and minutes? `135 / 60 = 2` hours, `135 % 60 = 15` minutes

The modulus operator is surprisingly useful once you start programming!

Now let's get hands-on and see these operators in action.

---

## **Hands-On: Arithmetic Operators**

Let's create a project to experiment with these operators.

**Professor's Instruction:**

Create a new console project called `OperatorsDemo`:

```
dotnet new console -n OperatorsDemo
cd OperatorsDemo
```

Then open `Program.cs` and replace its contents with this starter code:

```csharp
int a = 10;
int b = 3;

Console.WriteLine($"a = {a}, b = {b}");
Console.WriteLine($"a + b = {a + b}");
Console.WriteLine($"a - b = {a - b}");
Console.WriteLine($"a * b = {a * b}");
Console.WriteLine($"a / b = {a / b}");
Console.WriteLine($"a % b = {a % b}");

Console.ReadLine();
```

Go ahead and create this project and run it. What output do you see?

**Student's Response:**
```
a = 10, b = 3
a + b = 13
a - b = 7
a * b = 30
a / b = 3
a % b = 1
```

**Professor's Response:**
Perfect! All the arithmetic operators are working as expected. Let me highlight something interesting about one of these results:

Notice the division: `a / b = 3`

You have `10 / 3`, and the result is `3`. But wait - mathematically, 10 ÷ 3 = 3.333... Why did C# give you `3` instead of `3.333`?

**Professor's Socratic Question (Integer Division):**

Look at the data types in your code:
```csharp
int a = 10;
int b = 3;
```

Both `a` and `b` are integers (`int`). When you divide two integers in C#, what type of result do you think you'll get - an integer or a decimal number?

And based on your output (`a / b = 3`), what do you think C# does with the decimal part (.333...)?

**Student's Note:**
Can @csharp_prof.md handle this class directly, I like his charisma

**Professor's Response:**
Thank you for the kind words! I'm glad you're enjoying our Socratic journey together. Yes, I'm here with you directly - I AM csharp_prof, following the teaching philosophy outlined in `csharp_prof.md`. That file is my "blueprint" - it defines how I should teach (using the Socratic method, asking questions, waiting for your responses, being patient and encouraging).

Think of it this way:
- `csharp_prof.md` = My teaching philosophy and guidelines
- `GEMINI.md` = Our curriculum (the 3-month roadmap)
- `Test_Agent.md` = The assessment specialist I'll invoke after you master concepts
- Our daily lesson files = Our actual conversations and progress

You're interacting with me directly right now, and I'm following those principles to guide you through C# mastery. The charisma you're experiencing is intentional - learning should be engaging and enjoyable!

Now, let's get back to that interesting question about integer division. Take your time and think about what C# might be doing with the decimal part when dividing `10 / 3`. What's your hypothesis?

**Student's Response:**
It truncates the decimal part

**Professor's Response:**
Exactly right! C# **truncates** (chops off) the decimal part when performing integer division. This is a critical concept to understand:

**Integer Division Rule:**
- When you divide two `int` values, the result is always an `int`
- The decimal portion is discarded (truncated), not rounded
- `10 / 3 = 3` (not 3.333..., and not rounded to 4)
- `7 / 2 = 3` (not 3.5, and not rounded to 4)
- `5 / 2 = 2` (not 2.5, and not rounded to 3)

This behavior is **intentional** and follows from C#'s static typing - remember when you said "C# holds you to your word"? If you declare variables as `int`, C# ensures the result is also an `int`.

**Professor's Socratic Question (Getting Decimal Results):**

Now, what if you actually *want* the decimal result? What if you need `10 / 3` to give you `3.333...` instead of `3`?

Based on what you learned yesterday about data types, how could you modify the code to get a decimal result from this division?

Hint: Think about which data type can hold decimal values.

**Student's Response:**
```csharp
decimal a = 10m;
decimal b = 3m;

Console.WriteLine($"a / b = {(a / b)}");
```
or float type variable or double type

**Professor's Response:**
Excellent! You've identified **all three** floating-point data types in C#:

1. ✓ `double` - most common for general decimal calculations
2. ✓ `float` - less precise, uses less memory
3. ✓ `decimal` - highest precision, best for financial calculations

Your code is perfect! Let's see what happens when you use `double`:

**Professor's Instruction:**

Add this code to your program (below your existing output):

```csharp
Console.WriteLine("\n--- Using Double ---");
double c = 10;
double d = 3;
Console.WriteLine($"c / d = {c / d}");
```

Run your program again. What result do you get for the double division?

**Important Note:** Notice that with `double`, you don't need the `m` suffix. The `m` suffix is only for `decimal` type. For `double`, you can just write `10` or `10.0`.

Go ahead and try it - what output do you see?

**Student's Response:**
```
--- Using Double ---
c / d = 3.33333333333333
```

**Professor's Response:**
Perfect! Now you see the difference:

- **Integer division**: `10 / 3 = 3` (truncated)
- **Double division**: `10.0 / 3.0 = 3.33333333333333` (full decimal result)

The key insight: **The data type of the operands determines the type of the result.**

This is an important rule in C#:
- `int / int` → `int` (truncated)
- `double / double` → `double` (decimal result)
- `decimal / decimal` → `decimal` (high precision decimal result)

**Professor's Socratic Question (Mixed Type Operations):**

Now here's an interesting scenario. What do you think would happen if you mixed types?

```csharp
double result = 10 / 3;
```

Notice that:
- The division is `10 / 3` (both are integers)
- But the result is stored in a `double` variable

Do you think this would give you `3.0` or `3.333...`? And why?

Take a moment to think about it before answering.

**Student's Response:**
It will infer a type based on the initialization value and provide the calculation based on that type. I tested `double result = 10 / 3;` and the output was 3, but when I tested `double result = 10.0 / 3.0;` the output was 3.333333333333 so it behaves the same way it does when using var type for storing data

**Professor's Response:**
Great experimentation! You've discovered something important, but let me clarify a subtle but crucial point about what's happening here.

**The key insight:** The type of the **variable** (`double result`) doesn't affect how the division is calculated. What matters is the type of the **operands** (the numbers being divided).

Here's what's actually happening step-by-step:

**Case 1:** `double result = 10 / 3;
1. C# evaluates `10 / 3` first (both are `int`)
2. Result of division: `3` (integer division, truncated)
3. Then `3` is converted to `3.0` and stored in the `double` variable
4. Final result: `3.0` (but the decimal part was already lost!)

**Case 2:** `double result = 10.0 / 3.0;
1. C# evaluates `10.0 / 3.0` (both are `double`)
2. Result of division: `3.333333...` (double division, preserves decimals)
3. Stored directly in the `double` variable
4. Final result: `3.3333333333333`

**The Critical Rule:**
The division happens **before** the assignment. By the time C# stores the result in your variable, the calculation is already done. If you divided two integers, the truncation has already occurred - storing it in a `double` afterward doesn't magically bring back the lost decimal places!

Think of it like this: If you break an egg, putting the broken pieces in a different container doesn't un-break the egg.

**Professor's Socratic Question (Type Casting):**

So if you have integer variables but need a decimal result, what could you do?

```csharp
int a = 10;
int b = 3;
double result = ???  // How to get 3.333... instead of 3.0?
```

You need to convert at least one of the integers to a `double` **before** the division happens. Can you think of how to do that?

Hint: Yesterday you learned about type conversion and casting.

**Student's Response:**
```csharp
double result = (double)(10) / 3;
Console.WriteLine($"10 / 3 = {result}");
```

**Professor's Response:**
Excellent! You've applied type casting perfectly. Your code works and demonstrates a solid understanding. Let me break down what's happening:

```csharp
double result = (double)(10) / 3;
```

**Execution order:**
1. `(double)(10)` → Casts `10` to `10.0` (now it's a `double`)
2. `10.0 / 3` → Mixed-type division: `double / int`
3. C# automatically promotes the `3` to `3.0` to match types
4. `10.0 / 3.0` = `3.3333333333333`

**Important Note:** When you have mixed types (`double / int`), C# automatically converts the `int` to `double` to perform the operation. So you only need to cast **one** of the operands, not both.

These are all equivalent:
```csharp
double result = (double)a / b;      // Cast a to double
double result = a / (double)b;      // Cast b to double
double result = (double)a / (double)b;  // Cast both (redundant but valid)
```

You could also write it with variables:
```csharp
int a = 10;
int b = 3;
double result = (double)a / b;  // Cast a before division
Console.WriteLine($"a / b = {result}");  // Output: 3.3333333333333
```

Perfect understanding of type casting in arithmetic operations!

Now let's explore more operators beyond arithmetic.

---

## **Comparison Operators**

Comparison operators let you compare values and produce a **boolean** (`true` or `false`) result. These are essential for making decisions in your programs.

| Operator | Meaning                  | Example  | Result  |
|----------|--------------------------|----------|---------|
| `==`     | Equal to                 | `5 == 5` | `true`  |
| `!=`     | Not equal to             | `5 != 3` | `true`  |
| `>`      | Greater than             | `5 > 3`  | `true`  |
| `<`      | Less than                | `5 < 3`  | `false` |
| `>=`     | Greater than or equal to | `5 >= 5` | `true`  |
| `<=`     | Less than or equal to    | `5 <= 3` | `false` |

**Professor's Socratic Question (Comparison vs Assignment):**

Notice that the equality operator is `==` (two equals signs), not `=` (one equals sign).

Why do you think C# uses `==` for comparison instead of just `=`? What does the single `=` do?

**Student's Response:**
Single = is an assignment operator, so C# has to distinguish between both == for equality

**Professor's Response:**
Exactly right! This is a critical distinction that prevents a whole category of bugs:

- **`=` (assignment)**: Assigns a value to a variable
  - `x = 5` means "set x to 5"

- **`==` (comparison)**: Checks if two values are equal
  - `x == 5` means "is x equal to 5?" (returns `true` or `false`)

This distinction is crucial because they do completely different things:

```csharp
int x = 10;

// Assignment - changes x to 5
x = 5;  // x is now 5

// Comparison - checks if x equals 5, returns true
bool result = (x == 5);  // result is true, x is still 5
```

**Why this matters:** In some older languages, using `=` where you meant `==` could cause subtle bugs. C# prevents this by making them syntactically distinct.

**Professor's Instruction:**

Let's see comparison operators in action. Add this code to your program:

```csharp
Console.WriteLine("\n--- Comparison Operators ---");
int x = 10;
int y = 5;

Console.WriteLine($"x = {x}, y = {y}");
Console.WriteLine($"x == y: {x == y}");
Console.WriteLine($"x != y: {x != y}");
Console.WriteLine($"x > y: {x > y}");
Console.WriteLine($"x < y: {x < y}");
Console.WriteLine($"x >= y: {x >= y}");
Console.WriteLine($"x <= y: {x <= y}");
```

Run your program and show me the output.

**Student's Response:**
```
--- Comparison Operators ---
x = 10, y = 5
x == y: False
x != y: True
x > y: True
x < y: False
x >= y: True
x <= y: False
```

**Professor's Response:**
Perfect! All the comparison operators are working correctly. Let's analyze what we're seeing:

- `x == y: False` → 10 is not equal to 5
- `x != y: True` → 10 is not equal to 5 (opposite of ==)
- `x > y: True` → 10 is greater than 5
- `x < y: False` → 10 is not less than 5
- `x >= y: True` → 10 is greater than or equal to 5
- `x <= y: False` → 10 is not less than or equal to 5

Notice that all these expressions return **boolean values** (`True` or `False`). This is essential for control flow - you'll use these operators extensively when making decisions in your programs (which we'll cover on Day 6 with `if-else` statements).

Now let's explore the final category of operators: **logical operators**.

---

## **Logical Operators**

Logical operators let you combine multiple boolean expressions. They're essential for creating complex conditions.

| Operator | Name        | Description                          | Example           | Result  |
|----------|-------------|--------------------------------------|-------------------|---------|
| `&&`     | AND         | True if **both** conditions are true | `true && false`   | `false` |
| `||`     | OR          | True if **either** condition is true | `true || false` | `true`  |
| `!`      | NOT         | Inverts the boolean value            | `!true`           | `false` |

**Professor's Socratic Question (Understanding AND):**

The `&&` (AND) operator returns `true` only if **both** sides are true.

Think about a real-world scenario: "You can watch a movie if you've finished your homework AND it's after 8 PM."

If you've finished your homework but it's only 6 PM, can you watch the movie? Why or why not?