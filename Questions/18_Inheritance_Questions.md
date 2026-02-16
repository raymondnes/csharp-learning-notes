# Inheritance - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Inheritance concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a simple inheritance hierarchy.

**Requirements:**
1. Create a base `Vehicle` class with Make and Model properties
2. Create a `Car` class that inherits from Vehicle
3. Add a Doors property to Car
4. Create a car and display all properties

**Expected Output:**
```
Vehicle created through inheritance:
Make: Toyota
Model: Camry
Doors: 4
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Use the base constructor.

**Requirements:**
1. Create `Person` with Name and Age (constructor required)
2. Create `Student` inheriting Person, adding StudentId
3. Student constructor should call base constructor
4. Create a student and display info

**Expected Output:**
```
Creating Student (calls Person constructor first)...
Person constructor called: Alice, 20
Student constructor called: S12345

Student Info:
Name: Alice (inherited)
Age: 20 (inherited)
StudentId: S12345 (Student-specific)
```

---

## Level 3: Application (Easy)

**Challenge:** Override a method.

**Requirements:**
1. Create `Animal` with virtual `Speak()` method
2. Create `Dog`, `Cat`, `Cow` that override `Speak()`
3. Each animal makes its own sound
4. Create array of Animals and call Speak on each

**Expected Output:**
```
Animal sounds:
Dog: Woof! Woof!
Cat: Meow!
Cow: Moo!
Generic Animal: *silence*
```

---

## Level 4: Application (Easy)

**Challenge:** Use protected members.

**Requirements:**
1. Create `BankAccount` with protected `balance`
2. Add public `Deposit()` and `GetBalance()` methods
3. Create `SavingsAccount` that accesses protected balance
4. Add `ApplyInterest()` that modifies balance
5. Demonstrate protected access works in derived class

**Expected Output:**
```
Base BankAccount:
Initial balance: $0.00
After deposit $1000: $1000.00

SavingsAccount (inherits BankAccount):
Initial balance: $0.00
Deposit $500: $500.00
Apply 5% interest: $525.00
(Accessed protected 'balance' directly in derived class)
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build an employee hierarchy with pay calculation.

**Requirements:**
1. `Employee` base: Name, BaseSalary, virtual `CalculatePay()`
2. `HourlyEmployee`: HoursWorked, HourlyRate, override CalculatePay
3. `SalariedEmployee`: override returns BaseSalary
4. `CommissionEmployee`: Commission rate, TotalSales
5. Calculate and display pay for each type

**Expected Output:**
```
╔════════════════════════════════════════════════════════╗
║              EMPLOYEE PAY CALCULATION                   ║
╠════════════════════════════════════════════════════════╣

HourlyEmployee: Bob
  Hours: 45, Rate: $25/hr
  Overtime: 5 hours × 1.5
  Pay: $1,187.50

SalariedEmployee: Alice
  Base Salary: $5,000/month
  Pay: $5,000.00

CommissionEmployee: Carol
  Base: $2,000, Sales: $50,000, Commission: 5%
  Pay: $2,000 + $2,500 = $4,500.00

Total Payroll: $10,687.50
```

---

## Level 6: Integration (Moderate)

**Challenge:** Use base methods in overrides.

**Requirements:**
1. Create `Logger` with virtual `Log(message)` that adds timestamp
2. Create `FileLogger` that overrides and adds file info
3. Create `ConsoleLogger` that adds color info
4. Call base.Log() in each override
5. Show combined output

**Expected Output:**
```
Base Logger:
[2024-01-15 10:30:00] Test message

FileLogger (extends base):
[2024-01-15 10:30:01] (→ log.txt) File operation started
   Base + File destination

ConsoleLogger (extends base):
[2024-01-15 10:30:02] [GREEN] Success message
   Base + Console color
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Build a shape hierarchy with area/perimeter.

**Requirements:**
1. Abstract `Shape` with Color, abstract Area/Perimeter
2. `Circle` (radius)
3. `Rectangle` (width, height)
4. `Triangle` (three sides)
5. `Square` inheriting from Rectangle
6. Calculate totals for array of shapes

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════╗
║                    SHAPE CALCULATOR                           ║
╠══════════════════════════════════════════════════════════════╣

Shape 1: Red Circle (r=5)
  Area: 78.54
  Perimeter: 31.42

Shape 2: Blue Rectangle (10x5)
  Area: 50.00
  Perimeter: 30.00

Shape 3: Green Triangle (3,4,5)
  Area: 6.00
  Perimeter: 12.00

Shape 4: Yellow Square (7) [inherits Rectangle]
  Area: 49.00
  Perimeter: 28.00

═══════════════════════════════════════════════════════════════
Total Area: 183.54
Total Perimeter: 101.42
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a game character hierarchy.

**Requirements:**
1. `Character` base: Name, Health, virtual Attack/Defend
2. `Warrior`: High health, sword attacks
3. `Mage`: Low health, spell attacks, mana system
4. `Archer`: Medium health, arrow attacks, quiver system
5. Implement combat simulation using polymorphism

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    CHARACTER BATTLE SYSTEM                        ║
╠══════════════════════════════════════════════════════════════════╣

Creating characters:
Warrior "Conan": HP 150, Attack: Sword Strike
Mage "Gandalf": HP 80, Mana 100, Attack: Fireball
Archer "Legolas": HP 100, Arrows 20, Attack: Arrow Shot

═══ BATTLE SIMULATION ═══

Round 1:
  Conan attacks Gandalf with Sword Strike!
  Damage: 25, Gandalf HP: 55

  Gandalf casts Fireball! (Mana: 100 → 80)
  Damage: 30, Conan HP: 120

Round 2:
  Legolas shoots Arrow Shot! (Arrows: 20 → 19)
  Damage: 20, Gandalf HP: 35

  Gandalf casts Fireball! (Mana: 80 → 60)
  Damage: 30, Legolas HP: 70

Final Status:
  Conan: 120/150 HP
  Gandalf: 35/80 HP, 60 Mana
  Legolas: 70/100 HP, 19 Arrows
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive vehicle hierarchy.

**Requirements:**
1. Base `Vehicle`: Brand, Model, Year, FuelLevel, virtual methods
2. `Car`: Doors, TrunkSpace, CarType (Sedan/SUV/Sports)
3. `Motorcycle`: EngineCC, HasSidecar
4. `Truck`: CargoCapacity, NumAxles
5. `ElectricCar` extending Car: BatteryLevel, Range
6. Each has unique travel behavior
7. Implement garage management system

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                      VEHICLE MANAGEMENT SYSTEM                            ║
╠══════════════════════════════════════════════════════════════════════════╣

Garage Inventory:
─────────────────────────────────────────────────────────────────────────────
ID  │ Type         │ Vehicle              │ Year │ Fuel/Battery │ Status
────┼──────────────┼──────────────────────┼──────┼──────────────┼──────────
V01 │ Car (Sedan)  │ Toyota Camry         │ 2022 │ 75% fuel     │ Ready
V02 │ Motorcycle   │ Honda CBR600         │ 2021 │ 90% fuel     │ Ready
V03 │ Truck        │ Ford F-150           │ 2020 │ 50% fuel     │ Ready
V04 │ ElectricCar  │ Tesla Model 3        │ 2023 │ 80% battery  │ Ready

═══════════════════════════════════════════════════════════════════════════
                          TRAVEL SIMULATION
═══════════════════════════════════════════════════════════════════════════

Trip: 100 miles

V01 (Toyota Camry):
  Type: Car → Sedan
  Fuel consumption: 3.3 gallons (30 mpg)
  Trip time: 1.5 hours
  Trunk space available: Yes

V02 (Honda CBR600):
  Type: Motorcycle → Sport (600cc)
  Fuel consumption: 2.0 gallons (50 mpg)
  Trip time: 1.2 hours
  Note: No cargo capacity

V03 (Ford F-150):
  Type: Truck → 2 axles
  Fuel consumption: 5.0 gallons (20 mpg)
  Trip time: 1.7 hours
  Cargo: 2000 lbs capacity

V04 (Tesla Model 3):
  Type: ElectricCar → extends Car
  Battery consumption: 25% (300mi range)
  Trip time: 1.5 hours
  Supercharger needed: No

═══════════════════════════════════════════════════════════════════════════
                           INHERITANCE TREE
═══════════════════════════════════════════════════════════════════════════
Vehicle (base)
├── Car
│   └── ElectricCar
├── Motorcycle
└── Truck

Methods inherited/overridden:
  Vehicle.Travel() - virtual
  ├── Car.Travel() - override, uses doors check
  │   └── ElectricCar.Travel() - override, uses battery
  ├── Motorcycle.Travel() - override, faster travel
  └── Truck.Travel() - override, considers cargo weight

═══════════════════════════════════════════════════════════════════════════
                              SUMMARY
═══════════════════════════════════════════════════════════════════════════
Total vehicles: 4
Using polymorphism: All vehicles processed as Vehicle[]
Inheritance levels: Up to 3 (Vehicle → Car → ElectricCar)
Methods demonstrated: Constructor chaining, virtual/override, base calls

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Proper use of virtual/override
- Constructor chaining with base
- Protected members where appropriate
- Polymorphic array processing
- Multi-level inheritance demonstrated

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does inheritance work properly?
2. **Base/Derived**: Proper use of base keyword?
3. **Overriding**: Virtual/override used correctly?
4. **Polymorphism**: Can derived types be used as base?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Inheritance concepts.*
