# Topic 38: Caching Strategies

## Introduction

Caching stores frequently accessed data in fast storage to reduce database load, decrease latency, and improve scalability. It's essential for high-performance applications.

## Types of Caching

| Type | Location | Use Case |
|------|----------|----------|
| **In-Memory** | Application memory | Single server, fast access |
| **Distributed** | Redis, Memcached | Multiple servers, shared cache |
| **Response** | HTTP level | API responses, static content |
| **Output** | Server-side | Rendered pages/views |

## In-Memory Caching

### Setup

```csharp
// Program.cs
builder.Services.AddMemoryCache();
```

### Basic Usage

```csharp
public class ProductService
{
    private readonly IMemoryCache _cache;
    private readonly IProductRepository _repository;

    public ProductService(IMemoryCache cache, IProductRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"product_{id}";

        // Try to get from cache
        if (_cache.TryGetValue(cacheKey, out Product? product))
        {
            return product;
        }

        // Not in cache, get from database
        product = await _repository.GetByIdAsync(id);

        if (product != null)
        {
            // Store in cache with expiration
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            _cache.Set(cacheKey, product, cacheOptions);
        }

        return product;
    }
}
```

### GetOrCreate Pattern

```csharp
public async Task<Product?> GetProductAsync(int id)
{
    return await _cache.GetOrCreateAsync($"product_{id}", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
        entry.SlidingExpiration = TimeSpan.FromMinutes(2);

        return await _repository.GetByIdAsync(id);
    });
}
```

### Cache Expiration Options

```csharp
var options = new MemoryCacheEntryOptions()
    // Absolute: Remove after 10 minutes regardless of access
    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))

    // Sliding: Remove if not accessed for 2 minutes
    .SetSlidingExpiration(TimeSpan.FromMinutes(2))

    // Priority: What to evict first when memory is low
    .SetPriority(CacheItemPriority.High)

    // Size: For memory limits
    .SetSize(1)

    // Callback when removed
    .RegisterPostEvictionCallback((key, value, reason, state) =>
    {
        Console.WriteLine($"Cache entry {key} was removed: {reason}");
    });
```

## Distributed Caching with Redis

### Setup

```bash
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

```csharp
// Program.cs
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyApp_";
});
```

### Usage

```csharp
public class ProductService
{
    private readonly IDistributedCache _cache;
    private readonly IProductRepository _repository;

    public ProductService(IDistributedCache cache, IProductRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"product_{id}";

        // Try to get from cache
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<Product>(cachedData);
        }

        // Get from database
        var product = await _repository.GetByIdAsync(id);

        if (product != null)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(product),
                options);
        }

        return product;
    }

    public async Task InvalidateProductCacheAsync(int id)
    {
        await _cache.RemoveAsync($"product_{id}");
    }
}
```

### Generic Cache Service

```csharp
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);
}

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await _cache.GetStringAsync(key);
        return data == null ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(expiration ?? _defaultExpiration);

        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var cached = await GetAsync<T>(key);
        if (cached != null)
            return cached;

        var value = await factory();
        await SetAsync(key, value, expiration);
        return value;
    }
}
```

## Cache-Aside Pattern

The most common caching pattern:

```csharp
public class CacheAsideProductService
{
    private readonly ICacheService _cache;
    private readonly IProductRepository _repository;

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"products:{id}";

        // 1. Check cache
        var product = await _cache.GetAsync<Product>(cacheKey);
        if (product != null)
            return product;

        // 2. Cache miss - load from database
        product = await _repository.GetByIdAsync(id);
        if (product == null)
            return null;

        // 3. Store in cache
        await _cache.SetAsync(cacheKey, product, TimeSpan.FromMinutes(15));

        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        // Update database
        await _repository.UpdateAsync(product);

        // Invalidate cache
        await _cache.RemoveAsync($"products:{product.Id}");
    }
}
```

## Response Caching

### Setup

```csharp
// Program.cs
builder.Services.AddResponseCaching();

var app = builder.Build();
app.UseResponseCaching();
```

### Usage

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // Cache for 60 seconds
    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }

    // Cache with variation by query string
    [HttpGet("search")]
    [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "category", "page" })]
    public async Task<ActionResult<IEnumerable<Product>>> Search(string category, int page = 1)
    {
        return Ok(await _repository.SearchAsync(category, page));
    }

    // No caching
    [HttpGet("{id}")]
    [ResponseCache(NoStore = true)]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        return Ok(await _repository.GetByIdAsync(id));
    }
}
```

## Output Caching (.NET 7+)

```csharp
// Program.cs
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromMinutes(5)));

    options.AddPolicy("Products", builder =>
        builder.Expire(TimeSpan.FromMinutes(10))
               .Tag("products"));
});

var app = builder.Build();
app.UseOutputCache();
```

```csharp
[HttpGet]
[OutputCache(PolicyName = "Products")]
public async Task<ActionResult<IEnumerable<Product>>> GetAll() { }

// Invalidate
[HttpPost]
public async Task<ActionResult<Product>> Create(ProductDto dto)
{
    var product = await _service.CreateAsync(dto);

    // Invalidate all cached responses tagged with "products"
    await _outputCache.EvictByTagAsync("products", default);

    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
}
```

## Cache Invalidation Strategies

### 1. Time-Based (TTL)

```csharp
// Expire after 10 minutes
.SetAbsoluteExpiration(TimeSpan.FromMinutes(10))

// Expire if not accessed for 2 minutes
.SetSlidingExpiration(TimeSpan.FromMinutes(2))
```

### 2. Event-Based

```csharp
public class ProductService
{
    public async Task UpdateProductAsync(Product product)
    {
        await _repository.UpdateAsync(product);

        // Invalidate specific cache
        await _cache.RemoveAsync($"product_{product.Id}");

        // Invalidate related caches
        await _cache.RemoveAsync($"products_category_{product.CategoryId}");
        await _cache.RemoveAsync("products_all");
    }
}
```

### 3. Cache Tags (Redis)

```csharp
public class TaggedCacheService
{
    private readonly IConnectionMultiplexer _redis;

    public async Task SetWithTagsAsync<T>(string key, T value, string[] tags, TimeSpan expiration)
    {
        var db = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(value);

        await db.StringSetAsync(key, json, expiration);

        // Add key to each tag's set
        foreach (var tag in tags)
        {
            await db.SetAddAsync($"tag:{tag}", key);
        }
    }

    public async Task InvalidateByTagAsync(string tag)
    {
        var db = _redis.GetDatabase();
        var keys = await db.SetMembersAsync($"tag:{tag}");

        foreach (var key in keys)
        {
            await db.KeyDeleteAsync(key.ToString());
        }

        await db.KeyDeleteAsync($"tag:{tag}");
    }
}
```

## Caching Best Practices

### 1. Cache Key Design

```csharp
// Good - specific, hierarchical
$"products:{productId}"
$"products:category:{categoryId}:page:{page}"
$"users:{userId}:orders"

// Bad - vague, no structure
$"data_{id}"
$"cache_1"
```

### 2. Null Caching

```csharp
// Cache "not found" results to prevent repeated DB queries
public async Task<Product?> GetProductAsync(int id)
{
    var cacheKey = $"product_{id}";

    if (_cache.TryGetValue(cacheKey, out CacheEntry<Product> entry))
    {
        return entry.Value;  // May be null
    }

    var product = await _repository.GetByIdAsync(id);

    // Cache even null results (with shorter expiration)
    var options = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(product != null
            ? TimeSpan.FromMinutes(10)
            : TimeSpan.FromMinutes(1));

    _cache.Set(cacheKey, new CacheEntry<Product> { Value = product }, options);

    return product;
}
```

### 3. Cache Stampede Prevention

```csharp
public class StampedePreventionCache
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
    {
        var cached = await _cache.GetAsync<T>(key);
        if (cached != null)
            return cached;

        await _semaphore.WaitAsync();
        try
        {
            // Check again after acquiring lock
            cached = await _cache.GetAsync<T>(key);
            if (cached != null)
                return cached;

            var value = await factory();
            await _cache.SetAsync(key, value);
            return value;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
```

## Summary

| Strategy | Use Case |
|----------|----------|
| In-Memory | Single server, frequently accessed data |
| Distributed (Redis) | Multiple servers, shared state |
| Response Caching | HTTP responses, public data |
| Output Caching | Server-rendered content |
| Cache-Aside | Most common pattern |

Effective caching dramatically improves application performance and scalability.
