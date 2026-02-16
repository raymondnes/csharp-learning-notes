# If-Else Statements and Comparison Operators - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of If-Else Statements concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a program that checks if a number is positive, negative, or zero.

**Requirements:**
1. Declare an integer variable with any value
2. Use if-else statements to determine if it's positive, negative, or zero
3. Print an appropriate message for each case

**Expected Output (for value 7):**
```
The number 7 is positive.
```

**Expected Output (for value -3):**
```
The number -3 is negative.
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a simple pass/fail checker for exam scores.

**Requirements:**
1. Declare an integer variable for a test score (0-100)
2. If score is 60 or above, print "PASS"
3. Otherwise, print "FAIL"
4. Also display the score in the output

**Expected Output (score = 75):**
```
Score: 75
Result: PASS
```

**Expected Output (score = 45):**
```
Score: 45
Result: FAIL
```

---

## Level 3: Application (Easy)

**Challenge:** Create a letter grade calculator using else-if chains.

**Requirements:**
1. Accept a numeric score (0-100)
2. Convert to letter grades:
   - 90-100: A
   - 80-89: B
   - 70-79: C
   - 60-69: D
   - Below 60: F
3. Display both the score and the grade

**Expected Output:**
```
Score: 85
Grade: B
```

---

## Level 4: Application (Easy)

**Challenge:** Create a program that determines eligibility to vote.

**Requirements:**
1. Store an age value and a citizenship status (boolean)
2. Use logical operators (&&) to check both conditions:
   - Must be 18 or older
   - Must be a citizen
3. Display whether the person can vote and explain why if they can't

**Expected Outputs:**
```
Age: 20, Citizen: True
You are eligible to vote!
```

```
Age: 16, Citizen: True
You cannot vote: You must be 18 or older.
```

```
Age: 25, Citizen: False
You cannot vote: You must be a citizen.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a movie ticket pricing system with multiple discounts.

**Requirements:**
1. Base ticket price is $12.00
2. Apply discounts based on age:
   - Under 12: 50% off
   - 65 and over: 40% off
   - 13-17: 25% off
3. If it's a Tuesday, everyone gets an additional $2 off
4. Display the original price, any discounts applied, and final price

**Variables to use:** age, isTuesday

**Expected Output:**
```
=== MOVIE TICKET ===
Original Price: $12.00
Age Category: Child (Under 12)
Age Discount: -$6.00
Tuesday Discount: -$2.00
------------------
Final Price: $4.00
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a comprehensive number analyzer using all comparison and logical operators.

**Requirements:**
1. Ask for a number (use a hardcoded value for simplicity)
2. Determine and display:
   - Whether it's positive, negative, or zero
   - Whether it's even or odd (only if not zero)
   - Whether it's divisible by 5
   - Whether it's a "teen" number (13-19)
   - Whether it's within the range 1-100
3. Use appropriate logical operators

**Expected Output (for number 15):**
```
Analyzing number: 15
✓ Positive
✓ Odd
✓ Divisible by 5
✓ Teen number (13-19)
✓ Within range 1-100
```

**Expected Output (for number 24):**
```
Analyzing number: 24
✓ Positive
✓ Even
✗ Not divisible by 5
✗ Not a teen number
✓ Within range 1-100
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a login authentication system with multiple validation checks.

**Requirements:**
1. Define valid credentials: username = "admin", password = "secure123"
2. Create test input variables for attempted login
3. Implement the following checks in order:
   - Username cannot be empty
   - Password cannot be empty
   - Password must be at least 8 characters
   - Username and password must match stored credentials
4. Display specific error messages for each failure case
5. Track and display the number of validation checks passed
6. Use nested if statements and logical operators appropriately

**Expected Output (successful login):**
```
=== LOGIN SYSTEM ===
Username: admin
Password: ********

Validation Results:
✓ Username provided
✓ Password provided
✓ Password length valid (8+ chars)
✓ Credentials match

ACCESS GRANTED
All 4 checks passed.
```

**Expected Output (wrong password):**
```
=== LOGIN SYSTEM ===
Username: admin
Password: ********

Validation Results:
✓ Username provided
✓ Password provided
✓ Password length valid (8+ chars)
✗ Invalid credentials

ACCESS DENIED
3 of 4 checks passed.
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a shipping cost calculator with complex conditional logic.

**Requirements:**
1. Calculate shipping based on:
   - Package weight (in kg)
   - Destination zone (1: Local, 2: Regional, 3: National, 4: International)
   - Shipping speed (1: Standard, 2: Express, 3: Overnight)
2. Base rates per kg by zone:
   - Zone 1: $2.00/kg
   - Zone 2: $4.00/kg
   - Zone 3: $7.00/kg
   - Zone 4: $15.00/kg
3. Speed multipliers:
   - Standard: 1x
   - Express: 1.5x
   - Overnight: 2.5x
4. Special rules:
   - Packages over 20kg get 10% weight discount
   - International overnight not available (show error)
   - Minimum charge is $5.00
5. Use ternary operators where appropriate
6. Display itemized breakdown

**Expected Output:**
```
╔═══════════════════════════════════════╗
║        SHIPPING CALCULATOR            ║
╠═══════════════════════════════════════╣
║ Weight: 25.0 kg                       ║
║ Destination: Zone 3 (National)        ║
║ Speed: Express                        ║
╠═══════════════════════════════════════╣
║ CALCULATION:                          ║
║ Base Rate: $7.00/kg                   ║
║ Weight Cost: 25.0 × $7.00 = $175.00   ║
║ Heavy Package Discount: -$17.50       ║
║ Speed Multiplier (1.5x): +$78.75      ║
╠═══════════════════════════════════════╣
║ TOTAL: $236.25                        ║
╚═══════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive "Smart Decision Engine" that evaluates loan applications.

**Requirements:**
1. Evaluate a loan application based on multiple factors:
   - Credit score (300-850)
   - Annual income
   - Requested loan amount
   - Employment years
   - Existing debt
   - Age
2. Implement scoring system:
   - Credit score: Excellent (750+), Good (700-749), Fair (650-699), Poor (below 650)
   - Debt-to-income ratio: (existing debt + requested loan) / annual income
   - Employment stability: 2+ years = stable
3. Decision rules:
   - AUTO APPROVE: Credit >= 750 AND debt-to-income < 30% AND employment >= 2 years
   - MANUAL REVIEW: Credit >= 650 AND debt-to-income < 50%
   - AUTO DENY: Credit < 600 OR debt-to-income > 60% OR age < 18
   - All other cases: MANUAL REVIEW with specific flags
4. Display:
   - All input factors with assessments
   - Calculated ratios
   - Risk flags (list all concerns)
   - Final decision with reasoning
   - Recommended actions

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    LOAN APPLICATION ANALYZER                      ║
╠══════════════════════════════════════════════════════════════════╣
║                      APPLICANT INFORMATION                        ║
╠══════════════════════════════════════════════════════════════════╣
║ Age:                 28                    ✓ Eligible             ║
║ Credit Score:        725                   [GOOD]                 ║
║ Annual Income:       $65,000.00                                   ║
║ Requested Amount:    $15,000.00                                   ║
║ Existing Debt:       $8,000.00                                    ║
║ Employment:          3 years               ✓ Stable               ║
╠══════════════════════════════════════════════════════════════════╣
║                      CALCULATED METRICS                           ║
╠══════════════════════════════════════════════════════════════════╣
║ Total Debt After Loan:    $23,000.00                              ║
║ Debt-to-Income Ratio:     35.38%          [MODERATE]              ║
║ Loan-to-Income Ratio:     23.08%          [ACCEPTABLE]            ║
║ Monthly Payment (est.):   $285.00 (@ 5% for 60 months)            ║
║ Payment-to-Income:        5.26%           [LOW RISK]              ║
╠══════════════════════════════════════════════════════════════════╣
║                       RISK ASSESSMENT                             ║
╠══════════════════════════════════════════════════════════════════╣
║ ⚠ Credit score below excellent (750+)                            ║
║ ⚠ Debt-to-income ratio above optimal (30%)                       ║
║ ✓ Stable employment history                                       ║
║ ✓ Loan amount reasonable for income                               ║
╠══════════════════════════════════════════════════════════════════╣
║                         DECISION                                  ║
╠══════════════════════════════════════════════════════════════════╣
║                                                                   ║
║  ┌─────────────────────────────────────────────────────────────┐ ║
║  │              ★ MANUAL REVIEW REQUIRED ★                     │ ║
║  └─────────────────────────────────────────────────────────────┘ ║
║                                                                   ║
║ Reasoning:                                                        ║
║ - Good credit score (725) meets minimum threshold                 ║
║ - Debt-to-income (35.38%) within acceptable range                 ║
║ - Manual review needed due to moderate risk factors               ║
║                                                                   ║
║ Recommended Actions:                                              ║
║ 1. Verify employment and income documentation                     ║
║ 2. Consider requiring collateral                                  ║
║ 3. May offer reduced amount ($12,000) for auto-approval          ║
╚══════════════════════════════════════════════════════════════════╝
```

**Additional Requirements:**
- Use all comparison operators (==, !=, >, <, >=, <=)
- Use all logical operators (&&, ||, !)
- Implement ternary operators for status labels
- Use nested if statements appropriately
- Include comprehensive comments explaining decision logic
- Code must be well-organized and readable
- All monetary values should be formatted properly

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does the logic produce correct results?
2. **Operator Usage**: Are comparison and logical operators used correctly?
3. **Code Structure**: Are if-else chains properly structured?
4. **Readability**: Is the code easy to understand?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of If-Else Statements concepts.*
