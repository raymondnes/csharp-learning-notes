# Topic 30: Unit Testing with xUnit - Assessment Questions

## Level 1 (Trivial)

**Question:** Write a simple test method that verifies 2 + 2 equals 4.

```csharp
// Complete this test class
public class MathTests
{
    // Write a test that asserts 2 + 2 == 4
}
```

**Expected Solution:**
```csharp
public class MathTests
{
    [Fact]
    public void Add_TwoAndTwo_EqualsFour()
    {
        int result = 2 + 2;

        Assert.Equal(4, result);
    }
}
```

---

## Level 2 (Trivial)

**Question:** Write a test that verifies a string contains a specific substring.

```csharp
public class StringHelper
{
    public string Greet(string name) => $"Hello, {name}!";
}

// Write a test that verifies Greet("World") contains "World"
```

**Expected Solution:**
```csharp
public class StringHelperTests
{
    [Fact]
    public void Greet_WithName_ContainsNameInResult()
    {
        var helper = new StringHelper();

        string result = helper.Greet("World");

        Assert.Contains("World", result);
    }
}
```

---

## Level 3 (Easy)

**Question:** Write a Theory test with InlineData to test multiple cases.

```csharp
public class NumberChecker
{
    public bool IsPositive(int number) => number > 0;
}

// Write a Theory test with at least 4 test cases (mix of positive and negative numbers)
```

**Expected Solution:**
```csharp
public class NumberCheckerTests
{
    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-5, false)]
    [InlineData(-100, false)]
    public void IsPositive_WithVariousNumbers_ReturnsExpectedResult(int number, bool expected)
    {
        var checker = new NumberChecker();

        bool result = checker.IsPositive(number);

        Assert.Equal(expected, result);
    }
}
```

---

## Level 4 (Easy)

**Question:** Write a test that verifies an exception is thrown.

```csharp
public class Validator
{
    public void ValidateAge(int age)
    {
        if (age < 0)
            throw new ArgumentException("Age cannot be negative", nameof(age));
        if (age > 150)
            throw new ArgumentException("Age cannot exceed 150", nameof(age));
    }
}

// Write tests for both exception cases
```

**Expected Solution:**
```csharp
public class ValidatorTests
{
    private readonly Validator _validator = new();

    [Fact]
    public void ValidateAge_NegativeAge_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(
            () => _validator.ValidateAge(-1)
        );

        Assert.Equal("age", ex.ParamName);
        Assert.Contains("negative", ex.Message);
    }

    [Fact]
    public void ValidateAge_AgeOver150_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(
            () => _validator.ValidateAge(151)
        );

        Assert.Contains("150", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(25)]
    [InlineData(150)]
    public void ValidateAge_ValidAge_DoesNotThrow(int age)
    {
        var exception = Record.Exception(() => _validator.ValidateAge(age));

        Assert.Null(exception);
    }
}
```

---

## Level 5 (Moderate)

**Question:** Test a shopping cart class with multiple test scenarios.

```csharp
public class ShoppingCart
{
    private readonly List<CartItem> _items = new();

    public void AddItem(string name, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name required");
        if (price <= 0)
            throw new ArgumentException("Price must be positive");
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");

        var existing = _items.FirstOrDefault(i => i.Name == name);
        if (existing != null)
            existing.Quantity += quantity;
        else
            _items.Add(new CartItem { Name = name, Price = price, Quantity = quantity });
    }

    public decimal GetTotal() => _items.Sum(i => i.Price * i.Quantity);
    public int GetItemCount() => _items.Sum(i => i.Quantity);
    public void Clear() => _items.Clear();
}

public class CartItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

// Write comprehensive tests for ShoppingCart
```

**Expected Solution:**
```csharp
public class ShoppingCartTests
{
    private readonly ShoppingCart _cart;

    public ShoppingCartTests()
    {
        _cart = new ShoppingCart();
    }

    [Fact]
    public void AddItem_ValidItem_IncreasesItemCount()
    {
        _cart.AddItem("Apple", 1.50m, 3);

        Assert.Equal(3, _cart.GetItemCount());
    }

    [Fact]
    public void AddItem_SameItemTwice_CombinesQuantity()
    {
        _cart.AddItem("Apple", 1.50m, 2);
        _cart.AddItem("Apple", 1.50m, 3);

        Assert.Equal(5, _cart.GetItemCount());
    }

    [Fact]
    public void GetTotal_MultipleItems_ReturnsCorrectSum()
    {
        _cart.AddItem("Apple", 1.00m, 3);   // 3.00
        _cart.AddItem("Banana", 0.50m, 4);  // 2.00

        Assert.Equal(5.00m, _cart.GetTotal());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void AddItem_InvalidName_ThrowsArgumentException(string name)
    {
        Assert.Throws<ArgumentException>(() => _cart.AddItem(name, 1.00m, 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddItem_InvalidPrice_ThrowsArgumentException(decimal price)
    {
        Assert.Throws<ArgumentException>(() => _cart.AddItem("Apple", price, 1));
    }

    [Fact]
    public void Clear_WithItems_RemovesAllItems()
    {
        _cart.AddItem("Apple", 1.00m, 5);
        _cart.Clear();

        Assert.Equal(0, _cart.GetItemCount());
        Assert.Equal(0m, _cart.GetTotal());
    }
}
```

---

## Level 6 (Moderate)

**Question:** Test a password validator with various rules.

```csharp
public class PasswordValidator
{
    public ValidationResult Validate(string password)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(password))
        {
            errors.Add("Password is required");
            return new ValidationResult(false, errors);
        }

        if (password.Length < 8)
            errors.Add("Password must be at least 8 characters");
        if (password.Length > 50)
            errors.Add("Password must not exceed 50 characters");
        if (!password.Any(char.IsUpper))
            errors.Add("Password must contain an uppercase letter");
        if (!password.Any(char.IsLower))
            errors.Add("Password must contain a lowercase letter");
        if (!password.Any(char.IsDigit))
            errors.Add("Password must contain a digit");
        if (!password.Any(c => !char.IsLetterOrDigit(c)))
            errors.Add("Password must contain a special character");

        return new ValidationResult(errors.Count == 0, errors);
    }
}

public record ValidationResult(bool IsValid, List<string> Errors);

// Write thorough tests covering all validation rules
```

**Expected Solution:**
```csharp
public class PasswordValidatorTests
{
    private readonly PasswordValidator _validator = new();

    [Fact]
    public void Validate_ValidPassword_ReturnsSuccess()
    {
        var result = _validator.Validate("SecurePass123!");

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_NullOrEmpty_ReturnsRequiredError(string password)
    {
        var result = _validator.Validate(password);

        Assert.False(result.IsValid);
        Assert.Contains("required", result.Errors[0].ToLower());
    }

    [Fact]
    public void Validate_TooShort_ReturnsLengthError()
    {
        var result = _validator.Validate("Ab1!");

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("8 characters"));
    }

    [Fact]
    public void Validate_NoUppercase_ReturnsUppercaseError()
    {
        var result = _validator.Validate("lowercase123!");

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("uppercase"));
    }

    [Fact]
    public void Validate_NoLowercase_ReturnsLowercaseError()
    {
        var result = _validator.Validate("UPPERCASE123!");

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("lowercase"));
    }

    [Fact]
    public void Validate_NoDigit_ReturnsDigitError()
    {
        var result = _validator.Validate("NoDigitsHere!");

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("digit"));
    }

    [Fact]
    public void Validate_NoSpecialChar_ReturnsSpecialCharError()
    {
        var result = _validator.Validate("NoSpecial123");

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("special"));
    }

    [Fact]
    public void Validate_MultipleFailures_ReturnsAllErrors()
    {
        var result = _validator.Validate("abc");

        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count >= 4); // short, no upper, no digit, no special
    }
}
```

---

## Level 7 (Challenging)

**Question:** Test an order processing service with complex business logic.

```csharp
public class OrderService
{
    private readonly List<Order> _orders = new();
    private int _nextId = 1;

    public Order CreateOrder(string customerId, List<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer ID required");
        if (items == null || !items.Any())
            throw new ArgumentException("Order must have items");
        if (items.Any(i => i.Quantity <= 0))
            throw new ArgumentException("All items must have positive quantity");

        var order = new Order
        {
            Id = _nextId++,
            CustomerId = customerId,
            Items = items,
            Subtotal = items.Sum(i => i.Price * i.Quantity),
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };

        order.Tax = order.Subtotal * 0.08m;
        order.Total = order.Subtotal + order.Tax;

        // Apply discount for orders over $100
        if (order.Subtotal > 100)
        {
            order.Discount = order.Subtotal * 0.10m;
            order.Total = order.Subtotal - order.Discount + order.Tax;
        }

        _orders.Add(order);
        return order;
    }

    public bool CancelOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null) return false;
        if (order.Status != OrderStatus.Pending) return false;

        order.Status = OrderStatus.Cancelled;
        return true;
    }

    public Order? GetOrder(int id) => _orders.FirstOrDefault(o => o.Id == id);
}

public class Order
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class OrderItem
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public enum OrderStatus { Pending, Processing, Shipped, Cancelled }

// Write comprehensive tests
```

**Expected Solution:**
```csharp
public class OrderServiceTests
{
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _service = new OrderService();
    }

    private List<OrderItem> CreateItems(decimal price = 10m, int quantity = 1)
    {
        return new List<OrderItem>
        {
            new() { ProductName = "Test Product", Price = price, Quantity = quantity }
        };
    }

    [Fact]
    public void CreateOrder_ValidInput_ReturnsOrderWithId()
    {
        var order = _service.CreateOrder("CUST001", CreateItems());

        Assert.True(order.Id > 0);
        Assert.Equal("CUST001", order.CustomerId);
        Assert.Equal(OrderStatus.Pending, order.Status);
    }

    [Fact]
    public void CreateOrder_ValidInput_CalculatesSubtotalCorrectly()
    {
        var items = new List<OrderItem>
        {
            new() { ProductName = "A", Price = 10m, Quantity = 2 },  // 20
            new() { ProductName = "B", Price = 5m, Quantity = 3 }    // 15
        };

        var order = _service.CreateOrder("CUST001", items);

        Assert.Equal(35m, order.Subtotal);
    }

    [Fact]
    public void CreateOrder_ValidInput_CalculatesTax()
    {
        var order = _service.CreateOrder("CUST001", CreateItems(100m, 1));

        Assert.Equal(8m, order.Tax);  // 100 * 0.08
    }

    [Fact]
    public void CreateOrder_Over100_AppliesDiscount()
    {
        var order = _service.CreateOrder("CUST001", CreateItems(50m, 3)); // 150 subtotal

        Assert.Equal(15m, order.Discount);  // 150 * 0.10
        Assert.Equal(150m - 15m + 12m, order.Total);  // subtotal - discount + tax
    }

    [Fact]
    public void CreateOrder_Under100_NoDiscount()
    {
        var order = _service.CreateOrder("CUST001", CreateItems(10m, 5)); // 50 subtotal

        Assert.Equal(0m, order.Discount);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void CreateOrder_InvalidCustomerId_ThrowsException(string customerId)
    {
        Assert.Throws<ArgumentException>(() =>
            _service.CreateOrder(customerId, CreateItems()));
    }

    [Fact]
    public void CreateOrder_NullItems_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.CreateOrder("CUST001", null));
    }

    [Fact]
    public void CreateOrder_EmptyItems_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.CreateOrder("CUST001", new List<OrderItem>()));
    }

    [Fact]
    public void CreateOrder_ZeroQuantity_ThrowsException()
    {
        var items = new List<OrderItem>
        {
            new() { ProductName = "A", Price = 10m, Quantity = 0 }
        };

        Assert.Throws<ArgumentException>(() =>
            _service.CreateOrder("CUST001", items));
    }

    [Fact]
    public void CancelOrder_PendingOrder_ReturnsTrue()
    {
        var order = _service.CreateOrder("CUST001", CreateItems());

        bool result = _service.CancelOrder(order.Id);

        Assert.True(result);
        Assert.Equal(OrderStatus.Cancelled, _service.GetOrder(order.Id)?.Status);
    }

    [Fact]
    public void CancelOrder_NonExistentOrder_ReturnsFalse()
    {
        bool result = _service.CancelOrder(999);

        Assert.False(result);
    }

    [Fact]
    public void GetOrder_ExistingOrder_ReturnsOrder()
    {
        var created = _service.CreateOrder("CUST001", CreateItems());

        var found = _service.GetOrder(created.Id);

        Assert.NotNull(found);
        Assert.Equal(created.Id, found.Id);
    }
}
```

---

## Level 8 (Challenging)

**Question:** Test an authentication service with token generation and validation.

```csharp
public class AuthService
{
    private readonly Dictionary<string, User> _users = new();
    private readonly Dictionary<string, (string UserId, DateTime Expiry)> _tokens = new();
    private readonly TimeSpan _tokenLifetime = TimeSpan.FromHours(1);

    public void RegisterUser(string email, string password)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format");
        if (password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");
        if (_users.ContainsKey(email.ToLower()))
            throw new InvalidOperationException("Email already registered");

        _users[email.ToLower()] = new User
        {
            Email = email.ToLower(),
            PasswordHash = HashPassword(password),
            CreatedAt = DateTime.UtcNow
        };
    }

    public string? Login(string email, string password)
    {
        if (!_users.TryGetValue(email.ToLower(), out var user))
            return null;
        if (user.PasswordHash != HashPassword(password))
            return null;

        var token = Guid.NewGuid().ToString();
        _tokens[token] = (user.Email, DateTime.UtcNow.Add(_tokenLifetime));
        return token;
    }

    public bool ValidateToken(string token)
    {
        if (!_tokens.TryGetValue(token, out var tokenData))
            return false;
        if (tokenData.Expiry < DateTime.UtcNow)
        {
            _tokens.Remove(token);
            return false;
        }
        return true;
    }

    public void Logout(string token) => _tokens.Remove(token);

    private bool IsValidEmail(string email) =>
        !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");

    private string HashPassword(string password) =>
        Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password + "salt"));
}

public class User
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Write comprehensive tests covering all scenarios
```

**Expected Solution:**
```csharp
public class AuthServiceTests
{
    private readonly AuthService _auth;

    public AuthServiceTests()
    {
        _auth = new AuthService();
    }

    #region Registration Tests

    [Fact]
    public void RegisterUser_ValidCredentials_Succeeds()
    {
        var exception = Record.Exception(() =>
            _auth.RegisterUser("test@example.com", "password123"));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("no-at-sign.com")]
    [InlineData("no-dot@com")]
    [InlineData("")]
    [InlineData(null)]
    public void RegisterUser_InvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.Throws<ArgumentException>(() =>
            _auth.RegisterUser(email, "password123"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("short")]
    [InlineData("1234567")]
    public void RegisterUser_ShortPassword_ThrowsArgumentException(string password)
    {
        Assert.Throws<ArgumentException>(() =>
            _auth.RegisterUser("test@example.com", password));
    }

    [Fact]
    public void RegisterUser_DuplicateEmail_ThrowsInvalidOperationException()
    {
        _auth.RegisterUser("test@example.com", "password123");

        Assert.Throws<InvalidOperationException>(() =>
            _auth.RegisterUser("test@example.com", "differentpassword"));
    }

    [Fact]
    public void RegisterUser_EmailIsCaseInsensitive()
    {
        _auth.RegisterUser("Test@Example.COM", "password123");

        Assert.Throws<InvalidOperationException>(() =>
            _auth.RegisterUser("test@example.com", "password123"));
    }

    #endregion

    #region Login Tests

    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        _auth.RegisterUser("test@example.com", "password123");

        var token = _auth.Login("test@example.com", "password123");

        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void Login_WrongPassword_ReturnsNull()
    {
        _auth.RegisterUser("test@example.com", "password123");

        var token = _auth.Login("test@example.com", "wrongpassword");

        Assert.Null(token);
    }

    [Fact]
    public void Login_NonExistentUser_ReturnsNull()
    {
        var token = _auth.Login("nonexistent@example.com", "password123");

        Assert.Null(token);
    }

    [Fact]
    public void Login_EmailIsCaseInsensitive()
    {
        _auth.RegisterUser("test@example.com", "password123");

        var token = _auth.Login("TEST@EXAMPLE.COM", "password123");

        Assert.NotNull(token);
    }

    [Fact]
    public void Login_MultipleTimes_ReturnsDifferentTokens()
    {
        _auth.RegisterUser("test@example.com", "password123");

        var token1 = _auth.Login("test@example.com", "password123");
        var token2 = _auth.Login("test@example.com", "password123");

        Assert.NotEqual(token1, token2);
    }

    #endregion

    #region Token Validation Tests

    [Fact]
    public void ValidateToken_ValidToken_ReturnsTrue()
    {
        _auth.RegisterUser("test@example.com", "password123");
        var token = _auth.Login("test@example.com", "password123");

        bool isValid = _auth.ValidateToken(token);

        Assert.True(isValid);
    }

    [Fact]
    public void ValidateToken_InvalidToken_ReturnsFalse()
    {
        bool isValid = _auth.ValidateToken("invalid-token");

        Assert.False(isValid);
    }

    [Fact]
    public void ValidateToken_AfterLogout_ReturnsFalse()
    {
        _auth.RegisterUser("test@example.com", "password123");
        var token = _auth.Login("test@example.com", "password123");
        _auth.Logout(token);

        bool isValid = _auth.ValidateToken(token);

        Assert.False(isValid);
    }

    #endregion

    #region Logout Tests

    [Fact]
    public void Logout_ValidToken_InvalidatesToken()
    {
        _auth.RegisterUser("test@example.com", "password123");
        var token = _auth.Login("test@example.com", "password123");

        _auth.Logout(token);

        Assert.False(_auth.ValidateToken(token));
    }

    [Fact]
    public void Logout_InvalidToken_DoesNotThrow()
    {
        var exception = Record.Exception(() => _auth.Logout("invalid-token"));

        Assert.Null(exception);
    }

    #endregion
}
```

---

## Level 9 (Expert)

**Question:** Create a comprehensive test suite for a banking transaction service with concurrent operations, audit logging, and rollback capabilities.

```csharp
public class BankingService
{
    private readonly Dictionary<string, Account> _accounts = new();
    private readonly List<Transaction> _transactions = new();
    private readonly object _lock = new object();

    public string CreateAccount(string ownerName, decimal initialDeposit = 0)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("Owner name required");
        if (initialDeposit < 0)
            throw new ArgumentException("Initial deposit cannot be negative");

        var accountNumber = GenerateAccountNumber();
        var account = new Account
        {
            AccountNumber = accountNumber,
            OwnerName = ownerName,
            Balance = initialDeposit,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        lock (_lock) { _accounts[accountNumber] = account; }

        if (initialDeposit > 0)
            LogTransaction(accountNumber, TransactionType.Deposit, initialDeposit, "Initial deposit");

        return accountNumber;
    }

    public void Deposit(string accountNumber, decimal amount, string description = "Deposit")
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive");

        lock (_lock)
        {
            var account = GetAccountOrThrow(accountNumber);
            account.Balance += amount;
            LogTransaction(accountNumber, TransactionType.Deposit, amount, description);
        }
    }

    public void Withdraw(string accountNumber, decimal amount, string description = "Withdrawal")
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive");

        lock (_lock)
        {
            var account = GetAccountOrThrow(accountNumber);
            if (account.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");
            account.Balance -= amount;
            LogTransaction(accountNumber, TransactionType.Withdrawal, amount, description);
        }
    }

    public void Transfer(string fromAccount, string toAccount, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Transfer amount must be positive");
        if (fromAccount == toAccount)
            throw new ArgumentException("Cannot transfer to same account");

        lock (_lock)
        {
            var from = GetAccountOrThrow(fromAccount);
            var to = GetAccountOrThrow(toAccount);

            if (from.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");

            from.Balance -= amount;
            to.Balance += amount;

            LogTransaction(fromAccount, TransactionType.Transfer, -amount, $"Transfer to {toAccount}");
            LogTransaction(toAccount, TransactionType.Transfer, amount, $"Transfer from {fromAccount}");
        }
    }

    public decimal GetBalance(string accountNumber)
    {
        lock (_lock)
        {
            return GetAccountOrThrow(accountNumber).Balance;
        }
    }

    public IEnumerable<Transaction> GetTransactionHistory(string accountNumber)
    {
        return _transactions.Where(t => t.AccountNumber == accountNumber)
                           .OrderByDescending(t => t.Timestamp)
                           .ToList();
    }

    public void CloseAccount(string accountNumber)
    {
        lock (_lock)
        {
            var account = GetAccountOrThrow(accountNumber);
            if (account.Balance != 0)
                throw new InvalidOperationException("Account must have zero balance to close");
            account.IsActive = false;
        }
    }

    private Account GetAccountOrThrow(string accountNumber)
    {
        if (!_accounts.TryGetValue(accountNumber, out var account))
            throw new ArgumentException("Account not found");
        if (!account.IsActive)
            throw new InvalidOperationException("Account is closed");
        return account;
    }

    private void LogTransaction(string accountNumber, TransactionType type, decimal amount, string description)
    {
        _transactions.Add(new Transaction
        {
            Id = Guid.NewGuid().ToString(),
            AccountNumber = accountNumber,
            Type = type,
            Amount = amount,
            Description = description,
            Timestamp = DateTime.UtcNow,
            BalanceAfter = _accounts[accountNumber].Balance
        });
    }

    private string GenerateAccountNumber() => $"ACC{DateTime.UtcNow.Ticks}";
}

public class Account
{
    public string AccountNumber { get; set; }
    public string OwnerName { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class Transaction
{
    public string Id { get; set; }
    public string AccountNumber { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal BalanceAfter { get; set; }
}

public enum TransactionType { Deposit, Withdrawal, Transfer }

// Write a comprehensive test suite covering all edge cases, validation, and concurrency
```

**Expected Solution:**
```csharp
public class BankingServiceTests
{
    private readonly BankingService _bank;

    public BankingServiceTests()
    {
        _bank = new BankingService();
    }

    #region Account Creation Tests

    [Fact]
    public void CreateAccount_ValidOwner_ReturnsAccountNumber()
    {
        var accountNumber = _bank.CreateAccount("John Doe");

        Assert.NotNull(accountNumber);
        Assert.StartsWith("ACC", accountNumber);
    }

    [Fact]
    public void CreateAccount_WithInitialDeposit_SetsBalance()
    {
        var accountNumber = _bank.CreateAccount("John Doe", 500m);

        Assert.Equal(500m, _bank.GetBalance(accountNumber));
    }

    [Fact]
    public void CreateAccount_WithInitialDeposit_LogsTransaction()
    {
        var accountNumber = _bank.CreateAccount("John Doe", 500m);

        var transactions = _bank.GetTransactionHistory(accountNumber);

        Assert.Single(transactions);
        Assert.Equal(TransactionType.Deposit, transactions.First().Type);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void CreateAccount_InvalidOwnerName_ThrowsArgumentException(string name)
    {
        Assert.Throws<ArgumentException>(() => _bank.CreateAccount(name));
    }

    [Fact]
    public void CreateAccount_NegativeDeposit_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _bank.CreateAccount("John", -100m));
    }

    #endregion

    #region Deposit Tests

    [Fact]
    public void Deposit_ValidAmount_IncreasesBalance()
    {
        var account = _bank.CreateAccount("John");
        _bank.Deposit(account, 100m);

        Assert.Equal(100m, _bank.GetBalance(account));
    }

    [Fact]
    public void Deposit_ValidAmount_LogsTransaction()
    {
        var account = _bank.CreateAccount("John");
        _bank.Deposit(account, 100m, "Salary");

        var tx = _bank.GetTransactionHistory(account).First();

        Assert.Equal(TransactionType.Deposit, tx.Type);
        Assert.Equal(100m, tx.Amount);
        Assert.Equal("Salary", tx.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Deposit_InvalidAmount_ThrowsArgumentException(decimal amount)
    {
        var account = _bank.CreateAccount("John");

        Assert.Throws<ArgumentException>(() => _bank.Deposit(account, amount));
    }

    [Fact]
    public void Deposit_NonExistentAccount_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _bank.Deposit("INVALID", 100m));
    }

    #endregion

    #region Withdrawal Tests

    [Fact]
    public void Withdraw_SufficientFunds_DecreasesBalance()
    {
        var account = _bank.CreateAccount("John", 500m);

        _bank.Withdraw(account, 200m);

        Assert.Equal(300m, _bank.GetBalance(account));
    }

    [Fact]
    public void Withdraw_InsufficientFunds_ThrowsInvalidOperationException()
    {
        var account = _bank.CreateAccount("John", 100m);

        Assert.Throws<InvalidOperationException>(() => _bank.Withdraw(account, 200m));
    }

    [Fact]
    public void Withdraw_ExactBalance_LeavesZero()
    {
        var account = _bank.CreateAccount("John", 100m);

        _bank.Withdraw(account, 100m);

        Assert.Equal(0m, _bank.GetBalance(account));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Withdraw_InvalidAmount_ThrowsArgumentException(decimal amount)
    {
        var account = _bank.CreateAccount("John", 500m);

        Assert.Throws<ArgumentException>(() => _bank.Withdraw(account, amount));
    }

    #endregion

    #region Transfer Tests

    [Fact]
    public void Transfer_ValidAmount_MovesMoneyCorrectly()
    {
        var from = _bank.CreateAccount("John", 500m);
        var to = _bank.CreateAccount("Jane", 100m);

        _bank.Transfer(from, to, 200m);

        Assert.Equal(300m, _bank.GetBalance(from));
        Assert.Equal(300m, _bank.GetBalance(to));
    }

    [Fact]
    public void Transfer_ValidAmount_LogsTransactionsOnBothAccounts()
    {
        var from = _bank.CreateAccount("John", 500m);
        var to = _bank.CreateAccount("Jane");

        _bank.Transfer(from, to, 200m);

        var fromTx = _bank.GetTransactionHistory(from)
            .First(t => t.Type == TransactionType.Transfer);
        var toTx = _bank.GetTransactionHistory(to).First();

        Assert.Equal(-200m, fromTx.Amount);
        Assert.Equal(200m, toTx.Amount);
    }

    [Fact]
    public void Transfer_InsufficientFunds_ThrowsAndDoesNotModifyBalances()
    {
        var from = _bank.CreateAccount("John", 100m);
        var to = _bank.CreateAccount("Jane", 50m);

        Assert.Throws<InvalidOperationException>(() => _bank.Transfer(from, to, 200m));

        Assert.Equal(100m, _bank.GetBalance(from));
        Assert.Equal(50m, _bank.GetBalance(to));
    }

    [Fact]
    public void Transfer_ToSameAccount_ThrowsArgumentException()
    {
        var account = _bank.CreateAccount("John", 500m);

        Assert.Throws<ArgumentException>(() => _bank.Transfer(account, account, 100m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Transfer_InvalidAmount_ThrowsArgumentException(decimal amount)
    {
        var from = _bank.CreateAccount("John", 500m);
        var to = _bank.CreateAccount("Jane");

        Assert.Throws<ArgumentException>(() => _bank.Transfer(from, to, amount));
    }

    #endregion

    #region Account Closure Tests

    [Fact]
    public void CloseAccount_ZeroBalance_Succeeds()
    {
        var account = _bank.CreateAccount("John");

        var exception = Record.Exception(() => _bank.CloseAccount(account));

        Assert.Null(exception);
    }

    [Fact]
    public void CloseAccount_WithBalance_ThrowsInvalidOperationException()
    {
        var account = _bank.CreateAccount("John", 100m);

        Assert.Throws<InvalidOperationException>(() => _bank.CloseAccount(account));
    }

    [Fact]
    public void ClosedAccount_CannotDeposit()
    {
        var account = _bank.CreateAccount("John");
        _bank.CloseAccount(account);

        Assert.Throws<InvalidOperationException>(() => _bank.Deposit(account, 100m));
    }

    [Fact]
    public void ClosedAccount_CannotTransferTo()
    {
        var from = _bank.CreateAccount("John", 500m);
        var to = _bank.CreateAccount("Jane");
        _bank.CloseAccount(to);

        Assert.Throws<InvalidOperationException>(() => _bank.Transfer(from, to, 100m));
    }

    #endregion

    #region Transaction History Tests

    [Fact]
    public void GetTransactionHistory_MultipleTransactions_ReturnsInReverseChronologicalOrder()
    {
        var account = _bank.CreateAccount("John", 100m);
        _bank.Deposit(account, 50m, "Deposit 1");
        _bank.Deposit(account, 25m, "Deposit 2");

        var history = _bank.GetTransactionHistory(account).ToList();

        Assert.Equal("Deposit 2", history[0].Description);
        Assert.Equal("Deposit 1", history[1].Description);
        Assert.Equal("Initial deposit", history[2].Description);
    }

    [Fact]
    public void GetTransactionHistory_RecordsBalanceAfterEachTransaction()
    {
        var account = _bank.CreateAccount("John", 100m);
        _bank.Deposit(account, 50m);
        _bank.Withdraw(account, 30m);

        var history = _bank.GetTransactionHistory(account).ToList();

        Assert.Equal(120m, history[0].BalanceAfter);  // After withdrawal
        Assert.Equal(150m, history[1].BalanceAfter);  // After deposit
        Assert.Equal(100m, history[2].BalanceAfter);  // Initial
    }

    #endregion

    #region Concurrency Tests

    [Fact]
    public void ConcurrentDeposits_MaintainsCorrectBalance()
    {
        var account = _bank.CreateAccount("John");
        var tasks = new List<Task>();

        for (int i = 0; i < 100; i++)
        {
            tasks.Add(Task.Run(() => _bank.Deposit(account, 10m)));
        }

        Task.WaitAll(tasks.ToArray());

        Assert.Equal(1000m, _bank.GetBalance(account));
    }

    [Fact]
    public void ConcurrentTransfers_MaintainsTotalBalance()
    {
        var account1 = _bank.CreateAccount("John", 1000m);
        var account2 = _bank.CreateAccount("Jane", 1000m);
        var tasks = new List<Task>();

        for (int i = 0; i < 50; i++)
        {
            tasks.Add(Task.Run(() => _bank.Transfer(account1, account2, 10m)));
            tasks.Add(Task.Run(() => _bank.Transfer(account2, account1, 10m)));
        }

        Task.WaitAll(tasks.ToArray());

        var totalBalance = _bank.GetBalance(account1) + _bank.GetBalance(account2);
        Assert.Equal(2000m, totalBalance);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void CreateMultipleAccounts_GeneratesUniqueNumbers()
    {
        var accounts = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            accounts.Add(_bank.CreateAccount($"User{i}"));
        }

        Assert.Equal(10, accounts.Distinct().Count());
    }

    [Fact]
    public void LargeTransactionAmount_HandledCorrectly()
    {
        var account = _bank.CreateAccount("John", 1_000_000_000m);

        _bank.Withdraw(account, 999_999_999m);

        Assert.Equal(1m, _bank.GetBalance(account));
    }

    [Fact]
    public void DecimalPrecision_Maintained()
    {
        var account = _bank.CreateAccount("John", 100.01m);
        _bank.Deposit(account, 0.99m);

        Assert.Equal(101.00m, _bank.GetBalance(account));
    }

    #endregion
}
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | Basic test structure with [Fact] and Assert |
| 3-4 | [Theory] with InlineData, exception testing |
| 5-6 | Multiple test scenarios, comprehensive coverage |
| 7-8 | Complex business logic testing, edge cases |
| 9 | Concurrency testing, full coverage, professional quality |
