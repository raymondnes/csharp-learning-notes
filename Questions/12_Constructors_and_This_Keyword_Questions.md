# Constructors and the this Keyword - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Constructors concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a class with a default constructor that initializes fields.

**Requirements:**
1. Create a `Circle` class with a `Radius` field
2. Add a default constructor that sets `Radius` to 1.0
3. Create an object and display its radius

**Expected Output:**
```
Creating circle with default constructor...
Circle radius: 1
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a class with a parameterized constructor.

**Requirements:**
1. Create a `Person` class with `Name` and `Age` fields
2. Add a constructor that takes both values as parameters
3. Create a person object and display their information

**Expected Output:**
```
Creating person: Alice, 25
Person Info: Alice is 25 years old.
```

---

## Level 3: Application (Easy)

**Challenge:** Use the `this` keyword to distinguish fields from parameters.

**Requirements:**
1. Create a `Rectangle` class with `width` and `height` fields
2. Add a constructor where parameters have the same names as fields
3. Use `this` to correctly assign values
4. Add a method to display dimensions and area

**Expected Output:**
```
Rectangle created: 10 x 5
Area: 50
```

---

## Level 4: Application (Easy)

**Challenge:** Create multiple overloaded constructors.

**Requirements:**
1. Create a `Book` class with: `Title`, `Author`, `Year`, `Pages`
2. Add constructors:
   - Default (sets default values)
   - Title only
   - Title and Author
   - All parameters
3. Create objects using each constructor

**Expected Output:**
```
Book 1 (default): Unknown Title by Unknown Author (0), 0 pages
Book 2 (title): The Hobbit by Unknown Author (0), 0 pages
Book 3 (title+author): 1984 by George Orwell (0), 0 pages
Book 4 (all): Dune by Frank Herbert (1965), 412 pages
```

---

## Level 5: Integration (Moderate)

**Challenge:** Use constructor chaining with `this()`.

**Requirements:**
1. Create an `Employee` class with: `Id`, `Name`, `Department`, `Salary`, `HireDate`
2. Create one primary constructor that sets all fields
3. Create 3 additional constructors that chain to the primary
4. Show the chain being followed (print message in each constructor)
5. Demonstrate each constructor

**Expected Output:**
```
=== Constructor Chaining Demo ===

Creating Employee with all parameters:
  → Primary constructor called
Employee: E001, Alice Johnson, Engineering, $75000, 2024-01-15

Creating Employee with name only:
  → Name-only constructor called
  → Primary constructor called
Employee: AUTO, Alice Johnson, Unassigned, $50000, 2024-01-01

Creating Employee with default:
  → Default constructor called
  → Name-only constructor called
  → Primary constructor called
Employee: AUTO, New Employee, Unassigned, $50000, 2024-01-01
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create immutable objects using readonly fields and constructors.

**Requirements:**
1. Create an `ImmutablePoint` class with readonly `X` and `Y` fields
2. Only constructors can set these values
3. Add methods that return NEW points (don't modify this)
4. Demonstrate immutability

**Expected Output:**
```
=== Immutable Point Demo ===

Creating point (10, 20)
Point: (10, 20)

Attempting to create translated point (+5, +3):
Original: (10, 20)
Translated: (15, 23) <- New object created

Demonstrating immutability:
Original point unchanged: (10, 20)

Creating scaled point (x2):
Scaled: (20, 40)
Original still: (10, 20)
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a `TimeSpan`-like class with multiple construction methods.

**Requirements:**
1. Create a `Duration` class storing total seconds internally
2. Provide constructors for:
   - Total seconds
   - Minutes and seconds
   - Hours, minutes, and seconds
   - Days, hours, minutes, and seconds
3. Add static factory methods: `FromHours()`, `FromMinutes()`, `FromDays()`
4. Add methods: `Add()`, `Subtract()`, `ToString()`
5. Demonstrate all creation methods

**Expected Output:**
```
╔═══════════════════════════════════════════════════════════════╗
║                    DURATION CLASS DEMO                         ║
╠═══════════════════════════════════════════════════════════════╣

Construction Methods:
─────────────────────────────────────────────────────────────────
From seconds (3661):     1:01:01 (3661 total seconds)
From min:sec (90, 30):   1:30:30 (5430 total seconds)
From h:m:s (2, 30, 45):  2:30:45 (9045 total seconds)
From d:h:m:s (1, 2, 3, 4): 1d 2:03:04 (93784 total seconds)

Factory Methods:
─────────────────────────────────────────────────────────────────
Duration.FromHours(1.5):    1:30:00
Duration.FromMinutes(90):   1:30:00
Duration.FromDays(0.5):     12:00:00

Operations:
─────────────────────────────────────────────────────────────────
1:30:00 + 0:45:30 = 2:15:30
2:00:00 - 0:30:00 = 1:30:00

Comparison:
─────────────────────────────────────────────────────────────────
1:30:00 compared to 2:00:00: Shorter
2:00:00 compared to 1:30:00: Longer
1:30:00 compared to 1:30:00: Equal
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a builder pattern using constructors.

**Requirements:**
1. Create a `Pizza` class with many optional toppings
2. Create a `PizzaBuilder` class that:
   - Has a constructor for size (required)
   - Has methods for each topping that return `this`
   - Has a `Build()` method that creates the Pizza
3. Demonstrate creating different pizzas

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      PIZZA BUILDER DEMO                           ║
╠══════════════════════════════════════════════════════════════════╣

Building pizzas...

Pizza 1 (Simple):
  new PizzaBuilder("Medium")
      .WithCheese()
      .WithSauce("Tomato")
      .Build()

  Result: Medium Pizza
  - Tomato sauce
  - Cheese
  Price: $10.99

Pizza 2 (Loaded):
  new PizzaBuilder("Large")
      .WithCheese()
      .WithExtraCheese()
      .WithSauce("BBQ")
      .WithTopping("Pepperoni")
      .WithTopping("Mushrooms")
      .WithTopping("Onions")
      .WithTopping("Bell Peppers")
      .Build()

  Result: Large Pizza
  - BBQ sauce
  - Extra Cheese
  - Pepperoni, Mushrooms, Onions, Bell Peppers
  Price: $18.49

Pizza 3 (Custom):
  new PizzaBuilder("Small")
      .WithSauce("Alfredo")
      .WithTopping("Chicken")
      .WithTopping("Spinach")
      .Build()

  Result: Small Pizza (No Cheese)
  - Alfredo sauce
  - Chicken, Spinach
  Price: $11.49

╚══════════════════════════════════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a dependency injection container with constructor analysis.

**Requirements:**
1. Create a simple `Container` class that can:
   - Register types with their implementations
   - Resolve instances by analyzing constructors
   - Handle constructor parameters automatically
2. Create sample services:
   - `ILogger` → `ConsoleLogger`
   - `IDatabase` → `SqlDatabase(ILogger logger)`
   - `IUserService` → `UserService(IDatabase db, ILogger logger)`
3. Demonstrate automatic constructor injection
4. Handle missing registrations gracefully

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════╗
║              DEPENDENCY INJECTION CONTAINER DEMO                      ║
╠══════════════════════════════════════════════════════════════════════╣

=== REGISTRATION PHASE ===
Registering ILogger → ConsoleLogger
  Constructor: ConsoleLogger()
  Parameters: None

Registering IDatabase → SqlDatabase
  Constructor: SqlDatabase(ILogger)
  Parameters: ILogger (will be resolved)

Registering IUserService → UserService
  Constructor: UserService(IDatabase, ILogger)
  Parameters: IDatabase (will be resolved), ILogger (will be resolved)

=== RESOLUTION PHASE ===

Resolving ILogger...
  → Creating ConsoleLogger (no dependencies)
  ✓ Resolved: ConsoleLogger@12345

Resolving IDatabase...
  → SqlDatabase requires: ILogger
  → Resolving ILogger... (already resolved, reusing)
  → Creating SqlDatabase with resolved dependencies
  ✓ Resolved: SqlDatabase@67890

Resolving IUserService...
  → UserService requires: IDatabase, ILogger
  → Resolving IDatabase... (already resolved, reusing)
  → Resolving ILogger... (already resolved, reusing)
  → Creating UserService with resolved dependencies
  ✓ Resolved: UserService@11111

=== VERIFICATION ===

Testing resolved services:
Logger.Log("Test message")
  [LOG] Test message

Database.Connect()
  [LOG] Connecting to database...
  Connected to SQL Database

UserService.GetUser(1)
  [LOG] Fetching user 1
  [LOG] Database query executed
  User: { Id: 1, Name: "Alice" }

=== ERROR HANDLING ===

Attempting to resolve unregistered IEmailService...
  ✗ Error: No registration found for IEmailService

=== CONTAINER SUMMARY ===
Registered types: 3
Resolved instances: 3
Resolution cache hits: 4

╚══════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use reflection to analyze constructors (Type.GetConstructors())
- Properly chain constructor calls where appropriate
- Cache resolved instances
- Clear error messages for missing dependencies
- Comments explaining the DI concept

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Do constructors initialize objects properly?
2. **Chaining**: Is constructor chaining used correctly?
3. **this Keyword**: Is `this` used appropriately?
4. **Design**: Are constructors well-designed?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Constructors and this Keyword concepts.*
