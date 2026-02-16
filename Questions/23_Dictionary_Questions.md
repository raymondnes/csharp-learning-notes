# Dictionary<TKey, TValue> - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Dictionary concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create and populate a simple dictionary.

**Requirements:**
1. Create Dictionary<string, int> for student ages
2. Add 3 students using indexer
3. Access and display each student's age
4. Show the total count

**Expected Output:**
```
Student Ages Dictionary:

Adding students...
Added: Alice, Age: 20
Added: Bob, Age: 22
Added: Carol, Age: 21

Accessing by key:
Alice is 20 years old
Bob is 22 years old
Carol is 21 years old

Total students: 3
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Use TryGetValue for safe access.

**Requirements:**
1. Create Dictionary<string, string> for capitals
2. Add 3 country-capital pairs
3. Use TryGetValue to look up existing and non-existing keys
4. Display appropriate messages

**Expected Output:**
```
World Capitals Dictionary:

Looking up capitals...

USA: Found! Capital is Washington D.C.
France: Found! Capital is Paris
Germany: Not found in dictionary
Japan: Found! Capital is Tokyo

Safe access prevents crashes!
```

---

## Level 3: Application (Easy)

**Challenge:** Iterate through a dictionary.

**Requirements:**
1. Create Dictionary<string, double> for product prices
2. Add 5 products
3. Iterate using KeyValuePair
4. Iterate using deconstruction (key, value)
5. Display keys and values separately

**Expected Output:**
```
Product Catalog:

Using KeyValuePair:
  Apple: $1.50
  Banana: $0.75
  Orange: $2.00
  Milk: $3.50
  Bread: $2.25

Using Deconstruction:
  Apple costs $1.50
  Banana costs $0.75
  ...

Keys only: Apple, Banana, Orange, Milk, Bread
Values only: $1.50, $0.75, $2.00, $3.50, $2.25
```

---

## Level 4: Application (Easy)

**Challenge:** Update and remove dictionary entries.

**Requirements:**
1. Create Dictionary<int, string> for employee IDs
2. Add 4 employees
3. Update one employee's name
4. Remove an employee using Remove
5. Try to remove non-existent key
6. Show before and after states

**Expected Output:**
```
Employee Directory:

Initial state:
  101: Alice
  102: Bob
  103: Carol
  104: David

Updating ID 102 from "Bob" to "Robert"...
Updated successfully!

Removing ID 103...
Removed Carol successfully!

Trying to remove ID 999...
Key not found, nothing removed.

Final state:
  101: Alice
  102: Robert
  104: David

Count: 3
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a word frequency counter.

**Requirements:**
1. Take a sentence as input
2. Count occurrences of each word
3. Display counts sorted by frequency (descending)
4. Handle case-insensitivity
5. Show total unique words

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              WORD FREQUENCY COUNTER                         ║
╠════════════════════════════════════════════════════════════╣

Input: "The quick brown fox jumps over the lazy dog the fox"

Word Frequencies (sorted by count):
  "the": 3
  "fox": 2
  "quick": 1
  "brown": 1
  "jumps": 1
  "over": 1
  "lazy": 1
  "dog": 1

Statistics:
  Total words: 10
  Unique words: 8
  Most common: "the" (3 times)
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a simple phone book application.

**Requirements:**
1. Dictionary<string, string> for name-to-phone mapping
2. Add contacts (prevent duplicates)
3. Search by name (partial match)
4. Update existing contacts
5. Delete contacts
6. List all contacts alphabetically

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║                    PHONE BOOK                               ║
╠════════════════════════════════════════════════════════════╣

Adding contacts...
  Alice: 555-1234 ✓
  Bob: 555-5678 ✓
  Carol: 555-9012 ✓
  Alice: 555-0000 ✗ (already exists)

Searching for "Al":
  Found: Alice - 555-1234

Updating Bob's number...
  Bob: 555-5678 → 555-9999 ✓

Deleting Carol...
  Carol removed ✓

All Contacts (alphabetical):
  Alice: 555-1234
  Bob: 555-9999

Total contacts: 2
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Build a student grade management system.

**Requirements:**
1. Nested Dictionary: Dictionary<string, Dictionary<string, int>>
   - Student name → Subject → Grade
2. Add students with multiple subject grades
3. Calculate average per student
4. Find top performer per subject
5. Calculate class average per subject
6. Generate report

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   GRADE MANAGEMENT SYSTEM                         ║
╠══════════════════════════════════════════════════════════════════╣

Student Grades:

Alice:
  Math: 95
  English: 88
  Science: 92
  Average: 91.67

Bob:
  Math: 78
  English: 85
  Science: 80
  Average: 81.00

Carol:
  Math: 92
  English: 95
  Science: 88
  Average: 91.67

═══════════════════════════════════════════════════════════════════
Top Performers by Subject:
  Math: Alice (95)
  English: Carol (95)
  Science: Alice (92)

Class Averages:
  Math: 88.33
  English: 89.33
  Science: 86.67

Overall Class Average: 88.11
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Implement a simple caching system.

**Requirements:**
1. Create generic Cache<TKey, TValue> class
2. Use internal Dictionary for storage
3. GetOrAdd method: return cached or compute and cache
4. Set method with optional expiration tracking
5. Remove and Clear methods
6. Track cache hits and misses
7. Display cache statistics

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      CACHE SYSTEM                                 ║
╠══════════════════════════════════════════════════════════════════╣

Testing Cache<int, int> (squares cache):

cache.GetOrAdd(5, x => x * x)
  [MISS] Computing: 5 * 5 = 25
  Result: 25

cache.GetOrAdd(3, x => x * x)
  [MISS] Computing: 3 * 3 = 9
  Result: 9

cache.GetOrAdd(5, x => x * x)
  [HIT] Returning cached value
  Result: 25

cache.GetOrAdd(5, x => x * x)
  [HIT] Returning cached value
  Result: 25

cache.GetOrAdd(7, x => x * x)
  [MISS] Computing: 7 * 7 = 49
  Result: 49

═══════════════════════════════════════════════════════════════════
Cache Statistics:
  Total requests: 5
  Hits: 2 (40%)
  Misses: 3 (60%)
  Cached items: 3

Cached Keys: 5, 3, 7

Removing key 3...
  Removed successfully.

Clearing cache...
  Cache cleared.
  Cached items: 0
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete inventory management system.

**Requirements:**
1. Product class: Id, Name, Category, Price, Quantity
2. Dictionary<int, Product> for product storage
3. Dictionary<string, List<int>> for category indexing
4. Operations: Add, Update, Delete, Search by category
5. Low stock alerts (quantity < threshold)
6. Price analysis per category
7. Transaction history tracking
8. Generate comprehensive report

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                      INVENTORY MANAGEMENT SYSTEM                          ║
╠══════════════════════════════════════════════════════════════════════════╣

=== ADDING PRODUCTS ===

[ADD] ID: 1001, Apple (Fruit), $1.50 × 100
[ADD] ID: 1002, Banana (Fruit), $0.75 × 150
[ADD] ID: 1003, Milk (Dairy), $3.50 × 50
[ADD] ID: 1004, Cheese (Dairy), $5.00 × 30
[ADD] ID: 1005, Bread (Bakery), $2.25 × 45
[ADD] ID: 1006, Orange (Fruit), $2.00 × 80

=== CATEGORY INDEX ===

Fruit: [1001, 1002, 1006]
Dairy: [1003, 1004]
Bakery: [1005]

=== SEARCH BY CATEGORY: Fruit ===

Products in "Fruit":
  1001: Apple - $1.50 (100 in stock)
  1002: Banana - $0.75 (150 in stock)
  1006: Orange - $2.00 (80 in stock)

=== UPDATE OPERATION ===

Updating ID 1003 quantity: 50 → 15
  [WARNING] Low stock alert for Milk!

=== INVENTORY REPORT ===

╔══════════════════════════════════════════════════════════════════════════╗
║                         INVENTORY SUMMARY                                 ║
╠══════════════════════════════════════════════════════════════════════════╣

All Products:
─────────────────────────────────────────────────────────────────────────────
ID    │ Name      │ Category │ Price  │ Qty  │ Value    │ Status
──────┼───────────┼──────────┼────────┼──────┼──────────┼────────────────
1001  │ Apple     │ Fruit    │ $1.50  │ 100  │ $150.00  │ OK
1002  │ Banana    │ Fruit    │ $0.75  │ 150  │ $112.50  │ OK
1003  │ Milk      │ Dairy    │ $3.50  │ 15   │ $52.50   │ LOW STOCK ⚠
1004  │ Cheese    │ Dairy    │ $5.00  │ 30   │ $150.00  │ OK
1005  │ Bread     │ Bakery   │ $2.25  │ 45   │ $101.25  │ OK
1006  │ Orange    │ Fruit    │ $2.00  │ 80   │ $160.00  │ OK

═══════════════════════════════════════════════════════════════════════════

Category Analysis:
─────────────────────────────────────────────────────────────────────────────
Category │ Products │ Total Qty │ Avg Price │ Total Value
─────────┼──────────┼───────────┼───────────┼─────────────
Fruit    │ 3        │ 330       │ $1.42     │ $422.50
Dairy    │ 2        │ 45        │ $4.25     │ $202.50
Bakery   │ 1        │ 45        │ $2.25     │ $101.25

═══════════════════════════════════════════════════════════════════════════

Low Stock Alerts (Qty < 20):
  ⚠ 1003: Milk - Only 15 remaining!

═══════════════════════════════════════════════════════════════════════════

Transaction History:
  [10:30:00] ADD: 1001 Apple ×100
  [10:30:01] ADD: 1002 Banana ×150
  [10:30:02] ADD: 1003 Milk ×50
  [10:30:03] ADD: 1004 Cheese ×30
  [10:30:04] ADD: 1005 Bread ×45
  [10:30:05] ADD: 1006 Orange ×80
  [10:31:00] UPDATE: 1003 Qty: 50→15

═══════════════════════════════════════════════════════════════════════════

Grand Totals:
  Total Products: 6
  Total Items: 420
  Total Inventory Value: $726.25

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use Dictionary<int, Product> as primary storage
- Use Dictionary<string, List<int>> for category indexing
- Implement all CRUD operations
- Maintain category index on add/delete
- Track transaction history
- Calculate statistics using LINQ
- Handle edge cases (duplicate IDs, missing products)

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Dictionary operations work properly?
2. **Access Patterns**: Safe access with TryGetValue?
3. **Iteration**: Proper enumeration techniques?
4. **Design**: Good use of dictionary for the problem?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Dictionary concepts.*
