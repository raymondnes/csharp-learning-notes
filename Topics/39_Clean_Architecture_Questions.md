# Topic 39: Clean Architecture - Assessment Questions

## Level 1 (Trivial)

**Question:** List the four layers of Clean Architecture from innermost to outermost.

**Expected Solution:**
1. **Domain** (Core) - Entities, business rules, value objects
2. **Application** - Use cases, interfaces, DTOs
3. **Infrastructure** - Database, external services, implementations
4. **Presentation** - Controllers, UI, API endpoints

The Dependency Rule: Dependencies always point inward. Inner layers don't know about outer layers.

---

## Level 2 (Trivial)

**Question:** Which layer should each of these belong to?
- `OrderController`
- `Order` entity
- `IOrderRepository` interface
- `SqlOrderRepository` class
- `SubmitOrderUseCase`

**Expected Solution:**
- `OrderController` → **Presentation**
- `Order` entity → **Domain**
- `IOrderRepository` interface → **Application**
- `SqlOrderRepository` class → **Infrastructure**
- `SubmitOrderUseCase` → **Application**

---

## Level 3 (Easy)

**Question:** Create a domain entity for a `Product` with business rules.

**Expected Solution:**
```csharp
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    public Product(string name, decimal price, int initialStock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required");
        if (price <= 0)
            throw new DomainException("Price must be positive");
        if (initialStock < 0)
            throw new DomainException("Stock cannot be negative");

        Name = name;
        Price = price;
        StockQuantity = initialStock;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new DomainException("Price must be positive");
        Price = newPrice;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");
        StockQuantity += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");
        if (quantity > StockQuantity)
            throw new DomainException("Insufficient stock");
        StockQuantity -= quantity;
    }

    public bool IsInStock => StockQuantity > 0;
}
```

---

## Level 4 (Easy)

**Question:** Create a repository interface in the Application layer and implement it in Infrastructure.

**Expected Solution:**
```csharp
// Application/Interfaces/IProductRepository.cs
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}

// Infrastructure/Persistence/ProductRepository.cs
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
```

---

## Level 5 (Moderate)

**Question:** Create a use case for creating an order with validation.

**Expected Solution:**
```csharp
// Application/UseCases/CreateOrderUseCase.cs
public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateOrderUseCase(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task<CreateOrderResult> ExecuteAsync(CreateOrderCommand command)
    {
        // Validate customer exists
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
        if (customer == null)
            return CreateOrderResult.Failed("Customer not found");

        // Create order
        var order = new Order(command.CustomerId);

        // Add items
        foreach (var item in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                return CreateOrderResult.Failed($"Product {item.ProductId} not found");

            if (!product.IsInStock)
                return CreateOrderResult.Failed($"Product {product.Name} is out of stock");

            if (product.StockQuantity < item.Quantity)
                return CreateOrderResult.Failed($"Insufficient stock for {product.Name}");

            try
            {
                order.AddItem(product, item.Quantity);
            }
            catch (DomainException ex)
            {
                return CreateOrderResult.Failed(ex.Message);
            }
        }

        await _orderRepository.AddAsync(order);

        return CreateOrderResult.Success(order.Id);
    }
}

// Application/Commands/CreateOrderCommand.cs
public record CreateOrderCommand(
    int CustomerId,
    List<OrderItemDto> Items
);

public record OrderItemDto(int ProductId, int Quantity);

// Application/Results/CreateOrderResult.cs
public class CreateOrderResult
{
    public bool IsSuccess { get; private set; }
    public int? OrderId { get; private set; }
    public string? Error { get; private set; }

    public static CreateOrderResult Success(int orderId) =>
        new() { IsSuccess = true, OrderId = orderId };

    public static CreateOrderResult Failed(string error) =>
        new() { IsSuccess = false, Error = error };
}
```

---

## Level 6 (Moderate)

**Question:** Set up dependency injection for all layers.

**Expected Solution:**
```csharp
// Application/DependencyInjection.cs
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Use Cases
        services.AddScoped<CreateOrderUseCase>();
        services.AddScoped<SubmitOrderUseCase>();
        services.AddScoped<CancelOrderUseCase>();

        // Query Services
        services.AddScoped<IOrderQueryService, OrderQueryService>();
        services.AddScoped<IProductQueryService, ProductQueryService>();

        // Validators (if using FluentValidation)
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

// Infrastructure/DependencyInjection.cs
namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        // Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // External Services
        services.AddScoped<IEmailService, SendGridEmailService>();
        services.AddScoped<IPaymentGateway, StripePaymentGateway>();
        services.AddScoped<IEventPublisher, RabbitMQEventPublisher>();

        // Caching
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        return services;
    }
}

// WebApi/Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

## Level 7 (Challenging)

**Question:** Implement a domain entity with value objects and domain events.

**Expected Solution:**
```csharp
// Domain/ValueObjects/Money.cs
public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new DomainException("Amount cannot be negative");
        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Currency is required");

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new DomainException("Cannot add different currencies");
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator *(Money money, int multiplier)
    {
        return new Money(money.Amount * multiplier, money.Currency);
    }
}

// Domain/ValueObjects/Address.cs
public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country);

// Domain/Events/DomainEvent.cs
public abstract record DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}

// Domain/Events/OrderEvents.cs
public record OrderCreatedEvent(int OrderId, int CustomerId) : DomainEvent;
public record OrderSubmittedEvent(int OrderId, Money Total) : DomainEvent;
public record OrderCancelledEvent(int OrderId, string Reason) : DomainEvent;

// Domain/Entities/Order.cs
public class Order
{
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public Address ShippingAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public Money Total { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public Order(int customerId, Address shippingAddress, string currency = "USD")
    {
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        Status = OrderStatus.Draft;
        Total = new Money(0, currency);

        AddDomainEvent(new OrderCreatedEvent(Id, customerId));
    }

    public void AddItem(Product product, int quantity)
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Cannot modify submitted order");

        var item = new OrderItem(product.Id, product.Price, quantity);
        _items.Add(item);
        RecalculateTotal();
    }

    public void Submit()
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Order already submitted");
        if (!_items.Any())
            throw new DomainException("Cannot submit empty order");

        Status = OrderStatus.Submitted;
        AddDomainEvent(new OrderSubmittedEvent(Id, Total));
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            throw new DomainException("Cannot cancel shipped order");

        Status = OrderStatus.Cancelled;
        AddDomainEvent(new OrderCancelledEvent(Id, reason));
    }

    private void RecalculateTotal()
    {
        var sum = _items.Sum(i => i.Total.Amount);
        Total = new Money(sum, Total.Currency);
    }

    private void AddDomainEvent(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
```

---

## Level 8 (Challenging)

**Question:** Implement CQRS with MediatR.

**Expected Solution:**
```csharp
// Application/Commands/CreateOrderCommand.cs
public record CreateOrderCommand(
    int CustomerId,
    AddressDto ShippingAddress,
    List<OrderItemDto> Items
) : IRequest<Result<int>>;

// Application/Commands/Handlers/CreateOrderCommandHandler.cs
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var address = new Address(
            request.ShippingAddress.Street,
            request.ShippingAddress.City,
            request.ShippingAddress.State,
            request.ShippingAddress.PostalCode,
            request.ShippingAddress.Country);

        var order = new Order(request.CustomerId, address);

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                return Result<int>.Failure($"Product {item.ProductId} not found");

            try
            {
                order.AddItem(product, item.Quantity);
            }
            catch (DomainException ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result<int>.Success(order.Id);
    }
}

// Application/Queries/GetOrderQuery.cs
public record GetOrderQuery(int OrderId) : IRequest<OrderDto?>;

// Application/Queries/Handlers/GetOrderQueryHandler.cs
public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto?>
{
    private readonly IOrderQueryService _queryService;

    public GetOrderQueryHandler(IOrderQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken ct)
    {
        return await _queryService.GetByIdAsync(request.OrderId, ct);
    }
}

// Controller using MediatR
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _mediator.Send(new GetOrderQuery(id));
        return order == null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateOrderRequest request)
    {
        var result = await _mediator.Send(new CreateOrderCommand(
            request.CustomerId,
            request.ShippingAddress,
            request.Items));

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }
}
```

---

## Level 9 (Expert)

**Question:** Design a complete Clean Architecture solution with:
- Rich domain model with aggregates
- Domain events with handlers
- CQRS with separate read/write models
- Validation pipeline
- Unit of Work pattern

Provide the project structure and key implementations.

**Expected Solution:**
```
Solution/
├── src/
│   ├── Domain/
│   │   ├── Common/
│   │   │   ├── Entity.cs
│   │   │   ├── AggregateRoot.cs
│   │   │   ├── ValueObject.cs
│   │   │   └── DomainEvent.cs
│   │   ├── Orders/
│   │   │   ├── Order.cs (Aggregate Root)
│   │   │   ├── OrderItem.cs
│   │   │   ├── OrderStatus.cs
│   │   │   └── Events/
│   │   └── Products/
│   │       └── Product.cs
│   │
│   ├── Application/
│   │   ├── Common/
│   │   │   ├── Interfaces/
│   │   │   │   ├── IUnitOfWork.cs
│   │   │   │   └── IEventDispatcher.cs
│   │   │   ├── Behaviors/
│   │   │   │   ├── ValidationBehavior.cs
│   │   │   │   └── LoggingBehavior.cs
│   │   │   └── Results/
│   │   │       └── Result.cs
│   │   ├── Orders/
│   │   │   ├── Commands/
│   │   │   ├── Queries/
│   │   │   ├── EventHandlers/
│   │   │   └── Validators/
│   │   └── DependencyInjection.cs
│   │
│   ├── Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── UnitOfWork.cs
│   │   │   └── Repositories/
│   │   ├── Services/
│   │   └── DependencyInjection.cs
│   │
│   └── WebApi/
│       ├── Controllers/
│       ├── Middleware/
│       └── Program.cs
```

Key implementations shown in previous levels plus validation pipeline and domain event dispatching.

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Understand layers and dependencies |
| 3-4 | Domain entities, repository pattern |
| 5-6 | Use cases, dependency injection |
| 7-8 | Value objects, CQRS, domain events |
| 9 | Complete architecture with all patterns |
