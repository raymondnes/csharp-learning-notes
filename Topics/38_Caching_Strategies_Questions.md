# Topic 38: Caching Strategies - Assessment Questions

## Level 1 (Trivial)

**Question:** What is the difference between absolute and sliding expiration?

**Expected Solution:**
- **Absolute Expiration**: Cache entry expires after a fixed time regardless of access. Example: "Expire in 10 minutes from now."

- **Sliding Expiration**: Cache entry expires if not accessed within the time window. Each access resets the timer. Example: "Expire if not accessed for 2 minutes."

```csharp
// Absolute: Always expires at 10:10 if set at 10:00
.SetAbsoluteExpiration(TimeSpan.FromMinutes(10))

// Sliding: Resets expiration each time item is accessed
.SetSlidingExpiration(TimeSpan.FromMinutes(2))
```

---

## Level 2 (Trivial)

**Question:** Add basic in-memory caching to this method.

```csharp
public class ProductService
{
    private readonly IProductRepository _repository;

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
```

**Expected Solution:**
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

        if (_cache.TryGetValue(cacheKey, out Product? product))
        {
            return product;
        }

        product = await _repository.GetByIdAsync(id);

        if (product != null)
        {
            _cache.Set(cacheKey, product, TimeSpan.FromMinutes(10));
        }

        return product;
    }
}
```

---

## Level 3 (Easy)

**Question:** Use the GetOrCreateAsync pattern to simplify caching.

**Expected Solution:**
```csharp
public async Task<Product?> GetProductAsync(int id)
{
    return await _cache.GetOrCreateAsync($"product_{id}", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
        entry.SlidingExpiration = TimeSpan.FromMinutes(2);
        entry.Priority = CacheItemPriority.High;

        return await _repository.GetByIdAsync(id);
    });
}
```

---

## Level 4 (Easy)

**Question:** Configure Redis distributed caching and implement a simple get/set.

**Expected Solution:**
```csharp
// Program.cs
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyApp_";
});

// Service
public class ProductService
{
    private readonly IDistributedCache _cache;
    private readonly IProductRepository _repository;

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"product_{id}";

        var cachedJson = await _cache.GetStringAsync(cacheKey);
        if (cachedJson != null)
        {
            return JsonSerializer.Deserialize<Product>(cachedJson);
        }

        var product = await _repository.GetByIdAsync(id);

        if (product != null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(product),
                options);
        }

        return product;
    }
}
```

---

## Level 5 (Moderate)

**Question:** Create a generic cache service interface and Redis implementation.

**Expected Solution:**
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
    private readonly ILogger<RedisCacheService> _logger;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

    public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var data = await _cache.GetStringAsync(key);
            if (data == null)
            {
                _logger.LogDebug("Cache miss for key: {Key}", key);
                return default;
            }

            _logger.LogDebug("Cache hit for key: {Key}", key);
            return JsonSerializer.Deserialize<T>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cache key: {Key}", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? _defaultExpiration
            };

            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options);
            _logger.LogDebug("Cached key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting cache key: {Key}", key);
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _cache.RemoveAsync(key);
            _logger.LogDebug("Removed cache key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing cache key: {Key}", key);
        }
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var cached = await GetAsync<T>(key);
        if (cached != null)
            return cached;

        var value = await factory();
        if (value != null)
        {
            await SetAsync(key, value, expiration);
        }
        return value;
    }
}
```

---

## Level 6 (Moderate)

**Question:** Implement cache invalidation when data is updated.

**Expected Solution:**
```csharp
public class ProductService
{
    private readonly ICacheService _cache;
    private readonly IProductRepository _repository;

    private string ProductKey(int id) => $"products:{id}";
    private string CategoryProductsKey(int categoryId) => $"products:category:{categoryId}";
    private const string AllProductsKey = "products:all";

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _cache.GetOrCreateAsync(
            ProductKey(id),
            () => _repository.GetByIdAsync(id),
            TimeSpan.FromMinutes(15));
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _cache.GetOrCreateAsync(
            CategoryProductsKey(categoryId),
            () => _repository.GetByCategoryAsync(categoryId),
            TimeSpan.FromMinutes(10));
    }

    public async Task<Product> CreateProductAsync(CreateProductDto dto)
    {
        var product = await _repository.CreateAsync(dto);

        // Invalidate related caches
        await InvalidateProductCachesAsync(product);

        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var product = await _repository.UpdateAsync(id, dto);

        // Invalidate caches
        await InvalidateProductCachesAsync(product);

        return product;
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return;

        await _repository.DeleteAsync(id);
        await InvalidateProductCachesAsync(product);
    }

    private async Task InvalidateProductCachesAsync(Product product)
    {
        var tasks = new List<Task>
        {
            _cache.RemoveAsync(ProductKey(product.Id)),
            _cache.RemoveAsync(CategoryProductsKey(product.CategoryId)),
            _cache.RemoveAsync(AllProductsKey)
        };

        await Task.WhenAll(tasks);
    }
}
```

---

## Level 7 (Challenging)

**Question:** Implement cache stampede prevention using locking.

**Expected Solution:**
```csharp
public class StampedeProtectedCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null)
    {
        // First, try to get from cache without lock
        var cached = await GetAsync<T>(key);
        if (cached != null)
            return cached;

        // Get or create a lock for this specific key
        var lockObj = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        await lockObj.WaitAsync();
        try
        {
            // Double-check after acquiring lock
            cached = await GetAsync<T>(key);
            if (cached != null)
                return cached;

            // Only one thread reaches here
            var value = await factory();

            if (value != null)
            {
                await SetAsync(key, value, expiration);
            }

            return value;
        }
        finally
        {
            lockObj.Release();

            // Clean up lock if no one is waiting
            if (lockObj.CurrentCount == 1)
            {
                _locks.TryRemove(key, out _);
            }
        }
    }

    // ... other interface methods
}
```

---

## Level 8 (Challenging)

**Question:** Implement a multi-layer cache (L1 in-memory, L2 distributed).

**Expected Solution:**
```csharp
public class MultiLayerCacheService : ICacheService
{
    private readonly IMemoryCache _l1Cache;
    private readonly IDistributedCache _l2Cache;
    private readonly ILogger<MultiLayerCacheService> _logger;

    private readonly TimeSpan _l1Expiration = TimeSpan.FromMinutes(5);
    private readonly TimeSpan _l2Expiration = TimeSpan.FromMinutes(30);

    public MultiLayerCacheService(
        IMemoryCache l1Cache,
        IDistributedCache l2Cache,
        ILogger<MultiLayerCacheService> logger)
    {
        _l1Cache = l1Cache;
        _l2Cache = l2Cache;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        // Try L1 (in-memory) first
        if (_l1Cache.TryGetValue(key, out T? value))
        {
            _logger.LogDebug("L1 cache hit: {Key}", key);
            return value;
        }

        // Try L2 (distributed)
        var json = await _l2Cache.GetStringAsync(key);
        if (json != null)
        {
            _logger.LogDebug("L2 cache hit: {Key}", key);
            value = JsonSerializer.Deserialize<T>(json);

            // Populate L1 cache
            _l1Cache.Set(key, value, _l1Expiration);

            return value;
        }

        _logger.LogDebug("Cache miss: {Key}", key);
        return default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        // Set in L1
        _l1Cache.Set(key, value, expiration ?? _l1Expiration);

        // Set in L2
        var json = JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? _l2Expiration
        };
        await _l2Cache.SetStringAsync(key, json, options);

        _logger.LogDebug("Set cache: {Key}", key);
    }

    public async Task RemoveAsync(string key)
    {
        _l1Cache.Remove(key);
        await _l2Cache.RemoveAsync(key);
        _logger.LogDebug("Removed cache: {Key}", key);
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null)
    {
        var value = await GetAsync<T>(key);
        if (value != null)
            return value;

        value = await factory();
        if (value != null)
        {
            await SetAsync(key, value, expiration);
        }

        return value;
    }
}
```

---

## Level 9 (Expert)

**Question:** Implement a complete caching solution with:
- Multi-layer caching
- Tag-based invalidation
- Cache statistics/monitoring
- Automatic refresh before expiration
- Circuit breaker for cache failures

**Expected Solution:**
```csharp
// See comprehensive implementation in the topic file
// Key components:

public interface IAdvancedCacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, CacheOptions options);
    Task RemoveAsync(string key);
    Task InvalidateByTagAsync(string tag);
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, CacheOptions options);
    CacheStatistics GetStatistics();
}

public class CacheOptions
{
    public TimeSpan? AbsoluteExpiration { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();
    public bool RefreshBeforeExpiration { get; set; }
    public TimeSpan? RefreshWindow { get; set; }
}

public class CacheStatistics
{
    public long Hits { get; set; }
    public long Misses { get; set; }
    public double HitRate => (double)Hits / (Hits + Misses);
    public long L1Hits { get; set; }
    public long L2Hits { get; set; }
    public long Errors { get; set; }
}

// Implementation with all features including circuit breaker pattern
// for resilience when Redis is unavailable
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Basic concepts, simple caching |
| 3-4 | GetOrCreate, distributed cache |
| 5-6 | Generic service, invalidation |
| 7-8 | Stampede prevention, multi-layer |
| 9 | Enterprise caching solution |
