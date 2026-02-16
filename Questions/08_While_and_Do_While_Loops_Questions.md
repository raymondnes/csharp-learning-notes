# While and Do-While Loops - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of While and Do-While Loops concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Print numbers from 1 to 10 using a while loop.

**Requirements:**
1. Initialize a counter variable
2. Use a while loop to print each number
3. Increment the counter in the loop body

**Expected Output:**
```
1
2
3
4
5
6
7
8
9
10
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a countdown using a do-while loop.

**Requirements:**
1. Start at 5
2. Use do-while to count down to 1
3. Print "Blast off!" after the countdown

**Expected Output:**
```
5
4
3
2
1
Blast off!
```

---

## Level 3: Application (Easy)

**Challenge:** Calculate the sum of digits in a number using a while loop.

**Requirements:**
1. Use the number 12345
2. Extract each digit using modulo and division
3. Sum all digits
4. Show the extraction process

**Expected Output:**
```
Number: 12345
Extracting digits:
  12345 % 10 = 5 (sum = 5)
  1234 % 10 = 4 (sum = 9)
  123 % 10 = 3 (sum = 12)
  12 % 10 = 2 (sum = 14)
  1 % 10 = 1 (sum = 15)
Sum of digits: 15
```

---

## Level 4: Application (Easy)

**Challenge:** Demonstrate the difference between while and do-while with a condition that's initially false.

**Requirements:**
1. Set a variable to 0
2. Use while to check if > 0 and print a message
3. Use do-while to check if > 0 and print a message
4. Show which loop executed and which didn't

**Expected Output:**
```
Value: 0
Condition: value > 0

Testing while loop:
  (No output - loop body never executed)

Testing do-while loop:
  Executed once! Value is: 0

Conclusion: do-while always executes at least once.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create an input validation system that keeps asking until valid input is received.

**Requirements:**
1. Simulate user input with an array of test values: ["abc", "-5", "0", "150", "42"]
2. Use a do-while loop to process each input
3. Valid input: positive integer between 1 and 100
4. Show validation messages for each attempt
5. Stop when valid input is found

**Expected Output:**
```
Input Validation System
=======================

Attempt 1: "abc"
  Error: Not a valid number.

Attempt 2: "-5"
  Error: Number must be positive.

Attempt 3: "0"
  Error: Number must be at least 1.

Attempt 4: "150"
  Error: Number must not exceed 100.

Attempt 5: "42"
  Success! Valid input received: 42

Total attempts: 5
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a number guessing game simulator.

**Requirements:**
1. Secret number is 7 (predetermined)
2. Guesses sequence: [3, 9, 5, 8, 7] (simulated)
3. Use a do-while loop
4. Provide "too high" or "too low" hints
5. Track number of attempts
6. Display victory message with attempt count

**Expected Output:**
```
╔═══════════════════════════════════════╗
║       NUMBER GUESSING GAME            ║
║    (Secret number: 1-10)              ║
╚═══════════════════════════════════════╝

Attempt 1: Guessing 3
  → Too low!

Attempt 2: Guessing 9
  → Too high!

Attempt 3: Guessing 5
  → Too low!

Attempt 4: Guessing 8
  → Too high!

Attempt 5: Guessing 7
  → Correct!

🎉 You won in 5 attempts!
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement the Collatz Conjecture sequence.

**Requirements:**
1. Start with number 27
2. Rules: If even, divide by 2. If odd, multiply by 3 and add 1.
3. Continue until reaching 1
4. Use a while loop
5. Track all numbers in the sequence
6. Display statistics: total steps, maximum value reached

**Expected Output:**
```
Collatz Conjecture for starting number: 27
===========================================

Sequence:
27 → 82 → 41 → 124 → 62 → 31 → 94 → 47 → 142 → 71 →
214 → 107 → 322 → 161 → 484 → 242 → 121 → 364 → 182 → 91 →
274 → 137 → 412 → 206 → 103 → 310 → 155 → 466 → 233 → 700 →
350 → 175 → 526 → 263 → 790 → 395 → 1186 → 593 → 1780 → 890 →
445 → 1336 → 668 → 334 → 167 → 502 → 251 → 754 → 377 → 1132 →
... (continues)
→ 1

Statistics:
  Total steps: 111
  Maximum value: 9232
  Starting value: 27
  Sequence verified: ✓ (reached 1)
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a console-based menu system with multiple operations.

**Requirements:**
1. Create a main menu with options:
   - 1: Add numbers (sum running total)
   - 2: View total
   - 3: Reset total
   - 4: History (show all additions)
   - 5: Exit
2. Use do-while for the main menu loop
3. Use while loops for sub-operations where needed
4. Simulate user choices with a sequence
5. Maintain and display state throughout

**Simulated user sequence:**
```
choices = ["1", "25", "1", "30", "2", "1", "-15", "2", "4", "3", "2", "5"]
```

**Expected Output:**
```
╔═══════════════════════════════════════╗
║         CALCULATOR MENU               ║
╠═══════════════════════════════════════╣
║ 1. Add Number                         ║
║ 2. View Total                         ║
║ 3. Reset Total                        ║
║ 4. Show History                       ║
║ 5. Exit                               ║
╚═══════════════════════════════════════╝

Selection: 1
Enter number to add: 25
  Added 25 to total.

Selection: 1
Enter number to add: 30
  Added 30 to total.

Selection: 2
  Current Total: 55

Selection: 1
Enter number to add: -15
  Added -15 to total.

Selection: 2
  Current Total: 40

Selection: 4
  === HISTORY ===
  +25 = 25
  +30 = 55
  -15 = 40
  ===============

Selection: 3
  Total reset to 0.

Selection: 2
  Current Total: 0

Selection: 5
Goodbye! Final total was: 0
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Implement a prime factorization program with detailed output.

**Requirements:**
1. Find prime factors of a number (use 360 as test case)
2. Use nested while loops:
   - Outer: continue until number becomes 1
   - Inner: divide by current factor while divisible
3. Display:
   - Step-by-step factorization process
   - Factor table with counts
   - Prime factorization expression
   - Verification by multiplication
4. Also test with edge cases: 1, prime number (17), power of 2 (64)

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    PRIME FACTORIZATION                            ║
╠══════════════════════════════════════════════════════════════════╣
║ Number: 360                                                       ║
╠══════════════════════════════════════════════════════════════════╣

STEP-BY-STEP PROCESS:
─────────────────────────────────────────────────────────────────────
Step 1: 360 ÷ 2 = 180 (factor: 2)
Step 2: 180 ÷ 2 = 90  (factor: 2)
Step 3: 90 ÷ 2 = 45   (factor: 2)
Step 4: 45 ÷ 3 = 15   (factor: 3)
Step 5: 15 ÷ 3 = 5    (factor: 3)
Step 6: 5 ÷ 5 = 1     (factor: 5)

FACTOR TABLE:
─────────────────────────────────────────────────────────────────────
Factor  │ Count  │ Contribution
────────┼────────┼──────────────
    2   │   3    │ 2³ = 8
    3   │   2    │ 3² = 9
    5   │   1    │ 5¹ = 5
────────┴────────┴──────────────

PRIME FACTORIZATION:
─────────────────────────────────────────────────────────────────────
360 = 2³ × 3² × 5¹
360 = 8 × 9 × 5

VERIFICATION:
─────────────────────────────────────────────────────────────────────
2 × 2 × 2 × 3 × 3 × 5 = 360 ✓

═══════════════════════════════════════════════════════════════════

ADDITIONAL TEST CASES:
─────────────────────────────────────────────────────────────────────

Number: 1
  Result: 1 has no prime factors (by definition)

Number: 17
  Result: 17 is prime (17 = 17¹)

Number: 64
  Result: 64 = 2⁶
  Steps: 64 → 32 → 16 → 8 → 4 → 2 → 1

╚══════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use while loop for outer iteration (dividing down to 1)
- Use nested while loop for consecutive divisions by same factor
- Track factors in appropriate data structure
- Handle edge cases (1, prime numbers)
- Include verification multiplication
- Use clear variable names and comments

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does the loop logic work correctly?
2. **Loop Choice**: Is while or do-while used appropriately?
3. **Termination**: Does the loop terminate properly?
4. **Code Quality**: Clean, readable code with comments?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of While and Do-While Loops concepts.*
