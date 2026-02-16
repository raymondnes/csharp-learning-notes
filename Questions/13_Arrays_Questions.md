# Arrays - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Arrays concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create an array and display its elements.

**Requirements:**
1. Create an integer array with values: 10, 20, 30, 40, 50
2. Display each element with its index using a for loop
3. Display the array length

**Expected Output:**
```
Array contents:
Index 0: 10
Index 1: 20
Index 2: 30
Index 3: 40
Index 4: 50
Array length: 5
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Calculate the sum and average of array elements.

**Requirements:**
1. Create an array: { 85, 92, 78, 90, 88 }
2. Calculate the sum of all elements
3. Calculate the average
4. Display results

**Expected Output:**
```
Scores: 85, 92, 78, 90, 88
Sum: 433
Average: 86.60
```

---

## Level 3: Application (Easy)

**Challenge:** Find the minimum and maximum values in an array.

**Requirements:**
1. Create an array: { 23, 45, 12, 67, 34, 89, 21, 56 }
2. Find the minimum value and its index
3. Find the maximum value and its index
4. Calculate the range (max - min)

**Expected Output:**
```
Array: 23, 45, 12, 67, 34, 89, 21, 56
Minimum: 12 at index 2
Maximum: 89 at index 5
Range: 77
```

---

## Level 4: Application (Easy)

**Challenge:** Reverse an array in-place.

**Requirements:**
1. Create an array: { 1, 2, 3, 4, 5, 6, 7 }
2. Reverse it in-place (without using Array.Reverse)
3. Show before and after states
4. Explain the algorithm with step tracking

**Expected Output:**
```
Original: [1, 2, 3, 4, 5, 6, 7]

Reversing in-place:
  Swap index 0 and 6: [7, 2, 3, 4, 5, 6, 1]
  Swap index 1 and 5: [7, 6, 3, 4, 5, 2, 1]
  Swap index 2 and 4: [7, 6, 5, 4, 3, 2, 1]

Reversed: [7, 6, 5, 4, 3, 2, 1]
```

---

## Level 5: Integration (Moderate)

**Challenge:** Search and count operations on an array.

**Requirements:**
1. Create an array: { 5, 2, 8, 2, 9, 2, 1, 8, 3, 2 }
2. Find all indices where value 2 appears
3. Count occurrences of each unique value
4. Find the most frequent value

**Expected Output:**
```
Array: [5, 2, 8, 2, 9, 2, 1, 8, 3, 2]

Searching for value 2:
  Found at indices: 1, 3, 5, 9
  Total occurrences: 4

Value frequency:
  1 appears 1 time(s)
  2 appears 4 time(s)
  3 appears 1 time(s)
  5 appears 1 time(s)
  8 appears 2 time(s)
  9 appears 1 time(s)

Most frequent: 2 (4 times)
```

---

## Level 6: Integration (Moderate)

**Challenge:** Work with 2D arrays (matrices).

**Requirements:**
1. Create a 3x3 multiplication table matrix
2. Display it in formatted grid
3. Calculate row sums and column sums
4. Find the diagonal sum

**Expected Output:**
```
Multiplication Table (3x3):
╔═══════════════════════╗
║   1 │   2 │   3 ║
╠─────┼─────┼─────╣
║   2 │   4 │   6 ║
╠─────┼─────┼─────╣
║   3 │   6 │   9 ║
╚═══════════════════════╝

Row sums: [6, 12, 18]
Column sums: [6, 12, 18]
Diagonal sum (top-left to bottom-right): 14
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement sorting algorithms and compare them.

**Requirements:**
1. Create an array: { 64, 34, 25, 12, 22, 11, 90 }
2. Implement Bubble Sort (show each pass)
3. Implement Selection Sort (show each pass)
4. Compare number of swaps for each

**Expected Output:**
```
Original Array: [64, 34, 25, 12, 22, 11, 90]

═══ BUBBLE SORT ═══
Pass 1: [34, 25, 12, 22, 11, 64, 90] (5 swaps)
Pass 2: [25, 12, 22, 11, 34, 64, 90] (4 swaps)
Pass 3: [12, 22, 11, 25, 34, 64, 90] (3 swaps)
Pass 4: [12, 11, 22, 25, 34, 64, 90] (1 swap)
Pass 5: [11, 12, 22, 25, 34, 64, 90] (1 swap)
Pass 6: [11, 12, 22, 25, 34, 64, 90] (0 swaps)
Total swaps: 14
Sorted: [11, 12, 22, 25, 34, 64, 90]

═══ SELECTION SORT ═══
Pass 1: Find min (11), swap with index 0: [11, 34, 25, 12, 22, 64, 90]
Pass 2: Find min (12), swap with index 1: [11, 12, 25, 34, 22, 64, 90]
Pass 3: Find min (22), swap with index 2: [11, 12, 22, 34, 25, 64, 90]
Pass 4: Find min (25), swap with index 3: [11, 12, 22, 25, 34, 64, 90]
Pass 5: Find min (34), swap with index 4: [11, 12, 22, 25, 34, 64, 90]
Pass 6: Find min (64), swap with index 5: [11, 12, 22, 25, 34, 64, 90]
Total swaps: 6
Sorted: [11, 12, 22, 25, 34, 64, 90]

Comparison: Selection Sort used fewer swaps (6 vs 14)
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a jagged array representing a seating chart.

**Requirements:**
1. Create a theater seating chart with varying row sizes:
   - Row A: 8 seats
   - Row B: 10 seats
   - Row C: 12 seats
   - Row D: 12 seats
   - Row E: 10 seats
2. Implement: ReserveSeat, CancelReservation, IsAvailable, GetAvailableCount
3. Display visual seating chart (X=taken, O=available)
4. Demonstrate multiple reservations

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║                     THEATER SEATING                           ║
╠══════════════════════════════════════════════════════════════╣

Initial seating chart (O = available):
Row A:     O O O O O O O O
Row B:   O O O O O O O O O O
Row C: O O O O O O O O O O O O
Row D: O O O O O O O O O O O O
Row E:   O O O O O O O O O O

Making reservations:
✓ Reserved A-3
✓ Reserved A-4
✓ Reserved B-5
✓ Reserved C-6
✓ Reserved C-7
✗ Cannot reserve A-3 (already taken)

Updated seating chart:
Row A:     O O X X O O O O
Row B:   O O O O X O O O O O
Row C: O O O O O X X O O O O O
Row D: O O O O O O O O O O O O
Row E:   O O O O O O O O O O

Statistics:
  Total seats: 52
  Reserved: 5
  Available: 47

Cancelling A-3...
✓ Reservation cancelled

Row A:     O O O X O O O O
Available in Row A: 7/8
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Implement a Sudoku board validator and solver helper.

**Requirements:**
1. Create a 9x9 2D array representing a Sudoku board
2. Implement:
   - `IsValidPlacement(row, col, num)` - checks if number can go in cell
   - `IsRowValid(row)` - checks if row has no duplicates
   - `IsColumnValid(col)` - checks if column has no duplicates
   - `IsBoxValid(boxRow, boxCol)` - checks if 3x3 box is valid
   - `IsBoardValid()` - validates entire board
   - `GetPossibleValues(row, col)` - returns possible numbers for empty cell
3. Display board with formatting
4. Demonstrate validation with both valid and invalid boards

**Expected Output:**
```
╔════════════════════════════════════════════════════════════════════╗
║                      SUDOKU VALIDATOR                               ║
╠════════════════════════════════════════════════════════════════════╣

Sudoku Board:
╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗
║ 5 │ 3 │ . ║ . │ 7 │ . ║ . │ . │ . ║
╟───┼───┼───╫───┼───┼───╫───┼───┼───╢
║ 6 │ . │ . ║ 1 │ 9 │ 5 ║ . │ . │ . ║
╟───┼───┼───╫───┼───┼───╫───┼───┼───╢
║ . │ 9 │ 8 ║ . │ . │ . ║ . │ 6 │ . ║
╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣
║ 8 │ . │ . ║ . │ 6 │ . ║ . │ . │ 3 ║
╟───┼───┼───╫───┼───┼───╫───┼───┼───╢
║ 4 │ . │ . ║ 8 │ . │ 3 ║ . │ . │ 1 ║
╟───┼───┼───╫───┼───┼───╫───┼───┼───╢
║ 7 │ . │ . ║ . │ 2 │ . ║ . │ . │ 6 ║
╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣
║ . │ 6 │ . ║ . │ . │ . ║ 2 │ 8 │ . ║
╟───┼───┼───╫───┼───┼───╫───┼───┼───╢
║ . │ . │ . ║ 4 │ 1 │ 9 ║ . │ . │ 5 ║
╟───┼───┼───╫───┼───┼───╫───┼───┼───╢
║ . │ . │ . ║ . │ 8 │ . ║ . │ 7 │ 9 ║
╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝

=== VALIDATION TESTS ===

Testing Row 0: ✓ Valid (5, 3, 7 - no duplicates)
Testing Row 1: ✓ Valid
Testing Column 4: ✓ Valid (7, 9, 6, 2, 1, 8 - no duplicates)
Testing Box (0,0): ✓ Valid (5, 3, 6, 9, 8 - no duplicates)
Testing Box (1,1): ✓ Valid

Full Board Validation: ✓ Current state is valid

=== POSSIBLE VALUES ANALYSIS ===

Empty cell at (0, 2): Possible values: [1, 2, 4]
  Row 0 contains: 5, 3, 7
  Col 2 contains: 8
  Box contains: 5, 3, 6, 9, 8
  Eliminated: 3, 5, 6, 7, 8, 9

Empty cell at (0, 3): Possible values: [2, 4, 6]

=== INVALID BOARD TEST ===

Testing board with duplicate 5 in row 0:
Row 0: [5, 3, 5, ...]  <- Contains duplicate!
✗ Invalid: Row 0 has duplicate value 5

╚════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use 2D arrays for the board
- Proper bounds checking
- Clear separation of validation methods
- Handle empty cells (represented as 0)
- Comprehensive comments

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Do array operations work properly?
2. **Bounds Handling**: No IndexOutOfRangeException?
3. **Algorithm**: Is the logic sound?
4. **Code Quality**: Clean, efficient, readable?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Arrays concepts.*
