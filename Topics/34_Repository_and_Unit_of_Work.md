# Topic 34: Repository and Unit of Work Patterns

## Introduction

The Repository and Unit of Work patterns abstract data access, making your code more testable, maintainable, and independent of specific database technologies.

## The Repository Pattern

A Repository acts as an in-memory collection of domain objects, hiding the complexity of data access.

### Basic Repository Interface

```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
```

### Generic Repository Implementation

```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}
```

## Domain-Specific Repositories

Extend the generic repository for specific entities:

```csharp
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetInStockAsync();
    Task<Product?> GetWithDetailsAsync(int id);
}

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetInStockAsync()
    {
        return await _dbSet
            .Where(p => p.StockQuantity > 0)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Product?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
```

## The Unit of Work Pattern

Unit of Work maintains a list of objects affected by a business transaction and coordinates writing out changes.

### Unit of Work Interface

```csharp
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IOrderRepository Orders { get; }
    ICustomerRepository Customers { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
```

### Unit of Work Implementation

```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    private IProductRepository? _products;
    private ICategoryRepository? _categories;
    private IOrderRepository? _orders;
    private ICustomerRepository? _customers;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    // Lazy initialization of repositories
    public IProductRepository Products =>
        _products ??= new ProductRepository(_context);

    public ICategoryRepository Categories =>
        _categories ??= new CategoryRepository(_context);

    public IOrderRepository Orders =>
        _orders ??= new OrderRepository(_context);

    public ICustomerRepository Customers =>
        _customers ??= new CustomerRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync()!;
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
```

## Using Repository and Unit of Work

### In a Service

```csharp
public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Get customer
            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (customer == null)
                throw new NotFoundException("Customer not found");

            // Create order
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            // Add order items and update inventory
            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product {item.ProductId} not found");

                if (product.StockQuantity < item.Quantity)
                    throw new InsufficientStockException(product.Name);

                product.StockQuantity -= item.Quantity;
                _unitOfWork.Products.Update(product);

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            order.Total = order.Items.Sum(i => i.UnitPrice * i.Quantity);

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return order;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
```

### In an API Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _unitOfWork.Products.GetWithDetailsAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(int categoryId)
    {
        var products = await _unitOfWork.Products.GetByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            StockQuantity = dto.StockQuantity
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
```

## Dependency Injection Registration

```csharp
// Program.cs or Startup.cs
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
```

## Specification Pattern (Advanced)

For complex queries, use the Specification pattern:

```csharp
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int? Take { get; }
    int? Skip { get; }
}

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; private set; } = _ => true;
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int? Take { get; private set; }
    public int? Skip { get; private set; }

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
}

// Usage
public class ProductsByCategorySpec : Specification<Product>
{
    public ProductsByCategorySpec(int categoryId, int page, int pageSize)
    {
        AddCriteria(p => p.CategoryId == categoryId && p.IsActive);
        AddInclude(p => p.Category);
        ApplyOrderBy(p => p.Name);
        ApplyPaging((page - 1) * pageSize, pageSize);
    }
}

// In repository
public async Task<IEnumerable<T>> GetAsync(ISpecification<T> spec)
{
    var query = _dbSet.AsQueryable();

    query = query.Where(spec.Criteria);

    foreach (var include in spec.Includes)
    {
        query = query.Include(include);
    }

    if (spec.OrderBy != null)
        query = query.OrderBy(spec.OrderBy);

    if (spec.Skip.HasValue)
        query = query.Skip(spec.Skip.Value);

    if (spec.Take.HasValue)
        query = query.Take(spec.Take.Value);

    return await query.ToListAsync();
}
```

## Testing with Repositories

Repositories make testing easy:

```csharp
public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrder_ValidOrder_ReturnsOrder()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProducts = new Mock<IProductRepository>();
        var mockCustomers = new Mock<ICustomerRepository>();
        var mockOrders = new Mock<IOrderRepository>();

        var customer = new Customer { Id = 1, Name = "John" };
        var product = new Product { Id = 1, Name = "Laptop", Price = 999, StockQuantity = 10 };

        mockCustomers.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(customer);
        mockProducts.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

        mockUnitOfWork.Setup(u => u.Customers).Returns(mockCustomers.Object);
        mockUnitOfWork.Setup(u => u.Products).Returns(mockProducts.Object);
        mockUnitOfWork.Setup(u => u.Orders).Returns(mockOrders.Object);

        var service = new OrderService(mockUnitOfWork.Object);

        // Act
        var order = await service.CreateOrderAsync(new CreateOrderDto
        {
            CustomerId = 1,
            Items = new[] { new OrderItemDto { ProductId = 1, Quantity = 2 } }
        });

        // Assert
        Assert.NotNull(order);
        Assert.Equal(1998, order.Total);
        mockOrders.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
    }
}
```

## Summary

| Pattern | Purpose |
|---------|---------|
| Repository | Abstracts data access, provides collection-like interface |
| Unit of Work | Coordinates multiple repository operations in a transaction |
| Specification | Encapsulates query logic for reuse |

These patterns create a clean separation between your domain logic and data access, making your code more maintainable and testable.
