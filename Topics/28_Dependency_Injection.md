# Dependency Injection

## Introduction

Dependency Injection (DI) is a design pattern where objects receive their dependencies from external sources rather than creating them internally. ASP.NET Core has built-in DI support, making it easy to write loosely coupled, testable code.

## What is Dependency Injection?

Without DI (tightly coupled):
```csharp
class OrderService
{
    private EmailService _emailService;

    public OrderService()
    {
        _emailService = new EmailService();  // Creates its own dependency
    }
}
```

With DI (loosely coupled):
```csharp
class OrderService
{
    private readonly IEmailService _emailService;

    public OrderService(IEmailService emailService)  // Receives dependency
    {
        _emailService = emailService;
    }
}
```

## Why Use Dependency Injection?

1. **Loose coupling**: Classes don't create their dependencies
2. **Testability**: Easy to mock dependencies in tests
3. **Flexibility**: Swap implementations without changing code
4. **Maintainability**: Single place to configure services
5. **Lifetime management**: Framework manages object lifetimes

## Setting Up DI in ASP.NET Core

```csharp
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IConfigService, ConfigService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();
```

## Service Lifetimes

### Singleton
One instance for the entire application lifetime:
```csharp
builder.Services.AddSingleton<IConfigService, ConfigService>();

// Same instance everywhere, created once
```

### Scoped
One instance per HTTP request:
```csharp
builder.Services.AddScoped<IUserService, UserService>();

// Same instance within a request, new instance for each request
```

### Transient
New instance every time it's requested:
```csharp
builder.Services.AddTransient<IEmailService, EmailService>();

// New instance every time, never shared
```

### Choosing the Right Lifetime

| Lifetime | Use When |
|----------|----------|
| Singleton | Stateless services, configuration, caching |
| Scoped | Database contexts, request-specific data |
| Transient | Lightweight, stateless services |

## Creating Services

### Define Interface
```csharp
public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    Product Create(ProductDto dto);
    bool Update(int id, ProductDto dto);
    bool Delete(int id);
}
```

### Implement Service
```csharp
public class ProductService : IProductService
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int id) =>
        _products.FirstOrDefault(p => p.Id == id);

    public Product Create(ProductDto dto)
    {
        var product = new Product(_nextId++, dto.Name, dto.Price);
        _products.Add(product);
        return product;
    }

    public bool Update(int id, ProductDto dto)
    {
        var index = _products.FindIndex(p => p.Id == id);
        if (index == -1) return false;

        _products[index] = new Product(id, dto.Name, dto.Price);
        return true;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product is null) return false;

        _products.Remove(product);
        return true;
    }
}
```

### Register Service
```csharp
builder.Services.AddSingleton<IProductService, ProductService>();
```

## Injecting Services

### In Minimal API Endpoints
```csharp
app.MapGet("/api/products", (IProductService service) =>
{
    return service.GetAll();
});

app.MapGet("/api/products/{id}", (int id, IProductService service) =>
{
    var product = service.GetById(id);
    return product is not null
        ? Results.Ok(product)
        : Results.NotFound();
});

app.MapPost("/api/products", (ProductDto dto, IProductService service) =>
{
    var product = service.Create(dto);
    return Results.Created($"/api/products/{product.Id}", product);
});
```

### Multiple Dependencies
```csharp
app.MapPost("/api/orders", (
    OrderDto dto,
    IOrderService orderService,
    IInventoryService inventoryService,
    INotificationService notificationService) =>
{
    // Check inventory
    if (!inventoryService.CheckStock(dto.ProductId, dto.Quantity))
    {
        return Results.BadRequest("Insufficient stock");
    }

    // Create order
    var order = orderService.Create(dto);

    // Update inventory
    inventoryService.Reserve(dto.ProductId, dto.Quantity);

    // Send notification
    notificationService.SendOrderConfirmation(order);

    return Results.Created($"/api/orders/{order.Id}", order);
});
```

## Service Dependencies

Services can depend on other services:
```csharp
public class OrderService : IOrderService
{
    private readonly IProductService _productService;
    private readonly IEmailService _emailService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IProductService productService,
        IEmailService emailService,
        ILogger<OrderService> logger)
    {
        _productService = productService;
        _emailService = emailService;
        _logger = logger;
    }

    public Order CreateOrder(OrderDto dto)
    {
        _logger.LogInformation("Creating order for product {ProductId}", dto.ProductId);

        var product = _productService.GetById(dto.ProductId);
        if (product is null)
        {
            throw new InvalidOperationException("Product not found");
        }

        var order = new Order
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Quantity = dto.Quantity,
            Total = product.Price * dto.Quantity
        };

        _emailService.SendOrderConfirmation(order);

        return order;
    }
}
```

Register with dependencies:
```csharp
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
// DI container resolves dependencies automatically
```

## Registration Patterns

### Multiple Implementations
```csharp
// Register multiple implementations
builder.Services.AddTransient<INotificationService, EmailNotificationService>();
builder.Services.AddTransient<INotificationService, SmsNotificationService>();

// Get all implementations
app.MapPost("/notify", (IEnumerable<INotificationService> services, string message) =>
{
    foreach (var service in services)
    {
        service.Send(message);
    }
    return Results.Ok();
});
```

### Factory Registration
```csharp
builder.Services.AddTransient<IPaymentProcessor>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var environment = config["Environment"];

    return environment == "Production"
        ? new LivePaymentProcessor()
        : new TestPaymentProcessor();
});
```

### Conditional Registration
```csharp
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEmailService, FakeEmailService>();
}
else
{
    builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
}
```

## Built-in Services

ASP.NET Core provides many services:
```csharp
app.MapGet("/info", (
    IWebHostEnvironment env,
    IConfiguration config,
    ILogger<Program> logger) =>
{
    logger.LogInformation("Info endpoint called");

    return new
    {
        Environment = env.EnvironmentName,
        AppName = config["AppName"],
        ContentRoot = env.ContentRootPath
    };
});
```

## Practical Example

Complete DI setup:
```csharp
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Use services
app.MapGet("/api/products", (IProductService service) =>
    service.GetAllAsync());

app.MapGet("/api/products/{id}", async (int id, IProductService service) =>
{
    var product = await service.GetByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/api/products", async (ProductDto dto, IProductService service) =>
{
    var product = await service.CreateAsync(dto);
    return Results.Created($"/api/products/{product.Id}", product);
});

app.Run();

// Interfaces
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
}

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(ProductDto dto);
}

// Implementations
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public Task<IEnumerable<Product>> GetAllAsync() =>
        Task.FromResult<IEnumerable<Product>>(_products);

    public Task<Product?> GetByIdAsync(int id) =>
        Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<Product> AddAsync(Product product)
    {
        _products.Add(product);
        return Task.FromResult(product);
    }
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        _logger.LogInformation("Getting all products");
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Getting product {Id}", id);
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Product> CreateAsync(ProductDto dto)
    {
        _logger.LogInformation("Creating product {Name}", dto.Name);
        var product = new Product(0, dto.Name, dto.Price);
        return await _repository.AddAsync(product);
    }
}
```

## Summary

- DI provides loose coupling and better testability
- Register services with AddSingleton, AddScoped, AddTransient
- Inject via constructor or method parameters
- Services can depend on other services
- Built-in services available (ILogger, IConfiguration, etc.)
- Factory pattern for conditional creation
- Essential pattern for maintainable applications

Dependency Injection is fundamental to modern ASP.NET Core development.
