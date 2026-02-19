# Topic 32: Async/Await Deep Dive

## Introduction

Modern applications must handle I/O operations (database, files, network) without blocking. Async/await in C# allows writing asynchronous code that looks synchronous, improving scalability and responsiveness.

## Why Async Matters

```csharp
// Synchronous - blocks thread while waiting
public string GetWebPage(string url)
{
    using var client = new HttpClient();
    return client.GetStringAsync(url).Result;  // BLOCKS!
}

// Asynchronous - releases thread while waiting
public async Task<string> GetWebPageAsync(string url)
{
    using var client = new HttpClient();
    return await client.GetStringAsync(url);  // NON-BLOCKING
}
```

**Benefits:**
- **Scalability**: Handle more requests with fewer threads
- **Responsiveness**: UI doesn't freeze
- **Resource efficiency**: Threads aren't wasted waiting

## Task and Task<T>

`Task` represents an asynchronous operation.

```csharp
// Task with no return value
public async Task DoWorkAsync()
{
    await Task.Delay(1000);  // Simulate async work
    Console.WriteLine("Done!");
}

// Task with return value
public async Task<int> CalculateAsync()
{
    await Task.Delay(1000);
    return 42;
}

// Usage
await DoWorkAsync();
int result = await CalculateAsync();
```

## Async Method Anatomy

```csharp
public async Task<User> GetUserAsync(int id)
//     ^^^^^              ^^^^^
//     modifier           return Task<T>
{
    var user = await _repository.FindAsync(id);
    //         ^^^^^
    //         await suspends until complete

    return user;  // Returns User, not Task<User>
}
```

**Rules:**
1. Method marked `async`
2. Returns `Task`, `Task<T>`, or `ValueTask<T>`
3. Contains at least one `await`
4. By convention, ends with `Async`

## How Await Works

```csharp
public async Task ProcessAsync()
{
    Console.WriteLine("1. Before await");

    await Task.Delay(1000);  // Suspends here

    Console.WriteLine("2. After await");  // Continues after delay
}

// Output:
// 1. Before await
// (1 second pause)
// 2. After await
```

When `await` is reached:
1. If task is complete, continues immediately
2. If task is incomplete, method returns to caller
3. When task completes, method resumes from where it left off

## Multiple Awaits

### Sequential (One at a Time)

```csharp
public async Task<(User, Orders)> GetUserDataSequentialAsync(int userId)
{
    var user = await GetUserAsync(userId);        // Wait for user
    var orders = await GetOrdersAsync(userId);    // Then wait for orders
    return (user, orders);
}
// Total time: user time + orders time
```

### Parallel (All at Once)

```csharp
public async Task<(User, Orders)> GetUserDataParallelAsync(int userId)
{
    // Start both tasks
    var userTask = GetUserAsync(userId);
    var ordersTask = GetOrdersAsync(userId);

    // Wait for both to complete
    await Task.WhenAll(userTask, ordersTask);

    return (userTask.Result, ordersTask.Result);
}
// Total time: max(user time, orders time)
```

## Task.WhenAll and Task.WhenAny

### WhenAll - Wait for All

```csharp
public async Task ProcessAllUsersAsync(int[] userIds)
{
    // Create tasks for all users
    var tasks = userIds.Select(id => ProcessUserAsync(id));

    // Wait for all to complete
    await Task.WhenAll(tasks);

    Console.WriteLine("All users processed");
}

// With results
public async Task<User[]> GetUsersAsync(int[] ids)
{
    var tasks = ids.Select(id => GetUserAsync(id));
    return await Task.WhenAll(tasks);
}
```

### WhenAny - Wait for First

```csharp
public async Task<string> GetFastestResponseAsync(string[] urls)
{
    var tasks = urls.Select(url => FetchAsync(url));

    // Returns when ANY task completes
    var firstCompleted = await Task.WhenAny(tasks);

    return await firstCompleted;
}
```

## Cancellation with CancellationToken

Allow users to cancel long-running operations:

```csharp
public async Task<List<Product>> SearchAsync(
    string query,
    CancellationToken cancellationToken = default)
{
    var results = new List<Product>();

    foreach (var source in _dataSources)
    {
        // Check if cancelled before each operation
        cancellationToken.ThrowIfCancellationRequested();

        // Pass token to async operations
        var products = await source.SearchAsync(query, cancellationToken);
        results.AddRange(products);
    }

    return results;
}

// Usage
var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(5));  // Timeout

try
{
    var results = await SearchAsync("shoes", cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Search cancelled");
}
```

## Timeout Pattern

```csharp
public async Task<T> WithTimeoutAsync<T>(
    Task<T> task,
    TimeSpan timeout)
{
    using var cts = new CancellationTokenSource();

    var completedTask = await Task.WhenAny(
        task,
        Task.Delay(timeout, cts.Token)
    );

    if (completedTask == task)
    {
        cts.Cancel();  // Cancel the delay
        return await task;
    }

    throw new TimeoutException();
}

// Usage
try
{
    var result = await WithTimeoutAsync(
        SlowOperationAsync(),
        TimeSpan.FromSeconds(10)
    );
}
catch (TimeoutException)
{
    Console.WriteLine("Operation timed out");
}
```

## ConfigureAwait

Controls which thread resumes after await:

```csharp
// Library code - don't capture context
public async Task<string> FetchDataAsync()
{
    var response = await _client.GetStringAsync(url)
        .ConfigureAwait(false);  // Don't need original context

    return ProcessResponse(response);
}

// UI code - need to update UI on original thread
public async Task UpdateUIAsync()
{
    var data = await FetchDataAsync();  // Default: captures context
    lblResult.Text = data;  // Must run on UI thread
}
```

**Rules:**
- Use `ConfigureAwait(false)` in library code
- Use default in UI/ASP.NET code that needs context

## ValueTask for Performance

`ValueTask<T>` avoids allocation when result is often synchronous:

```csharp
// Regular Task - always allocates
public async Task<int> GetCachedValueAsync(string key)
{
    if (_cache.TryGetValue(key, out var value))
        return value;  // Allocates Task even for cached result

    return await FetchAsync(key);
}

// ValueTask - no allocation for cached result
public async ValueTask<int> GetCachedValueAsync(string key)
{
    if (_cache.TryGetValue(key, out var value))
        return value;  // No allocation!

    return await FetchAsync(key);
}
```

**When to use ValueTask:**
- Method often returns synchronously
- Hot path, performance critical
- Result is awaited only once

## Exception Handling

```csharp
public async Task ProcessAsync()
{
    try
    {
        await RiskyOperationAsync();
    }
    catch (HttpRequestException ex)
    {
        // Handle specific exception
        Console.WriteLine($"HTTP error: {ex.Message}");
    }
    catch (Exception ex)
    {
        // Handle any exception
        Console.WriteLine($"Error: {ex.Message}");
        throw;  // Re-throw if needed
    }
}
```

### Handling Multiple Task Exceptions

```csharp
public async Task ProcessAllAsync()
{
    var tasks = new[]
    {
        Task1Async(),
        Task2Async(),
        Task3Async()
    };

    try
    {
        await Task.WhenAll(tasks);
    }
    catch (Exception)
    {
        // Only catches first exception
        // Check each task for all exceptions
        foreach (var task in tasks)
        {
            if (task.IsFaulted)
            {
                Console.WriteLine($"Task failed: {task.Exception?.InnerException?.Message}");
            }
        }
    }
}
```

## Async Streams (IAsyncEnumerable)

Process items as they arrive:

```csharp
public async IAsyncEnumerable<Product> GetProductsAsync(
    [EnumeratorCancellation] CancellationToken ct = default)
{
    int page = 1;
    while (true)
    {
        var products = await _api.GetPageAsync(page, ct);
        if (!products.Any())
            yield break;

        foreach (var product in products)
        {
            yield return product;
        }

        page++;
    }
}

// Usage - process as they arrive
await foreach (var product in GetProductsAsync())
{
    Console.WriteLine(product.Name);
}
```

## Parallel Processing with SemaphoreSlim

Limit concurrent operations:

```csharp
public async Task ProcessWithLimitAsync(IEnumerable<string> urls, int maxConcurrent = 5)
{
    var semaphore = new SemaphoreSlim(maxConcurrent);

    var tasks = urls.Select(async url =>
    {
        await semaphore.WaitAsync();  // Wait for slot
        try
        {
            return await DownloadAsync(url);
        }
        finally
        {
            semaphore.Release();  // Release slot
        }
    });

    await Task.WhenAll(tasks);
}
```

## Common Mistakes

### 1. Async Void (Avoid!)

```csharp
// BAD - can't be awaited, exceptions crash app
public async void DoSomething()
{
    await Task.Delay(1000);
}

// GOOD - can be awaited, exceptions handled
public async Task DoSomethingAsync()
{
    await Task.Delay(1000);
}
```

Only use `async void` for event handlers:
```csharp
private async void Button_Click(object sender, EventArgs e)
{
    await DoWorkAsync();  // OK in event handlers
}
```

### 2. Blocking on Async (.Result, .Wait())

```csharp
// BAD - can cause deadlocks
public string GetData()
{
    return GetDataAsync().Result;  // BLOCKS!
}

// GOOD - async all the way
public async Task<string> GetDataAsync()
{
    return await FetchAsync();
}
```

### 3. Unnecessary Async

```csharp
// BAD - async overhead for nothing
public async Task<int> GetValueAsync()
{
    return await Task.FromResult(42);  // Unnecessary
}

// GOOD - no async needed
public Task<int> GetValueAsync()
{
    return Task.FromResult(42);
}

// OR if truly synchronous
public int GetValue() => 42;
```

### 4. Not Awaiting Tasks

```csharp
// BAD - fire and forget (exceptions lost)
public void Process()
{
    DoWorkAsync();  // Not awaited!
}

// GOOD - await or store reference
public async Task ProcessAsync()
{
    await DoWorkAsync();
}
```

## Best Practices

```csharp
public class DataService
{
    private readonly HttpClient _client;
    private readonly IMemoryCache _cache;

    // 1. Accept CancellationToken
    public async Task<Data> GetDataAsync(
        string id,
        CancellationToken ct = default)
    {
        // 2. Check cache first (sync path)
        if (_cache.TryGetValue(id, out Data cached))
            return cached;

        // 3. Use ConfigureAwait(false) in library code
        var response = await _client.GetAsync($"/api/data/{id}", ct)
            .ConfigureAwait(false);

        // 4. Check cancellation at logical points
        ct.ThrowIfCancellationRequested();

        var data = await response.Content.ReadFromJsonAsync<Data>(ct)
            .ConfigureAwait(false);

        // 5. Cache result
        _cache.Set(id, data, TimeSpan.FromMinutes(5));

        return data;
    }

    // 6. Parallel when independent
    public async Task<UserProfile> GetProfileAsync(int userId, CancellationToken ct = default)
    {
        var userTask = GetUserAsync(userId, ct);
        var ordersTask = GetOrdersAsync(userId, ct);
        var prefsTask = GetPreferencesAsync(userId, ct);

        await Task.WhenAll(userTask, ordersTask, prefsTask)
            .ConfigureAwait(false);

        return new UserProfile
        {
            User = await userTask,
            Orders = await ordersTask,
            Preferences = await prefsTask
        };
    }
}
```

## Summary

| Concept | Purpose |
|---------|---------|
| `async/await` | Non-blocking asynchronous code |
| `Task<T>` | Represents async operation with result |
| `Task.WhenAll` | Wait for multiple tasks |
| `Task.WhenAny` | Wait for first task |
| `CancellationToken` | Cancel operations |
| `ConfigureAwait(false)` | Don't capture context |
| `ValueTask<T>` | Performance optimization |
| `IAsyncEnumerable<T>` | Async streams |

Mastering async/await is essential for building scalable, responsive applications.
