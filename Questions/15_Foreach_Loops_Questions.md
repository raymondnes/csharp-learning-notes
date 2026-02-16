# Foreach Loops - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Foreach Loops concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Iterate through an array using foreach.

**Requirements:**
1. Create an array of 5 city names
2. Use foreach to print each city
3. Count the total number of cities printed

**Expected Output:**
```
Cities in my list:
- New York
- London
- Tokyo
- Paris
- Sydney
Total: 5 cities
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Calculate the sum and average using foreach.

**Requirements:**
1. Create a List of integers: 15, 25, 35, 45, 55
2. Use foreach to calculate the sum
3. Calculate the average
4. Display results

**Expected Output:**
```
Numbers: 15, 25, 35, 45, 55
Sum: 175
Average: 35.00
```

---

## Level 3: Application (Easy)

**Challenge:** Search for items in a collection.

**Requirements:**
1. Create a list of product names
2. Use foreach to find all products containing "Pro" (case-insensitive)
3. Display matching products and count

**Expected Output:**
```
All Products:
- MacBook Pro
- iPhone
- AirPods Pro
- iPad
- Apple Watch

Products containing "Pro":
  1. MacBook Pro
  2. AirPods Pro
Found: 2 products
```

---

## Level 4: Application (Easy)

**Challenge:** Iterate through a string character by character.

**Requirements:**
1. Take a word (use "Programming")
2. Use foreach to iterate through each character
3. Count vowels and consonants
4. Display each character with its classification

**Expected Output:**
```
Analyzing: "Programming"

P - consonant
r - consonant
o - VOWEL
g - consonant
r - consonant
a - VOWEL
m - consonant
m - consonant
i - VOWEL
n - consonant
g - consonant

Total characters: 11
Vowels: 3
Consonants: 8
```

---

## Level 5: Integration (Moderate)

**Challenge:** Work with a list of objects.

**Requirements:**
1. Create an `Employee` class with Name, Department, Salary
2. Create a list of 5 employees
3. Use foreach to:
   - Display all employees
   - Calculate total salary
   - Find employees in "Engineering" department
   - Find highest paid employee

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║                    EMPLOYEE REPORT                            ║
╠══════════════════════════════════════════════════════════════╣

All Employees:
─────────────────────────────────────────────────────────────────
Name              │ Department      │ Salary
──────────────────┼─────────────────┼─────────────
Alice Johnson     │ Engineering     │ $85,000
Bob Smith         │ Marketing       │ $72,000
Carol White       │ Engineering     │ $92,000
David Brown       │ HR              │ $65,000
Eve Davis         │ Engineering     │ $88,000

Engineering Team:
  - Alice Johnson ($85,000)
  - Carol White ($92,000)
  - Eve Davis ($88,000)

Statistics:
  Total Salary Budget: $402,000
  Average Salary: $80,400
  Highest Paid: Carol White ($92,000)
```

---

## Level 6: Integration (Moderate)

**Challenge:** Iterate through a Dictionary.

**Requirements:**
1. Create a dictionary of country capitals
2. Use foreach with KeyValuePair to display all entries
3. Find all countries starting with 'U'
4. Group countries by first letter

**Expected Output:**
```
╔═══════════════════════════════════════════════════╗
║              WORLD CAPITALS                        ║
╠═══════════════════════════════════════════════════╣

All Capitals:
  France → Paris
  Japan → Tokyo
  USA → Washington D.C.
  UK → London
  Germany → Berlin
  Australia → Canberra
  Brazil → Brasília

Countries starting with 'U':
  - USA (Capital: Washington D.C.)
  - UK (Capital: London)

Grouped by first letter:
  A: Australia
  B: Brazil
  F: France
  G: Germany
  J: Japan
  U: USA, UK
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Demonstrate break and continue with complex logic.

**Requirements:**
1. Create a list of numbers: 1 through 20
2. Use foreach with continue to skip multiples of 3
3. Use foreach with break to stop at the first number > 15
4. Find the first prime number > 10
5. Display step-by-step processing

**Expected Output:**
```
═══ SKIP MULTIPLES OF 3 ═══
Processing: 1 - Included
Processing: 2 - Included
Processing: 3 - SKIPPED (multiple of 3)
Processing: 4 - Included
Processing: 5 - Included
Processing: 6 - SKIPPED (multiple of 3)
...
Result: [1, 2, 4, 5, 7, 8, 10, 11, 13, 14, 16, 17, 19, 20]

═══ STOP AT FIRST > 15 ═══
Processing: 1 - Continue
Processing: 2 - Continue
...
Processing: 15 - Continue
Processing: 16 - BREAK (> 15)
Result: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]

═══ FIND FIRST PRIME > 10 ═══
Checking 11: Is prime? Yes - FOUND!
First prime > 10: 11
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Safely modify a collection during iteration.

**Requirements:**
1. Create a list of integers: 1, 5, 2, 8, 3, 9, 4, 7, 6
2. Demonstrate the error when modifying during foreach
3. Show three correct solutions:
   - Using RemoveAll
   - Using backwards for loop
   - Using ToList() copy
4. Remove all even numbers using each method
5. Compare results

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║          SAFE COLLECTION MODIFICATION                             ║
╠══════════════════════════════════════════════════════════════════╣

Original list: [1, 5, 2, 8, 3, 9, 4, 7, 6]
Task: Remove all even numbers

═══ METHOD 1: Direct modification (WRONG) ═══
Attempting to remove during foreach...
ERROR: InvalidOperationException - Collection was modified!

═══ METHOD 2: Using RemoveAll (Recommended) ═══
Code: list.RemoveAll(n => n % 2 == 0)
Result: [1, 5, 3, 9, 7]
Items removed: 4
✓ Success!

═══ METHOD 3: Backwards for loop ═══
Starting from index 8, going to 0...
  Index 8: 6 is even - REMOVED
  Index 7: 7 is odd - kept
  Index 6: 4 is even - REMOVED
  Index 5: 9 is odd - kept
  Index 4: 3 is odd - kept
  Index 3: 8 is even - REMOVED
  Index 2: 2 is even - REMOVED
  Index 1: 5 is odd - kept
  Index 0: 1 is odd - kept
Result: [1, 5, 3, 9, 7]
✓ Success!

═══ METHOD 4: Using ToList() copy ═══
Creating copy to iterate over...
foreach (var num in original.ToList())
  Checking 1: odd - keep
  Checking 5: odd - keep
  Checking 2: even - REMOVE from original
  ...
Result: [1, 5, 3, 9, 7]
✓ Success!

All methods produce same result: [1, 5, 3, 9, 7]
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a comprehensive report generator using foreach.

**Requirements:**
1. Create classes: `Transaction` (Date, Type, Amount, Category)
2. Create a list of 15+ transactions across different categories
3. Using only foreach loops (no LINQ), generate:
   - Monthly summary (group by month)
   - Category breakdown (income vs expense per category)
   - Running balance calculation
   - Top 3 largest transactions
   - Trend analysis (compare months)
4. Format output professionally

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    FINANCIAL REPORT - Q1 2024                             ║
╠══════════════════════════════════════════════════════════════════════════╣

═══════════════════════════════════════════════════════════════════════════
                            TRANSACTION LOG
═══════════════════════════════════════════════════════════════════════════
Date       │ Type    │ Category      │ Amount     │ Balance
───────────┼─────────┼───────────────┼────────────┼────────────
2024-01-05 │ Income  │ Salary        │ +$5,000.00 │ $5,000.00
2024-01-10 │ Expense │ Rent          │ -$1,500.00 │ $3,500.00
2024-01-15 │ Expense │ Groceries     │ -$350.00   │ $3,150.00
2024-01-20 │ Expense │ Utilities     │ -$150.00   │ $3,000.00
2024-02-05 │ Income  │ Salary        │ +$5,000.00 │ $8,000.00
2024-02-08 │ Income  │ Freelance     │ +$800.00   │ $8,800.00
2024-02-10 │ Expense │ Rent          │ -$1,500.00 │ $7,300.00
... (more transactions)

═══════════════════════════════════════════════════════════════════════════
                           MONTHLY SUMMARY
═══════════════════════════════════════════════════════════════════════════
Month     │ Income      │ Expenses    │ Net         │ Transactions
──────────┼─────────────┼─────────────┼─────────────┼─────────────
January   │ $5,000.00   │ $2,000.00   │ +$3,000.00  │ 4
February  │ $5,800.00   │ $2,100.00   │ +$3,700.00  │ 5
March     │ $5,200.00   │ $2,500.00   │ +$2,700.00  │ 6
──────────┴─────────────┴─────────────┴─────────────┴─────────────
TOTAL     │ $16,000.00  │ $6,600.00   │ +$9,400.00  │ 15

═══════════════════════════════════════════════════════════════════════════
                         CATEGORY BREAKDOWN
═══════════════════════════════════════════════════════════════════════════
Category      │ Type    │ Total       │ Count │ Average
──────────────┼─────────┼─────────────┼───────┼───────────
Salary        │ Income  │ $15,000.00  │ 3     │ $5,000.00
Freelance     │ Income  │ $1,000.00   │ 2     │ $500.00
Rent          │ Expense │ $4,500.00   │ 3     │ $1,500.00
Groceries     │ Expense │ $1,050.00   │ 3     │ $350.00
Utilities     │ Expense │ $450.00     │ 3     │ $150.00
Entertainment │ Expense │ $600.00     │ 3     │ $200.00

═══════════════════════════════════════════════════════════════════════════
                       TOP 3 TRANSACTIONS
═══════════════════════════════════════════════════════════════════════════
1. Salary (Jan 5) - $5,000.00
2. Salary (Feb 5) - $5,000.00
3. Salary (Mar 5) - $5,200.00

═══════════════════════════════════════════════════════════════════════════
                        TREND ANALYSIS
═══════════════════════════════════════════════════════════════════════════
January → February:
  Income:   +$800.00 (+16.0%)  ↑
  Expenses: +$100.00 (+5.0%)   ↑
  Net:      +$700.00 (+23.3%)  ↑

February → March:
  Income:   -$600.00 (-10.3%)  ↓
  Expenses: +$400.00 (+19.0%)  ↑
  Net:      -$1000.00 (-27.0%) ↓

Overall Q1 Trend: Stable with slight decline in March

═══════════════════════════════════════════════════════════════════════════
                           SUMMARY
═══════════════════════════════════════════════════════════════════════════
Starting Balance:  $0.00
Ending Balance:    $9,400.00
Total Income:      $16,000.00
Total Expenses:    $6,600.00
Savings Rate:      58.75%

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use ONLY foreach loops (no LINQ methods like GroupBy, OrderBy, etc.)
- Implement grouping manually using dictionaries
- Calculate running balance during iteration
- Sort manually for top transactions
- Clean code with helper methods

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does foreach iteration work properly?
2. **Read-Only Rule**: No attempts to modify iteration variable?
3. **Collection Safety**: Proper handling of modifications?
4. **Code Quality**: Clean, idiomatic foreach usage?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Foreach Loops concepts.*
