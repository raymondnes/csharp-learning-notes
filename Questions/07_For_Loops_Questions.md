# For Loops - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of For Loops concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Print numbers from 1 to 10 using a for loop.

**Requirements:**
1. Use a for loop with proper initialization, condition, and iteration
2. Print each number on a new line
3. Display a message before and after the loop

**Expected Output:**
```
Counting from 1 to 10:
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
Done counting!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Calculate and display the sum of numbers from 1 to N.

**Requirements:**
1. Set N = 10
2. Use a for loop to calculate the sum
3. Display the running total at each step
4. Show the final sum

**Expected Output:**
```
Calculating sum of 1 to 10:
After adding 1: Sum = 1
After adding 2: Sum = 3
After adding 3: Sum = 6
After adding 4: Sum = 10
After adding 5: Sum = 15
After adding 6: Sum = 21
After adding 7: Sum = 28
After adding 8: Sum = 36
After adding 9: Sum = 45
After adding 10: Sum = 55

Final sum: 55
```

---

## Level 3: Application (Easy)

**Challenge:** Print a countdown with custom messages at specific points.

**Requirements:**
1. Count down from 10 to 1
2. At 10: Print "Starting countdown..."
3. At 5: Print "Halfway there!"
4. At 3, 2, 1: Print the number
5. After 0: Print "Liftoff!"

**Expected Output:**
```
Starting countdown...
10
9
8
7
6
Halfway there!
5
4
3
2
1
Liftoff!
```

---

## Level 4: Application (Easy)

**Challenge:** Create a multiplication table for a given number.

**Requirements:**
1. Generate multiplication table for 7 (1 through 10)
2. Format output neatly with alignment
3. Include a header

**Expected Output:**
```
Multiplication Table for 7
========================
7 x  1 =  7
7 x  2 = 14
7 x  3 = 21
7 x  4 = 28
7 x  5 = 35
7 x  6 = 42
7 x  7 = 49
7 x  8 = 56
7 x  9 = 63
7 x 10 = 70
```

---

## Level 5: Integration (Moderate)

**Challenge:** Find all factors of a given number and count them.

**Requirements:**
1. Find all factors of 60
2. Use a for loop to check each number from 1 to 60
3. Display each factor found
4. Count total factors
5. Determine if the number is prime or composite

**Expected Output:**
```
Finding factors of 60:
Factors: 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60

Total factors: 12
60 is composite (more than 2 factors)

Factor pairs:
1 x 60
2 x 30
3 x 20
4 x 15
5 x 12
6 x 10
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a pattern printer using nested for loops.

**Requirements:**
1. Print a diamond pattern with height of 5 (widest point)
2. Use nested loops for spaces and stars
3. Handle both the top and bottom halves

**Expected Output:**
```
    *
   ***
  *****
 *******
*********
 *******
  *****
   ***
    *
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement a prime number sieve using for loops.

**Requirements:**
1. Find all prime numbers from 2 to 100
2. Use the Sieve of Eratosthenes algorithm
3. Use a boolean array to track prime status
4. Display primes in rows of 10
5. Show total count of primes found

**Expected Output:**
```
Prime Number Sieve (2 to 100)
=============================
  2   3   5   7  11  13  17  19  23  29
 31  37  41  43  47  53  59  61  67  71
 73  79  83  89  97

Total primes found: 25
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create an array manipulation program demonstrating advanced for loop usage.

**Requirements:**
1. Create an array of 15 random-ish numbers (use predefined values)
2. Use for loops to:
   - Display original array
   - Find minimum and maximum values (with indices)
   - Calculate average
   - Count numbers above and below average
   - Reverse the array in-place
   - Sort the array using bubble sort
3. Display results at each step

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║              ARRAY MANIPULATION DEMO                          ║
╠══════════════════════════════════════════════════════════════╣
║ Original Array:                                               ║
║ [23, 45, 12, 67, 34, 89, 21, 56, 78, 43, 90, 15, 38, 62, 29] ║
╠══════════════════════════════════════════════════════════════╣
║ Statistics:                                                   ║
║   Minimum: 12 (at index 2)                                    ║
║   Maximum: 90 (at index 10)                                   ║
║   Sum: 702                                                    ║
║   Average: 46.80                                              ║
║   Above average: 7 numbers                                    ║
║   Below average: 8 numbers                                    ║
╠══════════════════════════════════════════════════════════════╣
║ Reversed Array:                                               ║
║ [29, 62, 38, 15, 90, 43, 78, 56, 21, 89, 34, 67, 12, 45, 23] ║
╠══════════════════════════════════════════════════════════════╣
║ Sorted Array (Bubble Sort):                                   ║
║ [12, 15, 21, 23, 29, 34, 38, 43, 45, 56, 62, 67, 78, 89, 90] ║
║ Passes required: 14                                           ║
╚══════════════════════════════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a "Pascal's Triangle" generator with multiple display options.

**Requirements:**
1. Generate Pascal's Triangle with configurable rows (use 10 rows)
2. Use nested for loops to calculate each value
3. Implement three display modes:
   - Left-aligned (simple)
   - Centered (pyramid)
   - Right-aligned (inverted)
4. Calculate and display:
   - Sum of each row (should be powers of 2)
   - Maximum value in triangle
   - Total sum of all values
5. Use proper spacing for alignment
6. Include row numbers

**Expected Output:**
```
╔═══════════════════════════════════════════════════════════════════════╗
║                        PASCAL'S TRIANGLE                               ║
╠═══════════════════════════════════════════════════════════════════════╣
║                           CENTERED VIEW                                ║
╠═══════════════════════════════════════════════════════════════════════╣
Row 0:                              1                               = 1
Row 1:                            1   1                             = 2
Row 2:                          1   2   1                           = 4
Row 3:                        1   3   3   1                         = 8
Row 4:                      1   4   6   4   1                       = 16
Row 5:                    1   5  10  10   5   1                     = 32
Row 6:                  1   6  15  20  15   6   1                   = 64
Row 7:                1   7  21  35  35  21   7   1                 = 128
Row 8:              1   8  28  56  70  56  28   8   1               = 256
Row 9:            1   9  36  84 126 126  84  36   9   1             = 512
╠═══════════════════════════════════════════════════════════════════════╣
║                          LEFT-ALIGNED VIEW                             ║
╠═══════════════════════════════════════════════════════════════════════╣
Row 0: 1
Row 1: 1 1
Row 2: 1 2 1
Row 3: 1 3 3 1
Row 4: 1 4 6 4 1
Row 5: 1 5 10 10 5 1
Row 6: 1 6 15 20 15 6 1
Row 7: 1 7 21 35 35 21 7 1
Row 8: 1 8 28 56 70 56 28 8 1
Row 9: 1 9 36 84 126 126 84 36 9 1
╠═══════════════════════════════════════════════════════════════════════╣
║                             STATISTICS                                 ║
╠═══════════════════════════════════════════════════════════════════════╣
║ Number of rows:        10                                              ║
║ Maximum value:         126 (appears in row 9)                          ║
║ Total elements:        55                                              ║
║ Total sum:             1023 (2^10 - 1)                                 ║
║ Pattern verified:      ✓ Each row sum is power of 2                    ║
╚═══════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use for loops exclusively (no while loops)
- Calculate values using the formula: C(n,k) = C(n-1,k-1) + C(n-1,k)
- Or use the formula: C(n,k) = n! / (k! * (n-k)!)
- Implement proper spacing calculations
- Handle variable number widths for alignment
- Include comments explaining the mathematical logic

**Bonus Challenge (Optional):**
- Also display the triangle modulo 2 (creates Sierpinski pattern)

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does the loop produce expected output?
2. **Loop Structure**: Is the for loop properly constructed?
3. **Efficiency**: No unnecessary iterations or calculations?
4. **Code Quality**: Clean, readable, well-commented?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of For Loops concepts.*
