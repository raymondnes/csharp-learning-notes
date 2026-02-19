# Topic 35: JWT Authentication - Assessment Questions

## Level 1 (Trivial)

**Question:** What are the three parts of a JWT token?

**Expected Solution:**
1. **Header** - Contains the token type (JWT) and signing algorithm (e.g., HS256)
2. **Payload** - Contains the claims (user data, expiration, issuer, etc.)
3. **Signature** - Verifies the token hasn't been tampered with

Format: `header.payload.signature`

---

## Level 2 (Trivial)

**Question:** Add the `[Authorize]` attribute to protect these endpoints appropriately.

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAllOrders() { }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id) { }

    [HttpPost]
    public ActionResult<Order> CreateOrder(OrderDto dto) { }

    [HttpDelete("{id}")]
    public ActionResult DeleteOrder(int id) { }
}
```

**Expected Solution:**
```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]  // All endpoints require authentication
public class OrdersController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAllOrders() { }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id) { }

    [HttpPost]
    public ActionResult<Order> CreateOrder(OrderDto dto) { }

    [Authorize(Roles = "Admin")]  // Only admin can delete
    [HttpDelete("{id}")]
    public ActionResult DeleteOrder(int id) { }
}
```

---

## Level 3 (Easy)

**Question:** Create a simple token generation method that creates a JWT with basic claims.

**Expected Solution:**
```csharp
public class TokenService
{
    private readonly string _secretKey;
    private readonly string _issuer;

    public TokenService(IConfiguration config)
    {
        _secretKey = config["JwtSettings:SecretKey"]!;
        _issuer = config["JwtSettings:Issuer"]!;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

---

## Level 4 (Easy)

**Question:** Configure JWT authentication in Program.cs.

**Expected Solution:**
```csharp
var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
```

---

## Level 5 (Moderate)

**Question:** Create a login endpoint that validates credentials and returns a JWT.

**Expected Solution:**
```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(
        IUserRepository users,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _users = users;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        // Find user
        var user = await _users.GetByUsernameAsync(request.Username);
        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        // Verify password
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid credentials" });

        // Generate token
        var token = _tokenService.GenerateToken(user);

        return Ok(new LoginResponse
        {
            Token = token,
            Username = user.Username,
            Role = user.Role,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        });
    }
}

public record LoginRequest(string Username, string Password);

public record LoginResponse
{
    public string Token { get; init; } = "";
    public string Username { get; init; } = "";
    public string Role { get; init; } = "";
    public DateTime ExpiresAt { get; init; }
}
```

---

## Level 6 (Moderate)

**Question:** Get the current user's information from claims in a protected endpoint.

**Expected Solution:**
```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IUserRepository _users;

    public ProfileController(IUserRepository users)
    {
        _users = users;
    }

    [HttpGet]
    public async Task<ActionResult<UserProfile>> GetProfile()
    {
        // Get user ID from claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        // Get full user data
        var user = await _users.GetByIdAsync(userId);
        if (user == null)
            return NotFound();

        return Ok(new UserProfile
        {
            Id = user.Id,
            Username = User.FindFirst(ClaimTypes.Name)?.Value ?? "",
            Email = User.FindFirst(ClaimTypes.Email)?.Value ?? "",
            Role = User.FindFirst(ClaimTypes.Role)?.Value ?? "",
            IsAdmin = User.IsInRole("Admin"),
            // Additional data from database
            CreatedAt = user.CreatedAt,
            LastLogin = user.LastLogin
        });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProfile(UpdateProfileDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var user = await _users.GetByIdAsync(userId);
        if (user == null)
            return NotFound();

        user.Email = dto.Email;
        user.DisplayName = dto.DisplayName;

        await _users.UpdateAsync(user);

        return NoContent();
    }
}
```

---

## Level 7 (Challenging)

**Question:** Implement policy-based authorization with custom requirements.

**Expected Solution:**
```csharp
// Custom requirement
public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int MinimumAge { get; }
    public MinimumAgeRequirement(int minimumAge) => MinimumAge = minimumAge;
}

// Handler
public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var birthDateClaim = context.User.FindFirst("birthdate");
        if (birthDateClaim == null)
        {
            return Task.CompletedTask;
        }

        var birthDate = DateTime.Parse(birthDateClaim.Value);
        var age = DateTime.Today.Year - birthDate.Year;

        if (birthDate.Date > DateTime.Today.AddYears(-age))
            age--;

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

// Resource-based authorization
public class DocumentOwnerRequirement : IAuthorizationRequirement { }

public class DocumentOwnerHandler : AuthorizationHandler<DocumentOwnerRequirement, Document>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DocumentOwnerRequirement requirement,
        Document resource)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null && resource.OwnerId.ToString() == userId)
        {
            context.Succeed(requirement);
        }

        // Admins can access any document
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

// Registration
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));

    options.AddPolicy("AtLeast21", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(21)));

    options.AddPolicy("DocumentOwner", policy =>
        policy.Requirements.Add(new DocumentOwnerRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddScoped<IAuthorizationHandler, DocumentOwnerHandler>();

// Usage in controller
[Authorize(Policy = "AtLeast18")]
[HttpPost("restricted")]
public ActionResult RestrictedAction() { }

// Resource-based in controller
[HttpGet("documents/{id}")]
public async Task<ActionResult<Document>> GetDocument(int id)
{
    var document = await _documents.GetByIdAsync(id);
    if (document == null)
        return NotFound();

    var authResult = await _authorizationService.AuthorizeAsync(
        User, document, "DocumentOwner");

    if (!authResult.Succeeded)
        return Forbid();

    return Ok(document);
}
```

---

## Level 8 (Challenging)

**Question:** Implement refresh token functionality.

**Expected Solution:**
```csharp
public interface ITokenService
{
    TokenPair GenerateTokenPair(User user);
    Task<TokenPair?> RefreshTokensAsync(string refreshToken);
    Task RevokeAllUserTokensAsync(int userId);
}

public class TokenService : ITokenService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public TokenService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public TokenPair GenerateTokenPair(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        _context.SaveChanges();

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(15),
            RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7)
        };
    }

    public async Task<TokenPair?> RefreshTokensAsync(string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedToken == null)
            return null;

        if (storedToken.IsRevoked)
        {
            // Token reuse detected - revoke all tokens for this user
            await RevokeAllUserTokensAsync(storedToken.UserId);
            return null;
        }

        if (storedToken.ExpiresAt < DateTime.UtcNow)
            return null;

        // Revoke the old refresh token
        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;

        // Generate new token pair
        var newPair = GenerateTokenPair(storedToken.User);

        await _context.SaveChangesAsync();

        return newPair;
    }

    public async Task RevokeAllUserTokensAsync(int userId)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }

    private string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}

// Controller endpoint
[HttpPost("refresh")]
public async Task<ActionResult<TokenPair>> RefreshToken(RefreshRequest request)
{
    var newTokens = await _tokenService.RefreshTokensAsync(request.RefreshToken);

    if (newTokens == null)
        return Unauthorized("Invalid or expired refresh token");

    return Ok(newTokens);
}
```

---

## Level 9 (Expert)

**Question:** Implement a complete authentication system with:
- Registration with email validation
- Login with lockout after failed attempts
- JWT + Refresh tokens
- Password reset
- Two-factor authentication support

**Expected Solution:**
```csharp
// See comprehensive implementation in separate file
// Key components:

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterDto dto);
    Task<AuthResult> LoginAsync(LoginDto dto);
    Task<AuthResult> RefreshAsync(string refreshToken);
    Task<bool> LogoutAsync(string refreshToken);
    Task<bool> InitiatePasswordResetAsync(string email);
    Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    Task<bool> EnableTwoFactorAsync(int userId);
    Task<AuthResult> VerifyTwoFactorAsync(TwoFactorDto dto);
}

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly ILogger<AuthService> _logger;

    public async Task<AuthResult> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null)
            return AuthResult.Failed("Invalid credentials");

        // Check lockout
        if (await _userManager.IsLockedOutAsync(user))
        {
            _logger.LogWarning("Locked out user {Username} attempted login", dto.Username);
            return AuthResult.Failed("Account is locked. Try again later.");
        }

        // Verify password
        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            await _userManager.AccessFailedAsync(user);
            var remaining = await GetRemainingAttemptsAsync(user);
            return AuthResult.Failed($"Invalid credentials. {remaining} attempts remaining.");
        }

        // Reset failed attempts on successful login
        await _userManager.ResetAccessFailedCountAsync(user);

        // Check 2FA
        if (user.TwoFactorEnabled)
        {
            return AuthResult.RequiresTwoFactor(user.Id);
        }

        // Generate tokens
        var tokens = _tokenService.GenerateTokenPair(user);

        _logger.LogInformation("User {Username} logged in successfully", user.Username);

        return AuthResult.Success(tokens);
    }

    public async Task<AuthResult> VerifyTwoFactorAsync(TwoFactorDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
            return AuthResult.Failed("Invalid user");

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            _userManager.Options.Tokens.AuthenticatorTokenProvider,
            dto.Code);

        if (!isValid)
            return AuthResult.Failed("Invalid 2FA code");

        var tokens = _tokenService.GenerateTokenPair(user);
        return AuthResult.Success(tokens);
    }

    // Additional methods for password reset, 2FA setup, etc.
}

public class AuthResult
{
    public bool Succeeded { get; set; }
    public string? Error { get; set; }
    public TokenPair? Tokens { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public int? UserId { get; set; }

    public static AuthResult Success(TokenPair tokens) =>
        new() { Succeeded = true, Tokens = tokens };

    public static AuthResult Failed(string error) =>
        new() { Succeeded = false, Error = error };

    public static AuthResult RequiresTwoFactor(int userId) =>
        new() { RequiresTwoFactor = true, UserId = userId };
}
```

---

## Grading Criteria

| Level | Requirements |
|-------|--------------|
| 1-2 | JWT basics, [Authorize] attribute |
| 3-4 | Token generation, configuration |
| 5-6 | Login flow, claims access |
| 7-8 | Policies, refresh tokens |
| 9 | Complete auth system with security features |
