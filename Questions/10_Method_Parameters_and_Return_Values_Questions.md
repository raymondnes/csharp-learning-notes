# Method Parameters and Return Values - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Method Parameters and Return Values concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Demonstrate pass-by-value behavior with value types.

**Requirements:**
1. Create a method `TryToDouble(int number)` that doubles the parameter
2. Show that the original variable is unchanged after the method call
3. Print values before, inside, and after the method call

**Expected Output:**
```
Before: value = 10
Inside method: number = 20
After: value = 10 (unchanged)
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Use the `ref` keyword to modify a value.

**Requirements:**
1. Create a method `DoubleValue(ref int number)` that doubles the parameter
2. Show that the original variable IS changed after the method call
3. Print values before and after

**Expected Output:**
```
Before: value = 10
After: value = 20 (modified!)
```

---

## Level 3: Application (Easy)

**Challenge:** Create a swap method using ref parameters.

**Requirements:**
1. Create a `Swap(ref int a, ref int b)` method
2. Swap the values without using a third variable from outside
3. Test with two variables and show before/after values

**Expected Output:**
```
Before swap: x = 5, y = 10
After swap: x = 10, y = 5
```

---

## Level 4: Application (Easy)

**Challenge:** Use the `out` keyword to return multiple values.

**Requirements:**
1. Create a `Divide(int a, int b, out int quotient, out int remainder)` method
2. Return both the quotient and remainder of the division
3. Test with multiple pairs of numbers

**Expected Output:**
```
17 ÷ 5 = 3 remainder 2
20 ÷ 4 = 5 remainder 0
7 ÷ 3 = 2 remainder 1
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a method that uses tuples to return statistics.

**Requirements:**
1. Create `(int min, int max, double average) GetStats(params int[] numbers)`
2. Handle the edge case of empty array
3. Test with multiple arrays
4. Display results with proper formatting

**Expected Output:**
```
Array: [5, 2, 8, 1, 9]
  Min: 1
  Max: 9
  Average: 5.00

Array: [10, 10, 10]
  Min: 10
  Max: 10
  Average: 10.00

Array: [] (empty)
  Error: Cannot calculate stats for empty array
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a method combining params, optional parameters, and named arguments.

**Requirements:**
1. Create a logging method:
   ```csharp
   void Log(string message, string level = "INFO", bool timestamp = true, params string[] tags)
   ```
2. Demonstrate calling with:
   - Only required parameter
   - Required + some optional
   - Using named arguments
   - Using tags
3. Format output professionally

**Expected Output:**
```
[2024-01-15 10:30:00] [INFO] Application started
[2024-01-15 10:30:00] [ERROR] File not found
[INFO] Quick message (no timestamp)
[2024-01-15 10:30:00] [WARNING] Memory usage high [Tags: performance, system]
[2024-01-15 10:30:00] [DEBUG] User action [Tags: user, click, button]
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a TryParse pattern for a custom data type.

**Requirements:**
1. Create a `Point` struct with X and Y integer coordinates
2. Create `bool TryParsePoint(string input, out Point point)`
3. Valid formats: "(x,y)", "x,y", "x y"
4. Return false for invalid formats
5. Test with various inputs (valid and invalid)

**Expected Output:**
```
╔═══════════════════════════════════════════════════════════════╗
║                  POINT PARSER TEST                             ║
╠═══════════════════════════════════════════════════════════════╣
║ Input                    │ Result                              ║
╠══════════════════════════╪═════════════════════════════════════╣
║ "(10, 20)"               │ ✓ Point(10, 20)                     ║
║ "5,15"                   │ ✓ Point(5, 15)                      ║
║ "3 7"                    │ ✓ Point(3, 7)                       ║
║ "(100, 200)"             │ ✓ Point(100, 200)                   ║
║ "invalid"                │ ✗ Parse failed                      ║
║ "1,2,3"                  │ ✗ Parse failed                      ║
║ ""                       │ ✗ Parse failed                      ║
║ "abc, def"               │ ✗ Parse failed                      ║
╚═══════════════════════════════════════════════════════════════╝
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a comprehensive array manipulation library using various parameter techniques.

**Requirements:**
1. Implement these methods:
   - `void Fill(int[] array, int value)` - fills array with value
   - `void Resize(ref int[] array, int newSize)` - resizes array
   - `bool TryGet(int[] array, int index, out int value)` - safe access
   - `(int[] smaller, int[] larger) Split(int[] array, int threshold)`
   - `int[] Merge(params int[][] arrays)` - merge multiple arrays
2. Demonstrate each method with test cases
3. Handle edge cases properly

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║              ARRAY MANIPULATION LIBRARY                           ║
╠══════════════════════════════════════════════════════════════════╣

TEST: Fill
  Before: [0, 0, 0, 0, 0]
  Fill(array, 7)
  After:  [7, 7, 7, 7, 7]

TEST: Resize (ref)
  Before: [1, 2, 3] (length: 3)
  Resize(ref array, 5)
  After:  [1, 2, 3, 0, 0] (length: 5)
  Resize(ref array, 2)
  After:  [1, 2] (length: 2)

TEST: TryGet (out)
  Array: [10, 20, 30]
  TryGet(array, 1, out value) → true, value = 20
  TryGet(array, 5, out value) → false (out of bounds)

TEST: Split (tuple return)
  Array: [1, 5, 3, 8, 2, 9, 4]
  Split(array, 5) →
    Smaller: [1, 3, 2, 4]
    Larger:  [5, 8, 9]

TEST: Merge (params)
  Array1: [1, 2, 3]
  Array2: [4, 5]
  Array3: [6, 7, 8, 9]
  Merge(array1, array2, array3) → [1, 2, 3, 4, 5, 6, 7, 8, 9]

╚══════════════════════════════════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a "Method Signature Analyzer" that demonstrates all parameter passing mechanisms.

**Requirements:**
1. Create a demonstration program showing:
   - Value type pass-by-value vs reference
   - Reference type pass-by-value vs reference
   - ref, out, and in behavior
   - Tuple returns vs out parameters
   - params with other parameter types
2. For each mechanism, show:
   - Method signature
   - Call example
   - Before/after state
   - Memory behavior explanation
3. Create a tracking system to show when copies are made vs references passed
4. Format as an educational reference guide

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                     METHOD PARAMETER MECHANISMS                           ║
║                     Complete Reference Guide                              ║
╠══════════════════════════════════════════════════════════════════════════╣

┌──────────────────────────────────────────────────────────────────────────┐
│ 1. VALUE TYPE - PASS BY VALUE (Default)                                  │
├──────────────────────────────────────────────────────────────────────────┤
│ Signature: void Modify(int x)                                            │
│ Call:      Modify(number)                                                │
│                                                                          │
│ Memory Behavior:                                                         │
│   ┌─────┐  copy   ┌─────┐                                               │
│   │  10 │ ────── │  10 │ (separate memory)                             │
│   └─────┘         └─────┘                                               │
│   number          x (parameter)                                          │
│                                                                          │
│ Before: number = 10                                                      │
│ Inside: x modified to 99                                                 │
│ After:  number = 10 (unchanged - x was a copy)                          │
└──────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────────────────┐
│ 2. VALUE TYPE - PASS BY REFERENCE (ref)                                  │
├──────────────────────────────────────────────────────────────────────────┤
│ Signature: void Modify(ref int x)                                        │
│ Call:      Modify(ref number)                                            │
│                                                                          │
│ Memory Behavior:                                                         │
│   ┌─────┐  reference  ┌───┐                                             │
│   │  10 │ ◄───────── │ & │ (same memory)                               │
│   └─────┘             └───┘                                             │
│   number              x (alias)                                          │
│                                                                          │
│ Before: number = 10                                                      │
│ Inside: x modified to 99                                                 │
│ After:  number = 99 (changed - x was alias to number)                   │
└──────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────────────────┐
│ 3. OUT PARAMETER                                                         │
├──────────────────────────────────────────────────────────────────────────┤
│ Signature: bool TryParse(string s, out int result)                       │
│ Call:      TryParse("42", out int value)                                │
│                                                                          │
│ Key Points:                                                              │
│ • No initialization required before call                                 │
│ • Method MUST assign value before returning                              │
│ • Used for "additional return values"                                    │
│                                                                          │
│ Example:                                                                 │
│   if (int.TryParse("42", out int num))                                  │
│       Console.WriteLine(num);  // 42                                     │
└──────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────────────────┐
│ 4. IN PARAMETER (readonly reference)                                     │
├──────────────────────────────────────────────────────────────────────────┤
│ Signature: void Display(in LargeStruct data)                             │
│ Call:      Display(in myStruct)                                          │
│                                                                          │
│ Memory Behavior:                                                         │
│   ┌─────────┐  readonly ref  ┌───┐                                      │
│   │ struct  │ ◄───────────── │ & │ (no copy, can't modify)             │
│   └─────────┘                └───┘                                      │
│                                                                          │
│ Use Case: Large structs where copying is expensive                       │
│ Benefit:  No copy made, but modification prevented                       │
└──────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────────────────┐
│ 5. REFERENCE TYPE - PASS BY VALUE                                        │
├──────────────────────────────────────────────────────────────────────────┤
│ Signature: void Modify(int[] arr)                                        │
│                                                                          │
│ Memory Behavior:                                                         │
│   ┌─────────┐          ┌─────────┐                                      │
│   │ ref copy│ ────────│[1,2,3]  │ (same array!)                        │
│   └─────────┘          └─────────┘                                      │
│                                                                          │
│ • Modifying CONTENTS affects original: arr[0] = 99 ✓                    │
│ • Reassigning reference doesn't: arr = new[] {4,5,6} ✗                  │
└──────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────────────────┐
│ 6. TUPLES vs OUT (Returning Multiple Values)                             │
├──────────────────────────────────────────────────────────────────────────┤
│                                                                          │
│ OUT Parameters:                                                          │
│   void GetMinMax(int[] arr, out int min, out int max)                   │
│   GetMinMax(array, out int min, out int max);                           │
│                                                                          │
│ Tuple Return (Modern - Preferred):                                       │
│   (int min, int max) GetMinMax(int[] arr)                               │
│   var (min, max) = GetMinMax(array);                                    │
│                                                                          │
│ Comparison:                                                              │
│   • Tuples: Cleaner syntax, expressive, easy to use                     │
│   • Out: Required for TryParse pattern, slightly more performant        │
└──────────────────────────────────────────────────────────────────────────┘

╠══════════════════════════════════════════════════════════════════════════╣
║                        QUICK REFERENCE TABLE                              ║
╠══════════════════════════════════════════════════════════════════════════╣
║ Keyword │ Init Required │ Method Assigns │ Can Read │ Can Modify │ Copy? ║
╠═════════╪═══════════════╪════════════════╪══════════╪════════════╪═══════╣
║ (none)  │ Yes           │ No             │ Yes      │ Copy only  │ Yes   ║
║ ref     │ Yes           │ No             │ Yes      │ Yes        │ No    ║
║ out     │ No            │ Yes            │ After    │ Yes        │ No    ║
║ in      │ Yes           │ No             │ Yes      │ No         │ No    ║
╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Demonstrate each mechanism with working code examples
- Track and display copy operations vs reference operations
- Include comments explaining memory behavior
- Handle all edge cases properly

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Do parameters behave as expected?
2. **Understanding**: Is the mechanism used appropriately?
3. **Code Quality**: Clean, readable implementation?
4. **Edge Cases**: Proper handling of unusual situations?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Method Parameters and Return Values concepts.*
