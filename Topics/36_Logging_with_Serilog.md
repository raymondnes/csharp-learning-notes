# Topic 36: Logging with Serilog

## Introduction

Logging is essential for debugging, monitoring, and understanding application behavior in production. Serilog is the most popular structured logging library for .NET, offering powerful features and flexibility.

## Why Serilog?

- **Structured logging**: Log properties, not just strings
- **Multiple sinks**: Console, files, databases, cloud services
- **High performance**: Asynchronous, batched writes
- **Easy configuration**: Code or JSON-based setup
- **Rich ecosystem**: Many output targets (sinks) available

## Getting Started

### Install Packages

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

### Basic Setup

```csharp
// Program.cs
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    var app = builder.Build();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
```

## Log Levels

```csharp
Log.Verbose("Detailed debugging info");    // Most detailed
Log.Debug("Debugging information");        // Development info
Log.Information("Normal operation");       // Routine events
Log.Warning("Something unexpected");       // Potential problems
Log.Error("An error occurred");           // Failures
Log.Fatal("Critical failure");            // App crash imminent
```

## Structured Logging

The key advantage of Serilog is **structured logging**:

```csharp
// Bad - string concatenation (not searchable)
Log.Information("User " + userId + " logged in from " + ipAddress);

// Good - structured properties (searchable, analyzable)
Log.Information("User {UserId} logged in from {IpAddress}", userId, ipAddress);
```

### Why Structured?

```csharp
// This log entry:
Log.Information("Order {OrderId} placed by {CustomerId} for {Total:C}",
    orderId, customerId, total);

// Creates structured data:
// {
//   "Timestamp": "2024-01-15T10:30:00Z",
//   "Level": "Information",
//   "MessageTemplate": "Order {OrderId} placed by {CustomerId} for {Total:C}",
//   "Properties": {
//     "OrderId": 12345,
//     "CustomerId": "CUST001",
//     "Total": 99.99
//   }
// }
```

## Configuration from appsettings.json

```json
{
  "Serilog": {
    "Using": ["Serilog.Sinks.Console", "Serilog.Sinks.File"],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/app-.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 30,
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"]
  }
}
```

```csharp
// Program.cs
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
```

## Common Sinks

### File Sink with Rolling

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 10_000_000,  // 10MB
        rollOnFileSizeLimit: true,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
    )
    .CreateLogger();
```

### Seq (Log Server)

```bash
dotnet add package Serilog.Sinks.Seq
```

```csharp
.WriteTo.Seq("http://localhost:5341")
```

### SQL Server

```bash
dotnet add package Serilog.Sinks.MSSqlServer
```

```csharp
.WriteTo.MSSqlServer(
    connectionString: "Server=...;Database=Logs;",
    tableName: "ApplicationLogs",
    autoCreateSqlTable: true
)
```

## Enrichers

Add context to every log entry:

```bash
dotnet add package Serilog.Enrichers.Environment
dotnet add package Serilog.Enrichers.Thread
```

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "MyApp")
    .Enrich.WithProperty("Environment", "Production")
    .WriteTo.Console()
    .CreateLogger();
```

## Log Context

Add properties to a scope of logs:

```csharp
public class OrderService
{
    private readonly ILogger<OrderService> _logger;

    public async Task ProcessOrderAsync(Order order)
    {
        using (LogContext.PushProperty("OrderId", order.Id))
        using (LogContext.PushProperty("CustomerId", order.CustomerId))
        {
            _logger.LogInformation("Processing order");
            // All logs in this scope include OrderId and CustomerId

            await ValidateOrder(order);
            await ProcessPayment(order);
            await ShipOrder(order);

            _logger.LogInformation("Order processed successfully");
        }
    }
}
```

## Dependency Injection

```csharp
public class ProductService
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public Product GetProduct(int id)
    {
        _logger.LogInformation("Getting product {ProductId}", id);

        try
        {
            var product = _repository.GetById(id);

            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found", id);
                return null;
            }

            _logger.LogDebug("Found product {ProductName}", product.Name);
            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product {ProductId}", id);
            throw;
        }
    }
}
```

## Request Logging Middleware

```csharp
// Program.cs
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";

    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
        diagnosticContext.Set("UserId", httpContext.User?.FindFirst("sub")?.Value);
    };
});
```

## Filtering

Control what gets logged:

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Filter.ByExcluding(e =>
        e.Properties.ContainsKey("RequestPath") &&
        e.Properties["RequestPath"].ToString().Contains("health"))
    .WriteTo.Console()
    .CreateLogger();
```

## Async and Batching

For high-throughput scenarios:

```bash
dotnet add package Serilog.Sinks.Async
```

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(a => a.File("logs/app.log"))
    .WriteTo.Async(a => a.Seq("http://localhost:5341"))
    .CreateLogger();
```

## Best Practices

### 1. Use Structured Properties

```csharp
// Good
_logger.LogInformation("User {UserId} purchased {ProductCount} products for {Total:C}",
    userId, products.Count, total);

// Bad
_logger.LogInformation($"User {userId} purchased {products.Count} products for ${total}");
```

### 2. Include Relevant Context

```csharp
_logger.LogError(exception,
    "Failed to process order {OrderId} for customer {CustomerId}. Items: {@Items}",
    order.Id, order.CustomerId, order.Items);
```

### 3. Use Appropriate Log Levels

```csharp
_logger.LogDebug("Entering method with params: {@Parameters}", parameters);
_logger.LogInformation("Order {OrderId} created successfully", order.Id);
_logger.LogWarning("Retrying operation, attempt {Attempt} of {MaxAttempts}", attempt, maxAttempts);
_logger.LogError(ex, "Payment processing failed for order {OrderId}", orderId);
_logger.LogCritical(ex, "Database connection lost");
```

### 4. Don't Log Sensitive Data

```csharp
// Bad - logs password
_logger.LogInformation("User {Username} login with password {Password}", username, password);

// Good - no sensitive data
_logger.LogInformation("User {Username} login attempt", username);
```

## Complete Example

```csharp
// Program.cs
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Application", "MyApi")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Services
    builder.Services.AddControllers();

    var app = builder.Build();

    // Request logging
    app.UseSerilogRequestLogging();

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
```

## Summary

| Feature | Description |
|---------|-------------|
| Structured Logging | Log data as properties, not strings |
| Sinks | Output destinations (console, file, Seq, etc.) |
| Enrichers | Add context to all log entries |
| LogContext | Scoped properties for related logs |
| Filtering | Control what gets logged |
| Async | Non-blocking, batched logging |

Effective logging is crucial for maintaining production applications. Serilog makes it easy to implement comprehensive, searchable logging.
