# Static vs Instance Members - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Static vs Instance Members concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a class with both static and instance members.

**Requirements:**
1. Create a `Counter` class with a static `TotalCount` field
2. Add an instance `Name` field
3. Increment TotalCount in constructor
4. Create 3 counter objects and show the shared count

**Expected Output:**
```
Creating counters...
Counter "A" created. Total: 1
Counter "B" created. Total: 2
Counter "C" created. Total: 3

Each counter has its own name, but TotalCount is shared.
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a static utility class.

**Requirements:**
1. Create a static `StringHelper` class
2. Add static methods: `Reverse()`, `CountWords()`, `ToTitleCase()`
3. Demonstrate that no instantiation is needed

**Expected Output:**
```
Using static StringHelper methods:

Reverse("Hello"): olleH
CountWords("The quick brown fox"): 4
ToTitleCase("hello world"): Hello World

Note: No object creation needed - methods called on class directly.
```

---

## Level 3: Application (Easy)

**Challenge:** Implement auto-incrementing IDs using static field.

**Requirements:**
1. Create an `Employee` class with auto-generated ID
2. Use a private static counter
3. Each new employee gets the next ID
4. Create 5 employees and show their IDs

**Expected Output:**
```
Creating employees with auto-IDs:
Employee 1: Alice (ID: EMP001)
Employee 2: Bob (ID: EMP002)
Employee 3: Carol (ID: EMP003)
Employee 4: David (ID: EMP004)
Employee 5: Eve (ID: EMP005)

Total employees created: 5
```

---

## Level 4: Application (Easy)

**Challenge:** Demonstrate what static methods can and cannot access.

**Requirements:**
1. Create a class with both static and instance members
2. Create an instance method that accesses both
3. Create a static method showing it cannot access instance members
4. Add comments explaining why

**Expected Output:**
```
Instance Method Demo:
  Can access instanceField: 10 ✓
  Can access staticField: 100 ✓
  Can call InstanceMethod: ✓
  Can call StaticMethod: ✓

Static Method Demo:
  Cannot access instanceField: ✗ (would cause compile error)
  Can access staticField: 100 ✓
  Cannot call InstanceMethod: ✗ (no instance context)
  Can call StaticMethod: ✓

Reason: Static methods have no 'this' reference.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a configuration manager with static and instance parts.

**Requirements:**
1. Static members for global app config (AppName, Version)
2. Instance members for user-specific settings
3. Static method to get default settings
4. Instance method to customize settings

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║                  CONFIGURATION MANAGER                      ║
╠════════════════════════════════════════════════════════════╣

Global Settings (Static):
  App Name: MyApplication
  Version: 2.0.0
  Environment: Production

User Settings (Instance):

User: Alice
  Theme: Dark
  Language: English
  Notifications: Enabled

User: Bob
  Theme: Light
  Language: Spanish
  Notifications: Disabled

Both users share global settings but have personal preferences.
```

---

## Level 6: Integration (Moderate)

**Challenge:** Implement a static constructor with lazy initialization.

**Requirements:**
1. Create a `DatabaseConnection` class
2. Static constructor loads connection string from "config"
3. Track when static constructor runs
4. Show it only runs once, on first access

**Expected Output:**
```
Program starting...

First access to DatabaseConnection:
  [Static Constructor] Loading configuration...
  [Static Constructor] Connection string loaded
  [Static Constructor] Initialization complete

ConnectionString: Server=localhost;Database=MyApp

Second access (no static constructor):
  ConnectionString: Server=localhost;Database=MyApp
  (Static constructor did not run again)

Creating instance:
  [Instance Constructor] Creating connection object
  (Static constructor already ran)

Observation: Static constructor ran exactly once.
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Build a complete logging system with static and instance components.

**Requirements:**
1. Static `Logger` class for global logging
2. Static log level (Debug, Info, Warning, Error)
3. Instance `LogContext` for component-specific prefixes
4. Thread-safe log counter
5. Multiple output targets

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      LOGGING SYSTEM                               ║
╠══════════════════════════════════════════════════════════════════╣

Setting global log level: Info
(Debug messages will be hidden)

[2024-01-15 10:30:01] [INFO] Application starting
[2024-01-15 10:30:01] [INFO] [Database] Connecting to server
[2024-01-15 10:30:02] [INFO] [Database] Connection established
[2024-01-15 10:30:02] [WARNING] [Auth] Failed login attempt
[2024-01-15 10:30:03] [ERROR] [Payment] Transaction failed: Timeout

Debug message (hidden due to log level):
  Logger.Debug("Detailed info") - NOT SHOWN

Changing log level to Debug:
[2024-01-15 10:30:04] [DEBUG] [Cache] Cache miss for key: user_123

═══════════════════════════════════════════════════════════════════
                         LOG STATISTICS
═══════════════════════════════════════════════════════════════════
Total log entries: 6
  Debug: 1
  Info: 3
  Warning: 1
  Error: 1

Log contexts used:
  [Database]: 2 entries
  [Auth]: 1 entry
  [Payment]: 1 entry
  [Cache]: 1 entry
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Implement object pooling using static management.

**Requirements:**
1. Create a `Connection` class (expensive to create)
2. Create a static `ConnectionPool` manager
3. Pool reuses connections instead of creating new ones
4. Track created vs reused connections
5. Implement Get() and Return() methods

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    CONNECTION POOL DEMO                           ║
╠══════════════════════════════════════════════════════════════════╣

Pool initialized with max size: 5

Request 1: ConnectionPool.Get()
  Creating new connection (ID: CONN-001)
  Pool: 0 available, 1 in use

Request 2: ConnectionPool.Get()
  Creating new connection (ID: CONN-002)
  Pool: 0 available, 2 in use

Request 3: ConnectionPool.Get()
  Creating new connection (ID: CONN-003)
  Pool: 0 available, 3 in use

Returning connection CONN-001...
  Pool: 1 available, 2 in use

Request 4: ConnectionPool.Get()
  Reusing connection (ID: CONN-001)
  Pool: 0 available, 3 in use

Request 5: ConnectionPool.Get()
  Creating new connection (ID: CONN-004)
  Pool: 0 available, 4 in use

═══════════════════════════════════════════════════════════════════
                          STATISTICS
═══════════════════════════════════════════════════════════════════
Total connections created: 4
Connections reused: 1
Currently in use: 4
Currently available: 0
Pool efficiency: 20% reuse rate
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a complete game state management system.

**Requirements:**
1. Static `GameManager` (singleton pattern)
2. Static game state (score, level, settings)
3. Instance `Player` objects with individual stats
4. Static event system for game events
5. Static methods for save/load (simulated)
6. Instance methods for player actions

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                        GAME STATE MANAGER                                 ║
╠══════════════════════════════════════════════════════════════════════════╣

=== INITIALIZATION ===
[GameManager] Singleton instance created
[GameManager] Loading settings...
[GameManager] Game initialized

=== PLAYER CREATION ===
Creating Player: Hero
  Instance ID: P001
  Starting HP: 100
  Starting Gold: 0

Creating Player: Sidekick
  Instance ID: P002
  Starting HP: 80
  Starting Gold: 0

=== GAMEPLAY ===
[Event] Game Started - Level 1
[Static] GameManager.CurrentLevel: 1
[Static] GameManager.GlobalScore: 0

Hero defeats enemy!
  [Instance] Hero gains 50 XP
  [Instance] Hero gains 10 gold
  [Static] Global score +100

Sidekick finds treasure!
  [Instance] Sidekick gains 50 gold
  [Static] Global score +25

Level Complete!
  [Event] Level Complete
  [Static] GameManager.CurrentLevel: 2
  [Static] GameManager.GlobalScore: 125

=== SAVE GAME ===
[Static] GameManager.SaveGame()
  Saving global state:
    Level: 2
    Score: 125
    Players: 2
  Saving player states:
    Hero: HP=100, XP=50, Gold=10
    Sidekick: HP=80, XP=0, Gold=50
  [Event] Game Saved

=== LOAD GAME ===
[Static] GameManager.LoadGame()
  Restoring global state...
  Restoring player states...
  [Event] Game Loaded

═══════════════════════════════════════════════════════════════════════════
                              SUMMARY
═══════════════════════════════════════════════════════════════════════════

Static Members Used:
  - GameManager.Instance (singleton)
  - GameManager.CurrentLevel
  - GameManager.GlobalScore
  - GameManager.Settings
  - GameManager.SaveGame()
  - GameManager.LoadGame()
  - Event handlers (OnLevelComplete, OnScoreChange)

Instance Members Used:
  - Player.Id
  - Player.Name
  - Player.Health
  - Player.Experience
  - Player.Gold
  - Player.Attack()
  - Player.TakeDamage()

Design Pattern: Singleton for GameManager
Thread Safety: Lock on score updates
Memory: Single GameManager, multiple Players

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Implement singleton pattern correctly
- Use static for shared game state
- Use instance for player-specific data
- Demonstrate static events/callbacks
- Include static constructor for initialization
- Comments explaining static vs instance choices

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Static/instance used appropriately?
2. **Access Patterns**: Correct access through class vs object?
3. **Design**: Good separation of concerns?
4. **Understanding**: Can explain why each is static/instance?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Static vs Instance Members concepts.*
