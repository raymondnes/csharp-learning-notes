# Properties and Encapsulation - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Properties and Encapsulation concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a class with auto-implemented properties.

**Requirements:**
1. Create a `Book` class with auto-properties: Title, Author, Year
2. Create a book object and set properties
3. Display the book information

**Expected Output:**
```
Book created with auto-properties:
Title: The Great Gatsby
Author: F. Scott Fitzgerald
Year: 1925
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a read-only property.

**Requirements:**
1. Create a `Circle` class with Radius property
2. Add a read-only Area property (calculated)
3. Demonstrate that Area cannot be set directly

**Expected Output:**
```
Circle with radius 5:
Radius: 5
Area: 78.54 (read-only, calculated)

Attempting to set Area directly...
Compile error: Property 'Area' is read-only
```

---

## Level 3: Application (Easy)

**Challenge:** Add validation to a property setter.

**Requirements:**
1. Create a `Person` class with Age property
2. Age must be between 0 and 150
3. Throw ArgumentException for invalid values
4. Test with valid and invalid ages

**Expected Output:**
```
Setting valid ages:
Age set to 25 ✓
Age set to 0 ✓
Age set to 150 ✓

Setting invalid ages:
Age -5: ArgumentException - Age must be between 0 and 150
Age 200: ArgumentException - Age must be between 0 and 150
```

---

## Level 4: Application (Easy)

**Challenge:** Use different access modifiers on get and set.

**Requirements:**
1. Create a `BankAccount` class
2. Balance: public get, private set
3. Create Deposit and Withdraw methods
4. Show that Balance cannot be set directly from outside

**Expected Output:**
```
Creating account with initial balance $1000...

Reading balance: $1000.00 ✓ (public getter works)
Depositing $500: $1500.00 ✓
Withdrawing $200: $1300.00 ✓

Attempting to set balance directly...
Compile error: Property 'Balance' setter is inaccessible
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create computed properties from other properties.

**Requirements:**
1. Create a `Rectangle` class with Width and Height
2. Add computed properties: Area, Perimeter, IsSquare
3. Show how computed properties update automatically

**Expected Output:**
```
Rectangle 10 x 5:
  Width: 10
  Height: 5
  Area: 50 (computed)
  Perimeter: 30 (computed)
  IsSquare: False (computed)

Changing to 7 x 7:
  Width: 7
  Height: 7
  Area: 49 (auto-updated)
  Perimeter: 28 (auto-updated)
  IsSquare: True (auto-updated)
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a Temperature class with multiple unit properties.

**Requirements:**
1. Store temperature internally in Celsius
2. Create properties: Celsius, Fahrenheit, Kelvin
3. All three can get AND set (converts automatically)
4. Add validation (cannot go below absolute zero)

**Expected Output:**
```
Setting temperature to 100°C:
  Celsius: 100.00°C
  Fahrenheit: 212.00°F
  Kelvin: 373.15K

Setting temperature to 32°F:
  Celsius: 0.00°C
  Fahrenheit: 32.00°F
  Kelvin: 273.15K

Setting temperature to 0K (absolute zero):
  Celsius: -273.15°C
  Fahrenheit: -459.67°F
  Kelvin: 0.00K

Attempting -300°C (below absolute zero):
  Error: Temperature cannot be below absolute zero (-273.15°C)
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement property change notification.

**Requirements:**
1. Create a `Person` class with Name, Age, Email
2. Implement INotifyPropertyChanged interface concept (simplified)
3. Fire an event whenever any property changes
4. Track old and new values
5. Display change log

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║              PROPERTY CHANGE NOTIFICATION                     ║
╠══════════════════════════════════════════════════════════════╣

Creating person and subscribing to changes...

p.Name = "Alice";
  [CHANGE] Name: null → "Alice"

p.Age = 25;
  [CHANGE] Age: 0 → 25

p.Email = "alice@example.com";
  [CHANGE] Email: null → "alice@example.com"

p.Name = "Alice Johnson";
  [CHANGE] Name: "Alice" → "Alice Johnson"

p.Age = 25;  // Same value
  (No change event - value unchanged)

═══════════════════════════════════════════════════════════════
                      CHANGE LOG
═══════════════════════════════════════════════════════════════
1. Name changed: null → "Alice"
2. Age changed: 0 → 25
3. Email changed: null → "alice@example.com"
4. Name changed: "Alice" → "Alice Johnson"

Total changes: 4
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Build a configuration system with property validation and defaults.

**Requirements:**
1. Create `AppSettings` class with properties:
   - ServerUrl (required, must be valid URL)
   - Port (1024-65535, default 8080)
   - MaxConnections (1-1000, default 100)
   - Timeout (TimeSpan, 1s-5min, default 30s)
   - EnableLogging (bool, default true)
2. Implement TrySet methods that return bool instead of throwing
3. Show validation errors and default values

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   APP SETTINGS MANAGER                            ║
╠══════════════════════════════════════════════════════════════════╣

Default values:
  ServerUrl: (not set - required)
  Port: 8080
  MaxConnections: 100
  Timeout: 00:00:30
  EnableLogging: True

Setting values...

ServerUrl = "https://api.example.com" ✓
Port = 443 ✓
MaxConnections = 500 ✓
Timeout = 60 seconds ✓
EnableLogging = false ✓

Current settings:
  ServerUrl: https://api.example.com
  Port: 443
  MaxConnections: 500
  Timeout: 00:01:00
  EnableLogging: False

Testing invalid values:

TrySetPort(80):
  ✗ Failed: Port must be between 1024 and 65535
  Current value unchanged: 443

TrySetServerUrl("not-a-url"):
  ✗ Failed: Invalid URL format
  Current value unchanged: https://api.example.com

TrySetMaxConnections(5000):
  ✗ Failed: MaxConnections must be between 1 and 1000
  Current value unchanged: 500

TrySetTimeout(10 minutes):
  ✗ Failed: Timeout must be between 1 second and 5 minutes
  Current value unchanged: 00:01:00

Validation summary:
  Successful sets: 5
  Failed attempts: 4
  Configuration valid: True
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive Product inventory system with encapsulation.

**Requirements:**
1. Create `Product` class with:
   - Id (read-only, generated)
   - Name (required, 1-100 chars)
   - Price (positive, 2 decimal precision)
   - Quantity (non-negative, with low stock warning)
   - Category (from predefined list)
   - IsAvailable (computed: quantity > 0)
   - TotalValue (computed: price × quantity)
   - LowStockThreshold (configurable per product)
2. Create `Inventory` class managing products with:
   - Private collection
   - AddProduct, RemoveProduct, UpdateStock
   - Search by category
   - Stock alerts
3. Demonstrate full encapsulation with no direct field access

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    PRODUCT INVENTORY SYSTEM                               ║
╠══════════════════════════════════════════════════════════════════════════╣

Adding products to inventory...

Product added:
  ID: PRD-001 (auto-generated, read-only)
  Name: "Wireless Mouse"
  Price: $29.99 (validated to 2 decimals)
  Quantity: 50
  Category: Electronics
  Low Stock Threshold: 10
  Total Value: $1,499.50

Product added:
  ID: PRD-002
  Name: "USB-C Cable"
  Price: $9.99
  Quantity: 8 ⚠ LOW STOCK (below threshold of 10)
  Category: Electronics
  Total Value: $79.92

═══════════════════════════════════════════════════════════════════════════
                           INVENTORY VIEW
═══════════════════════════════════════════════════════════════════════════
ID       │ Name              │ Price   │ Qty  │ Status      │ Value
─────────┼───────────────────┼─────────┼──────┼─────────────┼──────────
PRD-001  │ Wireless Mouse    │ $29.99  │ 50   │ In Stock    │ $1,499.50
PRD-002  │ USB-C Cable       │ $9.99   │ 8    │ ⚠ Low Stock │ $79.92
PRD-003  │ Laptop Stand      │ $49.99  │ 0    │ Out of Stock│ $0.00
PRD-004  │ Webcam HD         │ $79.99  │ 25   │ In Stock    │ $1,999.75
─────────────────────────────────────────────────────────────────────────
Total Inventory Value: $3,579.17

═══════════════════════════════════════════════════════════════════════════
                        VALIDATION TESTS
═══════════════════════════════════════════════════════════════════════════

Attempting invalid operations:

Set price to -10:
  ✗ ArgumentException: Price must be positive

Set quantity to -5:
  ✗ ArgumentException: Quantity cannot be negative

Set name to empty:
  ✗ ArgumentException: Name is required (1-100 characters)

Set name to 150 chars:
  ✗ ArgumentException: Name must not exceed 100 characters

Directly access _quantity field:
  ✗ Not possible - field is private (encapsulation working!)

═══════════════════════════════════════════════════════════════════════════
                         STOCK UPDATES
═══════════════════════════════════════════════════════════════════════════

Selling 45 units of PRD-001:
  Before: Qty 50, Status: In Stock
  After: Qty 5, Status: ⚠ Low Stock
  Alert: Stock below threshold!

Restocking PRD-002 with 20 units:
  Before: Qty 8 ⚠
  After: Qty 28, Status: In Stock
  Stock normalized.

═══════════════════════════════════════════════════════════════════════════
                           SUMMARY
═══════════════════════════════════════════════════════════════════════════
Total Products: 4
In Stock: 2
Low Stock: 1
Out of Stock: 1
Total Value: $3,579.17

Encapsulation verified:
✓ All fields are private
✓ All access through properties
✓ Validation on all setters
✓ Computed properties working
✓ Read-only properties protected

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- No public fields - all data through properties
- Proper validation in all setters
- Computed properties for derived values
- Private setters where appropriate
- Init-only for immutable values
- Clean separation of concerns

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Encapsulation**: Are fields private with property access?
2. **Validation**: Do setters validate input properly?
3. **Access Control**: Are get/set accessors appropriately restricted?
4. **Computed Properties**: Do calculated values update correctly?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Properties and Encapsulation concepts.*
