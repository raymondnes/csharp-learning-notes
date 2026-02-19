# Topic 34: Repository and Unit of Work - Assessment Questions

## Level 1 (Trivial)

**Question:** Create a basic repository interface for a `Customer` entity.

**Expected Solution:**
```csharp
public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}
```

---

## Level 2 (Trivial)

**Question:** Implement the `GetByIdAsync` and `GetAllAsync` methods for a CustomerRepository.

**Expected Solution:**
```csharp
public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }
}
```

---

## Level 3 (Easy)

**Question:** Create a generic repository interface and implement it.

**Expected Solution:**
```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

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

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
```

---

## Level 4 (Easy)

**Question:** Create a Unit of Work interface with two repositories.

**Expected Solution:**
```csharp
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    Task<int> SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProductRepository? _products;
    private IOrderRepository? _orders;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products =>
        _products ??= new ProductRepository(_context);

    public IOrderRepository Orders =>
        _orders ??= new OrderRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

---

## Level 5 (Moderate)

**Question:** Extend a generic repository with a `FindAsync` method that accepts a predicate.

**Expected Solution:**
```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}

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

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}
```

---

## Level 6 (Moderate)

**Question:** Create a specialized ProductRepository with custom query methods.

**Expected Solution:**
```csharp
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetInStockAsync();
    Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
    Task<Product?> GetWithCategoryAsync(int id);
    Task<decimal> GetAveragePriceAsync();
}

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    private AppDbContext AppContext => (AppDbContext)_context;

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetInStockAsync()
    {
        return await _dbSet
            .Where(p => p.StockQuantity > 0)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold)
    {
        return await _dbSet
            .Where(p => p.StockQuantity <= threshold && p.StockQuantity > 0)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<Product?> GetWithCategoryAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        return await _dbSet.AverageAsync(p => p.Price);
    }
}
```

---

## Level 7 (Challenging)

**Question:** Implement a Unit of Work with transaction support.

**Expected Solution:**
```csharp
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    ICustomerRepository Customers { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    private IProductRepository? _products;
    private IOrderRepository? _orders;
    private ICustomerRepository? _customers;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products =>
        _products ??= new ProductRepository(_context);

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

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction in progress");

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
```

---

## Level 8 (Challenging)

**Question:** Create a service that uses Unit of Work to process an order with inventory updates.

**Expected Solution:**
```csharp
public class OrderProcessingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderProcessingService> _logger;

    public OrderProcessingService(IUnitOfWork unitOfWork, ILogger<OrderProcessingService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OrderResult> ProcessOrderAsync(CreateOrderDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Validate customer
            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (customer == null)
            {
                return OrderResult.Failure("Customer not found");
            }

            // Create order
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = new List<OrderItem>()
            };

            decimal total = 0;

            // Process each item
            foreach (var itemDto in dto.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);

                if (product == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return OrderResult.Failure($"Product {itemDto.ProductId} not found");
                }

                if (product.StockQuantity < itemDto.Quantity)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return OrderResult.Failure($"Insufficient stock for {product.Name}");
                }

                // Reserve inventory
                product.StockQuantity -= itemDto.Quantity;
                _unitOfWork.Products.Update(product);

                // Add order item
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                };

                order.Items.Add(orderItem);
                total += orderItem.UnitPrice * orderItem.Quantity;
            }

            order.Total = total;

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Order {OrderId} created successfully", order.Id);

            return OrderResult.Success(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order");
            await _unitOfWork.RollbackTransactionAsync();
            return OrderResult.Failure("An error occurred processing the order");
        }
    }
}

public class OrderResult
{
    public bool IsSuccess { get; set; }
    public Order? Order { get; set; }
    public string? ErrorMessage { get; set; }

    public static OrderResult Success(Order order) =>
        new() { IsSuccess = true, Order = order };

    public static OrderResult Failure(string error) =>
        new() { IsSuccess = false, ErrorMessage = error };
}
```

---

## Level 9 (Expert)

**Question:** Implement a complete repository pattern with specifications, pagination, and audit logging.

**Expected Solution:**
```csharp
// Specification pattern
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int? Skip { get; }
    int? Take { get; }
}

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; protected set; } = _ => true;
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; protected set; }
    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }

    protected void AddInclude(Expression<Func<T, object>> include) =>
        Includes.Add(include);

    protected void ApplyPaging(int page, int pageSize)
    {
        Skip = (page - 1) * pageSize;
        Take = pageSize;
    }
}

// Paginated result
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}

// Enhanced repository interface
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAsync(ISpecification<T> spec);
    Task<PagedResult<T>> GetPagedAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    Task<bool> AnyAsync(ISpecification<T> spec);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

// Implementation with audit support
public class AuditableRepository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly IAuditLogger _auditLogger;
    protected readonly ICurrentUserService _currentUser;

    public AuditableRepository(
        DbContext context,
        IAuditLogger auditLogger,
        ICurrentUserService currentUser)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _auditLogger = auditLogger;
        _currentUser = currentUser;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<PagedResult<T>> GetPagedAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec, ignorePaging: true);
        var totalCount = await query.CountAsync();

        var items = await ApplySpecification(spec).ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            Page = (spec.Skip ?? 0) / (spec.Take ?? 1) + 1,
            PageSize = spec.Take ?? totalCount
        };
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec, ignorePaging: true).CountAsync();
    }

    public async Task<bool> AnyAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).AnyAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        _auditLogger.LogCreate(typeof(T).Name, entity, _currentUser.UserId);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _auditLogger.LogUpdate(typeof(T).Name, entity, _currentUser.UserId);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _auditLogger.LogDelete(typeof(T).Name, entity, _currentUser.UserId);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec, bool ignorePaging = false)
    {
        var query = _dbSet.AsQueryable();

        query = query.Where(spec.Criteria);

        foreach (var include in spec.Includes)
        {
            query = query.Include(include);
        }

        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);
        else if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        if (!ignorePaging)
        {
            if (spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);
            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);
        }

        return query;
    }
}

// Example specification
public class ActiveProductsByCategorySpec : Specification<Product>
{
    public ActiveProductsByCategorySpec(int categoryId, int page, int pageSize)
    {
        Criteria = p => p.CategoryId == categoryId && p.IsActive;
        AddInclude(p => p.Category);
        OrderBy = p => p.Name;
        ApplyPaging(page, pageSize);
    }
}

// Usage in service
public class ProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Product>> GetProductsByCategoryAsync(
        int categoryId, int page = 1, int pageSize = 10)
    {
        var spec = new ActiveProductsByCategorySpec(categoryId, page, pageSize);
        return await _repository.GetPagedAsync(spec);
    }
}
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Basic repository interface and implementation |
| 3-4 | Generic repository, Unit of Work basics |
| 5-6 | Custom queries, specialized repositories |
| 7-8 | Transaction support, service integration |
| 9 | Specifications, pagination, auditing |
