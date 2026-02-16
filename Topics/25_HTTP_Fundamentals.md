# HTTP Fundamentals

## Introduction

HTTP (Hypertext Transfer Protocol) is the foundation of data communication on the web. Understanding HTTP is essential for building web applications and APIs with ASP.NET Core.

## What is HTTP?

HTTP is a request-response protocol between clients and servers:

```
Client (Browser/App)          Server (ASP.NET Core)
        |                              |
        |-------- HTTP Request ------->|
        |                              |
        |<------- HTTP Response -------|
        |                              |
```

## HTTP Request Structure

An HTTP request consists of:

```
GET /api/products/5 HTTP/1.1
Host: example.com
Accept: application/json
Authorization: Bearer token123

[Optional Request Body]
```

### Request Line
- **Method**: GET, POST, PUT, DELETE, etc.
- **Path**: /api/products/5
- **Version**: HTTP/1.1 or HTTP/2

### Headers
Key-value pairs providing metadata:
```
Host: example.com
Content-Type: application/json
Accept: application/json
Authorization: Bearer abc123
User-Agent: Mozilla/5.0
```

### Body (Optional)
Data sent with POST, PUT, PATCH requests:
```json
{
  "name": "New Product",
  "price": 29.99
}
```

## HTTP Response Structure

```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 85

{
  "id": 5,
  "name": "Product Name",
  "price": 29.99
}
```

### Status Line
- **Version**: HTTP/1.1
- **Status Code**: 200
- **Reason Phrase**: OK

### Headers
```
Content-Type: application/json
Content-Length: 85
Date: Mon, 15 Jan 2024 10:30:00 GMT
```

### Body
The response data (JSON, HTML, etc.)

## HTTP Methods (Verbs)

| Method | Purpose | Has Body | Idempotent |
|--------|---------|----------|------------|
| GET | Retrieve data | No | Yes |
| POST | Create resource | Yes | No |
| PUT | Update/Replace | Yes | Yes |
| PATCH | Partial update | Yes | No |
| DELETE | Remove resource | Usually No | Yes |
| HEAD | Get headers only | No | Yes |
| OPTIONS | Get allowed methods | No | Yes |

### GET - Retrieve Data
```
GET /api/products HTTP/1.1
Host: example.com

Response: List of products
```

### POST - Create Resource
```
POST /api/products HTTP/1.1
Host: example.com
Content-Type: application/json

{
  "name": "New Product",
  "price": 19.99
}

Response: Created product with ID
```

### PUT - Replace Resource
```
PUT /api/products/5 HTTP/1.1
Host: example.com
Content-Type: application/json

{
  "id": 5,
  "name": "Updated Product",
  "price": 24.99
}

Response: Updated product
```

### DELETE - Remove Resource
```
DELETE /api/products/5 HTTP/1.1
Host: example.com

Response: 204 No Content (success)
```

## HTTP Status Codes

### 1xx - Informational
- 100 Continue
- 101 Switching Protocols

### 2xx - Success
- **200 OK** - Request succeeded
- **201 Created** - Resource created
- **204 No Content** - Success, no body returned

### 3xx - Redirection
- **301 Moved Permanently**
- **302 Found (Temporary Redirect)**
- **304 Not Modified** (cached)

### 4xx - Client Errors
- **400 Bad Request** - Invalid request
- **401 Unauthorized** - Authentication required
- **403 Forbidden** - Not allowed
- **404 Not Found** - Resource doesn't exist
- **405 Method Not Allowed**
- **409 Conflict** - Resource conflict
- **422 Unprocessable Entity** - Validation failed

### 5xx - Server Errors
- **500 Internal Server Error**
- **502 Bad Gateway**
- **503 Service Unavailable**

## Common HTTP Headers

### Request Headers
```
Accept: application/json          // Preferred response format
Content-Type: application/json    // Body format
Authorization: Bearer <token>     // Authentication
User-Agent: Mozilla/5.0           // Client info
Cookie: session=abc123            // Session data
```

### Response Headers
```
Content-Type: application/json    // Body format
Content-Length: 1234              // Body size
Cache-Control: max-age=3600       // Caching rules
Set-Cookie: session=abc123        // Set client cookie
Location: /api/products/5         // Redirect location
```

## Content Types (MIME Types)

```
application/json        // JSON data
application/xml         // XML data
text/html               // HTML document
text/plain              // Plain text
multipart/form-data     // File uploads
application/x-www-form-urlencoded  // Form data
```

## Query Strings

Pass parameters in URL:
```
GET /api/products?category=electronics&minPrice=100&page=2
```

Parsed as:
- category = "electronics"
- minPrice = "100"
- page = "2"

## RESTful URL Patterns

REST (Representational State Transfer) conventions:

```
GET    /api/products          // Get all products
GET    /api/products/5        // Get product with ID 5
POST   /api/products          // Create new product
PUT    /api/products/5        // Replace product 5
PATCH  /api/products/5        // Update product 5 partially
DELETE /api/products/5        // Delete product 5

// Nested resources
GET    /api/customers/3/orders     // Get orders for customer 3
POST   /api/customers/3/orders     // Create order for customer 3
```

## HTTP in ASP.NET Core

### Accessing Request Data
```csharp
app.MapGet("/api/example", (HttpContext context) =>
{
    // Request method
    string method = context.Request.Method;

    // Request path
    string path = context.Request.Path;

    // Query string
    string? category = context.Request.Query["category"];

    // Headers
    string? auth = context.Request.Headers["Authorization"];

    // Content type
    string? contentType = context.Request.ContentType;

    return Results.Ok(new { method, path, category });
});
```

### Setting Response Data
```csharp
app.MapGet("/api/example", (HttpContext context) =>
{
    // Set status code
    context.Response.StatusCode = 200;

    // Set headers
    context.Response.Headers["X-Custom-Header"] = "Value";

    // Return with content type
    return Results.Json(new { message = "Hello" });
});
```

### Common Results
```csharp
Results.Ok(data)              // 200 with data
Results.Created("/url", data) // 201 with location
Results.NoContent()           // 204
Results.BadRequest(errors)    // 400
Results.NotFound()            // 404
Results.StatusCode(500)       // Custom status
```

## Request/Response Cycle

```
1. Client sends HTTP Request
   ↓
2. Server receives request
   ↓
3. Server routes to endpoint
   ↓
4. Endpoint processes request
   ↓
5. Server builds HTTP Response
   ↓
6. Client receives response
```

## Statelessness

HTTP is stateless - each request is independent:
- Server doesn't remember previous requests
- Client must send all needed info each time
- State maintained via cookies, tokens, or sessions

## HTTPS

HTTP Secure (HTTPS) encrypts communication:
- Uses TLS/SSL encryption
- Required for sensitive data
- Default in modern web development

```
https://example.com/api/products
```

## Summary

- HTTP is request-response protocol for web
- Methods: GET, POST, PUT, DELETE define actions
- Status codes indicate success/failure
- Headers provide metadata
- Body contains request/response data
- REST conventions organize API endpoints
- ASP.NET Core provides full HTTP access

Understanding HTTP is fundamental to building web APIs.
