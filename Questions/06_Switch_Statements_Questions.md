# Switch Statements - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Switch Statements concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a program that converts a number (1-7) to its corresponding day of the week.

**Requirements:**
1. Declare an integer variable with a value between 1 and 7
2. Use a switch statement to convert to day name
3. Include a default case for invalid numbers
4. Display the result

**Expected Output (for value 3):**
```
Day number: 3
Day name: Wednesday
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a simple calculator using a switch statement with character operations.

**Requirements:**
1. Declare two number variables and a char for the operation
2. Use switch to handle: '+', '-', '*', '/'
3. Display the calculation and result
4. Include a default case for unknown operations

**Expected Output (10, 3, '+'):**
```
10 + 3 = 13
```

---

## Level 3: Application (Easy)

**Challenge:** Create a month-to-season converter using case grouping.

**Requirements:**
1. Accept a month number (1-12)
2. Use case grouping to assign seasons:
   - December, January, February → Winter
   - March, April, May → Spring
   - June, July, August → Summer
   - September, October, November → Fall
3. Display the month number and season

**Expected Output (month = 7):**
```
Month 7 is in Summer
```

---

## Level 4: Application (Easy)

**Challenge:** Create a letter grade interpreter using switch with strings.

**Requirements:**
1. Accept a letter grade (A, B, C, D, F)
2. Use switch to provide feedback for each grade:
   - A: "Excellent! Outstanding performance."
   - B: "Good job! Above average."
   - C: "Satisfactory. Meets expectations."
   - D: "Needs improvement. Below average."
   - F: "Failed. Please seek additional help."
3. Handle both uppercase and lowercase input
4. Handle invalid grades

**Expected Output (grade = "B"):**
```
Grade: B
Feedback: Good job! Above average.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a switch expression to calculate shipping costs based on region.

**Requirements:**
1. Use a switch expression (C# 8.0+ syntax)
2. Calculate shipping based on region code:
   - "LOCAL" → $5.00
   - "REGIONAL" → $10.00
   - "NATIONAL" → $20.00
   - "INTERNATIONAL" → $50.00
   - Unknown → $0.00 with error message
3. Apply a weight multiplier: if weight > 10kg, multiply cost by 1.5
4. Display itemized breakdown

**Expected Output:**
```
Region: NATIONAL
Weight: 15 kg

Base shipping cost: $20.00
Weight surcharge (50%): $10.00
Total shipping: $30.00
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a vending machine simulator using switch with pattern matching.

**Requirements:**
1. Define product codes: A1, A2, B1, B2, C1, C2
2. Each product has a name and price
3. Accept money input and product code
4. Use switch to:
   - Identify the product
   - Check if enough money was provided
   - Calculate change
5. Handle invalid product codes
6. Display transaction summary

**Products:**
- A1: Cola - $1.50
- A2: Diet Cola - $1.50
- B1: Chips - $2.00
- B2: Pretzels - $1.75
- C1: Candy Bar - $1.25
- C2: Gum - $0.75

**Expected Output:**
```
╔═══════════════════════════════════╗
║      VENDING MACHINE              ║
╠═══════════════════════════════════╣
║ Selection: B1                     ║
║ Product: Chips                    ║
║ Price: $2.00                      ║
╠═══════════════════════════════════╣
║ Money Inserted: $5.00             ║
║ Change Due: $3.00                 ║
╠═══════════════════════════════════╣
║ Thank you for your purchase!      ║
╚═══════════════════════════════════╝
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a Roman numeral converter using switch with pattern matching and when guards.

**Requirements:**
1. Convert integers (1-3999) to Roman numerals
2. Use switch expressions with relational patterns
3. Handle invalid input (numbers outside range)
4. Display the conversion step by step

**Roman Numeral Rules:**
- I = 1, V = 5, X = 10, L = 50, C = 100, D = 500, M = 1000
- Use subtractive notation: IV = 4, IX = 9, XL = 40, XC = 90, CD = 400, CM = 900

**Expected Output (number = 1994):**
```
Converting 1994 to Roman Numerals:

Step-by-step breakdown:
  1994 ÷ 1000 = 1 remainder 994  → M
   994 ÷  900 = 1 remainder  94  → CM
    94 ÷   90 = 1 remainder   4  → XC
     4 ÷    4 = 1 remainder   0  → IV

Result: MCMXCIV
Verification: M(1000) + CM(900) + XC(90) + IV(4) = 1994 ✓
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a command-line game action handler using switch with tuples and multiple patterns.

**Requirements:**
1. Handle player actions in a simple RPG system
2. Actions are tuples: (action, target, modifier)
3. Use switch expression with tuple patterns
4. Actions to handle:
   - ("attack", enemy, weapon) → Calculate damage
   - ("heal", "self", item) → Restore health
   - ("defend", _, _) → Enter defensive stance
   - ("cast", target, spell) → Cast spell with mana cost
   - ("flee", _, _) → Attempt escape
5. Track and display player stats (health, mana)
6. Handle invalid action combinations

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║                    BATTLE SYSTEM                              ║
╠══════════════════════════════════════════════════════════════╣
║ Player Status: HP: 80/100  |  Mana: 45/50                    ║
╠══════════════════════════════════════════════════════════════╣

Action: ("cast", "goblin", "fireball")

╠══════════════════════════════════════════════════════════════╣
║ CAST SPELL: Fireball                                          ║
║ Target: Goblin                                                ║
║ Mana Cost: 15                                                 ║
║ Damage Dealt: 35                                              ║
╠══════════════════════════════════════════════════════════════╣
║ Updated Status: HP: 80/100  |  Mana: 30/50                   ║
╚══════════════════════════════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive "Expression Parser" that evaluates mathematical expressions using switch patterns.

**Requirements:**
1. Parse and evaluate simple mathematical expressions
2. Support operations: +, -, *, /, ^(power), %(modulo)
3. Support parentheses for grouping
4. Support special functions: sqrt, abs, sin, cos, log
5. Use switch expressions with pattern matching extensively
6. Implement operator precedence
7. Provide step-by-step evaluation breakdown
8. Handle errors gracefully (division by zero, invalid syntax)

**Token Types to Handle:**
- Numbers (integers and decimals)
- Operators (+, -, *, /, ^, %)
- Functions (sqrt, abs, sin, cos, log)
- Parentheses
- Negative numbers

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    EXPRESSION PARSER                              ║
╠══════════════════════════════════════════════════════════════════╣
║ Expression: sqrt(16) + 2 * (3 + 4) ^ 2                           ║
╠══════════════════════════════════════════════════════════════════╣
║                    PARSING BREAKDOWN                              ║
╠══════════════════════════════════════════════════════════════════╣
║ Tokens identified:                                                ║
║   [FUNC: sqrt] [PAREN: (] [NUM: 16] [PAREN: )]                   ║
║   [OP: +] [NUM: 2] [OP: *] [PAREN: (] [NUM: 3]                   ║
║   [OP: +] [NUM: 4] [PAREN: )] [OP: ^] [NUM: 2]                   ║
╠══════════════════════════════════════════════════════════════════╣
║                    EVALUATION STEPS                               ║
╠══════════════════════════════════════════════════════════════════╣
║ Step 1: sqrt(16) = 4                                              ║
║ Step 2: (3 + 4) = 7                                               ║
║ Step 3: 7 ^ 2 = 49                                                ║
║ Step 4: 2 * 49 = 98                                               ║
║ Step 5: 4 + 98 = 102                                              ║
╠══════════════════════════════════════════════════════════════════╣
║ RESULT: 102                                                       ║
╚══════════════════════════════════════════════════════════════════╝

Additional test cases:
─────────────────────────────────────────────────────────────────────
Expression: abs(-5) + abs(5)
Result: 10

Expression: 10 / (5 - 5)
Error: Division by zero at position 4

Expression: sin(0) + cos(0)
Result: 1 (sin(0)=0, cos(0)=1)

Expression: 2 ^ 3 ^ 2
Result: 512 (right-to-left: 3^2=9, 2^9=512)
```

**Code Requirements:**
- Use switch expressions for token classification
- Use switch for operation dispatch
- Use pattern matching for expression type detection
- Implement proper error handling with switch
- Include at least 5 different switch/switch expression usages
- Code must be modular with helper methods
- Include comprehensive comments

**Evaluation Note:** You don't need to implement a full recursive descent parser. A simplified approach that handles the shown examples is acceptable. Focus on demonstrating switch statement mastery.

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does the switch logic work properly?
2. **Syntax**: Is switch/switch expression syntax correct?
3. **Pattern Usage**: Are appropriate patterns used?
4. **Code Quality**: Is the code clean and well-organized?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Switch Statements concepts.*
