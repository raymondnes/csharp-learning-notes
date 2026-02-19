# Topic 32: Async/Await Deep Dive - Assessment Questions

## Level 1 (Trivial)

**Question:** Convert this synchronous method to async.

```csharp
public string GetGreeting(string name)
{
    Thread.Sleep(1000);  // Simulate delay
    return $"Hello, {name}!";
}
```

**Expected Solution:**
```csharp
public async Task<string> GetGreetingAsync(string name)
{
    await Task.Delay(1000);  // Non-blocking delay
    return $"Hello, {name}!";
}
```

---

## Level 2 (Trivial)

**Question:** Write a method that calls two async methods sequentially.

```csharp
public async Task<User> GetUserAsync(int id);
public async Task<List<Order>> GetOrdersAsync(int userId);

// Write GetUserWithOrdersAsync that gets user first, then their orders
```

**Expected Solution:**
```csharp
public async Task<(User User, List<Order> Orders)> GetUserWithOrdersAsync(int id)
{
    var user = await GetUserAsync(id);
    var orders = await GetOrdersAsync(user.Id);
    return (user, orders);
}
```

---

## Level 3 (Easy)

**Question:** Modify the previous solution to fetch user and orders in parallel.

**Expected Solution:**
```csharp
public async Task<(User User, List<Order> Orders)> GetUserWithOrdersParallelAsync(int id)
{
    var userTask = GetUserAsync(id);
    var ordersTask = GetOrdersAsync(id);

    await Task.WhenAll(userTask, ordersTask);

    return (await userTask, await ordersTask);
}
```

---

## Level 4 (Easy)

**Question:** Add a timeout to an async operation.

```csharp
public async Task<string> SlowApiCallAsync()
{
    await Task.Delay(10000);  // 10 seconds
    return "Result";
}

// Write a method that times out after 5 seconds
```

**Expected Solution:**
```csharp
public async Task<string> CallWithTimeoutAsync()
{
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

    try
    {
        var task = SlowApiCallAsync();
        var delayTask = Task.Delay(Timeout.Infinite, cts.Token);

        var completed = await Task.WhenAny(task, delayTask);

        if (completed == task)
        {
            return await task;
        }

        throw new TimeoutException("Operation timed out");
    }
    catch (OperationCanceledException)
    {
        throw new TimeoutException("Operation timed out");
    }
}

// Alternative simpler solution
public async Task<string> CallWithTimeoutAsync()
{
    var task = SlowApiCallAsync();

    if (await Task.WhenAny(task, Task.Delay(5000)) == task)
    {
        return await task;
    }

    throw new TimeoutException("Operation timed out after 5 seconds");
}
```

---

## Level 5 (Moderate)

**Question:** Implement a method that processes a list of URLs in parallel with a maximum concurrency limit.

```csharp
public async Task<string> DownloadAsync(string url);

// Process up to 3 URLs concurrently
public async Task<List<string>> DownloadAllAsync(List<string> urls)
{
    // Your implementation
}
```

**Expected Solution:**
```csharp
public async Task<List<string>> DownloadAllAsync(List<string> urls)
{
    var results = new List<string>();
    var semaphore = new SemaphoreSlim(3);  // Max 3 concurrent

    var tasks = urls.Select(async url =>
    {
        await semaphore.WaitAsync();
        try
        {
            return await DownloadAsync(url);
        }
        finally
        {
            semaphore.Release();
        }
    });

    var allResults = await Task.WhenAll(tasks);
    return allResults.ToList();
}
```

---

## Level 6 (Moderate)

**Question:** Implement cancellation support for a long-running operation.

```csharp
public class DataProcessor
{
    private readonly List<string> _dataSources;

    // Process each data source, but allow cancellation
    public async Task<List<ProcessResult>> ProcessAllAsync(CancellationToken ct = default)
    {
        // Your implementation
    }

    private async Task<ProcessResult> ProcessSourceAsync(string source, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        await Task.Delay(1000, ct);  // Simulate work
        return new ProcessResult { Source = source, Success = true };
    }
}

public class ProcessResult
{
    public string Source { get; set; }
    public bool Success { get; set; }
}
```

**Expected Solution:**
```csharp
public async Task<List<ProcessResult>> ProcessAllAsync(CancellationToken ct = default)
{
    var results = new List<ProcessResult>();

    foreach (var source in _dataSources)
    {
        ct.ThrowIfCancellationRequested();

        try
        {
            var result = await ProcessSourceAsync(source, ct);
            results.Add(result);
        }
        catch (OperationCanceledException)
        {
            throw;  // Propagate cancellation
        }
        catch (Exception ex)
        {
            results.Add(new ProcessResult
            {
                Source = source,
                Success = false,
                Error = ex.Message
            });
        }
    }

    return results;
}
```

---

## Level 7 (Challenging)

**Question:** Implement a retry pattern with exponential backoff for async operations.

```csharp
public class RetryPolicy
{
    public int MaxRetries { get; set; } = 3;
    public TimeSpan InitialDelay { get; set; } = TimeSpan.FromSeconds(1);

    public async Task<T> ExecuteAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        CancellationToken ct = default)
    {
        // Implement retry with exponential backoff
        // Delay doubles each retry: 1s, 2s, 4s, ...
    }
}
```

**Expected Solution:**
```csharp
public class RetryPolicy
{
    public int MaxRetries { get; set; } = 3;
    public TimeSpan InitialDelay { get; set; } = TimeSpan.FromSeconds(1);

    public async Task<T> ExecuteAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        CancellationToken ct = default)
    {
        var delay = InitialDelay;
        Exception? lastException = null;

        for (int attempt = 0; attempt <= MaxRetries; attempt++)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                return await operation(ct);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                lastException = ex;

                if (attempt < MaxRetries)
                {
                    await Task.Delay(delay, ct);
                    delay *= 2;  // Exponential backoff
                }
            }
        }

        throw new AggregateException(
            $"Operation failed after {MaxRetries + 1} attempts",
            lastException!
        );
    }
}

// Usage
var policy = new RetryPolicy { MaxRetries = 3, InitialDelay = TimeSpan.FromSeconds(1) };
var result = await policy.ExecuteAsync(async ct =>
{
    return await httpClient.GetStringAsync(url, ct);
});
```

---

## Level 8 (Challenging)

**Question:** Implement an async producer-consumer pattern using channels.

```csharp
// Producer generates items, consumer processes them
// Should support multiple producers and consumers
// Should gracefully handle completion

public class AsyncPipeline<T>
{
    public async Task ProduceAsync(T item);
    public async IAsyncEnumerable<T> ConsumeAsync(CancellationToken ct);
    public void Complete();
}
```

**Expected Solution:**
```csharp
using System.Threading.Channels;

public class AsyncPipeline<T>
{
    private readonly Channel<T> _channel;

    public AsyncPipeline(int capacity = 100)
    {
        _channel = Channel.CreateBounded<T>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false
        });
    }

    public async Task ProduceAsync(T item)
    {
        await _channel.Writer.WriteAsync(item);
    }

    public async IAsyncEnumerable<T> ConsumeAsync(
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var item in _channel.Reader.ReadAllAsync(ct))
        {
            yield return item;
        }
    }

    public void Complete()
    {
        _channel.Writer.Complete();
    }
}

// Usage example
public async Task RunPipelineAsync()
{
    var pipeline = new AsyncPipeline<int>();

    // Start producers
    var producers = Enumerable.Range(0, 3).Select(async i =>
    {
        for (int j = 0; j < 10; j++)
        {
            await pipeline.ProduceAsync(i * 100 + j);
            await Task.Delay(100);
        }
    });

    // Start consumers
    var consumers = Enumerable.Range(0, 2).Select(async i =>
    {
        await foreach (var item in pipeline.ConsumeAsync())
        {
            Console.WriteLine($"Consumer {i}: {item}");
        }
    });

    await Task.WhenAll(producers);
    pipeline.Complete();
    await Task.WhenAll(consumers);
}
```

---

## Level 9 (Expert)

**Question:** Implement a comprehensive async batch processor with the following features:
- Configurable batch size and concurrency
- Graceful shutdown with timeout
- Progress reporting
- Error handling with partial results
- Cancellation support

```csharp
public class BatchProcessorOptions
{
    public int BatchSize { get; set; } = 10;
    public int MaxConcurrency { get; set; } = 3;
    public TimeSpan ShutdownTimeout { get; set; } = TimeSpan.FromSeconds(30);
}

public class BatchResult<T>
{
    public List<T> SuccessfulResults { get; set; }
    public List<BatchError> Errors { get; set; }
    public int TotalProcessed { get; set; }
    public TimeSpan Duration { get; set; }
}

public class BatchError
{
    public int ItemIndex { get; set; }
    public string Error { get; set; }
}

public interface IProgress<T>
{
    void Report(T value);
}

public class BatchProcessor<TInput, TOutput>
{
    public async Task<BatchResult<TOutput>> ProcessAsync(
        IEnumerable<TInput> items,
        Func<TInput, CancellationToken, Task<TOutput>> processor,
        BatchProcessorOptions options,
        IProgress<int>? progress = null,
        CancellationToken ct = default);
}
```

**Expected Solution:**
```csharp
public class BatchProcessor<TInput, TOutput>
{
    public async Task<BatchResult<TOutput>> ProcessAsync(
        IEnumerable<TInput> items,
        Func<TInput, CancellationToken, Task<TOutput>> processor,
        BatchProcessorOptions options,
        IProgress<int>? progress = null,
        CancellationToken ct = default)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var results = new List<TOutput>();
        var errors = new List<BatchError>();
        var processedCount = 0;
        var itemList = items.ToList();

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        var semaphore = new SemaphoreSlim(options.MaxConcurrency);

        try
        {
            // Process in batches
            var batches = itemList
                .Select((item, index) => (item, index))
                .Chunk(options.BatchSize);

            foreach (var batch in batches)
            {
                ct.ThrowIfCancellationRequested();

                var batchTasks = batch.Select(async x =>
                {
                    await semaphore.WaitAsync(ct);
                    try
                    {
                        var result = await ProcessItemWithTimeoutAsync(
                            x.item, x.index, processor, cts.Token);
                        return (x.index, result, error: (string?)null);
                    }
                    catch (Exception ex) when (ex is not OperationCanceledException)
                    {
                        return (x.index, default(TOutput), error: ex.Message);
                    }
                    finally
                    {
                        semaphore.Release();
                        Interlocked.Increment(ref processedCount);
                        progress?.Report(processedCount);
                    }
                });

                var batchResults = await Task.WhenAll(batchTasks);

                foreach (var (index, result, error) in batchResults)
                {
                    if (error != null)
                    {
                        errors.Add(new BatchError { ItemIndex = index, Error = error });
                    }
                    else
                    {
                        results.Add(result!);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Handle graceful shutdown
            await GracefulShutdownAsync(cts, options.ShutdownTimeout);
        }

        stopwatch.Stop();

        return new BatchResult<TOutput>
        {
            SuccessfulResults = results,
            Errors = errors,
            TotalProcessed = processedCount,
            Duration = stopwatch.Elapsed
        };
    }

    private async Task<TOutput> ProcessItemWithTimeoutAsync(
        TInput item,
        int index,
        Func<TInput, CancellationToken, Task<TOutput>> processor,
        CancellationToken ct)
    {
        return await processor(item, ct);
    }

    private async Task GracefulShutdownAsync(
        CancellationTokenSource cts,
        TimeSpan timeout)
    {
        // Wait for in-progress items to complete (with timeout)
        try
        {
            await Task.Delay(timeout, cts.Token);
        }
        catch (OperationCanceledException)
        {
            // Expected when shutdown is triggered
        }
    }
}

// Extension for Chunk (for older .NET versions)
public static class EnumerableExtensions
{
    public static IEnumerable<TSource[]> Chunk<TSource>(
        this IEnumerable<TSource> source, int size)
    {
        return source
            .Select((item, index) => new { item, index })
            .GroupBy(x => x.index / size)
            .Select(g => g.Select(x => x.item).ToArray());
    }
}

// Usage example
public async Task UseBatchProcessorAsync()
{
    var processor = new BatchProcessor<string, int>();

    var items = Enumerable.Range(1, 100).Select(i => $"Item-{i}");

    var options = new BatchProcessorOptions
    {
        BatchSize = 10,
        MaxConcurrency = 5,
        ShutdownTimeout = TimeSpan.FromSeconds(10)
    };

    var progress = new Progress<int>(count =>
        Console.WriteLine($"Processed: {count}"));

    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromMinutes(5));

    var result = await processor.ProcessAsync(
        items,
        async (item, ct) =>
        {
            await Task.Delay(100, ct);  // Simulate work
            if (item.Contains("13"))
                throw new Exception("Unlucky item!");
            return item.Length;
        },
        options,
        progress,
        cts.Token
    );

    Console.WriteLine($"Completed in {result.Duration.TotalSeconds:F2}s");
    Console.WriteLine($"Successful: {result.SuccessfulResults.Count}");
    Console.WriteLine($"Errors: {result.Errors.Count}");

    foreach (var error in result.Errors)
    {
        Console.WriteLine($"  Item {error.ItemIndex}: {error.Error}");
    }
}
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Basic async/await syntax |
| 3-4 | Parallel operations, timeouts |
| 5-6 | Concurrency limits, cancellation |
| 7-8 | Retry patterns, channels |
| 9 | Production-ready batch processing |
