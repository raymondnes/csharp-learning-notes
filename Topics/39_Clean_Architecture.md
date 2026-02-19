# Topic 39: Clean Architecture

## Introduction

Clean Architecture separates concerns into layers, making applications maintainable, testable, and independent of frameworks, databases, and UI. The core business logic is protected from external dependencies.

## The Dependency Rule

**Dependencies point inward.** Inner layers don't know about outer layers.

```
┌──────────────────────────────────────┐
│           Presentation              │  ← Controllers, Views
├──────────────────────────────────────┤
│          Infrastructure             │  ← Database, APIs, Files
├──────────────────────────────────────┤
│           Application               │  ← Use Cases, Services
├──────────────────────────────────────┤
│             Domain                  │  ← Entities, Business Rules
└──────────────────────────────────────┘
```

## Layers Explained

### 1. Domain Layer (Core)

Contains enterprise business logic. No dependencies on other layers.

```csharp
// Entities
public class Order
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public OrderStatus Status { get; private set; }
    public decimal Total => Items.Sum(i => i.Total);

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");

        var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            Items.Add(new OrderItem(product.Id, product.Price, quantity));
        }
    }

    public void Submit()
    {
        if (!Items.Any())
            throw new DomainException("Cannot submit empty order");

        Status = OrderStatus.Submitted;
    }
}

// Value Objects
public record Money(decimal Amount, string Currency)
{
    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new DomainException("Cannot add different currencies");
        return new Money(a.Amount + b.Amount, a.Currency);
    }
}

// Domain Events
public record OrderSubmittedEvent(int OrderId, int CustomerId, decimal Total);

// Domain Exceptions
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
```

### 2. Application Layer

Contains application-specific business logic (use cases).

```csharp
// Use Case / Application Service
public class SubmitOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEventPublisher _eventPublisher;

    public SubmitOrderUseCase(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IEventPublisher eventPublisher)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<SubmitOrderResult> ExecuteAsync(SubmitOrderCommand command)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId);
        if (order == null)
            return SubmitOrderResult.NotFound();

        if (order.CustomerId != command.CustomerId)
            return SubmitOrderResult.Unauthorized();

        try
        {
            order.Submit();
            await _orderRepository.UpdateAsync(order);

            await _eventPublisher.PublishAsync(
                new OrderSubmittedEvent(order.Id, order.CustomerId, order.Total));

            return SubmitOrderResult.Success(order.Id);
        }
        catch (DomainException ex)
        {
            return SubmitOrderResult.Failed(ex.Message);
        }
    }
}

// Commands/Queries (CQRS)
public record SubmitOrderCommand(int OrderId, int CustomerId);
public record GetOrderQuery(int OrderId);

// Results
public class SubmitOrderResult
{
    public bool IsSuccess { get; private set; }
    public int? OrderId { get; private set; }
    public string? Error { get; private set; }

    public static SubmitOrderResult Success(int orderId) =>
        new() { IsSuccess = true, OrderId = orderId };
    public static SubmitOrderResult Failed(string error) =>
        new() { Error = error };
    public static SubmitOrderResult NotFound() =>
        new() { Error = "Order not found" };
    public static SubmitOrderResult Unauthorized() =>
        new() { Error = "Not authorized" };
}

// DTOs
public record OrderDto(int Id, string CustomerName, decimal Total, string Status);

// Interfaces (defined in Application, implemented in Infrastructure)
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByCustomerAsync(int customerId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : class;
}
```

### 3. Infrastructure Layer

Implements interfaces defined in Application layer.

```csharp
// Repository Implementation
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetByCustomerAsync(int customerId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}

// DbContext
public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

// External Service Implementation
public class EmailNotificationService : INotificationService
{
    private readonly IEmailClient _emailClient;

    public async Task NotifyOrderSubmittedAsync(Order order)
    {
        await _emailClient.SendAsync(/* ... */);
    }
}
```

### 4. Presentation Layer

Controllers, Views, API endpoints.

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly SubmitOrderUseCase _submitOrder;
    private readonly IOrderQueryService _queryService;

    public OrdersController(
        SubmitOrderUseCase submitOrder,
        IOrderQueryService queryService)
    {
        _submitOrder = submitOrder;
        _queryService = queryService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _queryService.GetByIdAsync(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    [HttpPost("{id}/submit")]
    public async Task<ActionResult> Submit(int id)
    {
        var customerId = int.Parse(User.FindFirst("sub")!.Value);
        var command = new SubmitOrderCommand(id, customerId);

        var result = await _submitOrder.ExecuteAsync(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new { result.OrderId });
    }
}
```

## Project Structure

```
Solution/
├── src/
│   ├── Domain/                    # No dependencies
│   │   ├── Entities/
│   │   ├── ValueObjects/
│   │   ├── Events/
│   │   ├── Exceptions/
│   │   └── Domain.csproj
│   │
│   ├── Application/               # Depends on: Domain
│   │   ├── Interfaces/
│   │   ├── UseCases/
│   │   ├── DTOs/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   └── Application.csproj
│   │
│   ├── Infrastructure/            # Depends on: Application, Domain
│   │   ├── Persistence/
│   │   ├── Services/
│   │   ├── Configuration/
│   │   └── Infrastructure.csproj
│   │
│   └── WebApi/                    # Depends on: Application, Infrastructure
│       ├── Controllers/
│       ├── Middleware/
│       ├── Program.cs
│       └── WebApi.csproj
│
└── tests/
    ├── Domain.Tests/
    ├── Application.Tests/
    └── Integration.Tests/
```

## Dependency Injection Setup

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

var app = builder.Build();
// ...
```

```csharp
// Application/DependencyInjection.cs
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register use cases
        services.AddScoped<SubmitOrderUseCase>();
        services.AddScoped<CreateOrderUseCase>();
        services.AddScoped<GetOrdersQueryHandler>();

        // Register MediatR (optional)
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

// Infrastructure/DependencyInjection.cs
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        // Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        // External services
        services.AddScoped<IEmailService, SendGridEmailService>();
        services.AddScoped<IPaymentGateway, StripePaymentGateway>();

        return services;
    }
}
```

## CQRS Pattern

Separate read and write operations:

```csharp
// Commands (writes)
public record CreateOrderCommand(int CustomerId, List<OrderItemDto> Items) : IRequest<int>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository _repository;

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity);
        }

        await _repository.AddAsync(order);
        return order.Id;
    }
}

// Queries (reads)
public record GetOrderQuery(int Id) : IRequest<OrderDto?>;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto?>
{
    private readonly IOrderQueryService _queryService;

    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken ct)
    {
        return await _queryService.GetByIdAsync(request.Id);
    }
}
```

## Benefits of Clean Architecture

1. **Testability**: Business logic is isolated and easy to test
2. **Independence**: Core doesn't depend on UI, database, or frameworks
3. **Maintainability**: Changes in one layer don't affect others
4. **Flexibility**: Easy to swap implementations (e.g., change database)

## Testing

```csharp
// Domain tests (no mocks needed)
public class OrderTests
{
    [Fact]
    public void Submit_WithItems_ChangesStatusToSubmitted()
    {
        var order = new Order(customerId: 1);
        order.AddItem(new Product(1, "Test", 10m), 2);

        order.Submit();

        Assert.Equal(OrderStatus.Submitted, order.Status);
    }

    [Fact]
    public void Submit_WithoutItems_ThrowsDomainException()
    {
        var order = new Order(customerId: 1);

        Assert.Throws<DomainException>(() => order.Submit());
    }
}

// Application tests (mock infrastructure)
public class SubmitOrderUseCaseTests
{
    [Fact]
    public async Task Execute_ValidOrder_ReturnsSuccess()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var mockEvents = new Mock<IEventPublisher>();

        var order = CreateOrderWithItems();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);

        var useCase = new SubmitOrderUseCase(mockRepo.Object, mockEvents.Object);

        var result = await useCase.ExecuteAsync(new SubmitOrderCommand(1, order.CustomerId));

        Assert.True(result.IsSuccess);
        mockEvents.Verify(e => e.PublishAsync(It.IsAny<OrderSubmittedEvent>()), Times.Once);
    }
}
```

## Summary

| Layer | Contains | Depends On |
|-------|----------|------------|
| Domain | Entities, Business Rules | Nothing |
| Application | Use Cases, Interfaces | Domain |
| Infrastructure | Implementations | Application, Domain |
| Presentation | Controllers, UI | Application |

Clean Architecture keeps your business logic protected and your application maintainable for years to come.
