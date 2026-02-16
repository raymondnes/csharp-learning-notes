# Routing and JSON

## Introduction

Routing determines how an application responds to client requests at specific endpoints. JSON (JavaScript Object Notation) is the standard format for data exchange in modern web APIs. Together, they form the foundation of RESTful API design.

## Understanding Routing

Routing matches incoming HTTP requests to endpoint handlers:

```csharp
var app = builder.Build();

// Route: /            Method: GET
app.MapGet("/", () => "Home");

// Route: /api/users   Method: GET
app.MapGet("/api/users", () => GetUsers());

// Route: /api/users   Method: POST
app.MapPost("/api/users", (User user) => CreateUser(user));
```

## Route Patterns

### Static Routes
```csharp
app.MapGet("/about", () => "About page");
app.MapGet("/api/products", () => GetAllProducts());
app.MapGet("/api/v1/customers", () => GetCustomers());
```

### Route Parameters
```csharp
// Single parameter
app.MapGet("/users/{id}", (int id) => $"User {id}");

// Multiple parameters
app.MapGet("/orders/{year}/{month}", (int year, int month) =>
    $"Orders for {month}/{year}");

// String parameter
app.MapGet("/products/{name}", (string name) => $"Product: {name}");
```

### Route Constraints
```csharp
// Integer constraint
app.MapGet("/users/{id:int}", (int id) => $"User ID: {id}");

// Min value
app.MapGet("/page/{num:min(1)}", (int num) => $"Page {num}");

// Range
app.MapGet("/rating/{value:range(1,5)}", (int value) => $"Rating: {value}");

// Alpha only
app.MapGet("/category/{name:alpha}", (string name) => $"Category: {name}");

// Regex pattern
app.MapGet("/code/{value:regex(^[A-Z]{{3}}$)}", (string value) => $"Code: {value}");

// Length constraint
app.MapGet("/slug/{value:length(3,50)}", (string value) => $"Slug: {value}");
```

### Common Constraints
| Constraint | Example | Matches |
|------------|---------|---------|
| int | {id:int} | 123, 456 |
| bool | {active:bool} | true, false |
| datetime | {date:datetime} | 2024-01-15 |
| decimal | {price:decimal} | 19.99 |
| guid | {id:guid} | CD2C1638-... |
| minlength(n) | {name:minlength(3)} | abc, abcd |
| maxlength(n) | {name:maxlength(10)} | short text |

### Optional Parameters
```csharp
// Optional route segment
app.MapGet("/products/{category?}", (string? category) =>
    category is null ? "All products" : $"Products in {category}");

// With default value
app.MapGet("/items/{page:int=1}", (int page) => $"Page {page}");
```

### Catch-All Parameters
```csharp
// Catches remaining path segments
app.MapGet("/files/{*path}", (string path) =>
    $"File path: {path}");

// /files/documents/report.pdf → path = "documents/report.pdf"
```

## Query String Parameters

```csharp
// /search?q=laptop
app.MapGet("/search", (string q) => $"Searching: {q}");

// Multiple parameters: /filter?category=electronics&minPrice=100
app.MapGet("/filter", (string category, decimal minPrice) =>
    $"Category: {category}, Min: {minPrice}");

// Optional with defaults
app.MapGet("/products", (int page = 1, int size = 10, string? sort = null) =>
{
    var sortInfo = sort ?? "default";
    return $"Page {page}, Size {size}, Sort: {sortInfo}";
});

// Array parameter: /tags?tag=c%23&tag=dotnet
app.MapGet("/tags", (string[] tag) => $"Tags: {string.Join(", ", tag)}");
```

## JSON in ASP.NET Core

### Automatic JSON Serialization
```csharp
// Objects are automatically serialized to JSON
app.MapGet("/user", () => new User { Id = 1, Name = "Alice" });

// Response:
// Content-Type: application/json
// {"id":1,"name":"Alice"}
```

### JSON Deserialization (Request Body)
```csharp
app.MapPost("/users", (User user) =>
{
    // user is automatically deserialized from JSON body
    return Results.Created($"/users/{user.Id}", user);
});

// Request body:
// {"id":1,"name":"Alice","email":"alice@test.com"}
```

### Configuring JSON Options
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Use PascalCase (default is camelCase)
    options.SerializerOptions.PropertyNamingPolicy = null;

    // Include null values
    options.SerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.Never;

    // Pretty print
    options.SerializerOptions.WriteIndented = true;

    // Handle enums as strings
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
```

### System.Text.Json Attributes
```csharp
public class Product
{
    [JsonPropertyName("product_id")]
    public int Id { get; set; }

    [JsonPropertyName("product_name")]
    public string Name { get; set; }

    [JsonIgnore]
    public string InternalCode { get; set; }

    [JsonPropertyOrder(1)]
    public decimal Price { get; set; }
}

// JSON output:
// {"product_id":1,"price":99.99,"product_name":"Laptop"}
```

### Manual JSON Serialization
```csharp
using System.Text.Json;

app.MapGet("/manual-json", () =>
{
    var data = new { message = "Hello", count = 42 };
    var json = JsonSerializer.Serialize(data);
    return Results.Content(json, "application/json");
});

app.MapPost("/parse-json", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    var json = await reader.ReadToEndAsync();
    var data = JsonSerializer.Deserialize<MyClass>(json);
    return Results.Ok(data);
});
```

## Complete Routing Example

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>
{
    new(1, "Laptop", "Electronics", 999.99m),
    new(2, "Shirt", "Clothing", 29.99m),
    new(3, "Phone", "Electronics", 599.99m)
};

// GET all with optional filtering
app.MapGet("/api/products", (string? category, decimal? minPrice, decimal? maxPrice) =>
{
    var result = products.AsQueryable();

    if (category is not null)
        result = result.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

    if (minPrice.HasValue)
        result = result.Where(p => p.Price >= minPrice.Value);

    if (maxPrice.HasValue)
        result = result.Where(p => p.Price <= maxPrice.Value);

    return Results.Ok(result);
});

// GET by ID with constraint
app.MapGet("/api/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null
        ? Results.Ok(product)
        : Results.NotFound(new { error = "Product not found", id });
});

// GET by category name
app.MapGet("/api/products/category/{name:alpha}", (string name) =>
{
    var result = products.Where(p =>
        p.Category.Equals(name, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(result);
});

// POST - create product (JSON body)
app.MapPost("/api/products", (ProductDto dto) =>
{
    var newId = products.Max(p => p.Id) + 1;
    var product = new Product(newId, dto.Name, dto.Category, dto.Price);
    products.Add(product);
    return Results.Created($"/api/products/{newId}", product);
});

// PUT - update product
app.MapPut("/api/products/{id:int}", (int id, ProductDto dto) =>
{
    var index = products.FindIndex(p => p.Id == id);
    if (index == -1)
        return Results.NotFound();

    products[index] = new Product(id, dto.Name, dto.Category, dto.Price);
    return Results.Ok(products[index]);
});

// DELETE
app.MapDelete("/api/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null)
        return Results.NotFound();

    products.Remove(product);
    return Results.NoContent();
});

app.Run();

record Product(int Id, string Name, string Category, decimal Price);
record ProductDto(string Name, string Category, decimal Price);
```

## Route Groups

```csharp
var api = app.MapGroup("/api");
var v1 = api.MapGroup("/v1");
var products = v1.MapGroup("/products");

products.MapGet("/", () => "List products");           // /api/v1/products
products.MapGet("/{id:int}", (int id) => $"Get {id}"); // /api/v1/products/5
products.MapPost("/", (Product p) => "Created");       // /api/v1/products
```

## Best Practices

1. **Use meaningful route patterns**: `/api/customers/{id}/orders`
2. **Apply appropriate constraints**: `{id:int}`, `{name:alpha}`
3. **Use consistent JSON naming**: Configure once, apply everywhere
4. **Validate route parameters**: Check constraints and handle errors
5. **Document your routes**: Use OpenAPI/Swagger

## Summary

- Routes map URLs to handler methods
- Route parameters: `{id}`, `{name:alpha}`, `{id:int:min(1)}`
- Query strings for filtering: `?page=1&sort=name`
- JSON is automatically serialized/deserialized
- Configure JSON options globally
- Use route groups for organization
- Apply constraints for type safety

Routing and JSON handling are essential for building clean, functional APIs.
