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

**Student's Response:**
I can't watch a movie because the second condition is not met which is 8 pm

**Professor's Response:**
Exactly! This perfectly demonstrates how the `&&` (AND) operator works:

- Condition 1: Homework finished? ✓ `true`
- Condition 2: After 8 PM? ✗ `false` (it's only 6 PM)
- **Result**: `true && false = false` → No movie!

With `&&`, **BOTH** conditions must be true for the overall result to be true. If even one condition is false, the entire expression is false.

Now let's think about the `||` (OR) operator:

**Professor's Socratic Question (Understanding OR):**

The `||` (OR) operator returns `true` if **either** (or both) conditions are true.

Using a similar scenario: "You can have dessert if you ate all your vegetables OR it's your birthday."

If you didn't eat your vegetables but it IS your birthday, can you have dessert? Why or why not?

**Student's Response:**
Yes, because one condition is met, my birthday

**Professor's Response:**
Perfect! This demonstrates how the `||` (OR) operator works:

- Condition 1: Ate all vegetables? ✗ `false`
- Condition 2: Is it your birthday? ✓ `true`
- **Result**: `false || true = true` → You get dessert!

With `||`, you only need **ONE** condition to be true for the overall result to be true. Both can be true, or just one - either way, the result is `true`.

Here's a summary of the logical operators:

**AND (`&&`) - Both must be true:**
- `true && true = true`
- `true && false = false`
- `false && true = false`
- `false && false = false`

**OR (`||`) - At least one must be true:**
- `true || true = true`
- `true || false = true`
- `false || true = true`
- `false || false = false`

**NOT (`!`) - Inverts the value:**
- `!true = false`
- `!false = true`

**Professor's Instruction:**

Let's see these in action. Add this code to your program:

```csharp
Console.WriteLine("\n--- Logical Operators ---");
bool hasLicense = true;
bool hasInsurance = false;
int age = 18;

Console.WriteLine($"hasLicense: {hasLicense}");
Console.WriteLine($"hasInsurance: {hasInsurance}");
Console.WriteLine($"age: {age}");
Console.WriteLine();

// AND operator
Console.WriteLine($"hasLicense && hasInsurance: {hasLicense && hasInsurance}");

// OR operator
Console.WriteLine($"hasLicense || hasInsurance: {hasLicense || hasInsurance}");

// NOT operator
Console.WriteLine($"!hasLicense: {!hasLicense}");
Console.WriteLine($"!hasInsurance: {!hasInsurance}");

// Combining comparison and logical operators
Console.WriteLine($"age >= 18 && hasLicense: {age >= 18 && hasLicense}");
```

Run your program and show me the output.

**Student's Response:**
```
--- Logical Operators ---
hasLicense: True
hasInsurance: False
age: 18

hasLicense && hasInsurance: False
hasLicense || hasInsurance: True
!hasLicense: False
!hasInsurance: True
age >= 18 && hasLicense: True
```

**Professor's Response:**
Excellent! Let's analyze each result to reinforce your understanding:

1. **`hasLicense && hasInsurance: False`**
   - `true && false = false`
   - You have a license BUT no insurance, so the AND condition fails

2. **`hasLicense || hasInsurance: True`**
   - `true || false = true`
   - You have at least ONE (the license), so the OR condition succeeds

3. **`!hasLicense: False`**
   - `!true = false`
   - NOT inverts the value - you DO have a license, so NOT having one is false

4. **`!hasInsurance: True`**
   - `!false = true`
   - You DON'T have insurance (false), so NOT having insurance is true

5. **`age >= 18 && hasLicense: True`**
   - `true && true = true`
   - This combines a comparison operator (`>=`) with a logical operator (`&&`)
   - Age is 18 or older (true) AND you have a license (true) → both true!

This last example is powerful - you're combining **comparison** and **logical** operators to create complex conditions. This is exactly how you'll make sophisticated decisions in your programs!

---

## **Compound Assignment Operators**

Before we wrap up, there's one more set of operators to learn: **compound assignment operators**. You've actually already used some of these on Day 2!

Remember when you wrote `counter += 5`? That's a compound assignment operator - a shorthand for common operations.

| Operator | Meaning           | Example      | Equivalent To  |
|----------|-------------------|--------------|----------------|
| `+=`     | Add and assign    | `x += 5`     | `x = x + 5`    |
| `-=`     | Subtract and assign | `x -= 3`   | `x = x - 3`    |
| `*=`     | Multiply and assign | `x *= 2`   | `x = x * 2`    |
| `/=`     | Divide and assign   | `x /= 4`   | `x = x / 4`    |
| `%=`     | Modulus and assign  | `x %= 3`   | `x = x % 3`    |

These are just convenient shortcuts - they make your code more concise and readable.

**Professor's Socratic Question (Understanding Compound Operators):**

If you have `int score = 100;` and then you write `score += 50;`, what's the value of `score` afterward?

And why might a programmer prefer writing `score += 50` instead of `score = score + 50`?

**Student's Response:**
The value of score is 150. A programmer prefers += because it's readable and concise

**Professor's Response:**
Exactly right on both counts!

- `score` starts at 100
- `score += 50` means `score = score + 50`
- So `score = 100 + 50 = 150`

And you've identified the key benefits of compound assignment operators:

1. ✓ **More concise** - Less typing, shorter code
2. ✓ **More readable** - The intent is clear: "add 50 to score"
3. ✓ **Less error-prone** - You don't repeat the variable name, so no chance of typos like `score = scroe + 50`

These operators become especially valuable when you have longer variable names:

```csharp
playerExperiencePoints += 250;  // Clean and clear

// vs

playerExperiencePoints = playerExperiencePoints + 250;  // More verbose, easier to mistype
```

---

## **Day 3 Summary**

Excellent work today! You've mastered a comprehensive set of operators in C#. Let me summarize everything you've learned:

**✓ Arithmetic Operators:**
- `+` (addition), `-` (subtraction), `*` (multiplication), `/` (division), `%` (modulus)
- Integer division truncates the decimal part
- Type casting with `(double)` to get decimal results from integer division

**✓ Comparison Operators:**
- `==` (equal), `!=` (not equal), `>`, `<`, `>=`, `<=`
- Return boolean values (`true` or `false`)
- `=` is assignment, `==` is comparison

**✓ Logical Operators:**
- `&&` (AND) - both conditions must be true
- `||` (OR) - at least one condition must be true
- `!` (NOT) - inverts the boolean value
- Can combine with comparison operators for complex conditions

**✓ Compound Assignment Operators:**
- `+=`, `-=`, `*=`, `/=`, `%=`
- Shorthand for common operations
- More concise and readable

**✓ Operator Precedence:**
- C# follows BODMAS/PEMDAS rules
- Parentheses override default precedence
- The type of operands determines the type of result

**Key Insights You Discovered:**

1. **Type matters in operations** - `int / int` gives `int`, not `double`
2. **Casting must happen before division** - Converting the result after is too late
3. **Logical operators mirror real-world logic** - AND requires all, OR requires any
4. **Compound operators improve code quality** - Less repetition, fewer errors

You've shown excellent understanding throughout today's lesson, experimenting with code and making logical connections between concepts!

**Professor's Final Reflection:**

You've now built a solid foundation in both **data storage** (variables and types from Day 2) and **data manipulation** (operators from Day 3). Before we move to Test_Agent, let me ask you one final reflective question:

Can you think of a practical scenario where you'd need to combine multiple types of operators - arithmetic, comparison, AND logical - all in one expression? What might that look like?

**Student's Response:**
```csharp
Console.WriteLine("\n----Combining multiple types of Operators (Arithmetic, Comparison and Logical) in One scenario");
Console.WriteLine("=== BANKING APP ===");
double accountBalance = 200;
double withdrawalAmount = 150;
bool isAccountActive = true;

Console.WriteLine($"Account Balance: {accountBalance}");
Console.WriteLine($"Withdrawal Amount: {withdrawalAmount}");
Console.WriteLine($"Is Account Active: {isAccountActive}");

bool canWithdraw = isAccountActive && (accountBalance >= withdrawalAmount) && ((accountBalance - withdrawalAmount) >= 0);
Console.WriteLine($"Can Withdraw: {canWithdraw}");
```

**Professor's Response:**
Outstanding! This is a **perfect** real-world example that combines all three operator types beautifully! Let me break down what makes this so impressive:

**Your expression:**
```csharp
bool canWithdraw = isAccountActive && (accountBalance >= withdrawalAmount) && ((accountBalance - withdrawalAmount) >= 0);
```

**Breaking it down:**

1. **Logical Operator (`&&`)**:
   - Combines three conditions that ALL must be true

2. **Comparison Operators (`>=`)**:
   - `accountBalance >= withdrawalAmount` → Do you have enough money?
   - `(accountBalance - withdrawalAmount) >= 0` → Will you have a non-negative balance after?

3. **Arithmetic Operator (`-`)**:
   - `accountBalance - withdrawalAmount` → Calculate the resulting balance

4. **Boolean check**:
   - `isAccountActive` → Is the account even active?

**Why this is excellent:**

✓ **Realistic scenario** - Banking apps do exactly this kind of validation
✓ **Multiple safeguards** - Checking account status AND sufficient funds AND non-negative result
✓ **Proper use of parentheses** - Making the order of operations explicit
✓ **Professional naming** - `canWithdraw` clearly indicates a boolean check

**One minor optimization note:**

The second and third conditions are actually redundant:
- If `accountBalance >= withdrawalAmount` is true, then `(accountBalance - withdrawalAmount) >= 0` is automatically true

So you could simplify to:
```csharp
bool canWithdraw = isAccountActive && (accountBalance >= withdrawalAmount);
```

But your version shows excellent defensive programming - being extra cautious about validation! In real banking systems, this kind of "double-checking" is often preferred for critical operations.

**This demonstrates mastery of:**
- Combining operator types
- Thinking through real-world logic
- Defensive programming practices
- Clean, readable code structure

Exceptional work! You've shown that you don't just understand operators syntactically - you understand how to apply them to solve real problems.

---

## **Test_Agent Invocation**

You've demonstrated deep understanding of operators and expressions through our Socratic dialogue. You can:
- Explain operator precedence and use parentheses effectively
- Distinguish between integer and floating-point division
- Apply type casting to control operation results
- Combine comparison and logical operators for complex conditions
- Use compound assignment operators idiomatically
- Create realistic scenarios that integrate multiple operator types

I'm now handing you over to Test_Agent for the Day 3 assessment. Test_Agent will verify your mastery through 9 progressive challenges covering:
- Arithmetic operations and operator precedence
- Integer vs. floating-point division
- Type casting in arithmetic
- Comparison operators
- Logical operators (AND, OR, NOT)
- Compound assignment operators
- Combining multiple operator types

Good luck!

---

## **Test_Agent Assessment - Day 3: Operators and Expressions**

**Concepts to be tested:**
- Arithmetic operators (+, -, *, /, %)
- Operator precedence and parentheses
- Integer division vs. floating-point division
- Type casting in arithmetic operations
- Comparison operators (==, !=, <, >, <=, >=)
- Logical operators (&&, ||, !)
- Compound assignment operators (+=, -=, *=, /=, %=)
- Combining multiple operator types

---

## **Level 1: Foundation (Trivial)**

**Challenge:**
Create a console application that performs basic arithmetic operations on two integers and displays the results.

**Requirements:**
1. Declare two `int` variables: `a = 20` and `b = 6`
2. Calculate and display the following using arithmetic operators:
   - Addition: `a + b`
   - Subtraction: `a - b`
   - Multiplication: `a * b`
   - Division: `a / b`
   - Modulus: `a % b`
3. Use string interpolation to display each result with a label

**Expected Output:**
```
a = 20, b = 6
Addition: 26
Subtraction: 14
Multiplication: 120
Division: 3
Modulus: 2
```

Show me your code and the output.

**Student's Code:**
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

✓ Correct variable declarations (`int a = 20`, `int b = 6`)
✓ All five arithmetic operators used correctly
✓ String interpolation used for all output
✓ Output format matches expected results exactly
✓ Division shows integer division (3, not 3.333...)
✓ Modulus correctly shows remainder (2)

**LEVEL 1: PASS**

---

## **Level 2: Foundation (Trivial)**

**Challenge:**
Demonstrate your understanding of operator precedence by evaluating expressions with multiple operators.

**Requirements:**
1. Declare three `int` variables: `x = 10`, `y = 5`, `z = 2`
2. Calculate and display the following expressions:
   - `x + y * z` (without parentheses)
   - `(x + y) * z` (with parentheses)
   - `x - y + z`
   - `x * y / z`
3. Use string interpolation to display each expression and its result

**Expected Output:**
```
x = 10, y = 5, z = 2
x + y * z = 20
(x + y) * z = 30
x - y + z = 7
x * y / z = 25
```

Show me your code and the output.

**Student's Code:**
```csharp
int x = 10;
int y = 5;
int z = 2;

Console.WriteLine($"x = {x}, y = {y}, z = {z}");
Console.WriteLine($"x + y * z = {x + y * z}");
Console.WriteLine($"(x + y) * z = {(x + y) * z}");
Console.WriteLine($"x - y + z = {x - y + z}");
Console.WriteLine($"x * y / z = {x * y / z}");
```

**Test_Agent Evaluation:**

✓ Correct variable declarations
✓ All expressions evaluated correctly
✓ `x + y * z = 20` (multiplication before addition: 10 + (5 * 2) = 10 + 10 = 20)
✓ `(x + y) * z = 30` (parentheses override precedence: (10 + 5) * 2 = 15 * 2 = 30)
✓ `x - y + z = 7` (left-to-right evaluation: 10 - 5 + 2 = 5 + 2 = 7)
✓ `x * y / z = 25` (left-to-right for equal precedence: 10 * 5 / 2 = 50 / 2 = 25)
✓ String interpolation used correctly
✓ Output matches expected format exactly

**LEVEL 2: PASS**

---

## **Level 3: Application (Easy)**

**Challenge:**
Create a program that demonstrates the difference between integer division and floating-point division.

**Requirements:**
1. Declare two `int` variables: `num1 = 17` and `num2 = 4`
2. Perform integer division and display the result
3. Perform the same division using `double` variables and display the result
4. Use string interpolation for all output

**Expected Output:**
```
Integer Division: 17 / 4 = 4
Floating-Point Division: 17.0 / 4.0 = 4.25
```

Show me your code and the output.

**Student's Code:**
```csharp
int num1 = 17;
int num2 = 4;
Console.WriteLine($"Integer Devision: 17 / 4 {num1 / num2}");
Console.WriteLine($"Floating-Point Division: 17.0 / 4.0 {(double)num1 / num2}");
```

**Test_Agent Evaluation:**

✓ Correct variable declarations
✓ Integer division performed correctly (17 / 4 = 4)
✓ Type casting used correctly `(double)num1` to get floating-point result
✓ Floating-point division result correct (4.25)
✓ String interpolation used
✗ Minor typo in output: "Devision" should be "Division"
✗ Output format doesn't match exactly - missing `=` sign between expression and result

**Expected format:** `Integer Division: 17 / 4 = 4`
**Your format:** `Integer Devision: 17 / 4 4`

The logic and code are correct, but the output formatting needs adjustment. Please fix the typo and add the `=` sign to match the expected output format exactly.

Try again!

**Student's Retry Code:**
```csharp
int num1 = 17;
int num2 = 4;
Console.WriteLine($"Integer Division: 17 / 4 = {num1 / num2}");
Console.WriteLine($"Floating-Point Division: 17.0 / 4.0 = {(double)num1 / num2}");
```

**Test_Agent Evaluation:**

✓ Correct variable declarations
✓ Integer division performed correctly (4)
✓ Floating-point division performed correctly (4.25)
✓ Type casting `(double)num1` used correctly
✓ String interpolation used for all output
✓ Typo fixed: "Division" spelled correctly
✓ Output format matches exactly with `=` sign
✓ Demonstrates clear understanding of integer vs. floating-point division

**LEVEL 3: PASS**

---

## **Level 4: Application (Easy)**

**Challenge:**
Create a program that uses comparison operators to compare two numbers and display the results.

**Requirements:**
1. Declare two `int` variables: `num1 = 15` and `num2 = 20`
2. Use all six comparison operators and display the results:
   - `==` (equal to)
   - `!=` (not equal to)
   - `>` (greater than)
   - `<` (less than)
   - `>=` (greater than or equal to)
   - `<=` (less than or equal to)
3. Use string interpolation for all output

**Expected Output:**
```
num1 = 15, num2 = 20
num1 == num2: False
num1 != num2: True
num1 > num2: False
num1 < num2: True
num1 >= num2: False
num1 <= num2: True
```

Show me your code and the output.

**Student's Code:**
```csharp
int num1 = 15;
int num2 = 20;
Console.WriteLine($"num1 = {num1}, num2 = {num2}");
Console.WriteLine($"num1 == num2: {num1 == num2}");
Console.WriteLine($"num1 != num2: {num1 != num2}");
Console.WriteLine($"num1 > num2: {num1 > num2}");
Console.WriteLine($"num1 < num2: {num1 < num2}");
Console.WriteLine($"num1 >= num2: {num1 >= num2}");
Console.WriteLine($"num1 <= num2: {num1 <= num2}");
```

**Test_Agent Evaluation:**

✓ Correct variable declarations (`num1 = 15`, `num2 = 20`)
✓ All six comparison operators used correctly
✓ All comparison results are correct
✓ String interpolation used for all output
✓ Output format matches expected results exactly
✓ Demonstrates understanding of all comparison operators

**LEVEL 4: PASS**

---

## **Level 5: Integration (Moderate)**

**Challenge:**
Create a program that uses logical operators (AND, OR, NOT) to evaluate complex conditions.

**Requirements:**
1. Declare three `bool` variables:
   - `hasDriverLicense = true`
   - `hasInsurance = false`
   - `isOver18 = true`
2. Evaluate and display the following conditions:
   - Can drive legally: `hasDriverLicense && hasInsurance && isOver18`
   - Has at least one requirement: `hasDriverLicense || hasInsurance || isOver18`
   - Missing insurance: `!hasInsurance`
   - Can drive with insurance waiver: `hasDriverLicense && isOver18 && !hasInsurance`
3. Use string interpolation for all output

**Expected Output:**
```
hasDriverLicense: True
hasInsurance: False
isOver18: True

Can drive legally: False
Has at least one requirement: True
Missing insurance: True
Can drive with insurance waiver: True
```

Show me your code and the output.
