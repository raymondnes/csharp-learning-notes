# Topic 31: Mocking and Test Doubles - Assessment Questions

## Level 1 (Trivial)

**Question:** Create a simple mock that returns a fixed value.

```csharp
public interface ICalculator
{
    int Add(int a, int b);
}

// Create a mock that returns 10 when Add is called with any arguments
```

**Expected Solution:**
```csharp
[Fact]
public void Mock_ReturnsFixedValue()
{
    var mock = new Mock<ICalculator>();

    mock.Setup(c => c.Add(It.IsAny<int>(), It.IsAny<int>()))
        .Returns(10);

    int result = mock.Object.Add(5, 3);

    Assert.Equal(10, result);
}
```

---

## Level 2 (Trivial)

**Question:** Create a mock that returns different values based on input.

```csharp
public interface IUserRepository
{
    string GetUserName(int id);
}

// Setup mock to return "John" for id=1 and "Jane" for id=2
```

**Expected Solution:**
```csharp
[Fact]
public void Mock_ReturnsDifferentValuesBasedOnInput()
{
    var mock = new Mock<IUserRepository>();

    mock.Setup(r => r.GetUserName(1)).Returns("John");
    mock.Setup(r => r.GetUserName(2)).Returns("Jane");

    Assert.Equal("John", mock.Object.GetUserName(1));
    Assert.Equal("Jane", mock.Object.GetUserName(2));
}
```

---

## Level 3 (Easy)

**Question:** Verify that a mock method was called with specific arguments.

```csharp
public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public class NotificationService
{
    private readonly IEmailService _email;

    public NotificationService(IEmailService email)
    {
        _email = email;
    }

    public void NotifyUser(string email, string message)
    {
        _email.SendEmail(email, "Notification", message);
    }
}

// Test that calling NotifyUser sends email with correct parameters
```

**Expected Solution:**
```csharp
[Fact]
public void NotifyUser_SendsEmailWithCorrectParameters()
{
    var mockEmail = new Mock<IEmailService>();
    var service = new NotificationService(mockEmail.Object);

    service.NotifyUser("test@example.com", "Hello World");

    mockEmail.Verify(e => e.SendEmail(
        "test@example.com",
        "Notification",
        "Hello World"
    ), Times.Once);
}
```

---

## Level 4 (Easy)

**Question:** Setup a mock to throw an exception.

```csharp
public interface IPaymentGateway
{
    bool ProcessPayment(decimal amount);
}

public class CheckoutService
{
    private readonly IPaymentGateway _gateway;

    public CheckoutService(IPaymentGateway gateway)
    {
        _gateway = gateway;
    }

    public string Checkout(decimal amount)
    {
        try
        {
            if (_gateway.ProcessPayment(amount))
                return "Payment successful";
            return "Payment declined";
        }
        catch (Exception)
        {
            return "Payment error";
        }
    }
}

// Test that service handles payment gateway exception gracefully
```

**Expected Solution:**
```csharp
[Fact]
public void Checkout_WhenGatewayThrows_ReturnsError()
{
    var mockGateway = new Mock<IPaymentGateway>();

    mockGateway.Setup(g => g.ProcessPayment(It.IsAny<decimal>()))
        .Throws(new Exception("Gateway unavailable"));

    var service = new CheckoutService(mockGateway.Object);

    string result = service.Checkout(100m);

    Assert.Equal("Payment error", result);
}

[Fact]
public void Checkout_WhenPaymentDeclined_ReturnsDeclined()
{
    var mockGateway = new Mock<IPaymentGateway>();

    mockGateway.Setup(g => g.ProcessPayment(It.IsAny<decimal>()))
        .Returns(false);

    var service = new CheckoutService(mockGateway.Object);

    string result = service.Checkout(100m);

    Assert.Equal("Payment declined", result);
}
```

---

## Level 5 (Moderate)

**Question:** Test a service with multiple dependencies using mocks.

```csharp
public interface IProductRepository
{
    Product? GetById(int id);
    void UpdateStock(int id, int quantity);
}

public interface IOrderRepository
{
    void Save(Order order);
}

public class OrderService
{
    private readonly IProductRepository _products;
    private readonly IOrderRepository _orders;

    public OrderService(IProductRepository products, IOrderRepository orders)
    {
        _products = products;
        _orders = orders;
    }

    public OrderResult CreateOrder(int productId, int quantity)
    {
        var product = _products.GetById(productId);
        if (product == null)
            return new OrderResult(false, "Product not found");

        if (product.Stock < quantity)
            return new OrderResult(false, "Insufficient stock");

        var order = new Order
        {
            ProductId = productId,
            Quantity = quantity,
            Total = product.Price * quantity
        };

        _orders.Save(order);
        _products.UpdateStock(productId, product.Stock - quantity);

        return new OrderResult(true, "Order created");
    }
}

public class Product { public int Id; public decimal Price; public int Stock; }
public class Order { public int ProductId; public int Quantity; public decimal Total; }
public record OrderResult(bool Success, string Message);

// Write tests for all scenarios
```

**Expected Solution:**
```csharp
public class OrderServiceTests
{
    private readonly Mock<IProductRepository> _mockProducts;
    private readonly Mock<IOrderRepository> _mockOrders;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _mockProducts = new Mock<IProductRepository>();
        _mockOrders = new Mock<IOrderRepository>();
        _service = new OrderService(_mockProducts.Object, _mockOrders.Object);
    }

    [Fact]
    public void CreateOrder_ProductNotFound_ReturnsFailure()
    {
        _mockProducts.Setup(p => p.GetById(1)).Returns((Product?)null);

        var result = _service.CreateOrder(1, 2);

        Assert.False(result.Success);
        Assert.Contains("not found", result.Message.ToLower());
        _mockOrders.Verify(o => o.Save(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public void CreateOrder_InsufficientStock_ReturnsFailure()
    {
        _mockProducts.Setup(p => p.GetById(1))
            .Returns(new Product { Id = 1, Price = 10m, Stock = 5 });

        var result = _service.CreateOrder(1, 10);

        Assert.False(result.Success);
        Assert.Contains("stock", result.Message.ToLower());
    }

    [Fact]
    public void CreateOrder_Valid_SavesOrder()
    {
        _mockProducts.Setup(p => p.GetById(1))
            .Returns(new Product { Id = 1, Price = 10m, Stock = 100 });

        var result = _service.CreateOrder(1, 5);

        Assert.True(result.Success);
        _mockOrders.Verify(o => o.Save(It.Is<Order>(order =>
            order.ProductId == 1 &&
            order.Quantity == 5 &&
            order.Total == 50m
        )), Times.Once);
    }

    [Fact]
    public void CreateOrder_Valid_UpdatesStock()
    {
        _mockProducts.Setup(p => p.GetById(1))
            .Returns(new Product { Id = 1, Price = 10m, Stock = 100 });

        _service.CreateOrder(1, 5);

        _mockProducts.Verify(p => p.UpdateStock(1, 95), Times.Once);
    }
}
```

---

## Level 6 (Moderate)

**Question:** Test async methods with mocks.

```csharp
public interface IWeatherApi
{
    Task<WeatherData?> GetWeatherAsync(string city);
}

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan expiry);
}

public class WeatherService
{
    private readonly IWeatherApi _api;
    private readonly ICacheService _cache;
    private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(30);

    public WeatherService(IWeatherApi api, ICacheService cache)
    {
        _api = api;
        _cache = cache;
    }

    public async Task<WeatherData?> GetWeatherAsync(string city)
    {
        var cacheKey = $"weather:{city.ToLower()}";

        // Try cache first
        var cached = await _cache.GetAsync<WeatherData>(cacheKey);
        if (cached != null)
            return cached;

        // Fetch from API
        var weather = await _api.GetWeatherAsync(city);
        if (weather != null)
        {
            await _cache.SetAsync(cacheKey, weather, _cacheExpiry);
        }

        return weather;
    }
}

public class WeatherData { public string City; public double Temperature; }

// Test cache hit, cache miss, and API failure scenarios
```

**Expected Solution:**
```csharp
public class WeatherServiceTests
{
    private readonly Mock<IWeatherApi> _mockApi;
    private readonly Mock<ICacheService> _mockCache;
    private readonly WeatherService _service;

    public WeatherServiceTests()
    {
        _mockApi = new Mock<IWeatherApi>();
        _mockCache = new Mock<ICacheService>();
        _service = new WeatherService(_mockApi.Object, _mockCache.Object);
    }

    [Fact]
    public async Task GetWeatherAsync_CacheHit_ReturnsFromCache()
    {
        var cachedData = new WeatherData { City = "London", Temperature = 15.5 };
        _mockCache.Setup(c => c.GetAsync<WeatherData>("weather:london"))
            .ReturnsAsync(cachedData);

        var result = await _service.GetWeatherAsync("London");

        Assert.Equal(cachedData, result);
        _mockApi.Verify(a => a.GetWeatherAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task GetWeatherAsync_CacheMiss_FetchesFromApi()
    {
        var apiData = new WeatherData { City = "Paris", Temperature = 20.0 };

        _mockCache.Setup(c => c.GetAsync<WeatherData>("weather:paris"))
            .ReturnsAsync((WeatherData?)null);

        _mockApi.Setup(a => a.GetWeatherAsync("Paris"))
            .ReturnsAsync(apiData);

        var result = await _service.GetWeatherAsync("Paris");

        Assert.Equal(apiData, result);
        _mockApi.Verify(a => a.GetWeatherAsync("Paris"), Times.Once);
    }

    [Fact]
    public async Task GetWeatherAsync_CacheMiss_CachesResult()
    {
        var apiData = new WeatherData { City = "Berlin", Temperature = 18.0 };

        _mockCache.Setup(c => c.GetAsync<WeatherData>("weather:berlin"))
            .ReturnsAsync((WeatherData?)null);

        _mockApi.Setup(a => a.GetWeatherAsync("Berlin"))
            .ReturnsAsync(apiData);

        await _service.GetWeatherAsync("Berlin");

        _mockCache.Verify(c => c.SetAsync(
            "weather:berlin",
            apiData,
            TimeSpan.FromMinutes(30)
        ), Times.Once);
    }

    [Fact]
    public async Task GetWeatherAsync_ApiReturnsNull_DoesNotCache()
    {
        _mockCache.Setup(c => c.GetAsync<WeatherData>("weather:unknown"))
            .ReturnsAsync((WeatherData?)null);

        _mockApi.Setup(a => a.GetWeatherAsync("Unknown"))
            .ReturnsAsync((WeatherData?)null);

        var result = await _service.GetWeatherAsync("Unknown");

        Assert.Null(result);
        _mockCache.Verify(c => c.SetAsync(
            It.IsAny<string>(),
            It.IsAny<WeatherData>(),
            It.IsAny<TimeSpan>()
        ), Times.Never);
    }

    [Fact]
    public async Task GetWeatherAsync_CityIsCaseInsensitive()
    {
        _mockCache.Setup(c => c.GetAsync<WeatherData>("weather:rome"))
            .ReturnsAsync((WeatherData?)null);

        await _service.GetWeatherAsync("ROME");

        _mockCache.Verify(c => c.GetAsync<WeatherData>("weather:rome"), Times.Once);
    }
}
```

---

## Level 7 (Challenging)

**Question:** Use callbacks to capture and verify complex interactions.

```csharp
public interface IAuditLogger
{
    void Log(AuditEntry entry);
}

public class AuditEntry
{
    public string Action { get; set; }
    public string UserId { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
}

public interface IUserRepository
{
    User? GetById(string id);
    void Update(User user);
}

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime LastModified { get; set; }
}

public class UserManagementService
{
    private readonly IUserRepository _users;
    private readonly IAuditLogger _audit;

    public UserManagementService(IUserRepository users, IAuditLogger audit)
    {
        _users = users;
        _audit = audit;
    }

    public bool UpdateUserEmail(string userId, string newEmail, string modifiedBy)
    {
        var user = _users.GetById(userId);
        if (user == null) return false;

        var oldEmail = user.Email;
        user.Email = newEmail;
        user.LastModified = DateTime.UtcNow;

        _users.Update(user);

        _audit.Log(new AuditEntry
        {
            Action = "EmailChanged",
            UserId = modifiedBy,
            Details = $"Changed email for {userId} from {oldEmail} to {newEmail}",
            Timestamp = DateTime.UtcNow
        });

        return true;
    }
}

// Write tests that capture and verify the audit log entries
```

**Expected Solution:**
```csharp
public class UserManagementServiceTests
{
    private readonly Mock<IUserRepository> _mockUsers;
    private readonly Mock<IAuditLogger> _mockAudit;
    private readonly UserManagementService _service;
    private readonly List<AuditEntry> _capturedLogs;

    public UserManagementServiceTests()
    {
        _mockUsers = new Mock<IUserRepository>();
        _mockAudit = new Mock<IAuditLogger>();
        _capturedLogs = new List<AuditEntry>();

        // Capture all audit log calls
        _mockAudit.Setup(a => a.Log(It.IsAny<AuditEntry>()))
            .Callback<AuditEntry>(entry => _capturedLogs.Add(entry));

        _service = new UserManagementService(_mockUsers.Object, _mockAudit.Object);
    }

    [Fact]
    public void UpdateUserEmail_ValidUser_LogsAuditEntry()
    {
        var user = new User { Id = "U001", Name = "John", Email = "old@test.com" };
        _mockUsers.Setup(u => u.GetById("U001")).Returns(user);

        _service.UpdateUserEmail("U001", "new@test.com", "ADMIN001");

        Assert.Single(_capturedLogs);
        var log = _capturedLogs[0];
        Assert.Equal("EmailChanged", log.Action);
        Assert.Equal("ADMIN001", log.UserId);
        Assert.Contains("old@test.com", log.Details);
        Assert.Contains("new@test.com", log.Details);
    }

    [Fact]
    public void UpdateUserEmail_ValidUser_UpdatesUserCorrectly()
    {
        var user = new User { Id = "U001", Name = "John", Email = "old@test.com" };
        User? updatedUser = null;

        _mockUsers.Setup(u => u.GetById("U001")).Returns(user);
        _mockUsers.Setup(u => u.Update(It.IsAny<User>()))
            .Callback<User>(u => updatedUser = u);

        var beforeUpdate = DateTime.UtcNow;
        _service.UpdateUserEmail("U001", "new@test.com", "ADMIN001");

        Assert.NotNull(updatedUser);
        Assert.Equal("new@test.com", updatedUser.Email);
        Assert.True(updatedUser.LastModified >= beforeUpdate);
    }

    [Fact]
    public void UpdateUserEmail_UserNotFound_DoesNotLogAudit()
    {
        _mockUsers.Setup(u => u.GetById("INVALID")).Returns((User?)null);

        var result = _service.UpdateUserEmail("INVALID", "new@test.com", "ADMIN001");

        Assert.False(result);
        Assert.Empty(_capturedLogs);
        _mockUsers.Verify(u => u.Update(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void UpdateUserEmail_AuditContainsCorrectTimestamp()
    {
        var user = new User { Id = "U001", Email = "old@test.com" };
        _mockUsers.Setup(u => u.GetById("U001")).Returns(user);

        var beforeCall = DateTime.UtcNow;
        _service.UpdateUserEmail("U001", "new@test.com", "ADMIN001");
        var afterCall = DateTime.UtcNow;

        var log = _capturedLogs[0];
        Assert.True(log.Timestamp >= beforeCall && log.Timestamp <= afterCall);
    }
}
```

---

## Level 8 (Challenging)

**Question:** Test a workflow that requires specific call sequences.

```csharp
public interface ITransactionManager
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}

public interface IInventoryService
{
    bool ReserveItems(List<OrderItem> items);
    void ReleaseItems(List<OrderItem> items);
}

public interface IPaymentService
{
    PaymentResult Charge(decimal amount, string paymentMethod);
    void Refund(string transactionId);
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string TransactionId { get; set; }
    public string Error { get; set; }
}

public class OrderItem { public string ProductId; public int Quantity; }

public class CheckoutOrchestrator
{
    private readonly ITransactionManager _transaction;
    private readonly IInventoryService _inventory;
    private readonly IPaymentService _payment;

    public CheckoutOrchestrator(
        ITransactionManager transaction,
        IInventoryService inventory,
        IPaymentService payment)
    {
        _transaction = transaction;
        _inventory = inventory;
        _payment = payment;
    }

    public CheckoutResult ProcessCheckout(List<OrderItem> items, decimal total, string paymentMethod)
    {
        _transaction.BeginTransaction();

        try
        {
            // Step 1: Reserve inventory
            if (!_inventory.ReserveItems(items))
            {
                _transaction.Rollback();
                return new CheckoutResult(false, "Items unavailable");
            }

            // Step 2: Process payment
            var paymentResult = _payment.Charge(total, paymentMethod);
            if (!paymentResult.Success)
            {
                _inventory.ReleaseItems(items);
                _transaction.Rollback();
                return new CheckoutResult(false, $"Payment failed: {paymentResult.Error}");
            }

            _transaction.Commit();
            return new CheckoutResult(true, "Order completed", paymentResult.TransactionId);
        }
        catch (Exception ex)
        {
            _transaction.Rollback();
            return new CheckoutResult(false, $"Error: {ex.Message}");
        }
    }
}

public record CheckoutResult(bool Success, string Message, string? TransactionId = null);

// Write tests verifying correct call sequences for success and failure scenarios
```

**Expected Solution:**
```csharp
public class CheckoutOrchestratorTests
{
    private readonly Mock<ITransactionManager> _mockTransaction;
    private readonly Mock<IInventoryService> _mockInventory;
    private readonly Mock<IPaymentService> _mockPayment;
    private readonly CheckoutOrchestrator _orchestrator;
    private readonly MockSequence _sequence;

    public CheckoutOrchestratorTests()
    {
        _mockTransaction = new Mock<ITransactionManager>(MockBehavior.Strict);
        _mockInventory = new Mock<IInventoryService>();
        _mockPayment = new Mock<IPaymentService>();
        _sequence = new MockSequence();

        _orchestrator = new CheckoutOrchestrator(
            _mockTransaction.Object,
            _mockInventory.Object,
            _mockPayment.Object
        );
    }

    private List<OrderItem> CreateItems() =>
        new() { new OrderItem { ProductId = "P1", Quantity = 2 } };

    [Fact]
    public void ProcessCheckout_Success_CallsInCorrectOrder()
    {
        var callOrder = new List<string>();

        _mockTransaction.Setup(t => t.BeginTransaction())
            .Callback(() => callOrder.Add("Begin"));
        _mockTransaction.Setup(t => t.Commit())
            .Callback(() => callOrder.Add("Commit"));

        _mockInventory.Setup(i => i.ReserveItems(It.IsAny<List<OrderItem>>()))
            .Callback(() => callOrder.Add("Reserve"))
            .Returns(true);

        _mockPayment.Setup(p => p.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Callback(() => callOrder.Add("Charge"))
            .Returns(new PaymentResult { Success = true, TransactionId = "TXN123" });

        var result = _orchestrator.ProcessCheckout(CreateItems(), 100m, "card");

        Assert.True(result.Success);
        Assert.Equal(new[] { "Begin", "Reserve", "Charge", "Commit" }, callOrder);
    }

    [Fact]
    public void ProcessCheckout_InventoryFails_RollsBackWithoutPayment()
    {
        var callOrder = new List<string>();

        _mockTransaction.Setup(t => t.BeginTransaction())
            .Callback(() => callOrder.Add("Begin"));
        _mockTransaction.Setup(t => t.Rollback())
            .Callback(() => callOrder.Add("Rollback"));

        _mockInventory.Setup(i => i.ReserveItems(It.IsAny<List<OrderItem>>()))
            .Callback(() => callOrder.Add("Reserve"))
            .Returns(false);

        var result = _orchestrator.ProcessCheckout(CreateItems(), 100m, "card");

        Assert.False(result.Success);
        Assert.Equal(new[] { "Begin", "Reserve", "Rollback" }, callOrder);
        _mockPayment.Verify(p => p.Charge(It.IsAny<decimal>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void ProcessCheckout_PaymentFails_ReleasesInventoryAndRollsBack()
    {
        var callOrder = new List<string>();

        _mockTransaction.Setup(t => t.BeginTransaction())
            .Callback(() => callOrder.Add("Begin"));
        _mockTransaction.Setup(t => t.Rollback())
            .Callback(() => callOrder.Add("Rollback"));

        _mockInventory.Setup(i => i.ReserveItems(It.IsAny<List<OrderItem>>()))
            .Callback(() => callOrder.Add("Reserve"))
            .Returns(true);
        _mockInventory.Setup(i => i.ReleaseItems(It.IsAny<List<OrderItem>>()))
            .Callback(() => callOrder.Add("Release"));

        _mockPayment.Setup(p => p.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Callback(() => callOrder.Add("Charge"))
            .Returns(new PaymentResult { Success = false, Error = "Declined" });

        var result = _orchestrator.ProcessCheckout(CreateItems(), 100m, "card");

        Assert.False(result.Success);
        Assert.Contains("Declined", result.Message);
        Assert.Equal(new[] { "Begin", "Reserve", "Charge", "Release", "Rollback" }, callOrder);
    }

    [Fact]
    public void ProcessCheckout_ExceptionThrown_RollsBack()
    {
        _mockTransaction.Setup(t => t.BeginTransaction());
        _mockTransaction.Setup(t => t.Rollback());

        _mockInventory.Setup(i => i.ReserveItems(It.IsAny<List<OrderItem>>()))
            .Throws(new Exception("Database error"));

        var result = _orchestrator.ProcessCheckout(CreateItems(), 100m, "card");

        Assert.False(result.Success);
        Assert.Contains("Database error", result.Message);
        _mockTransaction.Verify(t => t.Rollback(), Times.Once);
        _mockTransaction.Verify(t => t.Commit(), Times.Never);
    }

    [Fact]
    public void ProcessCheckout_Success_ReturnsTransactionId()
    {
        _mockTransaction.Setup(t => t.BeginTransaction());
        _mockTransaction.Setup(t => t.Commit());

        _mockInventory.Setup(i => i.ReserveItems(It.IsAny<List<OrderItem>>()))
            .Returns(true);

        _mockPayment.Setup(p => p.Charge(100m, "card"))
            .Returns(new PaymentResult { Success = true, TransactionId = "TXN456" });

        var result = _orchestrator.ProcessCheckout(CreateItems(), 100m, "card");

        Assert.Equal("TXN456", result.TransactionId);
    }
}
```

---

## Level 9 (Expert)

**Question:** Create a comprehensive test suite for a complex event-driven notification system.

```csharp
public interface INotificationChannel
{
    string ChannelType { get; }
    Task<bool> SendAsync(string recipient, NotificationMessage message);
}

public interface IUserPreferenceRepository
{
    Task<UserPreferences?> GetByUserIdAsync(string userId);
}

public interface INotificationRepository
{
    Task SaveAsync(NotificationRecord record);
    Task<IEnumerable<NotificationRecord>> GetByUserIdAsync(string userId);
}

public interface ITemplateEngine
{
    string Render(string templateName, Dictionary<string, object> data);
}

public class NotificationMessage
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public NotificationPriority Priority { get; set; }
}

public enum NotificationPriority { Low, Normal, High, Critical }

public class UserPreferences
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool EmailEnabled { get; set; }
    public bool SmsEnabled { get; set; }
    public bool PushEnabled { get; set; }
    public NotificationPriority MinimumPriority { get; set; }
}

public class NotificationRecord
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Channel { get; set; }
    public string Subject { get; set; }
    public bool Success { get; set; }
    public DateTime SentAt { get; set; }
    public string? ErrorMessage { get; set; }
}

public class NotificationService
{
    private readonly IEnumerable<INotificationChannel> _channels;
    private readonly IUserPreferenceRepository _preferences;
    private readonly INotificationRepository _notifications;
    private readonly ITemplateEngine _templates;

    public NotificationService(
        IEnumerable<INotificationChannel> channels,
        IUserPreferenceRepository preferences,
        INotificationRepository notifications,
        ITemplateEngine templates)
    {
        _channels = channels;
        _preferences = preferences;
        _notifications = notifications;
        _templates = templates;
    }

    public async Task<NotificationResult> NotifyUserAsync(
        string userId,
        string templateName,
        Dictionary<string, object> data,
        NotificationPriority priority = NotificationPriority.Normal)
    {
        var prefs = await _preferences.GetByUserIdAsync(userId);
        if (prefs == null)
            return new NotificationResult(false, "User not found");

        if (priority < prefs.MinimumPriority)
            return new NotificationResult(true, "Notification below user threshold");

        var message = new NotificationMessage
        {
            Subject = _templates.Render($"{templateName}_subject", data),
            Body = _templates.Render($"{templateName}_body", data),
            Priority = priority
        };

        var results = new List<ChannelResult>();

        foreach (var channel in _channels)
        {
            var (enabled, recipient) = GetChannelConfig(prefs, channel.ChannelType);
            if (!enabled || string.IsNullOrEmpty(recipient))
                continue;

            try
            {
                var success = await channel.SendAsync(recipient, message);
                results.Add(new ChannelResult(channel.ChannelType, success, null));

                await _notifications.SaveAsync(new NotificationRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    Channel = channel.ChannelType,
                    Subject = message.Subject,
                    Success = success,
                    SentAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                results.Add(new ChannelResult(channel.ChannelType, false, ex.Message));

                await _notifications.SaveAsync(new NotificationRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    Channel = channel.ChannelType,
                    Subject = message.Subject,
                    Success = false,
                    SentAt = DateTime.UtcNow,
                    ErrorMessage = ex.Message
                });
            }
        }

        if (!results.Any())
            return new NotificationResult(false, "No enabled channels");

        var anySuccess = results.Any(r => r.Success);
        return new NotificationResult(anySuccess, anySuccess ? "Sent" : "All channels failed", results);
    }

    private (bool enabled, string? recipient) GetChannelConfig(UserPreferences prefs, string channelType)
    {
        return channelType.ToLower() switch
        {
            "email" => (prefs.EmailEnabled, prefs.Email),
            "sms" => (prefs.SmsEnabled, prefs.Phone),
            "push" => (prefs.PushEnabled, prefs.UserId),
            _ => (false, null)
        };
    }
}

public record NotificationResult(bool Success, string Message, List<ChannelResult>? ChannelResults = null);
public record ChannelResult(string Channel, bool Success, string? Error);

// Write a comprehensive test suite covering all scenarios
```

**Expected Solution:**
```csharp
public class NotificationServiceTests
{
    private readonly Mock<IUserPreferenceRepository> _mockPrefs;
    private readonly Mock<INotificationRepository> _mockNotifications;
    private readonly Mock<ITemplateEngine> _mockTemplates;
    private readonly Mock<INotificationChannel> _mockEmailChannel;
    private readonly Mock<INotificationChannel> _mockSmsChannel;
    private readonly Mock<INotificationChannel> _mockPushChannel;
    private readonly NotificationService _service;
    private readonly List<NotificationRecord> _savedNotifications;

    public NotificationServiceTests()
    {
        _mockPrefs = new Mock<IUserPreferenceRepository>();
        _mockNotifications = new Mock<INotificationRepository>();
        _mockTemplates = new Mock<ITemplateEngine>();

        _mockEmailChannel = new Mock<INotificationChannel>();
        _mockEmailChannel.Setup(c => c.ChannelType).Returns("email");

        _mockSmsChannel = new Mock<INotificationChannel>();
        _mockSmsChannel.Setup(c => c.ChannelType).Returns("sms");

        _mockPushChannel = new Mock<INotificationChannel>();
        _mockPushChannel.Setup(c => c.ChannelType).Returns("push");

        _savedNotifications = new List<NotificationRecord>();
        _mockNotifications.Setup(n => n.SaveAsync(It.IsAny<NotificationRecord>()))
            .Callback<NotificationRecord>(r => _savedNotifications.Add(r))
            .Returns(Task.CompletedTask);

        _mockTemplates.Setup(t => t.Render(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
            .Returns((string name, Dictionary<string, object> _) => $"Rendered: {name}");

        _service = new NotificationService(
            new[] { _mockEmailChannel.Object, _mockSmsChannel.Object, _mockPushChannel.Object },
            _mockPrefs.Object,
            _mockNotifications.Object,
            _mockTemplates.Object
        );
    }

    private UserPreferences CreatePrefs(bool email = true, bool sms = false, bool push = false)
    {
        return new UserPreferences
        {
            UserId = "USER001",
            Email = "user@test.com",
            Phone = "+1234567890",
            EmailEnabled = email,
            SmsEnabled = sms,
            PushEnabled = push,
            MinimumPriority = NotificationPriority.Low
        };
    }

    #region User Preference Tests

    [Fact]
    public async Task NotifyUserAsync_UserNotFound_ReturnsFailure()
    {
        _mockPrefs.Setup(p => p.GetByUserIdAsync("INVALID"))
            .ReturnsAsync((UserPreferences?)null);

        var result = await _service.NotifyUserAsync("INVALID", "welcome", new());

        Assert.False(result.Success);
        Assert.Contains("not found", result.Message.ToLower());
    }

    [Fact]
    public async Task NotifyUserAsync_BelowMinimumPriority_SkipsNotification()
    {
        var prefs = CreatePrefs();
        prefs.MinimumPriority = NotificationPriority.High;
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        var result = await _service.NotifyUserAsync("USER001", "update", new(), NotificationPriority.Normal);

        Assert.True(result.Success);
        Assert.Contains("threshold", result.Message.ToLower());
        _mockEmailChannel.Verify(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()), Times.Never);
    }

    #endregion

    #region Channel Selection Tests

    [Fact]
    public async Task NotifyUserAsync_OnlyEnabledChannelsUsed()
    {
        var prefs = CreatePrefs(email: true, sms: false, push: false);
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()))
            .ReturnsAsync(true);

        await _service.NotifyUserAsync("USER001", "test", new());

        _mockEmailChannel.Verify(c => c.SendAsync("user@test.com", It.IsAny<NotificationMessage>()), Times.Once);
        _mockSmsChannel.Verify(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()), Times.Never);
        _mockPushChannel.Verify(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()), Times.Never);
    }

    [Fact]
    public async Task NotifyUserAsync_MultipleChannelsEnabled_SendsToAll()
    {
        var prefs = CreatePrefs(email: true, sms: true, push: true);
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(true);
        _mockSmsChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(true);
        _mockPushChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(true);

        var result = await _service.NotifyUserAsync("USER001", "test", new());

        Assert.Equal(3, result.ChannelResults?.Count);
        Assert.All(result.ChannelResults!, r => Assert.True(r.Success));
    }

    [Fact]
    public async Task NotifyUserAsync_NoChannelsEnabled_ReturnsFailure()
    {
        var prefs = CreatePrefs(email: false, sms: false, push: false);
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        var result = await _service.NotifyUserAsync("USER001", "test", new());

        Assert.False(result.Success);
        Assert.Contains("no enabled", result.Message.ToLower());
    }

    #endregion

    #region Template Rendering Tests

    [Fact]
    public async Task NotifyUserAsync_RendersCorrectTemplates()
    {
        var prefs = CreatePrefs();
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(true);

        var data = new Dictionary<string, object> { ["name"] = "John" };

        await _service.NotifyUserAsync("USER001", "welcome", data);

        _mockTemplates.Verify(t => t.Render("welcome_subject", data), Times.Once);
        _mockTemplates.Verify(t => t.Render("welcome_body", data), Times.Once);
    }

    [Fact]
    public async Task NotifyUserAsync_MessageContainsRenderedContent()
    {
        var prefs = CreatePrefs();
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        NotificationMessage? capturedMessage = null;
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()))
            .Callback<string, NotificationMessage>((_, m) => capturedMessage = m)
            .ReturnsAsync(true);

        _mockTemplates.Setup(t => t.Render("alert_subject", It.IsAny<Dictionary<string, object>>()))
            .Returns("Alert Subject");
        _mockTemplates.Setup(t => t.Render("alert_body", It.IsAny<Dictionary<string, object>>()))
            .Returns("Alert Body");

        await _service.NotifyUserAsync("USER001", "alert", new());

        Assert.Equal("Alert Subject", capturedMessage?.Subject);
        Assert.Equal("Alert Body", capturedMessage?.Body);
    }

    #endregion

    #region Notification Recording Tests

    [Fact]
    public async Task NotifyUserAsync_Success_RecordsNotification()
    {
        var prefs = CreatePrefs();
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(true);

        await _service.NotifyUserAsync("USER001", "test", new());

        Assert.Single(_savedNotifications);
        var record = _savedNotifications[0];
        Assert.Equal("USER001", record.UserId);
        Assert.Equal("email", record.Channel);
        Assert.True(record.Success);
        Assert.Null(record.ErrorMessage);
    }

    [Fact]
    public async Task NotifyUserAsync_ChannelFails_RecordsFailure()
    {
        var prefs = CreatePrefs();
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(false);

        await _service.NotifyUserAsync("USER001", "test", new());

        Assert.Single(_savedNotifications);
        Assert.False(_savedNotifications[0].Success);
    }

    [Fact]
    public async Task NotifyUserAsync_ChannelThrows_RecordsError()
    {
        var prefs = CreatePrefs();
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()))
            .ThrowsAsync(new Exception("SMTP error"));

        await _service.NotifyUserAsync("USER001", "test", new());

        Assert.Single(_savedNotifications);
        Assert.False(_savedNotifications[0].Success);
        Assert.Equal("SMTP error", _savedNotifications[0].ErrorMessage);
    }

    #endregion

    #region Partial Success Tests

    [Fact]
    public async Task NotifyUserAsync_SomeChannelsFail_ReturnsPartialSuccess()
    {
        var prefs = CreatePrefs(email: true, sms: true, push: false);
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(true);
        _mockSmsChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(false);

        var result = await _service.NotifyUserAsync("USER001", "test", new());

        Assert.True(result.Success); // At least one succeeded
        Assert.Equal(2, result.ChannelResults?.Count);
        Assert.Single(result.ChannelResults!, r => r.Success);
        Assert.Single(result.ChannelResults!, r => !r.Success);
    }

    [Fact]
    public async Task NotifyUserAsync_AllChannelsFail_ReturnsFailure()
    {
        var prefs = CreatePrefs(email: true, sms: true, push: false);
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(false);
        _mockSmsChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>())).ReturnsAsync(false);

        var result = await _service.NotifyUserAsync("USER001", "test", new());

        Assert.False(result.Success);
        Assert.Contains("all channels failed", result.Message.ToLower());
    }

    #endregion

    #region Priority Tests

    [Fact]
    public async Task NotifyUserAsync_SetsPriorityOnMessage()
    {
        var prefs = CreatePrefs();
        _mockPrefs.Setup(p => p.GetByUserIdAsync("USER001")).ReturnsAsync(prefs);

        NotificationMessage? capturedMessage = null;
        _mockEmailChannel.Setup(c => c.SendAsync(It.IsAny<string>(), It.IsAny<NotificationMessage>()))
            .Callback<string, NotificationMessage>((_, m) => capturedMessage = m)
            .ReturnsAsync(true);

        await _service.NotifyUserAsync("USER001", "urgent", new(), NotificationPriority.Critical);

        Assert.Equal(NotificationPriority.Critical, capturedMessage?.Priority);
    }

    #endregion
}
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Basic mock setup with Returns |
| 3-4 | Verify calls, exception testing |
| 5-6 | Multiple mocks, async mocking |
| 7-8 | Callbacks, sequence verification |
| 9 | Complex scenarios, comprehensive coverage |
