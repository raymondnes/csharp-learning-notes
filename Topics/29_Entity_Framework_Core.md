# Entity Framework Core

## Introduction

Entity Framework Core (EF Core) is Microsoft's modern Object-Relational Mapper (ORM) for .NET. It allows you to work with databases using C# objects, eliminating the need for most data-access code you'd typically have to write.

## What is an ORM?

An ORM maps between:
- Database tables → C# classes
- Table rows → Object instances
- Table columns → Class properties

```
Database Table: Products          C# Class: Product
┌────┬─────────┬─────────┐       class Product
│ Id │ Name    │ Price   │       {
├────┼─────────┼─────────┤  →        int Id
│ 1  │ Laptop  │ 999.99  │           string Name
│ 2  │ Phone   │ 599.99  │           decimal Price
└────┴─────────┴─────────┘       }
```

## Setting Up EF Core

### Install Packages
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer    # For SQL Server
dotnet add package Microsoft.EntityFrameworkCore.Sqlite       # For SQLite
dotnet add package Microsoft.EntityFrameworkCore.InMemory     # For testing
dotnet add package Microsoft.EntityFrameworkCore.Tools        # For migrations
```

### Create Entity Classes (Models)
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    // Navigation property
    public Category? Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property (collection)
    public List<Product> Products { get; set; } = new();
}
```

### Create DbContext
```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
```

### Configure in Program.cs
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Or with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Or with In-Memory (for testing)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));
```

### Connection String (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyApp;Trusted_Connection=True;"
  }
}
```

## Migrations

Migrations track changes to your model and update the database:

### Create Migration
```bash
dotnet ef migrations add InitialCreate
```

### Apply Migration
```bash
dotnet ef database update
```

### Remove Last Migration
```bash
dotnet ef migrations remove
```

## CRUD Operations

### Create (INSERT)
```csharp
app.MapPost("/api/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});
```

### Read (SELECT)
```csharp
// Get all
app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

// Get by ID
app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

// With filtering
app.MapGet("/api/products/search", async (string name, AppDbContext db) =>
    await db.Products
        .Where(p => p.Name.Contains(name))
        .ToListAsync());
```

### Update (UPDATE)
```csharp
app.MapPut("/api/products/{id}", async (int id, Product input, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = input.Name;
    product.Price = input.Price;

    await db.SaveChangesAsync();
    return Results.Ok(product);
});
```

### Delete (DELETE)
```csharp
app.MapDelete("/api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
```

## Querying with LINQ

EF Core translates LINQ to SQL:

```csharp
// Simple query
var products = await db.Products.ToListAsync();

// Where clause
var expensiveProducts = await db.Products
    .Where(p => p.Price > 100)
    .ToListAsync();

// Ordering
var sortedProducts = await db.Products
    .OrderBy(p => p.Name)
    .ToListAsync();

// Pagination
var page = await db.Products
    .Skip(20)
    .Take(10)
    .ToListAsync();

// Select specific columns
var names = await db.Products
    .Select(p => new { p.Id, p.Name })
    .ToListAsync();

// First or default
var product = await db.Products
    .FirstOrDefaultAsync(p => p.Id == 5);

// Count
var count = await db.Products.CountAsync();

// Any
var hasExpensive = await db.Products.AnyAsync(p => p.Price > 1000);
```

## Relationships

### One-to-Many
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
    public int CategoryId { get; set; }      // Foreign key
    public Category Category { get; set; }    // Navigation property
}
```

### Including Related Data
```csharp
// Eager loading with Include
var productsWithCategory = await db.Products
    .Include(p => p.Category)
    .ToListAsync();

// Multiple levels
var categoriesWithProducts = await db.Categories
    .Include(c => c.Products)
    .ThenInclude(p => p.Reviews)
    .ToListAsync();
```

## Model Configuration

### Using Data Annotations
```csharp
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
}
```

### Using Fluent API
```csharp
public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        });
    }
}
```

## Complete Example

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Name = "Laptop", Price = 999.99m },
            new Product { Name = "Phone", Price = 599.99m }
        );
        db.SaveChanges();
    }
}

// CRUD endpoints
app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
    await db.Products.FindAsync(id) is Product product
        ? Results.Ok(product)
        : Results.NotFound());

app.MapPost("/api/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id}", async (int id, Product input, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = input.Name;
    product.Price = input.Price;
    await db.SaveChangesAsync();

    return Results.Ok(product);
});

app.MapDelete("/api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

// Models
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

// DbContext
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}
```

## Summary

- EF Core is an ORM that maps objects to database tables
- DbContext manages database connection and entities
- DbSet<T> represents a table/collection
- LINQ queries are translated to SQL
- Migrations track and apply schema changes
- Navigation properties handle relationships
- Include() for eager loading related data
- Data annotations or Fluent API for configuration

EF Core dramatically simplifies database operations in .NET applications.
