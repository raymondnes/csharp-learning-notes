# Methods - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Methods concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create and call a simple method that prints a greeting.

**Requirements:**
1. Create a void method named `SayHello` with no parameters
2. The method should print "Hello, World!"
3. Call the method three times from main

**Expected Output:**
```
Hello, World!
Hello, World!
Hello, World!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a method that takes a parameter and uses it.

**Requirements:**
1. Create a method `GreetPerson` that takes a `string name` parameter
2. The method should print "Hello, [name]!"
3. Call it with three different names

**Expected Output:**
```
Hello, Alice!
Hello, Bob!
Hello, Charlie!
```

---

## Level 3: Application (Easy)

**Challenge:** Create a method that returns a calculated value.

**Requirements:**
1. Create a method `CalculateCircleArea` that takes a `double radius` parameter
2. Return the area using formula: π × r²
3. Use `Math.PI` for pi
4. Call the method with radius 5 and display the result formatted to 2 decimal places

**Expected Output:**
```
Circle with radius 5.0
Area: 78.54 square units
```

---

## Level 4: Application (Easy)

**Challenge:** Create multiple overloaded methods.

**Requirements:**
1. Create three versions of a `Multiply` method:
   - Two integers
   - Two doubles
   - Three integers
2. Demonstrate calling each version
3. Show the result of each call

**Expected Output:**
```
Multiply(3, 4) = 12
Multiply(2.5, 3.5) = 8.75
Multiply(2, 3, 4) = 24
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a method with default parameters and demonstrate its usage.

**Requirements:**
1. Create a method `FormatPrice` with parameters:
   - `decimal amount` (required)
   - `string currency` (default: "$")
   - `int decimals` (default: 2)
2. The method should return a formatted price string
3. Demonstrate calling with: all parameters, only amount, and amount with currency

**Expected Output:**
```
Full params: FormatPrice(19.99, "€", 2) = €19.99
Default decimals: FormatPrice(19.99, "£") = £19.99
All defaults: FormatPrice(19.99) = $19.99
More decimals: FormatPrice(19.999, "$", 3) = $19.999
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a suite of string utility methods.

**Requirements:**
1. Create these methods:
   - `string Reverse(string text)` - reverses the string
   - `int CountVowels(string text)` - counts vowels
   - `bool IsPalindrome(string text)` - checks if palindrome (ignore case)
   - `string TruncateWithEllipsis(string text, int maxLength)` - truncates and adds "..."
2. Demonstrate each method with appropriate test cases

**Expected Output:**
```
String Utilities Demo
=====================

Reverse:
  "Hello" → "olleH"
  "CSharp" → "prahSC"

Count Vowels:
  "Hello World" has 3 vowels
  "AEIOU" has 5 vowels

Is Palindrome:
  "Radar" → True
  "Hello" → False
  "A man a plan a canal Panama" → True (ignoring spaces)

Truncate:
  "Hello World" (max 5) → "Hello..."
  "Hi" (max 5) → "Hi"
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a mini math library with multiple helper methods.

**Requirements:**
1. Create these methods:
   - `bool IsPrime(int n)` - checks if number is prime
   - `int Factorial(int n)` - calculates n!
   - `int GCD(int a, int b)` - finds greatest common divisor
   - `int LCM(int a, int b)` - finds least common multiple (use GCD)
   - `int[] GetFactors(int n)` - returns array of all factors
2. Use helper methods within other methods where appropriate
3. Handle edge cases (negative numbers, zero)

**Expected Output:**
```
╔═══════════════════════════════════════════════════════════════╗
║                    MATH LIBRARY DEMO                           ║
╠═══════════════════════════════════════════════════════════════╣
║ Prime Check:                                                   ║
║   IsPrime(17) = True                                           ║
║   IsPrime(18) = False                                          ║
║   IsPrime(2) = True                                            ║
║   IsPrime(1) = False                                           ║
╠═══════════════════════════════════════════════════════════════╣
║ Factorial:                                                     ║
║   5! = 120                                                     ║
║   0! = 1                                                       ║
║   10! = 3628800                                                ║
╠═══════════════════════════════════════════════════════════════╣
║ GCD & LCM:                                                     ║
║   GCD(48, 18) = 6                                              ║
║   LCM(48, 18) = 144                                            ║
║   GCD(100, 35) = 5                                             ║
║   LCM(100, 35) = 700                                           ║
╠═══════════════════════════════════════════════════════════════╣
║ Factors:                                                       ║
║   Factors of 24: [1, 2, 3, 4, 6, 8, 12, 24]                   ║
║   Factors of 17: [1, 17]                                       ║
╚═══════════════════════════════════════════════════════════════╝
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a text analysis toolkit with chained method calls.

**Requirements:**
1. Create these methods:
   - `int WordCount(string text)`
   - `int SentenceCount(string text)` - count '.', '!', '?'
   - `int CharacterCount(string text, bool includeSpaces)`
   - `Dictionary<char, int> LetterFrequency(string text)` - count each letter
   - `double AverageWordLength(string text)`
   - `string GetReadabilityLevel(string text)` - uses other methods
2. Analyze a sample paragraph using all methods
3. Display comprehensive statistics

**Test paragraph:**
```
"The quick brown fox jumps over the lazy dog. This sentence contains every letter of the alphabet! How amazing is that?"
```

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    TEXT ANALYSIS REPORT                           ║
╠══════════════════════════════════════════════════════════════════╣
║ Text: "The quick brown fox jumps over the lazy dog. This         ║
║       sentence contains every letter of the alphabet!            ║
║       How amazing is that?"                                       ║
╠══════════════════════════════════════════════════════════════════╣
║                      BASIC STATISTICS                             ║
╠══════════════════════════════════════════════════════════════════╣
║ Word Count:              20                                       ║
║ Sentence Count:          3                                        ║
║ Characters (with spaces): 113                                     ║
║ Characters (no spaces):   94                                      ║
║ Average Word Length:      4.7                                     ║
╠══════════════════════════════════════════════════════════════════╣
║                    LETTER FREQUENCY                               ║
╠══════════════════════════════════════════════════════════════════╣
║ Most common: e (11), t (8), a (6), o (5), h (5)                  ║
║ Least common: j (1), k (1), p (1), x (1), z (1)                  ║
║ Unique letters: 26 (complete alphabet!)                          ║
╠══════════════════════════════════════════════════════════════════╣
║ Readability Level: Easy (Short sentences, common words)          ║
╚══════════════════════════════════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a complete expression calculator using methods.

**Requirements:**
1. Create a calculator that evaluates simple expressions
2. Required methods:
   - `double Evaluate(string expression)` - main entry point
   - `double PerformOperation(double a, double b, char op)`
   - `(double value, int nextIndex) ParseNumber(string expr, int startIndex)`
   - `bool IsOperator(char c)`
   - `int GetPrecedence(char op)`
   - `double ApplyFunction(string func, double value)`
3. Support:
   - Basic operations: +, -, *, /
   - Parentheses for grouping
   - Functions: sqrt, abs, sin, cos
   - Negative numbers
4. Implement proper operator precedence
5. Handle errors gracefully with meaningful messages

**Test expressions:**
```
"3 + 4 * 2"
"(3 + 4) * 2"
"10 / 2 - 3"
"sqrt(16) + abs(-5)"
"2 * (3 + 4) - sqrt(9)"
"-5 + 3"
"10 / 0" (should handle error)
```

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                  EXPRESSION CALCULATOR                            ║
╠══════════════════════════════════════════════════════════════════╣

Expression: 3 + 4 * 2
Parsing: [3] [+] [4] [*] [2]
Evaluation:
  Step 1: 4 * 2 = 8 (higher precedence)
  Step 2: 3 + 8 = 11
Result: 11

────────────────────────────────────────────────────────────────────

Expression: (3 + 4) * 2
Parsing: [(] [3] [+] [4] [)] [*] [2]
Evaluation:
  Step 1: (3 + 4) = 7 (parentheses first)
  Step 2: 7 * 2 = 14
Result: 14

────────────────────────────────────────────────────────────────────

Expression: sqrt(16) + abs(-5)
Parsing: [sqrt] [(] [16] [)] [+] [abs] [(] [-5] [)]
Evaluation:
  Step 1: sqrt(16) = 4
  Step 2: abs(-5) = 5
  Step 3: 4 + 5 = 9
Result: 9

────────────────────────────────────────────────────────────────────

Expression: 2 * (3 + 4) - sqrt(9)
Evaluation:
  Step 1: (3 + 4) = 7
  Step 2: sqrt(9) = 3
  Step 3: 2 * 7 = 14
  Step 4: 14 - 3 = 11
Result: 11

────────────────────────────────────────────────────────────────────

Expression: 10 / 0
Error: Division by zero

────────────────────────────────────────────────────────────────────

Expression: -5 + 3
Result: -2

╠══════════════════════════════════════════════════════════════════╣
║                        SUMMARY                                    ║
╠══════════════════════════════════════════════════════════════════╣
║ Expressions evaluated: 7                                          ║
║ Successful: 6                                                     ║
║ Errors: 1                                                         ║
╚══════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use multiple helper methods (minimum 5)
- Demonstrate method composition (methods calling other methods)
- Use return values effectively
- Include expression-bodied methods where appropriate
- Handle edge cases
- Include comprehensive comments

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Do methods work as specified?
2. **Method Design**: Proper return types, parameters, naming?
3. **Code Organization**: Methods used appropriately?
4. **Best Practices**: Following C# conventions?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Methods concepts.*
