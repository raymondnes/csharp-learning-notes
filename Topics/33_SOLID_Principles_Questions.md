# Topic 33: SOLID Principles - Assessment Questions

## Level 1 (Trivial)

**Question:** Identify which SOLID principle is violated and explain why.

```csharp
public class UserService
{
    public void CreateUser(User user) { /* ... */ }
    public void SendWelcomeEmail(User user) { /* ... */ }
    public void LogUserActivity(User user, string activity) { /* ... */ }
    public string GenerateUserReport(User user) { /* ... */ }
}
```

**Expected Solution:**
This violates the **Single Responsibility Principle (SRP)**.

The class has multiple reasons to change:
1. User creation logic changes
2. Email sending logic changes
3. Logging logic changes
4. Report generation changes

Should be split into: `UserService`, `EmailService`, `ActivityLogger`, `UserReportGenerator`

---

## Level 2 (Trivial)

**Question:** Refactor this code to follow the Single Responsibility Principle.

```csharp
public class Invoice
{
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; }

    public void CalculateTotal() { /* calculation */ }
    public void SaveToDatabase() { /* database logic */ }
    public void SendEmail() { /* email logic */ }
    public void PrintInvoice() { /* printing logic */ }
}
```

**Expected Solution:**
```csharp
public class Invoice
{
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; }
}

public class InvoiceCalculator
{
    public decimal CalculateTotal(Invoice invoice) { /* calculation */ }
}

public class InvoiceRepository
{
    public void Save(Invoice invoice) { /* database logic */ }
}

public class InvoiceEmailer
{
    public void Send(Invoice invoice) { /* email logic */ }
}

public class InvoicePrinter
{
    public void Print(Invoice invoice) { /* printing logic */ }
}
```

---

## Level 3 (Easy)

**Question:** Refactor to follow the Open/Closed Principle.

```csharp
public class ShippingCalculator
{
    public decimal CalculateCost(string shippingType, decimal weight)
    {
        if (shippingType == "Standard")
            return weight * 5;
        else if (shippingType == "Express")
            return weight * 10;
        else if (shippingType == "Overnight")
            return weight * 20;
        else
            throw new ArgumentException("Unknown shipping type");
    }
}
```

**Expected Solution:**
```csharp
public interface IShippingStrategy
{
    decimal CalculateCost(decimal weight);
}

public class StandardShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight) => weight * 5;
}

public class ExpressShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight) => weight * 10;
}

public class OvernightShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight) => weight * 20;
}

// Can add new shipping types without modifying existing code
public class InternationalShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight) => weight * 30;
}

public class ShippingCalculator
{
    public decimal CalculateCost(IShippingStrategy strategy, decimal weight)
    {
        return strategy.CalculateCost(weight);
    }
}
```

---

## Level 4 (Easy)

**Question:** Fix this Liskov Substitution Principle violation.

```csharp
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public int GetArea() => Width * Height;
}

public class Square : Rectangle
{
    public override int Width
    {
        get => base.Width;
        set { base.Width = value; base.Height = value; }
    }

    public override int Height
    {
        get => base.Height;
        set { base.Height = value; base.Width = value; }
    }
}

// This code breaks with Square
public void TestRectangle(Rectangle rect)
{
    rect.Width = 5;
    rect.Height = 10;
    Assert.Equal(50, rect.GetArea());  // Fails for Square!
}
```

**Expected Solution:**
```csharp
public interface IShape
{
    int GetArea();
}

public class Rectangle : IShape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public int GetArea() => Width * Height;
}

public class Square : IShape
{
    public int Side { get; set; }

    public int GetArea() => Side * Side;
}

// Now shapes work correctly
public void ProcessShape(IShape shape)
{
    var area = shape.GetArea();  // Works for any shape
}
```

---

## Level 5 (Moderate)

**Question:** Apply the Interface Segregation Principle.

```csharp
public interface IEmployee
{
    void Work();
    void TakeVacation();
    void ReceiveSalary();
    void AttendMeetings();
    void ManageTeam();
    void ConductPerformanceReviews();
    void ApproveExpenses();
}

public class Developer : IEmployee
{
    public void Work() { /* codes */ }
    public void TakeVacation() { /* takes time off */ }
    public void ReceiveSalary() { /* gets paid */ }
    public void AttendMeetings() { /* attends */ }
    public void ManageTeam() => throw new NotSupportedException();
    public void ConductPerformanceReviews() => throw new NotSupportedException();
    public void ApproveExpenses() => throw new NotSupportedException();
}
```

**Expected Solution:**
```csharp
public interface IEmployee
{
    void Work();
    void TakeVacation();
    void ReceiveSalary();
}

public interface IMeetingAttendee
{
    void AttendMeetings();
}

public interface IManager
{
    void ManageTeam();
    void ConductPerformanceReviews();
}

public interface IExpenseApprover
{
    void ApproveExpenses();
}

public class Developer : IEmployee, IMeetingAttendee
{
    public void Work() { /* codes */ }
    public void TakeVacation() { /* takes time off */ }
    public void ReceiveSalary() { /* gets paid */ }
    public void AttendMeetings() { /* attends */ }
}

public class TeamLead : IEmployee, IMeetingAttendee, IManager
{
    public void Work() { /* leads */ }
    public void TakeVacation() { /* takes time off */ }
    public void ReceiveSalary() { /* gets paid */ }
    public void AttendMeetings() { /* attends */ }
    public void ManageTeam() { /* manages */ }
    public void ConductPerformanceReviews() { /* reviews */ }
}

public class Director : IEmployee, IMeetingAttendee, IManager, IExpenseApprover
{
    public void Work() { /* directs */ }
    public void TakeVacation() { /* takes time off */ }
    public void ReceiveSalary() { /* gets paid */ }
    public void AttendMeetings() { /* attends */ }
    public void ManageTeam() { /* manages */ }
    public void ConductPerformanceReviews() { /* reviews */ }
    public void ApproveExpenses() { /* approves */ }
}
```

---

## Level 6 (Moderate)

**Question:** Apply the Dependency Inversion Principle.

```csharp
public class NotificationService
{
    private readonly SmtpClient _smtpClient;
    private readonly TwilioClient _twilioClient;
    private readonly SqlConnection _database;

    public NotificationService()
    {
        _smtpClient = new SmtpClient("smtp.server.com");
        _twilioClient = new TwilioClient("api-key");
        _database = new SqlConnection("connection-string");
    }

    public void SendNotification(int userId, string message)
    {
        // Get user preferences from database
        var preferences = GetUserPreferences(userId);

        if (preferences.EmailEnabled)
            _smtpClient.Send(preferences.Email, "Notification", message);

        if (preferences.SmsEnabled)
            _twilioClient.SendSms(preferences.Phone, message);

        // Log notification
        LogNotification(userId, message);
    }
}
```

**Expected Solution:**
```csharp
// Abstractions
public interface IEmailSender
{
    void Send(string to, string subject, string body);
}

public interface ISmsSender
{
    void Send(string phoneNumber, string message);
}

public interface IUserPreferenceRepository
{
    UserPreferences GetByUserId(int userId);
}

public interface INotificationLogger
{
    void Log(int userId, string message);
}

// Implementations
public class SmtpEmailSender : IEmailSender
{
    private readonly string _server;
    public SmtpEmailSender(string server) => _server = server;
    public void Send(string to, string subject, string body) { /* SMTP logic */ }
}

public class TwilioSmsSender : ISmsSender
{
    private readonly string _apiKey;
    public TwilioSmsSender(string apiKey) => _apiKey = apiKey;
    public void Send(string phoneNumber, string message) { /* Twilio logic */ }
}

// Service depends on abstractions
public class NotificationService
{
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly IUserPreferenceRepository _preferences;
    private readonly INotificationLogger _logger;

    public NotificationService(
        IEmailSender emailSender,
        ISmsSender smsSender,
        IUserPreferenceRepository preferences,
        INotificationLogger logger)
    {
        _emailSender = emailSender;
        _smsSender = smsSender;
        _preferences = preferences;
        _logger = logger;
    }

    public void SendNotification(int userId, string message)
    {
        var prefs = _preferences.GetByUserId(userId);

        if (prefs.EmailEnabled)
            _emailSender.Send(prefs.Email, "Notification", message);

        if (prefs.SmsEnabled)
            _smsSender.Send(prefs.Phone, message);

        _logger.Log(userId, message);
    }
}
```

---

## Level 7 (Challenging)

**Question:** Design a payment processing system following all SOLID principles.

Requirements:
- Support multiple payment methods (Credit Card, PayPal, Bank Transfer)
- Validate payment details
- Process payment
- Send receipt
- Log transaction

```csharp
// Design the interfaces and classes
```

**Expected Solution:**
```csharp
// Domain
public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string CustomerId { get; set; }
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string TransactionId { get; set; }
    public string? ErrorMessage { get; set; }
}

// SRP: Separate concerns
public interface IPaymentValidator
{
    ValidationResult Validate(PaymentRequest request);
}

public interface IPaymentProcessor
{
    PaymentResult Process(PaymentRequest request);
}

public interface IReceiptSender
{
    void SendReceipt(string customerId, PaymentResult result);
}

public interface ITransactionLogger
{
    void Log(PaymentRequest request, PaymentResult result);
}

// OCP: Payment method strategies
public interface IPaymentMethod
{
    string Name { get; }
    bool Supports(PaymentRequest request);
    PaymentResult Execute(PaymentRequest request);
}

public class CreditCardPayment : IPaymentMethod
{
    public string Name => "CreditCard";
    public bool Supports(PaymentRequest request) => true;
    public PaymentResult Execute(PaymentRequest request)
    {
        // Credit card processing logic
        return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
    }
}

public class PayPalPayment : IPaymentMethod
{
    public string Name => "PayPal";
    public bool Supports(PaymentRequest request) => true;
    public PaymentResult Execute(PaymentRequest request)
    {
        // PayPal processing logic
        return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
    }
}

// LSP: All payment methods work the same way
// ISP: Small, focused interfaces
// DIP: Depend on abstractions

public class PaymentService
{
    private readonly IPaymentValidator _validator;
    private readonly IReceiptSender _receiptSender;
    private readonly ITransactionLogger _logger;
    private readonly IEnumerable<IPaymentMethod> _paymentMethods;

    public PaymentService(
        IPaymentValidator validator,
        IReceiptSender receiptSender,
        ITransactionLogger logger,
        IEnumerable<IPaymentMethod> paymentMethods)
    {
        _validator = validator;
        _receiptSender = receiptSender;
        _logger = logger;
        _paymentMethods = paymentMethods;
    }

    public PaymentResult ProcessPayment(PaymentRequest request, string methodName)
    {
        // Validate
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
            return new PaymentResult { Success = false, ErrorMessage = validation.Error };

        // Find payment method
        var method = _paymentMethods.FirstOrDefault(m => m.Name == methodName);
        if (method == null)
            return new PaymentResult { Success = false, ErrorMessage = "Unsupported payment method" };

        // Process
        var result = method.Execute(request);

        // Log
        _logger.Log(request, result);

        // Send receipt on success
        if (result.Success)
            _receiptSender.SendReceipt(request.CustomerId, result);

        return result;
    }
}
```

---

## Level 8 (Challenging)

**Question:** Refactor this legacy code to follow SOLID principles while maintaining backward compatibility.

```csharp
public class ReportGenerator
{
    private SqlConnection _connection;

    public ReportGenerator()
    {
        _connection = new SqlConnection("Server=...;Database=...;");
    }

    public string GenerateReport(string reportType, DateTime startDate, DateTime endDate, string format)
    {
        _connection.Open();

        string data;
        if (reportType == "Sales")
        {
            var cmd = new SqlCommand("SELECT * FROM Sales WHERE Date BETWEEN @start AND @end", _connection);
            cmd.Parameters.AddWithValue("@start", startDate);
            cmd.Parameters.AddWithValue("@end", endDate);
            data = ProcessSalesData(cmd.ExecuteReader());
        }
        else if (reportType == "Inventory")
        {
            var cmd = new SqlCommand("SELECT * FROM Inventory", _connection);
            data = ProcessInventoryData(cmd.ExecuteReader());
        }
        else
        {
            throw new ArgumentException("Unknown report type");
        }

        _connection.Close();

        if (format == "PDF")
            return ConvertToPdf(data);
        else if (format == "Excel")
            return ConvertToExcel(data);
        else if (format == "HTML")
            return ConvertToHtml(data);
        else
            return data;
    }

    private string ProcessSalesData(SqlDataReader reader) { /* ... */ return ""; }
    private string ProcessInventoryData(SqlDataReader reader) { /* ... */ return ""; }
    private string ConvertToPdf(string data) { /* ... */ return ""; }
    private string ConvertToExcel(string data) { /* ... */ return ""; }
    private string ConvertToHtml(string data) { /* ... */ return ""; }
}
```

**Expected Solution:**
```csharp
// Data access abstraction (DIP)
public interface IReportDataSource
{
    string GetData(DateTime? startDate = null, DateTime? endDate = null);
}

// Report data sources (OCP - can add new sources)
public class SalesReportDataSource : IReportDataSource
{
    private readonly IDbConnection _connection;

    public SalesReportDataSource(IDbConnection connection)
    {
        _connection = connection;
    }

    public string GetData(DateTime? startDate = null, DateTime? endDate = null)
    {
        // Sales-specific data retrieval
        return "sales data";
    }
}

public class InventoryReportDataSource : IReportDataSource
{
    private readonly IDbConnection _connection;

    public InventoryReportDataSource(IDbConnection connection)
    {
        _connection = connection;
    }

    public string GetData(DateTime? startDate = null, DateTime? endDate = null)
    {
        // Inventory-specific data retrieval
        return "inventory data";
    }
}

// Format abstraction (OCP)
public interface IReportFormatter
{
    string Format { get; }
    string FormatReport(string data);
}

public class PdfFormatter : IReportFormatter
{
    public string Format => "PDF";
    public string FormatReport(string data) { /* PDF conversion */ return "pdf"; }
}

public class ExcelFormatter : IReportFormatter
{
    public string Format => "Excel";
    public string FormatReport(string data) { /* Excel conversion */ return "excel"; }
}

public class HtmlFormatter : IReportFormatter
{
    public string Format => "HTML";
    public string FormatReport(string data) { /* HTML conversion */ return "html"; }
}

// Factory for backward compatibility
public interface IReportDataSourceFactory
{
    IReportDataSource Create(string reportType);
}

public class ReportDataSourceFactory : IReportDataSourceFactory
{
    private readonly IDbConnection _connection;

    public ReportDataSourceFactory(IDbConnection connection)
    {
        _connection = connection;
    }

    public IReportDataSource Create(string reportType)
    {
        return reportType switch
        {
            "Sales" => new SalesReportDataSource(_connection),
            "Inventory" => new InventoryReportDataSource(_connection),
            _ => throw new ArgumentException($"Unknown report type: {reportType}")
        };
    }
}

// New clean service
public class ReportService
{
    private readonly IReportDataSourceFactory _dataSourceFactory;
    private readonly IEnumerable<IReportFormatter> _formatters;

    public ReportService(
        IReportDataSourceFactory dataSourceFactory,
        IEnumerable<IReportFormatter> formatters)
    {
        _dataSourceFactory = dataSourceFactory;
        _formatters = formatters;
    }

    public string GenerateReport(
        string reportType,
        string format,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var dataSource = _dataSourceFactory.Create(reportType);
        var data = dataSource.GetData(startDate, endDate);

        var formatter = _formatters.FirstOrDefault(f => f.Format == format);
        if (formatter == null)
            return data;

        return formatter.FormatReport(data);
    }
}

// Backward-compatible wrapper (maintains old API)
public class ReportGenerator
{
    private readonly ReportService _service;

    public ReportGenerator()
    {
        var connection = new SqlConnection("Server=...;Database=...;");
        var factory = new ReportDataSourceFactory(connection);
        var formatters = new IReportFormatter[]
        {
            new PdfFormatter(),
            new ExcelFormatter(),
            new HtmlFormatter()
        };
        _service = new ReportService(factory, formatters);
    }

    public string GenerateReport(string reportType, DateTime startDate, DateTime endDate, string format)
    {
        return _service.GenerateReport(reportType, format, startDate, endDate);
    }
}
```

---

## Level 9 (Expert)

**Question:** Design a complete e-commerce order processing system following all SOLID principles. Include:
- Order validation
- Inventory management
- Payment processing
- Shipping calculation
- Notification system
- Audit logging

The system should be easily testable and support adding new payment methods, shipping providers, and notification channels without modifying existing code.

**Expected Solution:**
```csharp
// See full implementation in a separate file due to length
// Key design points:

// 1. SRP - Each class has one responsibility
public interface IOrderValidator { ValidationResult Validate(Order order); }
public interface IInventoryManager { bool Reserve(List<OrderItem> items); void Release(List<OrderItem> items); }
public interface IPaymentProcessor { PaymentResult Process(PaymentInfo payment); }
public interface IShippingCalculator { ShippingQuote Calculate(Order order, Address destination); }
public interface INotificationService { Task NotifyAsync(string userId, Notification notification); }
public interface IAuditLogger { void Log(AuditEvent auditEvent); }

// 2. OCP - Strategies for extension
public interface IPaymentMethod { string Code { get; } PaymentResult Process(decimal amount); }
public interface IShippingProvider { string Code { get; } ShippingQuote Quote(Order order, Address destination); }
public interface INotificationChannel { string Type { get; } Task SendAsync(string recipient, string message); }

// 3. LSP - All implementations are substitutable
public class CreditCardPayment : IPaymentMethod { /* ... */ }
public class PayPalPayment : IPaymentMethod { /* ... */ }
public class StripePayment : IPaymentMethod { /* ... */ }

// 4. ISP - Small, focused interfaces
public interface IOrderReader { Order? GetById(int id); IEnumerable<Order> GetByCustomer(string customerId); }
public interface IOrderWriter { void Save(Order order); void Update(Order order); }
public interface IOrderRepository : IOrderReader, IOrderWriter { }

// 5. DIP - All dependencies injected
public class OrderService
{
    public OrderService(
        IOrderRepository orders,
        IOrderValidator validator,
        IInventoryManager inventory,
        IEnumerable<IPaymentMethod> paymentMethods,
        IEnumerable<IShippingProvider> shippingProviders,
        INotificationService notifications,
        IAuditLogger audit)
    {
        // All dependencies are abstractions
    }

    public async Task<OrderResult> ProcessOrderAsync(OrderRequest request)
    {
        // Orchestrates all components
        // Each component is replaceable and testable
    }
}

// Registration in DI container
public static class ServiceRegistration
{
    public static IServiceCollection AddOrderProcessing(this IServiceCollection services)
    {
        // Core services
        services.AddScoped<IOrderRepository, SqlOrderRepository>();
        services.AddScoped<IOrderValidator, OrderValidator>();
        services.AddScoped<IInventoryManager, InventoryManager>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IAuditLogger, AuditLogger>();

        // Payment methods (OCP - add new without modifying)
        services.AddScoped<IPaymentMethod, CreditCardPayment>();
        services.AddScoped<IPaymentMethod, PayPalPayment>();
        services.AddScoped<IPaymentMethod, StripePayment>();

        // Shipping providers
        services.AddScoped<IShippingProvider, FedExProvider>();
        services.AddScoped<IShippingProvider, UpsProvider>();
        services.AddScoped<IShippingProvider, UspsProvider>();

        // Notification channels
        services.AddScoped<INotificationChannel, EmailChannel>();
        services.AddScoped<INotificationChannel, SmsChannel>();
        services.AddScoped<INotificationChannel, PushChannel>();

        // Main service
        services.AddScoped<OrderService>();

        return services;
    }
}
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Identify and fix SRP violations |
| 3-4 | Apply OCP and LSP correctly |
| 5-6 | Apply ISP and DIP correctly |
| 7-8 | Design systems using all principles |
| 9 | Full architecture with testability |
