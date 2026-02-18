# Projects 61-80: Expert Projects (ASP.NET Core & EF Core)

## Project 61: Hello API

**Concepts:** Minimal API basics

Create your first API:
- Single GET endpoint
- Return JSON
- Test with browser/curl

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, API!");
app.MapGet("/api/info", () => new { Name = "My First API", Version = "1.0" });

app.Run();
```

---

## Project 62: Products API

**Concepts:** CRUD endpoints, in-memory data

Create products API:
- GET all products
- GET single product
- POST create product
- PUT update product
- DELETE product

```csharp
var products = new List<Product>();

app.MapGet("/api/products", () => products);
app.MapGet("/api/products/{id}", (int id) =>
    products.FirstOrDefault(p => p.Id == id) is Product p
        ? Results.Ok(p)
        : Results.NotFound());
app.MapPost("/api/products", (Product product) =>
{
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});
```

---

## Project 63: Todo API

**Concepts:** Full REST implementation

Create complete Todo API:
- All CRUD operations
- Filter by completion status
- Mark as complete endpoint
- Proper status codes

---

## Project 64: Users API with DI

**Concepts:** Dependency injection

Create users API with services:
- IUserService interface
- UserService implementation
- Register in DI container
- Inject into endpoints

```csharp
builder.Services.AddSingleton<IUserService, UserService>();

app.MapGet("/api/users", (IUserService service) => service.GetAll());
app.MapGet("/api/users/{id}", (int id, IUserService service) =>
    service.GetById(id) is User u ? Results.Ok(u) : Results.NotFound());
```

---

## Project 65: Notes API

**Concepts:** Service layer pattern

Create notes API:
- Note entity
- INoteRepository (data access)
- INoteService (business logic)
- API endpoints

---

## Project 66: Blog API

**Concepts:** Relationships, nested routes

Create blog API:
- Posts and Comments
- GET /posts/{id}/comments
- POST /posts/{id}/comments
- Nested data in responses

---

## Project 67: Auth Simulation

**Concepts:** Middleware concepts, headers

Create authentication simulation:
- API key in header
- Validate on each request
- Return 401 if invalid
- Protected endpoints

```csharp
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/protected"))
    {
        var apiKey = context.Request.Headers["X-API-Key"].FirstOrDefault();
        if (apiKey != "secret-key")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }
    await next();
});
```

---

## Project 68: Paginated API

**Concepts:** Query parameters, pagination

Create paginated API:
- page and pageSize parameters
- Return pagination metadata
- Sort parameter
- Filter parameters

```csharp
app.MapGet("/api/products", (int page = 1, int pageSize = 10, string? sort = null) =>
{
    var query = products.AsQueryable();

    if (sort == "price") query = query.OrderBy(p => p.Price);
    if (sort == "name") query = query.OrderBy(p => p.Name);

    var total = query.Count();
    var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    return new
    {
        Data = items,
        Page = page,
        PageSize = pageSize,
        TotalItems = total,
        TotalPages = (int)Math.Ceiling(total / (double)pageSize)
    };
});
```

---

## Project 69: EF Core Setup

**Concepts:** DbContext, models, configuration

Set up Entity Framework Core:
- Create entity classes
- Create DbContext
- Configure connection
- Use in-memory database for testing

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}

// In Program.cs
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));
```

---

## Project 70: Products with EF

**Concepts:** EF CRUD operations

Create Products API with EF Core:
- Use DbContext for data access
- Async operations
- SaveChangesAsync
- Find, Add, Remove

```csharp
app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

app.MapPost("/api/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});
```

---

## Project 71: Categories & Products

**Concepts:** EF relationships, Include

Create related entities:
- Category has many Products
- Product belongs to Category
- Include related data
- Nested routes

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.Include(p => p.Category).ToListAsync());
```

---

## Project 72: Order System

**Concepts:** Complex relationships

Create order system:
- Customer, Order, OrderItem, Product
- One-to-many relationships
- Many-to-many (Order-Product through OrderItem)
- Calculate totals

---

## Project 73: Repository Pattern

**Concepts:** Repository abstraction

Implement repository pattern:
- IRepository<T> interface
- EfRepository<T> implementation
- Inject repository, not DbContext
- Unit testing ready

```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}
```

---

## Project 74: Unit of Work

**Concepts:** Unit of Work pattern

Implement Unit of Work:
- IUnitOfWork interface
- Multiple repositories
- Single SaveChanges
- Transaction support

---

## Project 75: API with Validation

**Concepts:** Data validation

Add validation to API:
- Required fields
- Range validation
- Custom validation
- Return validation errors

```csharp
app.MapPost("/api/products", (Product product) =>
{
    var errors = new List<string>();

    if (string.IsNullOrEmpty(product.Name))
        errors.Add("Name is required");
    if (product.Price <= 0)
        errors.Add("Price must be positive");

    if (errors.Any())
        return Results.BadRequest(new { Errors = errors });

    // Save product...
    return Results.Created($"/api/products/{product.Id}", product);
});
```

---

## Project 76: Caching Service

**Concepts:** Service patterns, caching

Create caching service:
- ICacheService interface
- In-memory implementation
- Cache products
- Cache invalidation

---

## Project 77: Background Tasks

**Concepts:** Async patterns

Create background processing:
- Long-running operations
- Progress tracking
- Async service methods
- Task status checking

---

## Project 78: E-Commerce API

**Concepts:** Full application

Build complete e-commerce API:
- Products, Categories, Customers, Orders
- Shopping cart functionality
- Order processing
- Inventory management
- Full CRUD for all entities

---

## Project 79: Task Management API

**Concepts:** Complete system

Build task management system:
- Projects and Tasks
- Task assignments
- Status workflow
- Due dates and priorities
- Filtering and search

---

## Project 80: Blog Platform

**Concepts:** Capstone project

Build complete blog platform:
- Authors, Posts, Comments, Tags
- Post publishing workflow (draft/published)
- Tag management (many-to-many)
- Comment moderation
- Search functionality
- Pagination
- Full CRUD
- Proper error handling
- Clean architecture

```csharp
// Entities
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public List<PostTag> PostTags { get; set; } = new();
}

// Services
public interface IPostService
{
    Task<PagedResult<PostDto>> GetPublishedPostsAsync(int page, int pageSize);
    Task<PostDto?> GetBySlugAsync(string slug);
    Task<PostDto> CreateAsync(CreatePostDto dto);
    Task<PostDto> PublishAsync(int id);
    Task<bool> AddCommentAsync(int postId, CreateCommentDto dto);
}

// Endpoints
var posts = app.MapGroup("/api/posts");

posts.MapGet("/", async (IPostService service, int page = 1, int pageSize = 10) =>
    await service.GetPublishedPostsAsync(page, pageSize));

posts.MapGet("/{slug}", async (string slug, IPostService service) =>
    await service.GetBySlugAsync(slug) is PostDto post
        ? Results.Ok(post)
        : Results.NotFound());

posts.MapPost("/", async (CreatePostDto dto, IPostService service) =>
{
    var post = await service.CreateAsync(dto);
    return Results.Created($"/api/posts/{post.Slug}", post);
});

posts.MapPost("/{id}/publish", async (int id, IPostService service) =>
{
    var post = await service.PublishAsync(id);
    return Results.Ok(post);
});

posts.MapPost("/{id}/comments", async (int id, CreateCommentDto dto, IPostService service) =>
{
    var success = await service.AddCommentAsync(id, dto);
    return success ? Results.Created() : Results.NotFound();
});
```

---

## Congratulations!

Completing all 80 projects means you've built:
- 20 beginner console applications
- 20 intermediate OOP projects
- 20 advanced pattern implementations
- 20 web API applications

You're now ready for real-world C# development!
