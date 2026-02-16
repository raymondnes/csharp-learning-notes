# Arithmetic Operators - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Arithmetic Operators concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a program that demonstrates all five basic arithmetic operators.

**Requirements:**
1. Declare two integer variables with values 15 and 4
2. Display the result of addition, subtraction, multiplication, division, and modulus
3. Label each output clearly

**Expected Output:**
```
Addition: 15 + 4 = 19
Subtraction: 15 - 4 = 11
Multiplication: 15 * 4 = 60
Division: 15 / 4 = 3
Modulus: 15 % 4 = 3
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a simple calculator that adds three numbers and shows the total and average.

**Requirements:**
1. Store three integer values: 25, 30, and 45
2. Calculate their sum
3. Calculate their average as a decimal number
4. Display both results with clear labels

**Expected Output:**
```
Number 1: 25
Number 2: 30
Number 3: 45
Sum: 100
Average: 33.33
```

---

## Level 3: Application (Easy)

**Challenge:** Demonstrate the difference between integer division and floating-point division.

**Requirements:**
1. Divide 17 by 5 using integer division
2. Divide 17 by 5 using floating-point division
3. Show both results and explain the difference in comments
4. Include an example where integer division would cause a logic error

**Expected Output:**
```
Integer division: 17 / 5 = 3
Floating-point division: 17 / 5 = 3.4
Lost precision: 0.4

Real-world example:
Splitting $17 among 5 people:
  Integer division says: $3 each (loses $2!)
  Correct calculation: $3.40 each
```

---

## Level 4: Application (Easy)

**Challenge:** Create a program that uses the modulus operator to solve practical problems.

**Requirements:**
1. Check if 127 is even or odd using modulus
2. Extract the last digit of 9876 using modulus
3. Calculate how many weeks and remaining days are in 45 days
4. Determine if 144 is divisible by 12

**Expected Output:**
```
Is 127 even? False (127 % 2 = 1)
Last digit of 9876: 6
45 days = 6 weeks and 3 days
Is 144 divisible by 12? True (144 % 12 = 0)
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a time converter that breaks down total seconds into hours, minutes, and seconds.

**Requirements:**
1. Start with a total of 7385 seconds
2. Calculate hours, minutes, and remaining seconds
3. Use division and modulus operators appropriately
4. Display in both raw and formatted output
5. Verify your math by converting back to total seconds

**Expected Output:**
```
Total seconds: 7385

Breakdown:
  Hours:   2
  Minutes: 3
  Seconds: 5

Formatted: 2:03:05

Verification: (2 * 3600) + (3 * 60) + 5 = 7385 ✓
```

---

## Level 6: Integration (Moderate)

**Challenge:** Demonstrate compound assignment operators and increment/decrement behavior.

**Requirements:**
1. Start with a variable `value = 100`
2. Apply each compound operator (`+=`, `-=`, `*=`, `/=`, `%=`) sequentially, showing the result after each
3. Demonstrate the difference between pre-increment and post-increment
4. Show what value is returned/used in each case

**Expected Output:**
```
Starting value: 100

Compound operators (sequential):
  After += 50:  150
  After -= 30:  120
  After *= 2:   240
  After /= 4:   60
  After %= 7:   4

Pre vs Post increment:
  x = 10
  Post-increment (x++): returns 10, x is now 11
  Pre-increment (++x):  returns 12, x is now 12

Pre vs Post decrement:
  y = 10
  Post-decrement (y--): returns 10, y is now 9
  Pre-decrement (--y):  returns 8, y is now 8
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a financial calculator that handles currency calculations with proper precision.

**Requirements:**
1. Calculate the total cost of items: 3 items at $19.99 each
2. Apply an 8.25% sales tax
3. Apply a 15% discount to the subtotal (before tax)
4. Calculate the final total: (subtotal - discount) + tax
5. Use `decimal` type for all currency calculations
6. Format all output to 2 decimal places with dollar signs
7. Show the savings from the discount

**Expected Output:**
```
╔═══════════════════════════════════════╗
║         PURCHASE SUMMARY              ║
╠═══════════════════════════════════════╣
║ Items: 3 × $19.99        =    $59.97  ║
║ Discount (15%):          -     $9.00  ║
║ ─────────────────────────────────────║
║ Subtotal:                =    $50.97  ║
║ Tax (8.25%):             +     $4.21  ║
║ ─────────────────────────────────────║
║ TOTAL:                   =    $55.18  ║
╠═══════════════════════════════════════╣
║ You saved $9.00 today!                ║
╚═══════════════════════════════════════╝
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a program that demonstrates operator precedence with complex expressions.

**Requirements:**
1. Evaluate: `2 + 3 * 4 - 6 / 2` and show step-by-step evaluation
2. Show how parentheses change the result of the same numbers
3. Create an expression that demonstrates left-to-right evaluation
4. Create a "guess the result" section with 4 expressions of increasing complexity
5. Include the actual results and explanations

**Expected Output:**
```
═══ OPERATOR PRECEDENCE DEMONSTRATION ═══

Expression: 2 + 3 * 4 - 6 / 2

Step-by-step (following precedence):
  1. 3 * 4 = 12
  2. 6 / 2 = 3
  3. 2 + 12 - 3 = 11
Result: 11

Same numbers with parentheses:
  (2 + 3) * 4 - 6 / 2  = 17
  2 + 3 * (4 - 6) / 2  = 0
  (2 + 3) * (4 - 6) / 2 = -5
  ((2 + 3) * 4 - 6) / 2 = 7

Left-to-right evaluation:
  100 / 10 / 2 = ?
  Step 1: 100 / 10 = 10
  Step 2: 10 / 2 = 5
  Result: 5 (NOT 100 / 5 = 20!)

═══ CHALLENGE: GUESS THE RESULT ═══

Expression 1: 8 + 4 / 2
  Answer: 10 (division first: 4/2=2, then 8+2=10)

Expression 2: 15 % 4 * 3 + 1
  Answer: 10 (15%4=3, 3*3=9, 9+1=10)

Expression 3: 20 / 4 / 2 + 3 * 2
  Answer: 8 (20/4=5, 5/2=2, 3*2=6, 2+6=8)

Expression 4: 5 + 10 % 3 * 2 - 8 / 4
  Answer: 5 (10%3=1, 1*2=2, 8/4=2, 5+2-2=5)
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive "Math Expression Analyzer" that evaluates and explains expressions.

**Requirements:**
1. Create a program that takes a mathematical scenario and computes multiple related values
2. Implement calculations for a loan payment scenario:
   - Principal: $10,000
   - Annual interest rate: 6%
   - Loan term: 3 years
3. Calculate:
   - Monthly interest rate
   - Total number of payments
   - Simple interest over the term
   - Total amount with simple interest
   - Monthly payment (simple interest model)
4. Create a compound interest comparison showing yearly balance for 5 years
5. Use the formula: A = P(1 + r)^n for compound interest (use Math.Pow)
6. Show a formatted amortization-style summary table
7. Calculate and display the difference between simple and compound interest
8. All monetary values must use `decimal` and show 2 decimal places

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    LOAN ANALYSIS CALCULATOR                       ║
╠══════════════════════════════════════════════════════════════════╣
║ LOAN PARAMETERS                                                   ║
╠══════════════════════════════════════════════════════════════════╣
║ Principal Amount:           $10,000.00                            ║
║ Annual Interest Rate:       6.00%                                 ║
║ Loan Term:                  3 years (36 months)                   ║
╠══════════════════════════════════════════════════════════════════╣
║ SIMPLE INTEREST CALCULATION                                       ║
╠══════════════════════════════════════════════════════════════════╣
║ Monthly Interest Rate:      0.50%                                 ║
║ Total Interest:             $1,800.00                             ║
║ Total Repayment:            $11,800.00                            ║
║ Monthly Payment:            $327.78                               ║
╠══════════════════════════════════════════════════════════════════╣
║ COMPOUND INTEREST COMPARISON (5 YEARS)                            ║
╠═══════╤════════════════╤═════════════════╤═══════════════════════╣
║ Year  │ Simple Interest │ Compound Interest│ Difference           ║
╠═══════╪════════════════╪═════════════════╪═══════════════════════╣
║   1   │    $10,600.00  │     $10,600.00  │           $0.00       ║
║   2   │    $11,200.00  │     $11,236.00  │          $36.00       ║
║   3   │    $11,800.00  │     $11,910.16  │         $110.16       ║
║   4   │    $12,400.00  │     $12,624.77  │         $224.77       ║
║   5   │    $13,000.00  │     $13,382.26  │         $382.26       ║
╠══════════════════════════════════════════════════════════════════╣
║ SUMMARY                                                           ║
╠══════════════════════════════════════════════════════════════════╣
║ After 5 years:                                                    ║
║   Simple Interest Total:    $13,000.00 (Interest: $3,000.00)     ║
║   Compound Interest Total:  $13,382.26 (Interest: $3,382.26)     ║
║   Additional cost (compound): $382.26                             ║
║   Compound interest is 12.74% more expensive over 5 years         ║
╚══════════════════════════════════════════════════════════════════╝
```

**Additional Requirements:**
- Use appropriate arithmetic operators throughout
- Demonstrate compound assignment where appropriate
- All calculations must be mathematically correct
- Include comments explaining the formulas used
- Code must be well-organized and readable

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Are calculations accurate?
2. **Operator Usage**: Are the right operators used?
3. **Type Handling**: Are appropriate numeric types used?
4. **Code Quality**: Is the code clean and readable?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Arithmetic Operators concepts.*
