# Abstract Classes - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Abstract Classes concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a simple abstract class and derived class.

**Requirements:**
1. Create abstract `Animal` class with abstract `Speak()` method
2. Add concrete `Sleep()` method that prints a message
3. Create `Dog` class that implements `Speak()`
4. Demonstrate that Animal cannot be instantiated

**Expected Output:**
```
Attempting to use abstract class...
Cannot create instance of abstract Animal class!

Creating Dog (derived class):
Dog says: Woof! Woof!
Dog is sleeping... Zzz

Abstract classes provide the blueprint, derived classes provide implementation.
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create abstract class with abstract property.

**Requirements:**
1. Create abstract `Shape` with abstract `Area` property
2. Create `Square` that implements the property
3. Create `Circle` that implements the property
4. Display areas for both shapes

**Expected Output:**
```
Abstract property demonstration:

Square (side = 5):
  Area: 25.00

Circle (radius = 3):
  Area: 28.27

Same property, different implementations!
```

---

## Level 3: Application (Easy)

**Challenge:** Mix abstract and concrete members.

**Requirements:**
1. Create abstract `Employee` with Name property and abstract `CalculatePay()`
2. Add concrete `DisplayInfo()` method
3. Create `HourlyEmployee` with hours and rate
4. Create `SalariedEmployee` with monthly salary
5. Process both polymorphically

**Expected Output:**
```
Employee Payroll System:

Employee: Alice (Hourly)
  Hours: 45, Rate: $25.00
  Pay: $1,187.50 (includes overtime)

Employee: Bob (Salaried)
  Monthly Salary: $5,000.00
  Pay: $5,000.00

Total Payroll: $6,187.50
```

---

## Level 4: Application (Easy)

**Challenge:** Use constructor in abstract class.

**Requirements:**
1. Create abstract `Vehicle` with Brand, Model in constructor
2. Add abstract `Start()` method
3. Create `Car` that calls base constructor and adds Doors
4. Create `Motorcycle` that calls base constructor
5. Show constructor chaining

**Expected Output:**
```
Creating vehicles with constructor chaining:

Creating Car:
  [Vehicle constructor] Brand: Toyota, Model: Camry
  [Car constructor] Adding 4 doors
  Starting: Toyota Camry engine ignition!

Creating Motorcycle:
  [Vehicle constructor] Brand: Harley, Model: Sportster
  [Motorcycle constructor] Engine size: 1200cc
  Starting: Harley Sportster kickstart!

Constructor chain: Derived → Base → execution
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a notification system with abstract base.

**Requirements:**
1. Abstract `Notification` with Recipient, Message, abstract `Send()`
2. Add concrete `Validate()` method
3. `EmailNotification` with subject, implements Send
4. `SMSNotification` with character limit check
5. `PushNotification` with device token
6. Process list of notifications

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              NOTIFICATION SYSTEM                            ║
╠════════════════════════════════════════════════════════════╣

Processing notifications...

[EMAIL] To: user@example.com
  Subject: Welcome!
  Validating... ✓
  Sending via SMTP... Sent!

[SMS] To: +1-555-1234
  Message: Your code is 123456 (18 chars)
  Validating... ✓
  Sending via SMS gateway... Sent!

[PUSH] To Device: iPhone-ABC123
  Alert: New message received
  Validating... ✓
  Sending via APNs... Sent!

All 3 notifications sent successfully.
```

---

## Level 6: Integration (Moderate)

**Challenge:** Implement template method pattern.

**Requirements:**
1. Abstract `ReportGenerator` with template method `Generate()`
2. Template calls: GatherData(), FormatData(), OutputReport()
3. GatherData and FormatData are abstract
4. OutputReport is virtual with default
5. Create `SalesReport` and `InventoryReport`

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              REPORT GENERATION SYSTEM                       ║
╠════════════════════════════════════════════════════════════╣

Generating Sales Report...
  [Step 1] Gathering sales data from database...
  [Step 2] Formatting as currency values...
  [Step 3] Outputting to PDF...
  Report complete: sales_report.pdf

Generating Inventory Report...
  [Step 1] Gathering inventory counts...
  [Step 2] Formatting with stock levels...
  [Step 3] Outputting to Excel... (custom override)
  Report complete: inventory_report.xlsx

Template method ensured consistent process!
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Build a payment processing system.

**Requirements:**
1. Abstract `PaymentProcessor` with:
   - Properties: Amount, Currency, TransactionId
   - Abstract: ValidatePayment(), ProcessPayment(), GenerateReceipt()
   - Concrete: ExecuteTransaction() that calls all in order
2. `CreditCardProcessor` with card validation
3. `PayPalProcessor` with email verification
4. `CryptoProcessor` with wallet address validation
5. Include error handling

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   PAYMENT PROCESSING SYSTEM                       ║
╠══════════════════════════════════════════════════════════════════╣

Transaction 1: Credit Card Payment
  Amount: $150.00 USD
  [Validate] Checking card number ****1234... ✓
  [Validate] Checking expiry date... ✓
  [Validate] Checking CVV... ✓
  [Process] Contacting payment gateway...
  [Process] Authorization received!
  [Receipt] Transaction ID: TXN-001
            Method: Credit Card
            Amount: $150.00
            Status: APPROVED

Transaction 2: PayPal Payment
  Amount: $75.50 USD
  [Validate] Verifying PayPal account: user@email.com... ✓
  [Process] Redirecting to PayPal...
  [Process] Payment confirmed!
  [Receipt] Transaction ID: TXN-002
            Method: PayPal
            Amount: $75.50
            Status: APPROVED

Transaction 3: Crypto Payment
  Amount: 0.005 BTC
  [Validate] Verifying wallet address... ✓
  [Process] Broadcasting to blockchain...
  [Process] Awaiting confirmations...
  [Receipt] Transaction ID: TXN-003
            Method: Bitcoin
            Amount: 0.005 BTC
            Status: PENDING (1/3 confirmations)

═══════════════════════════════════════════════════════════════════
Summary: 3 transactions processed
  Approved: 2
  Pending: 1
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a game entity system with abstract hierarchy.

**Requirements:**
1. Abstract `GameEntity` base: Position, Health, abstract Update(), abstract Render()
2. Abstract `Character` extends GameEntity: Name, Speed, abstract Attack()
3. `Player` class: Experience, Level, inventory
4. `Enemy` class: Damage, AI behavior
5. `NPC` class: Dialogue, quest info
6. Simulate game loop calling Update/Render

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    GAME ENTITY SYSTEM                             ║
╠══════════════════════════════════════════════════════════════════╣

Initializing game entities...

Entity: Player "Hero"
  Position: (100, 200)
  Health: 100/100
  Level: 5, XP: 2500

Entity: Enemy "Goblin"
  Position: (150, 180)
  Health: 50/50
  Damage: 10

Entity: NPC "Merchant"
  Position: (200, 200)
  Health: 999/999
  Has Quest: Yes

═══════════════════════════════════════════════════════════════════
                         GAME LOOP
═══════════════════════════════════════════════════════════════════

Frame 1:
  [Player] Update: Processing input, moving to (105, 200)
  [Player] Render: Drawing hero sprite at (105, 200)
  [Enemy] Update: AI calculating path to player
  [Enemy] Render: Drawing goblin at (145, 185)
  [NPC] Update: Checking player proximity
  [NPC] Render: Drawing merchant with quest marker

Frame 2:
  [Player] Attack! Sword swing for 25 damage
  [Enemy] Takes 25 damage, Health: 25/50
  [Enemy] Attack! Claw swipe for 10 damage
  [Player] Takes 10 damage, Health: 90/100

Combat Summary:
  Hero: 90/100 HP
  Goblin: 25/50 HP

Abstract hierarchy: GameEntity → Character → Player/Enemy/NPC
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete file system simulation with abstract classes.

**Requirements:**
1. Abstract `FileSystemItem`: Name, Path, Size, Created, abstract GetInfo(), abstract Delete()
2. `File` class: Extension, Content, Read(), Write()
3. `Directory` class: Contains items (composite pattern), Add(), Remove(), ListContents()
4. Abstract `Permission` system integrated
5. `TextFile`, `ImageFile`, `ExecutableFile` extending File
6. Implement recursive operations (size calculation, search)
7. Include proper error handling

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                      FILE SYSTEM SIMULATION                               ║
╠══════════════════════════════════════════════════════════════════════════╣

Creating file system structure...

/root
├── /documents
│   ├── report.txt (TextFile, 2.5 KB)
│   ├── notes.txt (TextFile, 1.2 KB)
│   └── /images
│       ├── photo.jpg (ImageFile, 150 KB)
│       └── logo.png (ImageFile, 45 KB)
├── /programs
│   └── app.exe (ExecutableFile, 5.2 MB)
└── readme.txt (TextFile, 0.5 KB)

═══════════════════════════════════════════════════════════════════════════
                          FILE OPERATIONS
═══════════════════════════════════════════════════════════════════════════

Reading TextFile: /root/documents/report.txt
  Content: "Quarterly sales report..."
  Permissions: Read ✓, Write ✓

Reading ImageFile: /root/documents/images/photo.jpg
  Dimensions: 1920x1080
  Format: JPEG
  Permissions: Read ✓, Write ✗

Executing: /root/programs/app.exe
  [Permission Check] Execute permission... ✓
  [Execution] Running application...
  [Output] "Hello from app.exe!"

═══════════════════════════════════════════════════════════════════════════
                        RECURSIVE OPERATIONS
═══════════════════════════════════════════════════════════════════════════

Calculating total size of /root:
  /root/documents: 3.7 KB
    /root/documents/images: 195 KB
  /root/programs: 5.2 MB
  /root/readme.txt: 0.5 KB
  ─────────────────────
  Total: 5.4 MB

Searching for "*.txt" in /root:
  Found: /root/documents/report.txt
  Found: /root/documents/notes.txt
  Found: /root/readme.txt
  Total: 3 files

Searching for files > 100 KB:
  Found: /root/documents/images/photo.jpg (150 KB)
  Found: /root/programs/app.exe (5.2 MB)
  Total: 2 files

═══════════════════════════════════════════════════════════════════════════
                          DELETE OPERATION
═══════════════════════════════════════════════════════════════════════════

Deleting /root/documents/images...
  [Check] Directory not empty
  [Recursive] Deleting photo.jpg... ✓
  [Recursive] Deleting logo.png... ✓
  [Delete] Removing directory /images... ✓

Updated structure:
/root
├── /documents
│   ├── report.txt
│   └── notes.txt
├── /programs
│   └── app.exe
└── readme.txt

═══════════════════════════════════════════════════════════════════════════
                         CLASS HIERARCHY
═══════════════════════════════════════════════════════════════════════════

FileSystemItem (abstract)
├── File (abstract)
│   ├── TextFile
│   ├── ImageFile
│   └── ExecutableFile
└── Directory

Permission (abstract)
├── ReadPermission
├── WritePermission
└── ExecutePermission

Abstract methods implemented:
  FileSystemItem.GetInfo() - 6 implementations
  FileSystemItem.Delete() - 4 implementations
  File.Read() - 3 implementations
  File.Write() - 2 implementations (ExecutableFile read-only)

Composite Pattern: Directory contains FileSystemItem[]

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Proper abstract class hierarchy
- Composite pattern for Directory
- Abstract methods with meaningful implementations
- Recursive operations through abstraction
- Error handling for permissions and operations
- Clear demonstration of abstract vs concrete members

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Abstract class implemented properly?
2. **Implementation**: All abstract members overridden?
3. **Design**: Good use of abstract vs concrete?
4. **Architecture**: Clean inheritance hierarchy?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Abstract Classes concepts.*
