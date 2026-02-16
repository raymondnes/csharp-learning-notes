# Try-Catch Error Handling - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Exception Handling concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Write a basic try-catch block to handle a division error.

**Requirements:**
1. Attempt to divide 10 by 0
2. Catch the exception and display an error message
3. Show the program continues after the catch

**Expected Output:**
```
Attempting to divide 10 by 0...
Error caught: Attempted to divide by zero.
Program continues normally.
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Handle a FormatException when parsing user input.

**Requirements:**
1. Try to parse "hello" as an integer
2. Catch the FormatException
3. Display the exception message and type

**Expected Output:**
```
Attempting to parse "hello" as integer...
Exception Type: FormatException
Message: Input string was not in a correct format.
Parsing failed!
```

---

## Level 3: Application (Easy)

**Challenge:** Use multiple catch blocks for different exceptions.

**Requirements:**
1. Create an array with 3 elements
2. Try to access index 10 (IndexOutOfRangeException)
3. Try to parse invalid string (FormatException)
4. Have specific catch blocks for each
5. Include a general Exception catch as fallback

**Expected Output:**
```
Test 1: Accessing invalid array index...
Caught IndexOutOfRangeException: Index was outside the bounds of the array.

Test 2: Parsing invalid number...
Caught FormatException: Input string was not in a correct format.

Test 3: Other error...
Caught general Exception: [message]
```

---

## Level 4: Application (Easy)

**Challenge:** Use the finally block for cleanup.

**Requirements:**
1. Create a simulation where a resource is "opened"
2. Cause an exception during processing
3. Use finally to ensure "cleanup" always happens
4. Show cleanup runs even after exception

**Expected Output:**
```
Opening resource...
Resource opened successfully.

Processing data...
ERROR: Simulated processing error!

Finally block executing...
Resource closed and cleaned up.

Note: Program did not crash, cleanup completed!
```

---

## Level 5: Integration (Moderate)

**Challenge:** Throw and catch custom exceptions.

**Requirements:**
1. Create a `NegativeNumberException` (inherits from Exception)
2. Create a method `CalculateSquareRoot(double n)` that throws this exception for negative numbers
3. Handle both negative input and the NaN case
4. Display appropriate error messages

**Expected Output:**
```
Testing CalculateSquareRoot:

Input: 16
  Result: 4

Input: -4
  Error: NegativeNumberException - Cannot calculate square root of negative number: -4

Input: 25
  Result: 5

Input: -100
  Error: NegativeNumberException - Cannot calculate square root of negative number: -100
```

---

## Level 6: Integration (Moderate)

**Challenge:** Implement a robust input validation system.

**Requirements:**
1. Create a method that gets a valid integer between min and max
2. Handle: FormatException, OverflowException, ArgumentOutOfRangeException
3. Keep asking until valid input (simulate with test inputs)
4. Track and display number of attempts

**Simulated inputs:** "abc", "99999999999999", "-5", "150", "42"
**Valid range:** 1 to 100

**Expected Output:**
```
Getting number between 1 and 100...

Attempt 1: "abc"
  Invalid format - please enter a number.

Attempt 2: "99999999999999"
  Number too large - overflow error.

Attempt 3: "-5"
  Out of range - must be between 1 and 100.

Attempt 4: "150"
  Out of range - must be between 1 and 100.

Attempt 5: "42"
  Valid input!

Result: 42 (after 5 attempts)
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Build an exception logging system.

**Requirements:**
1. Create an `ExceptionLogger` class that:
   - Logs exceptions with timestamp
   - Tracks exception counts by type
   - Stores last N exceptions (use 5)
2. Generate several different exceptions
3. Display log summary

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    EXCEPTION LOGGER DEMO                          ║
╠══════════════════════════════════════════════════════════════════╣

Generating exceptions...

[2024-01-15 10:30:01] FormatException: Input string was not in a correct format.
[2024-01-15 10:30:02] DivideByZeroException: Attempted to divide by zero.
[2024-01-15 10:30:03] IndexOutOfRangeException: Index was outside the bounds of the array.
[2024-01-15 10:30:04] NullReferenceException: Object reference not set to an instance of an object.
[2024-01-15 10:30:05] ArgumentException: Value does not fall within the expected range.
[2024-01-15 10:30:06] FormatException: Input string was not in a correct format.

═══════════════════════════════════════════════════════════════════
                         LOG SUMMARY
═══════════════════════════════════════════════════════════════════
Total Exceptions: 6

By Type:
  FormatException: 2
  DivideByZeroException: 1
  IndexOutOfRangeException: 1
  NullReferenceException: 1
  ArgumentException: 1

Last 5 Exceptions:
  1. [10:30:02] DivideByZeroException
  2. [10:30:03] IndexOutOfRangeException
  3. [10:30:04] NullReferenceException
  4. [10:30:05] ArgumentException
  5. [10:30:06] FormatException
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Implement a retry mechanism with exponential backoff.

**Requirements:**
1. Create a method that simulates a network call (fails 3 times then succeeds)
2. Implement retry logic with:
   - Maximum 5 attempts
   - Exponential backoff (1s, 2s, 4s, 8s delays - simulate with messages)
   - Different handling for transient vs. permanent errors
3. Track and display each attempt

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   RETRY MECHANISM DEMO                            ║
╠══════════════════════════════════════════════════════════════════╣

Attempting network operation (max 5 retries)...

Attempt 1/5:
  Status: FAILED
  Error: TransientNetworkException - Connection timeout
  Action: Retrying in 1 second...

Attempt 2/5:
  Status: FAILED
  Error: TransientNetworkException - Server busy
  Action: Retrying in 2 seconds...

Attempt 3/5:
  Status: FAILED
  Error: TransientNetworkException - Connection reset
  Action: Retrying in 4 seconds...

Attempt 4/5:
  Status: SUCCESS
  Response: Data received successfully!

═══════════════════════════════════════════════════════════════════
                          SUMMARY
═══════════════════════════════════════════════════════════════════
Final Result: SUCCESS
Total Attempts: 4
Time Elapsed: 7 seconds (simulated)
Retries Used: 3

───────────────────────────────────────────────────────────────────

Testing permanent error scenario...

Attempt 1/5:
  Status: FAILED
  Error: PermanentException - Invalid API key
  Action: Not retrying - permanent error detected

Final Result: FAILED
Reason: Permanent error - retries would not help
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a comprehensive transaction system with rollback.

**Requirements:**
1. Create a `Transaction` class that simulates database operations
2. Implement operations: BeginTransaction, Commit, Rollback
3. Create `BankTransfer` that:
   - Debits source account
   - Credits destination account
   - Rolls back on any failure
4. Handle multiple exception types
5. Ensure data integrity (no partial transfers)
6. Log all operations

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    TRANSACTION SYSTEM DEMO                                ║
╠══════════════════════════════════════════════════════════════════════════╣

Initial State:
  Account A: $1,000.00
  Account B: $500.00

═══════════════════════════════════════════════════════════════════════════
                      TEST 1: Successful Transfer
═══════════════════════════════════════════════════════════════════════════
Transferring $200 from Account A to Account B...

[10:30:01] BEGIN TRANSACTION (ID: TXN001)
[10:30:01] DEBIT: Account A -$200.00 (Pending: $800.00)
[10:30:02] CREDIT: Account B +$200.00 (Pending: $700.00)
[10:30:02] COMMIT: Transaction TXN001 committed successfully

Result:
  Account A: $800.00
  Account B: $700.00
Status: ✓ Transfer successful

═══════════════════════════════════════════════════════════════════════════
                      TEST 2: Insufficient Funds
═══════════════════════════════════════════════════════════════════════════
Transferring $5000 from Account A to Account B...

[10:30:03] BEGIN TRANSACTION (ID: TXN002)
[10:30:03] DEBIT: Account A -$5000.00 FAILED
[10:30:03] Exception: InsufficientFundsException
[10:30:03] ROLLBACK: Transaction TXN002 rolled back

Result:
  Account A: $800.00 (unchanged)
  Account B: $700.00 (unchanged)
Status: ✗ Transfer failed - Insufficient funds

═══════════════════════════════════════════════════════════════════════════
                      TEST 3: Credit Failure (Rollback Demo)
═══════════════════════════════════════════════════════════════════════════
Transferring $100 to INVALID account...

[10:30:04] BEGIN TRANSACTION (ID: TXN003)
[10:30:04] DEBIT: Account A -$100.00 (Pending: $700.00)
[10:30:05] CREDIT: INVALID account FAILED
[10:30:05] Exception: AccountNotFoundException
[10:30:05] ROLLBACK: Reversing debit to Account A
[10:30:05] ROLLBACK: Account A restored to $800.00
[10:30:05] ROLLBACK: Transaction TXN003 rolled back completely

Result:
  Account A: $800.00 (restored)
  Account B: $700.00 (unchanged)
Status: ✗ Transfer failed - Destination account not found

═══════════════════════════════════════════════════════════════════════════
                        TRANSACTION LOG
═══════════════════════════════════════════════════════════════════════════
TXN001 │ A → B   │ $200.00  │ COMMITTED │ 10:30:01-10:30:02
TXN002 │ A → B   │ $5000.00 │ ROLLED BACK │ 10:30:03
TXN003 │ A → INV │ $100.00  │ ROLLED BACK │ 10:30:04-10:30:05

═══════════════════════════════════════════════════════════════════════════
                            SUMMARY
═══════════════════════════════════════════════════════════════════════════
Total Transactions: 3
Successful: 1
Failed: 2
  - Insufficient Funds: 1
  - Account Not Found: 1

Data Integrity: ✓ All balances correct, no orphaned operations

Final Balances:
  Account A: $800.00 (started: $1000, transferred out: $200)
  Account B: $700.00 (started: $500, transferred in: $200)
  Total: $1,500.00 (unchanged - system balanced)

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use try-catch-finally appropriately
- Implement proper rollback logic
- Create custom exception classes
- Ensure no partial state changes on failure
- Log all operations with timestamps
- Clean code with proper exception handling patterns

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Are exceptions caught and handled properly?
2. **Best Practices**: Specific catches? No swallowing? Proper re-throw?
3. **Resource Management**: Finally/using for cleanup?
4. **Robustness**: Does the program recover gracefully?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Try-Catch Error Handling concepts.*
