# Topic 35: JWT Authentication

## Introduction

JSON Web Tokens (JWT) are the standard for securing modern APIs. A JWT is a compact, URL-safe token that contains claims about the user, digitally signed to ensure authenticity.

## JWT Structure

A JWT consists of three parts separated by dots: `header.payload.signature`

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4ifQ.
SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

### Header
```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

### Payload (Claims)
```json
{
  "sub": "1234567890",
  "name": "John Doe",
  "email": "john@example.com",
  "role": "Admin",
  "iat": 1516239022,
  "exp": 1516242622
}
```

### Signature
```
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret
)
```

## Setting Up JWT in ASP.NET Core

### 1. Install Packages

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### 2. Configure Services

```csharp
// Program.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero  // No tolerance for expiration
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Add middleware (order matters!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
```

### 3. Configuration (appsettings.json)

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32Characters!",
    "Issuer": "YourAppName",
    "Audience": "YourAppUsers",
    "ExpirationMinutes": 60
  }
}
```

## Token Generation Service

```csharp
public interface ITokenService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        var secretKey = _config["JwtSettings:SecretKey"]!;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("department", user.Department ?? ""),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(
                double.Parse(_config["JwtSettings:ExpirationMinutes"]!)),
            SigningCredentials = credentials,
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = true,
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
```

## Authentication Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterDto dto)
    {
        if (await _userService.UsernameExistsAsync(dto.Username))
            return BadRequest("Username already exists");

        var user = await _userService.CreateUserAsync(dto);
        var token = _tokenService.GenerateToken(user);

        return Ok(new AuthResponse
        {
            Token = token,
            Username = user.Username,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginDto dto)
    {
        var user = await _userService.ValidateUserAsync(dto.Username, dto.Password);

        if (user == null)
            return Unauthorized("Invalid username or password");

        var token = _tokenService.GenerateToken(user);

        return Ok(new AuthResponse
        {
            Token = token,
            Username = user.Username,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        });
    }
}

public class RegisterDto
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class LoginDto
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

public class AuthResponse
{
    public string Token { get; set; } = "";
    public string Username { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
}
```

## Protecting Endpoints

### Basic Authorization

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // Public - no auth required
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_products);
    }

    // Requires authentication
    [Authorize]
    [HttpPost]
    public ActionResult<Product> Create(ProductDto dto)
    {
        // Only authenticated users can create
        return Ok(new Product());
    }

    // Requires specific role
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        // Only admins can delete
        return NoContent();
    }

    // Multiple roles (any of these)
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public ActionResult Update(int id, ProductDto dto)
    {
        return Ok();
    }
}
```

### Policy-Based Authorization

```csharp
// Configure policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("AtLeast18", policy =>
        policy.RequireClaim("age", "18", "19", "20")); // etc.

    options.AddPolicy("CanEditProducts", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("permission", "products.edit") ||
            context.User.IsInRole("Admin")));

    options.AddPolicy("SameDepartment", policy =>
        policy.Requirements.Add(new SameDepartmentRequirement()));
});

// Use policies
[Authorize(Policy = "CanEditProducts")]
[HttpPut("{id}")]
public ActionResult UpdateProduct(int id) { }
```

### Custom Authorization Handler

```csharp
public class SameDepartmentRequirement : IAuthorizationRequirement { }

public class SameDepartmentHandler : AuthorizationHandler<SameDepartmentRequirement, Product>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SameDepartmentRequirement requirement,
        Product resource)
    {
        var userDepartment = context.User.FindFirst("department")?.Value;

        if (resource.Department == userDepartment)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

// Register handler
builder.Services.AddScoped<IAuthorizationHandler, SameDepartmentHandler>();
```

## Accessing User Claims

```csharp
[Authorize]
[HttpGet("profile")]
public ActionResult<UserProfile> GetProfile()
{
    // Get claims from the authenticated user
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var username = User.FindFirst(ClaimTypes.Name)?.Value;
    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;

    // Or use the User.Identity
    var name = User.Identity?.Name;
    var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

    // Check role
    var isAdmin = User.IsInRole("Admin");

    return Ok(new UserProfile
    {
        UserId = userId,
        Username = username,
        Email = email,
        Role = role,
        IsAdmin = isAdmin
    });
}
```

## Refresh Tokens

For longer sessions, implement refresh tokens:

```csharp
public class TokenPair
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public DateTime AccessTokenExpiry { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = "";
    public int UserId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
}

public interface ITokenService
{
    TokenPair GenerateTokenPair(User user);
    Task<TokenPair?> RefreshTokensAsync(string refreshToken);
    Task RevokeRefreshTokenAsync(string refreshToken);
}

public class TokenService : ITokenService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public TokenPair GenerateTokenPair(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        // Store refresh token in database
        _context.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        });
        _context.SaveChanges();

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiry = DateTime.UtcNow.AddMinutes(15),
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };
    }

    public async Task<TokenPair?> RefreshTokensAsync(string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedToken == null || storedToken.IsRevoked ||
            storedToken.ExpiresAt < DateTime.UtcNow)
        {
            return null;
        }

        // Revoke old token
        storedToken.IsRevoked = true;

        // Generate new pair
        return GenerateTokenPair(storedToken.User);
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
```

## Complete Auth Flow Example

```csharp
// User Service
public interface IUserService
{
    Task<User> CreateUserAsync(RegisterDto dto);
    Task<User?> ValidateUserAsync(string username, string password);
    Task<bool> UsernameExistsAsync(string username);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(RegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> ValidateUserAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
}
```

## Security Best Practices

1. **Use strong secrets** (at least 256 bits / 32 characters)
2. **Short access token lifetime** (15-60 minutes)
3. **Store secrets securely** (environment variables, Azure Key Vault)
4. **Use HTTPS only**
5. **Validate all claims**
6. **Implement token refresh** for better UX
7. **Log authentication events**

## Summary

| Component | Purpose |
|-----------|---------|
| JWT | Stateless authentication token |
| Claims | User information in token |
| [Authorize] | Protect endpoints |
| Roles | Group-based access control |
| Policies | Fine-grained authorization |
| Refresh Token | Extend sessions securely |

JWT authentication is essential for modern API development and microservices architectures.
