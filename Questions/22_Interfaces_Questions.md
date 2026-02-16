# Interfaces - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Interface concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create and implement a simple interface.

**Requirements:**
1. Create `IAnimal` interface with `Speak()` method
2. Create `Dog` and `Cat` classes implementing the interface
3. Use interface type to hold different animals
4. Call Speak() on each

**Expected Output:**
```
Using IAnimal interface:

IAnimal animal = new Dog();
animal.Speak() → Woof! Woof!

animal = new Cat();
animal.Speak() → Meow!

Same interface, different implementations!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create interface with properties.

**Requirements:**
1. Create `IProduct` interface with Name (get), Price (get/set)
2. Create `Book` and `Electronics` implementing IProduct
3. Display products through interface reference

**Expected Output:**
```
Products implementing IProduct:

Book:
  Name: C# Programming
  Price: $49.99

Electronics:
  Name: Wireless Mouse
  Price: $29.99

Both accessed through IProduct interface.
```

---

## Level 3: Application (Easy)

**Challenge:** Implement multiple interfaces.

**Requirements:**
1. Create `IReadable` with `Read()` returning string
2. Create `IWritable` with `Write(string)` method
3. Create `TextFile` implementing both interfaces
4. Demonstrate using both interface references

**Expected Output:**
```
TextFile implements multiple interfaces:

Using IWritable:
  file.Write("Hello, World!")
  Content written successfully.

Using IReadable:
  content = file.Read()
  Content: "Hello, World!"

One class, multiple contracts!
```

---

## Level 4: Application (Easy)

**Challenge:** Use interface for polymorphic collection.

**Requirements:**
1. Create `IShape` with `GetArea()` and `GetName()`
2. Create `Circle`, `Rectangle`, `Triangle` implementing IShape
3. Create List<IShape> with all shapes
4. Loop and calculate total area

**Expected Output:**
```
Processing shapes through IShape interface:

Shape 1: Circle (r=5)
  Area: 78.54

Shape 2: Rectangle (4x6)
  Area: 24.00

Shape 3: Triangle (base=3, h=4)
  Area: 6.00

Total Area: 108.54
All shapes processed polymorphically!
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a payment system with interfaces.

**Requirements:**
1. `IPaymentProcessor` with `Process(amount)` and `Validate()`
2. Create `CreditCardPayment`, `PayPalPayment`, `CryptoPayment`
3. Each has unique validation logic
4. Process payments through interface

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              PAYMENT PROCESSING SYSTEM                      ║
╠════════════════════════════════════════════════════════════╣

Processing Credit Card Payment:
  [Validate] Card: ****1234... ✓
  [Process] Amount: $150.00... Success!

Processing PayPal Payment:
  [Validate] Email: user@email.com... ✓
  [Process] Amount: $75.50... Success!

Processing Crypto Payment:
  [Validate] Wallet: 0x1234...abcd... ✓
  [Process] Amount: 0.05 BTC... Pending confirmation

All payments processed through IPaymentProcessor!
```

---

## Level 6: Integration (Moderate)

**Challenge:** Implement interface inheritance.

**Requirements:**
1. `IEntity` with `Id` property
2. `IAuditable` extends IEntity, adds CreatedAt, ModifiedAt
3. `ISoftDeletable` extends IEntity, adds IsDeleted, DeletedAt
4. Create `User` class implementing both
5. Demonstrate using different interface references

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              INTERFACE INHERITANCE DEMO                     ║
╠════════════════════════════════════════════════════════════╣

Creating User (implements IAuditable, ISoftDeletable):

As IEntity:
  Id: 1

As IAuditable:
  Id: 1
  Created: 2024-01-15 10:30:00
  Modified: 2024-01-15 10:30:00

As ISoftDeletable:
  Id: 1
  IsDeleted: false
  DeletedAt: null

Soft deleting user...

As ISoftDeletable:
  IsDeleted: true
  DeletedAt: 2024-01-15 11:00:00

Same object, different interface views!
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a notification system with multiple interfaces.

**Requirements:**
1. `INotification` - Send(message), GetStatus()
2. `ISchedulable` - Schedule(datetime), Cancel()
3. `IRetryable` - Retry(), GetRetryCount()
4. `EmailNotification` implements all three
5. `SMSNotification` implements INotification, IRetryable
6. `PushNotification` implements INotification only
7. Process notifications demonstrating interface segregation

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   NOTIFICATION SYSTEM                             ║
╠══════════════════════════════════════════════════════════════════╣

Email Notification (INotification + ISchedulable + IRetryable):
  [Schedule] Set for: 2024-01-15 14:00
  [Send] Sending to: user@example.com
  [Status] Failed - server timeout
  [Retry] Attempt 1/3...
  [Status] Sent successfully
  [Retry Count] 1

SMS Notification (INotification + IRetryable):
  [Send] Sending to: +1-555-1234
  [Status] Sent successfully
  (No scheduling - not ISchedulable)

Push Notification (INotification only):
  [Send] Sending to device: ABC123
  [Status] Delivered
  (No scheduling or retry)

═══════════════════════════════════════════════════════════════════
Interface Implementation Summary:
  EmailNotification: INotification ✓, ISchedulable ✓, IRetryable ✓
  SMSNotification:   INotification ✓, ISchedulable ✗, IRetryable ✓
  PushNotification:  INotification ✓, ISchedulable ✗, IRetryable ✗

Interface Segregation Principle demonstrated!
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Build a data access layer with repository interfaces.

**Requirements:**
1. `IRepository<T>` - GetById, GetAll, Add, Update, Delete
2. `IReadOnlyRepository<T>` - GetById, GetAll only
3. `IAuditedRepository<T>` extends IRepository, adds audit tracking
4. `UserRepository` implements IAuditedRepository<User>
5. `ConfigRepository` implements IReadOnlyRepository<Config>
6. Demonstrate CRUD operations through interfaces

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   DATA ACCESS LAYER                               ║
╠══════════════════════════════════════════════════════════════════╣

=== UserRepository (IAuditedRepository<User>) ===

Add User:
  [Audit] Action: CREATE, User: Alice, Time: 10:30:00
  User added with ID: 1

Get User by ID:
  [Query] SELECT * FROM Users WHERE Id = 1
  Found: Alice (alice@email.com)

Update User:
  [Audit] Action: UPDATE, User: Alice, Time: 10:31:00
  User email updated

Get All Users:
  [Query] SELECT * FROM Users
  1. Alice (alice@email.com)
  2. Bob (bob@email.com)

Delete User:
  [Audit] Action: DELETE, User: Bob, Time: 10:32:00
  User soft-deleted

=== ConfigRepository (IReadOnlyRepository<Config>) ===

Get Config by ID:
  [Query] SELECT * FROM Config WHERE Key = 'AppName'
  Found: AppName = "MyApplication"

Get All Configs:
  [Query] SELECT * FROM Config
  AppName: MyApplication
  Version: 2.0.0
  MaxUsers: 100

Attempting Update (Read-Only):
  ERROR: IReadOnlyRepository does not support Update!

═══════════════════════════════════════════════════════════════════
Repository Pattern Summary:
  Full CRUD: IRepository<T>
  Read-Only: IReadOnlyRepository<T>
  Audited: IAuditedRepository<T> (extends IRepository)
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete plugin architecture with interfaces.

**Requirements:**
1. `IPlugin` - Name, Version, Initialize(), Execute(), Shutdown()
2. `IConfigurable` - Configure(settings), GetConfiguration()
3. `ILoggable` - SetLogger(ILogger), Log(message)
4. `IHealthCheck` - CheckHealth() returns HealthStatus
5. Create `PluginManager` that works with IPlugin
6. Create 3+ plugins with different interface combinations
7. Implement dependency injection through interfaces
8. Health monitoring system

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                      PLUGIN ARCHITECTURE                                  ║
╠══════════════════════════════════════════════════════════════════════════╣

[PluginManager] Initializing plugin system...

=== PLUGIN DISCOVERY ===

Discovered plugins:
  1. LoggingPlugin v1.0.0
     Interfaces: IPlugin, IConfigurable
  2. CachePlugin v2.1.0
     Interfaces: IPlugin, IConfigurable, IHealthCheck
  3. AuthPlugin v1.5.0
     Interfaces: IPlugin, IConfigurable, ILoggable, IHealthCheck
  4. MetricsPlugin v1.2.0
     Interfaces: IPlugin, ILoggable

=== CONFIGURATION PHASE ===

Configuring IConfigurable plugins...

[LoggingPlugin] Configure:
  LogLevel: Debug
  OutputPath: /var/log/app.log

[CachePlugin] Configure:
  CacheSize: 100MB
  TTL: 3600 seconds

[AuthPlugin] Configure:
  Provider: OAuth2
  TokenExpiry: 1 hour

=== LOGGER INJECTION ===

Injecting ILogger into ILoggable plugins...

[AuthPlugin] Logger set: FileLogger
[MetricsPlugin] Logger set: FileLogger

=== INITIALIZATION PHASE ===

[PluginManager] Initializing all plugins...

[LoggingPlugin] Initialize:
  Creating log file...
  Log rotation configured... ✓

[CachePlugin] Initialize:
  Allocating memory...
  Cache warmed up... ✓

[AuthPlugin] Initialize:
  Connecting to OAuth provider...
  [LOG] AuthPlugin: OAuth2 connection established
  Token service ready... ✓

[MetricsPlugin] Initialize:
  Starting metrics collector...
  [LOG] MetricsPlugin: Collection interval: 10s
  Metrics endpoint ready... ✓

=== HEALTH CHECK ===

[PluginManager] Running health checks on IHealthCheck plugins...

[CachePlugin] Health Check:
  Memory Usage: 45/100 MB
  Hit Rate: 87%
  Status: HEALTHY ✓

[AuthPlugin] Health Check:
  OAuth Connection: Active
  Token Refresh: OK
  Failed Auths (last hour): 3
  Status: HEALTHY ✓

=== EXECUTION PHASE ===

[PluginManager] Executing all plugins...

[LoggingPlugin] Execute:
  Processing log queue...
  Flushed 150 entries

[CachePlugin] Execute:
  Evicting stale entries...
  Freed 5MB

[AuthPlugin] Execute:
  Refreshing tokens...
  [LOG] AuthPlugin: 12 tokens refreshed

[MetricsPlugin] Execute:
  Collecting metrics...
  [LOG] MetricsPlugin: CPU: 45%, Memory: 2.1GB

=== SHUTDOWN PHASE ===

[PluginManager] Graceful shutdown...

[MetricsPlugin] Shutdown:
  [LOG] MetricsPlugin: Final flush
  Metrics saved... ✓

[AuthPlugin] Shutdown:
  [LOG] AuthPlugin: Revoking active sessions
  Cleanup complete... ✓

[CachePlugin] Shutdown:
  Persisting cache to disk...
  Cleanup complete... ✓

[LoggingPlugin] Shutdown:
  Final log flush...
  Log file closed... ✓

═══════════════════════════════════════════════════════════════════════════
                          ARCHITECTURE SUMMARY
═══════════════════════════════════════════════════════════════════════════

Interfaces Used:
  IPlugin         - Core plugin contract (4 implementations)
  IConfigurable   - Runtime configuration (3 implementations)
  ILoggable       - Logging capability (2 implementations)
  IHealthCheck    - Health monitoring (2 implementations)
  ILogger         - Dependency injection target

Plugin Matrix:
                  │ IPlugin │ IConfigurable │ ILoggable │ IHealthCheck │
──────────────────┼─────────┼───────────────┼───────────┼──────────────│
LoggingPlugin     │    ✓    │       ✓       │     ✗     │      ✗       │
CachePlugin       │    ✓    │       ✓       │     ✗     │      ✓       │
AuthPlugin        │    ✓    │       ✓       │     ✓     │      ✓       │
MetricsPlugin     │    ✓    │       ✗       │     ✓     │      ✗       │

Design Patterns:
  - Interface Segregation: Small, focused interfaces
  - Dependency Injection: ILogger injected into ILoggable
  - Plugin Pattern: IPlugin as core contract
  - Health Check Pattern: IHealthCheck for monitoring

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Multiple interfaces with clear responsibilities
- Interface segregation (not all plugins need all interfaces)
- Dependency injection through interfaces (ILogger)
- Type checking with `is` for conditional behavior
- Clean plugin lifecycle management
- Proper use of interface references for polymorphism

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Interface implemented properly?
2. **Contract**: All interface members implemented?
3. **Polymorphism**: Used through interface references?
4. **Design**: Good interface segregation?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Interface concepts.*
