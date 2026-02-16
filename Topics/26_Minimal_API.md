# Minimal API

## Introduction

Minimal APIs in ASP.NET Core provide a streamlined way to build HTTP APIs with minimal code and configuration. Introduced in .NET 6, they're perfect for microservices, small APIs, and learning web development.

## Your First Minimal API

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, World!");

app.Run();
```

That's a complete, working web API in just 4 lines!

## Project Setup

Create a new minimal API project:
```bash
dotnet new web -n MyApi
cd MyApi
dotnet run
```

## Basic Routing

### MapGet - Handle GET Requests
```csharp
app.MapGet("/", () => "Home page");
app.MapGet("/hello", () => "Hello!");
app.MapGet("/api/products", () => new[] { "Product1", "Product2" });
```

### MapPost - Handle POST Requests
```csharp
app.MapPost("/api/products", () => "Product created");
```

### MapPut - Handle PUT Requests
```csharp
app.MapPut("/api/products/{id}", (int id) => $"Updated product {id}");
```

### MapDelete - Handle DELETE Requests
```csharp
app.MapDelete("/api/products/{id}", (int id) => $"Deleted product {id}");
```

### MapPatch - Handle PATCH Requests
```csharp
app.MapPatch("/api/products/{id}", (int id) => $"Patched product {id}");
```

## Route Parameters

### Path Parameters
```csharp
// Single parameter
app.MapGet("/users/{id}", (int id) => $"User ID: {id}");

// Multiple parameters
app.MapGet("/users/{userId}/orders/{orderId}",
    (int userId, int orderId) => $"User {userId}, Order {orderId}");

// String parameter
app.MapGet("/greet/{name}", (string name) => $"Hello, {name}!");
```

### Query Parameters
```csharp
// Query string: /search?term=laptop
app.MapGet("/search", (string term) => $"Searching for: {term}");

// Optional query parameter
app.MapGet("/products", (int? page, int? limit) =>
    $"Page: {page ?? 1}, Limit: {limit ?? 10}");

// With default value
app.MapGet("/items", (int page = 1, int size = 10) =>
    $"Page {page}, Size {size}");
```

## Returning Data

### Plain Text
```csharp
app.MapGet("/text", () => "Plain text response");
```

### JSON (Automatic)
```csharp
app.MapGet("/json", () => new { Name = "John", Age = 30 });

app.MapGet("/product", () => new Product { Id = 1, Name = "Laptop", Price = 999 });

app.MapGet("/products", () => new List<Product>
{
    new Product { Id = 1, Name = "Laptop" },
    new Product { Id = 2, Name = "Phone" }
});
```

### Using Results
```csharp
app.MapGet("/ok", () => Results.Ok("Success"));
app.MapGet("/created", () => Results.Created("/items/1", new { Id = 1 }));
app.MapGet("/nocontent", () => Results.NoContent());
app.MapGet("/notfound", () => Results.NotFound());
app.MapGet("/bad", () => Results.BadRequest("Invalid input"));
```

## Request Body

### Receiving JSON Body
```csharp
app.MapPost("/api/products", (Product product) =>
{
    // product is automatically deserialized from JSON body
    return Results.Created($"/api/products/{product.Id}", product);
});

record Product(int Id, string Name, decimal Price);
```

### With Validation
```csharp
app.MapPost("/api/users", (User user) =>
{
    if (string.IsNullOrEmpty(user.Name))
    {
        return Results.BadRequest("Name is required");
    }
    if (user.Age < 0)
    {
        return Results.BadRequest("Age must be positive");
    }
    return Results.Ok(user);
});
```

## HTTP Context Access

```csharp
app.MapGet("/info", (HttpContext context) =>
{
    var method = context.Request.Method;
    var path = context.Request.Path;
    var userAgent = context.Request.Headers["User-Agent"];

    return new { method, path, userAgent = userAgent.ToString() };
});

app.MapGet("/headers", (HttpRequest request) =>
{
    return request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
});

app.MapGet("/custom-response", (HttpResponse response) =>
{
    response.Headers["X-Custom-Header"] = "CustomValue";
    return "Check the headers!";
});
```

## Complete CRUD Example

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory data store
var products = new List<Product>
{
    new Product(1, "Laptop", 999.99m),
    new Product(2, "Phone", 599.99m)
};
var nextId = 3;

// GET all products
app.MapGet("/api/products", () => products);

// GET single product
app.MapGet("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null
        ? Results.Ok(product)
        : Results.NotFound($"Product {id} not found");
});

// POST create product
app.MapPost("/api/products", (ProductDto dto) =>
{
    var product = new Product(nextId++, dto.Name, dto.Price);
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});

// PUT update product
app.MapPut("/api/products/{id}", (int id, ProductDto dto) =>
{
    var index = products.FindIndex(p => p.Id == id);
    if (index == -1)
    {
        return Results.NotFound();
    }

    products[index] = new Product(id, dto.Name, dto.Price);
    return Results.Ok(products[index]);
});

// DELETE product
app.MapDelete("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null)
    {
        return Results.NotFound();
    }

    products.Remove(product);
    return Results.NoContent();
});

app.Run();

record Product(int Id, string Name, decimal Price);
record ProductDto(string Name, decimal Price);
```

## Route Groups

Organize related endpoints:
```csharp
var products = app.MapGroup("/api/products");

products.MapGet("/", () => "Get all products");
products.MapGet("/{id}", (int id) => $"Get product {id}");
products.MapPost("/", () => "Create product");
products.MapPut("/{id}", (int id) => $"Update product {id}");
products.MapDelete("/{id}", (int id) => $"Delete product {id}");

// Nested groups
var v1 = app.MapGroup("/api/v1");
var v1Products = v1.MapGroup("/products");
v1Products.MapGet("/", () => "V1 Products");
```

## Dependency Injection

```csharp
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Inject via parameter
app.MapGet("/api/products", (IProductService service) =>
{
    return service.GetAll();
});

app.MapGet("/api/products/{id}", (int id, IProductService service) =>
{
    var product = service.GetById(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.Run();

// Service interface and implementation
interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}

class ProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product(1, "Laptop", 999),
        new Product(2, "Phone", 599)
    };

    public IEnumerable<Product> GetAll() => _products;
    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
}
```

## Error Handling

```csharp
app.MapGet("/api/products/{id}", (int id) =>
{
    try
    {
        var product = GetProduct(id);
        if (product is null)
        {
            return Results.NotFound(new { error = "Product not found", id });
        }
        return Results.Ok(product);
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: 500,
            title: "Internal Server Error"
        );
    }
});
```

## Async Endpoints

```csharp
app.MapGet("/api/products", async (IProductService service) =>
{
    var products = await service.GetAllAsync();
    return Results.Ok(products);
});

app.MapPost("/api/products", async (Product product, IProductService service) =>
{
    await service.AddAsync(product);
    return Results.Created($"/api/products/{product.Id}", product);
});
```

## Configuration

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors();

// Add JSON options
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null; // PascalCase
});

var app = builder.Build();

// Use CORS
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/", () => "Hello");

app.Run();
```

## Summary

- Minimal APIs provide streamlined HTTP API development
- Map methods: MapGet, MapPost, MapPut, MapDelete
- Parameters from route, query string, or body
- Results helper for HTTP responses
- Route groups organize endpoints
- Full dependency injection support
- Perfect for microservices and simple APIs

Minimal APIs make it easy to build web APIs with C#.
