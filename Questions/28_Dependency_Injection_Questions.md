# Dependency Injection - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Dependency Injection concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Register and use a simple service.

**Requirements:**
1. Create IGreetingService interface with Greet(name) method
2. Create GreetingService implementation
3. Register as Singleton
4. Inject into endpoint

**Expected Output:**
```
Dependency Injection Basics:

Registering service:
  builder.Services.AddSingleton<IGreetingService, GreetingService>();

GET /greet/Alice
  [Injected] IGreetingService
  Response: "Hello, Alice! Welcome!"

Service successfully injected and used.
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Demonstrate service lifetimes.

**Requirements:**
1. Create services that log their instance ID
2. Register one Singleton, one Scoped, one Transient
3. Show how instances differ across requests

**Expected Output:**
```
Service Lifetime Demo:

Request 1:
  Singleton ID: A1B2C3 (same always)
  Scoped ID: D4E5F6 (same within request)
  Transient ID: G7H8I9 (always new)
  Transient ID: J1K2L3 (different from above)

Request 2:
  Singleton ID: A1B2C3 (same as Request 1)
  Scoped ID: M4N5O6 (new for this request)
  Transient ID: P7Q8R9 (always new)

Singleton: 1 instance total
Scoped: 2 instances (one per request)
Transient: 4 instances (every injection)
```

---

## Level 3: Application (Easy)

**Challenge:** Create service with constructor dependencies.

**Requirements:**
1. IProductRepository interface
2. ProductService that depends on IProductRepository
3. Register both services
4. Show dependency chain resolution

**Expected Output:**
```
Service Dependencies:

Registrations:
  IProductRepository → InMemoryProductRepository (Singleton)
  IProductService → ProductService (Scoped)

ProductService constructor:
  public ProductService(IProductRepository repository)
  {
      _repository = repository;  // Injected automatically!
  }

GET /api/products
  [DI] Resolving IProductService
  [DI] → Needs IProductRepository
  [DI] → Resolved InMemoryProductRepository
  [DI] → Created ProductService
  Response: [{...}, {...}]

DI container resolved the entire dependency chain!
```

---

## Level 4: Application (Easy)

**Challenge:** Inject multiple services into endpoint.

**Requirements:**
1. Create IOrderService, IInventoryService, INotificationService
2. Endpoint that uses all three
3. Show proper injection syntax

**Expected Output:**
```
Multiple Service Injection:

POST /api/orders
Parameters:
  - OrderDto dto
  - IOrderService orderService
  - IInventoryService inventoryService
  - INotificationService notificationService

Processing:
  [Inventory] Checking stock... Available
  [Order] Creating order... Created (ID: 1001)
  [Notification] Sending confirmation... Sent

Response: 201 Created
{
  "orderId": 1001,
  "status": "confirmed",
  "notificationSent": true
}
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a layered service architecture.

**Requirements:**
1. Repository layer (data access)
2. Service layer (business logic)
3. Proper lifetime for each layer
4. Full CRUD through all layers

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              LAYERED ARCHITECTURE                           ║
╠════════════════════════════════════════════════════════════╣

Service Registration:
  IProductRepository → Singleton (data store)
  IProductService    → Scoped (business logic)
  IProductValidator  → Transient (validation)

Layer Flow:
  Endpoint → Service → Repository

GET /api/products
  [Endpoint] Calling IProductService.GetAll()
  [Service] Calling IProductRepository.GetAll()
  [Repository] Returning data
  [Service] Applying business rules
  [Endpoint] Returning response

POST /api/products
  [Endpoint] Calling IProductService.Create(dto)
  [Service] Calling IProductValidator.Validate(dto)
  [Validator] Validation passed
  [Service] Calling IProductRepository.Add(product)
  [Repository] Product stored (ID: 5)
  [Service] Returning created product
  [Endpoint] Returning 201 Created
```

---

## Level 6: Integration (Moderate)

**Challenge:** Implement factory pattern with DI.

**Requirements:**
1. IPaymentProcessor interface
2. Multiple implementations (Credit, PayPal, Crypto)
3. PaymentProcessorFactory that creates based on type
4. Register factory and use in endpoint

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              FACTORY PATTERN WITH DI                        ║
╠════════════════════════════════════════════════════════════╣

Registrations:
  CreditCardProcessor → Transient
  PayPalProcessor     → Transient
  CryptoProcessor     → Transient
  IPaymentProcessorFactory → Singleton

Factory Implementation:
  public IPaymentProcessor Create(PaymentType type)
  {
      return type switch
      {
          PaymentType.CreditCard => _services.GetRequiredService<CreditCardProcessor>(),
          PaymentType.PayPal => _services.GetRequiredService<PayPalProcessor>(),
          PaymentType.Crypto => _services.GetRequiredService<CryptoProcessor>(),
          _ => throw new ArgumentException("Unknown payment type")
      };
  }

POST /api/payments (type: creditcard)
  [Factory] Creating CreditCardProcessor
  [Processor] Processing $100.00 via Credit Card
  Response: {"success": true, "transactionId": "CC-001"}

POST /api/payments (type: paypal)
  [Factory] Creating PayPalProcessor
  [Processor] Processing $75.50 via PayPal
  Response: {"success": true, "transactionId": "PP-001"}
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Configure DI with environment-based services.

**Requirements:**
1. Different implementations for Dev/Prod
2. IEmailService with fake (dev) and real (prod) implementations
3. Configuration-based service selection
4. Demonstrate both environments

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║              ENVIRONMENT-BASED DI                                 ║
╠══════════════════════════════════════════════════════════════════╣

=== DEVELOPMENT ENVIRONMENT ===

Registration:
  if (env.IsDevelopment())
      services.AddSingleton<IEmailService, FakeEmailService>();

POST /api/users (Development)
  [FakeEmailService] Would send email to: user@test.com
  [FakeEmailService] Subject: Welcome!
  [FakeEmailService] (Email logged, not actually sent)
  Response: User created, welcome email "sent"

=== PRODUCTION ENVIRONMENT ===

Registration:
  if (env.IsProduction())
      services.AddSingleton<IEmailService, SmtpEmailService>();

POST /api/users (Production)
  [SmtpEmailService] Connecting to smtp.server.com
  [SmtpEmailService] Sending email to: user@test.com
  [SmtpEmailService] Email sent successfully
  Response: User created, welcome email sent

=== CONFIGURATION-BASED ===

appsettings.json:
  "EmailProvider": "SendGrid"

Registration:
  services.AddSingleton<IEmailService>(sp =>
  {
      var provider = config["EmailProvider"];
      return provider switch
      {
          "SendGrid" => new SendGridEmailService(config),
          "SMTP" => new SmtpEmailService(config),
          _ => new FakeEmailService()
      };
  });
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Build a plugin system using DI.

**Requirements:**
1. IPlugin interface with Initialize(), Execute(), GetName()
2. Multiple plugin implementations
3. Register all plugins
4. IPluginManager that orchestrates plugins
5. Inject IEnumerable<IPlugin> to get all

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      PLUGIN SYSTEM                                ║
╠══════════════════════════════════════════════════════════════════╣

Plugin Registrations:
  services.AddTransient<IPlugin, LoggingPlugin>();
  services.AddTransient<IPlugin, CachingPlugin>();
  services.AddTransient<IPlugin, MetricsPlugin>();
  services.AddSingleton<IPluginManager, PluginManager>();

PluginManager:
  public PluginManager(IEnumerable<IPlugin> plugins)
  {
      _plugins = plugins.ToList();
      // All registered IPlugin implementations injected!
  }

GET /api/plugins
  Discovered plugins: 3
  - LoggingPlugin v1.0
  - CachingPlugin v2.1
  - MetricsPlugin v1.5

POST /api/plugins/execute
  [PluginManager] Initializing plugins...
  [LoggingPlugin] Initialized
  [CachingPlugin] Initialized
  [MetricsPlugin] Initialized

  [PluginManager] Executing plugins...
  [LoggingPlugin] Execution complete
  [CachingPlugin] Execution complete
  [MetricsPlugin] Execution complete

  Response:
  {
    "pluginsExecuted": 3,
    "results": [
      {"plugin": "LoggingPlugin", "success": true},
      {"plugin": "CachingPlugin", "success": true},
      {"plugin": "MetricsPlugin", "success": true}
    ]
  }
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete application with full DI architecture.

**Requirements:**
1. Three-tier architecture (API, Service, Data)
2. Cross-cutting concerns (Logging, Caching, Validation)
3. Multiple service implementations swappable via config
4. Proper lifetime management
5. Service decorators/wrappers
6. Comprehensive dependency graph

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    ENTERPRISE DI ARCHITECTURE                             ║
╠══════════════════════════════════════════════════════════════════════════╣

=== SERVICE REGISTRATIONS ===

Data Layer (Singleton):
  IProductRepository     → SqlProductRepository
  IOrderRepository       → SqlOrderRepository
  ICustomerRepository    → SqlCustomerRepository
  IDbConnectionFactory   → SqlConnectionFactory

Service Layer (Scoped):
  IProductService        → ProductService
  IOrderService          → OrderService
  ICustomerService       → CustomerService

Cross-Cutting (Various):
  ILogger<T>             → Built-in (Singleton)
  ICacheService          → RedisCacheService (Singleton)
  IValidationService     → ValidationService (Transient)
  IEventPublisher        → EventPublisher (Scoped)

=== DEPENDENCY GRAPH ===

OrderService
├── IOrderRepository
├── IProductService
│   └── IProductRepository
│   └── ICacheService
├── ICustomerService
│   └── ICustomerRepository
├── IEventPublisher
└── ILogger<OrderService>

=== SERVICE DECORATORS ===

Registration:
  services.AddScoped<IProductService, ProductService>();
  services.Decorate<IProductService, CachedProductService>();
  services.Decorate<IProductService, LoggedProductService>();

Call flow:
  LoggedProductService.GetAll()
    → [Log] Getting all products
    → CachedProductService.GetAll()
      → [Cache] Checking cache...
      → [Cache] Miss - calling inner service
      → ProductService.GetAll()
        → [DB] Query executed
      → [Cache] Caching result
    → [Log] Retrieved 50 products
    → Return

=== REQUEST PROCESSING ===

POST /api/orders
{
  "customerId": 5,
  "items": [{"productId": 1, "quantity": 2}]
}

Processing:
  [DI] Resolving IOrderService (Scoped)
  [DI] → IOrderRepository (Singleton) - existing
  [DI] → IProductService (Scoped) - new
  [DI] → → IProductRepository (Singleton) - existing
  [DI] → → ICacheService (Singleton) - existing
  [DI] → ICustomerService (Scoped) - new
  [DI] → IEventPublisher (Scoped) - new
  [DI] → ILogger<OrderService> - existing

  [Validation] Validating order...
  [ProductService] Checking product availability
  [Cache] Product 1 found in cache
  [CustomerService] Validating customer 5
  [OrderService] Creating order
  [Repository] Order saved (ID: ORD-1001)
  [EventPublisher] Publishing OrderCreated event
  [Cache] Invalidating order cache

Response: 201 Created

=== STATISTICS ===

Service Instances Created This Request:
  Scoped: 4 (OrderService, ProductService, CustomerService, EventPublisher)
  Transient: 2 (Validators)
  Singleton: 0 (all reused)

Active Singleton Instances: 8
Total Scoped Instances (active request): 4

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Three-tier architecture
- Proper lifetime management
- Service decorator pattern
- Configuration-based registration
- Cross-cutting concerns
- Dependency graph management
- IEnumerable<T> for multiple implementations

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Services registered and resolved properly?
2. **Lifetimes**: Appropriate lifetime chosen?
3. **Design**: Clean dependency hierarchy?
4. **Patterns**: Proper DI patterns used?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Dependency Injection concepts.*
