# Topic 36: Logging with Serilog - Assessment Questions

## Level 1 (Trivial)

**Question:** Convert this string concatenation log to structured logging.

```csharp
_logger.LogInformation("User " + username + " logged in at " + DateTime.Now);
```

**Expected Solution:**
```csharp
_logger.LogInformation("User {Username} logged in at {LoginTime}", username, DateTime.Now);
```

---

## Level 2 (Trivial)

**Question:** What are the six log levels in Serilog from least to most severe?

**Expected Solution:**
1. **Verbose** - Most detailed, typically only enabled during development
2. **Debug** - Debugging information useful during development
3. **Information** - Normal operation, tracking general flow
4. **Warning** - Unexpected events that don't stop the application
5. **Error** - Errors and exceptions that need attention
6. **Fatal** - Critical failures that cause the application to crash

---

## Level 3 (Easy)

**Question:** Configure Serilog to write to both console and a daily rolling file.

**Expected Solution:**
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
```

---

## Level 4 (Easy)

**Question:** Add logging to this service method with appropriate log levels.

```csharp
public class OrderService
{
    public Order? GetOrder(int orderId)
    {
        var order = _repository.GetById(orderId);
        if (order == null)
            return null;
        return order;
    }
}
```

**Expected Solution:**
```csharp
public class OrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IOrderRepository _repository;

    public OrderService(ILogger<OrderService> logger, IOrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public Order? GetOrder(int orderId)
    {
        _logger.LogDebug("Getting order {OrderId}", orderId);

        try
        {
            var order = _repository.GetById(orderId);

            if (order == null)
            {
                _logger.LogWarning("Order {OrderId} not found", orderId);
                return null;
            }

            _logger.LogInformation("Retrieved order {OrderId} for customer {CustomerId}",
                orderId, order.CustomerId);

            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order {OrderId}", orderId);
            throw;
        }
    }
}
```

---

## Level 5 (Moderate)

**Question:** Configure Serilog from appsettings.json with different minimum levels for different namespaces.

**Expected Solution:**
```json
// appsettings.json
{
  "Serilog": {
    "Using": ["Serilog.Sinks.Console", "Serilog.Sinks.File"],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning",
        "System": "Warning",
        "MyApp.Services": "Debug"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/app-.log",
          "rollingInterval": "Day"
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName"]
  }
}
```

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
```

---

## Level 6 (Moderate)

**Question:** Use LogContext to add properties to a scope of related log entries.

**Expected Solution:**
```csharp
public class OrderProcessingService
{
    private readonly ILogger<OrderProcessingService> _logger;

    public async Task ProcessOrderAsync(Order order)
    {
        using (LogContext.PushProperty("OrderId", order.Id))
        using (LogContext.PushProperty("CustomerId", order.CustomerId))
        using (LogContext.PushProperty("CorrelationId", Guid.NewGuid()))
        {
            _logger.LogInformation("Starting order processing");

            try
            {
                await ValidateOrderAsync(order);
                _logger.LogDebug("Order validation passed");

                await ReserveInventoryAsync(order);
                _logger.LogDebug("Inventory reserved");

                await ProcessPaymentAsync(order);
                _logger.LogDebug("Payment processed");

                await SendConfirmationAsync(order);
                _logger.LogInformation("Order processing completed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order processing failed");
                throw;
            }
        }
    }
}
```

---

## Level 7 (Challenging)

**Question:** Create a logging middleware that logs request/response details with timing.

**Expected Solution:**
```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
            ?? Guid.NewGuid().ToString();

        using (LogContext.PushProperty("CorrelationId", correlationId))
        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        using (LogContext.PushProperty("RequestMethod", context.Request.Method))
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Request started: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            try
            {
                context.Response.Headers.Add("X-Correlation-ID", correlationId);
                await _next(context);

                stopwatch.Stop();

                _logger.LogInformation(
                    "Request completed: {Method} {Path} - {StatusCode} in {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex,
                    "Request failed: {Method} {Path} after {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}

// Extension method
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
```

---

## Level 8 (Challenging)

**Question:** Implement a custom enricher that adds user information to all log entries.

**Expected Solution:**
```csharp
public class UserEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var role = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId != null)
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", userId));

            if (username != null)
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Username", username));

            if (role != null)
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserRole", role));
        }
        else
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", "Anonymous"));
        }

        // Add request information
        if (httpContext != null)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ClientIP", httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown"));

            var userAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault();
            if (userAgent != null)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserAgent", userAgent));
            }
        }
    }
}

// Registration
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<UserEnricher>();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.With(services.GetRequiredService<UserEnricher>());
});
```

---

## Level 9 (Expert)

**Question:** Implement a complete logging strategy with:
- Structured logging for all services
- Request/response logging with sensitive data masking
- Performance logging for slow operations
- Error aggregation
- Different sinks for different environments

**Expected Solution:**
```csharp
// Sensitive data masking
public class SensitiveDataMasker
{
    private static readonly string[] SensitiveFields =
        { "password", "creditcard", "ssn", "token", "secret", "apikey" };

    public static object MaskSensitiveData(object data)
    {
        if (data is string str)
            return "***MASKED***";

        if (data is IDictionary<string, object> dict)
        {
            var masked = new Dictionary<string, object>();
            foreach (var kvp in dict)
            {
                if (SensitiveFields.Any(f =>
                    kvp.Key.Contains(f, StringComparison.OrdinalIgnoreCase)))
                {
                    masked[kvp.Key] = "***MASKED***";
                }
                else
                {
                    masked[kvp.Key] = MaskSensitiveData(kvp.Value);
                }
            }
            return masked;
        }

        return data;
    }
}

// Performance logging decorator
public class PerformanceLoggingDecorator<T> : DispatchProxy
{
    private T _target;
    private ILogger _logger;
    private readonly TimeSpan _slowThreshold = TimeSpan.FromSeconds(1);

    public static T Create(T target, ILogger logger)
    {
        var proxy = Create<T, PerformanceLoggingDecorator<T>>() as PerformanceLoggingDecorator<T>;
        proxy._target = target;
        proxy._logger = logger;
        return (T)(object)proxy;
    }

    protected override object Invoke(MethodInfo method, object[] args)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var result = method.Invoke(_target, args);

            if (result is Task task)
            {
                return WrapTask(task, method, stopwatch);
            }

            LogPerformance(method, stopwatch.Elapsed);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Method {Method} failed after {Elapsed}ms",
                method.Name, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }

    private void LogPerformance(MethodInfo method, TimeSpan elapsed)
    {
        if (elapsed > _slowThreshold)
        {
            _logger.LogWarning(
                "Slow operation: {Method} took {Elapsed}ms",
                method.Name, elapsed.TotalMilliseconds);
        }
        else
        {
            _logger.LogDebug(
                "Method {Method} completed in {Elapsed}ms",
                method.Name, elapsed.TotalMilliseconds);
        }
    }
}

// Complete Program.cs setup
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("Application", "MyApi")
    .Enrich.WithProperty("Version", Assembly.GetExecutingAssembly().GetName().Version?.ToString())
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}",
        restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/errors-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 90,
        restrictedToMinimumLevel: LogEventLevel.Error)
    .WriteTo.Conditional(
        _ => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production",
        wt => wt.Seq("http://seq-server:5341"))
    .CreateLogger();

try
{
    Log.Information("Starting application {Version} in {Environment}",
        Assembly.GetExecutingAssembly().GetName().Version,
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Services
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    var app = builder.Build();

    // Middleware
    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());

            if (httpContext.User.Identity?.IsAuthenticated == true)
            {
                diagnosticContext.Set("UserId", httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        };
    });

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

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Structured logging basics, log levels |
| 3-4 | Multiple sinks, service logging |
| 5-6 | Configuration, LogContext scopes |
| 7-8 | Middleware, custom enrichers |
| 9 | Complete logging strategy |
