# Classes and Objects - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Classes and Objects concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a simple class and instantiate an object.

**Requirements:**
1. Create a `Book` class with fields: `Title` (string) and `Author` (string)
2. Create a method `DisplayInfo()` that prints the book details
3. Create one book object and display its information

**Expected Output:**
```
Book Information:
Title: The Great Gatsby
Author: F. Scott Fitzgerald
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create multiple objects from the same class.

**Requirements:**
1. Create a `Car` class with fields: `Make`, `Model`, `Year`
2. Create a `Describe()` method that returns a formatted string
3. Create three different car objects
4. Display information for all cars

**Expected Output:**
```
Car 1: 2024 Tesla Model S
Car 2: 2023 BMW X5
Car 3: 2022 Honda Civic
```

---

## Level 3: Application (Easy)

**Challenge:** Create a class with methods that modify field values.

**Requirements:**
1. Create a `Counter` class with:
   - A private field `count` initialized to 0
   - Public method `Increment()` that increases count by 1
   - Public method `Decrement()` that decreases count by 1
   - Public method `GetCount()` that returns current count
   - Public method `Reset()` that sets count to 0
2. Demonstrate all methods

**Expected Output:**
```
Initial count: 0
After 3 increments: 3
After 1 decrement: 2
After reset: 0
```

---

## Level 4: Application (Easy)

**Challenge:** Use object initializer syntax and create an array of objects.

**Requirements:**
1. Create a `Student` class with: `Name`, `Grade`, `GPA` (double)
2. Create an array of 4 students using object initializer syntax
3. Display all students in a formatted table

**Expected Output:**
```
╔════════════════════════════════════════════╗
║            STUDENT ROSTER                  ║
╠════════════════╤═════════╤═════════════════╣
║ Name           │ Grade   │ GPA             ║
╠════════════════╪═════════╪═════════════════╣
║ Alice Johnson  │ 10      │ 3.85            ║
║ Bob Smith      │ 11      │ 3.42            ║
║ Carol White    │ 10      │ 3.91            ║
║ David Brown    │ 12      │ 3.67            ║
╚════════════════╧═════════╧═════════════════╝
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create interacting objects.

**Requirements:**
1. Create a `Product` class with: `Name`, `Price`, `Stock`
2. Create a `ShoppingCart` class that can:
   - Add products (track quantity for each)
   - Remove products
   - Calculate total price
   - Display cart contents
3. Demonstrate adding multiple products and checking out

**Expected Output:**
```
Adding products to cart...
Added: Laptop x1
Added: Mouse x2
Added: Keyboard x1

Shopping Cart:
─────────────────────────────────────────
Laptop       x1    @ $999.99    = $999.99
Mouse        x2    @ $29.99     = $59.98
Keyboard     x1    @ $79.99     = $79.99
─────────────────────────────────────────
Subtotal: $1,139.96
Tax (8%): $91.20
Total: $1,231.16
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a class demonstrating reference type behavior.

**Requirements:**
1. Create a `Coordinate` class with `X` and `Y` fields
2. Create a `Path` class that stores an array of Coordinates
3. Demonstrate:
   - Two variables referencing the same object
   - Modifying an object through one reference affects the other
   - Creating a true copy (new object with same values)
4. Clearly show output explaining what's happening

**Expected Output:**
```
=== Reference Type Demonstration ===

Creating original coordinate: (10, 20)
Creating reference (same object): coord2 = coord1

Modifying through coord2: coord2.X = 100
Result:
  coord1: (100, 20) <- Also changed!
  coord2: (100, 20)
Explanation: Both variables point to the same object.

Creating a copy (new object): coord3 = new Coordinate(coord1)
Modifying copy: coord3.X = 500
Result:
  coord1: (100, 20) <- Unchanged
  coord3: (500, 20) <- Only this changed
Explanation: coord3 is a different object.

Testing equality:
  coord1 == coord2: True (same reference)
  coord1 == coord3: False (different objects, even if values match)
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a simple game character system.

**Requirements:**
1. Create a `Character` class with:
   - Fields: Name, Health, MaxHealth, AttackPower, Defense, Level
   - Method: Attack(Character target) - deals damage
   - Method: TakeDamage(int damage) - reduces health considering defense
   - Method: Heal(int amount) - restores health up to max
   - Method: IsAlive() - returns bool
   - Method: LevelUp() - increases stats
   - Method: DisplayStatus() - shows character stats
2. Create a simple combat simulation between two characters
3. Continue combat until one character is defeated

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║                    BATTLE SIMULATION                          ║
╠══════════════════════════════════════════════════════════════╣

Creating characters...
Hero: HP 100/100, ATK 15, DEF 5, LVL 1
Goblin: HP 50/50, ATK 10, DEF 2, LVL 1

=== Battle Start! ===

Round 1:
  Hero attacks Goblin for 13 damage (15 - 2 DEF)
  Goblin HP: 37/50
  Goblin attacks Hero for 5 damage (10 - 5 DEF)
  Hero HP: 95/100

Round 2:
  Hero attacks Goblin for 13 damage
  Goblin HP: 24/50
  Goblin attacks Hero for 5 damage
  Hero HP: 90/100

Round 3:
  Hero attacks Goblin for 13 damage
  Goblin HP: 11/50
  Goblin attacks Hero for 5 damage
  Hero HP: 85/100

Round 4:
  Hero attacks Goblin for 13 damage
  Goblin HP: 0/50
  Goblin is defeated!

=== Battle End! ===
Winner: Hero
Final HP: 85/100
Rounds: 4

Hero gains experience and levels up!
Hero: HP 120/120, ATK 18, DEF 6, LVL 2
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a library management system using multiple classes.

**Requirements:**
1. Create classes:
   - `Book`: Title, Author, ISBN, IsAvailable
   - `Member`: Name, MemberId, BorrowedBooks (array)
   - `Library`: Books array, Members array, with methods for:
     - AddBook, RemoveBook
     - RegisterMember
     - BorrowBook(memberId, isbn)
     - ReturnBook(memberId, isbn)
     - DisplayCatalog
     - DisplayMemberInfo
2. Demonstrate all functionality

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    LIBRARY MANAGEMENT SYSTEM                      ║
╠══════════════════════════════════════════════════════════════════╣

Initializing library...
Added 5 books to catalog
Registered 2 members

=== LIBRARY CATALOG ===
ISBN          Title                          Author              Status
──────────────────────────────────────────────────────────────────────
978-0-13     Clean Code                     Robert Martin       Available
978-0-59     JavaScript: Good Parts         Douglas Crockford   Available
978-0-20     Design Patterns                Gang of Four        Available
978-0-07     The C Programming Lang         K&R                 Available
978-0-32     The Pragmatic Programmer       Hunt & Thomas       Available

=== MEMBER OPERATIONS ===

Member M001 (Alice) borrows "Clean Code"...
✓ Book borrowed successfully

Member M002 (Bob) borrows "Design Patterns"...
✓ Book borrowed successfully

Member M001 (Alice) tries to borrow "Design Patterns"...
✗ Book not available (already borrowed)

=== UPDATED CATALOG ===
978-0-13     Clean Code                     Robert Martin       Borrowed
978-0-59     JavaScript: Good Parts         Douglas Crockford   Available
978-0-20     Design Patterns                Gang of Four        Borrowed
978-0-07     The C Programming Lang         K&R                 Available
978-0-32     The Pragmatic Programmer       Hunt & Thomas       Available

=== MEMBER INFO ===
Member: Alice (M001)
  Borrowed Books: Clean Code

Member: Bob (M002)
  Borrowed Books: Design Patterns

Member M001 returns "Clean Code"...
✓ Book returned successfully

=== FINAL STATE ===
Available books: 4
Borrowed books: 1
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a complete banking system with multiple account types.

**Requirements:**
1. Create classes:
   - `Transaction`: Amount, Type (Deposit/Withdrawal/Transfer), Date, Description
   - `Account`: AccountNumber, Balance, Owner, TransactionHistory
     - Methods: Deposit, Withdraw, GetTransactionHistory, GetStatement
   - `CheckingAccount` (extends Account concepts): Overdraft limit
   - `SavingsAccount` (extends Account concepts): Interest rate, ApplyInterest
   - `Customer`: Name, CustomerId, Accounts array
   - `Bank`: Customers, Accounts, with methods for:
     - CreateCustomer
     - OpenAccount
     - Transfer (between accounts)
     - GenerateReport
2. Implement proper validation (insufficient funds, overdraft limits, etc.)
3. Generate formatted statements

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════╗
║                      BANKING SYSTEM DEMO                              ║
╠══════════════════════════════════════════════════════════════════════╣

=== CUSTOMER REGISTRATION ===
Created customer: Alice Johnson (C001)
Created customer: Bob Smith (C002)

=== ACCOUNT CREATION ===
Opened Checking Account CHK001 for Alice (Overdraft: $500)
Opened Savings Account SAV001 for Alice (Interest: 2.5%)
Opened Checking Account CHK002 for Bob (Overdraft: $200)

=== TRANSACTIONS ===
[CHK001] Deposit $1,000.00 - Initial deposit
[CHK001] Withdrawal $200.00 - ATM withdrawal
[CHK001] Balance: $800.00

[SAV001] Deposit $5,000.00 - Initial deposit
[SAV001] Applying monthly interest (2.5% / 12)
[SAV001] Interest earned: $10.42
[SAV001] Balance: $5,010.42

[CHK002] Deposit $500.00 - Initial deposit
[CHK002] Withdrawal $600.00 - Overdraft used
[CHK002] Balance: -$100.00 (Overdraft: $100.00 of $200.00 used)

=== TRANSFER ===
Transfer $300 from CHK001 to CHK002...
✓ Transfer successful
[CHK001] Balance: $500.00
[CHK002] Balance: $200.00

=== ACCOUNT STATEMENTS ===

┌────────────────────────────────────────────────────────────────────┐
│                     ACCOUNT STATEMENT                               │
│ Account: CHK001 (Checking)                                         │
│ Owner: Alice Johnson                                                │
│ Statement Period: 2024-01-01 to 2024-01-31                         │
├────────────────────────────────────────────────────────────────────┤
│ Date       │ Type        │ Description          │ Amount   │ Balance│
├────────────┼─────────────┼──────────────────────┼──────────┼────────┤
│ 2024-01-01 │ DEPOSIT     │ Initial deposit      │ +1000.00 │ 1000.00│
│ 2024-01-05 │ WITHDRAWAL  │ ATM withdrawal       │  -200.00 │  800.00│
│ 2024-01-15 │ TRANSFER    │ To CHK002            │  -300.00 │  500.00│
├────────────────────────────────────────────────────────────────────┤
│ Opening Balance: $0.00                                              │
│ Total Deposits: $1,000.00                                           │
│ Total Withdrawals: $500.00                                          │
│ Closing Balance: $500.00                                            │
└────────────────────────────────────────────────────────────────────┘

=== BANK SUMMARY ===
Total Customers: 2
Total Accounts: 3
  Checking Accounts: 2
  Savings Accounts: 1
Total Deposits: $6,500.00
Total Interest Paid: $10.42
Total Bank Assets: $5,710.42

╚══════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Proper encapsulation (private fields where appropriate)
- Meaningful method names
- Proper validation with error messages
- Clean code organization
- Comments explaining design decisions

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Do classes and objects work correctly?
2. **Design**: Is the class structure logical?
3. **Encapsulation**: Are fields properly protected?
4. **Code Quality**: Clean, readable, well-organized?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Classes and Objects concepts.*
