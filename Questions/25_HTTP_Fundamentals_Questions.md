# HTTP Fundamentals - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of HTTP Fundamentals concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Identify HTTP methods and their purposes.

**Requirements:**
1. Create a console app that displays a table of HTTP methods
2. Show: Method, Purpose, Has Body, Is Idempotent
3. Include GET, POST, PUT, DELETE, PATCH

**Expected Output:**
```
HTTP Methods Reference:

Method  │ Purpose              │ Has Body │ Idempotent
────────┼──────────────────────┼──────────┼───────────
GET     │ Retrieve data        │ No       │ Yes
POST    │ Create resource      │ Yes      │ No
PUT     │ Replace resource     │ Yes      │ Yes
DELETE  │ Remove resource      │ No*      │ Yes
PATCH   │ Partial update       │ Yes      │ No

* DELETE typically has no body but can have one

Key: Idempotent means multiple identical requests have same effect
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Display HTTP status code categories.

**Requirements:**
1. Create display of status code ranges
2. Show category name and common examples
3. Include 1xx through 5xx ranges

**Expected Output:**
```
HTTP Status Code Categories:

1xx - Informational
  100 Continue
  101 Switching Protocols

2xx - Success
  200 OK - Request succeeded
  201 Created - Resource created
  204 No Content - Success, empty body

3xx - Redirection
  301 Moved Permanently
  302 Found (Temporary redirect)
  304 Not Modified

4xx - Client Errors
  400 Bad Request
  401 Unauthorized
  403 Forbidden
  404 Not Found

5xx - Server Errors
  500 Internal Server Error
  502 Bad Gateway
  503 Service Unavailable
```

---

## Level 3: Application (Easy)

**Challenge:** Parse and display HTTP request components.

**Requirements:**
1. Given a sample HTTP request string, parse it
2. Extract: Method, Path, Headers, Body
3. Display each component clearly

**Expected Output:**
```
Parsing HTTP Request:

Raw Request:
POST /api/users HTTP/1.1
Host: example.com
Content-Type: application/json
Authorization: Bearer abc123

{"name":"Alice","email":"alice@email.com"}

Parsed Components:
──────────────────────────────────────────────
Method: POST
Path: /api/users
HTTP Version: HTTP/1.1

Headers:
  Host: example.com
  Content-Type: application/json
  Authorization: Bearer abc123

Body:
  {"name":"Alice","email":"alice@email.com"}

Body Type: JSON
```

---

## Level 4: Application (Easy)

**Challenge:** Match scenarios to appropriate HTTP methods and status codes.

**Requirements:**
1. Present 5 API scenarios
2. For each, identify correct HTTP method
3. Identify appropriate success and error status codes

**Expected Output:**
```
API Design Scenarios:

Scenario 1: Retrieve all products
  Method: GET
  Endpoint: /api/products
  Success: 200 OK
  Empty: 200 OK (with empty array)

Scenario 2: Create a new user
  Method: POST
  Endpoint: /api/users
  Success: 201 Created
  Duplicate: 409 Conflict

Scenario 3: Update user's email only
  Method: PATCH
  Endpoint: /api/users/{id}
  Success: 200 OK
  Not Found: 404 Not Found

Scenario 4: Replace entire product
  Method: PUT
  Endpoint: /api/products/{id}
  Success: 200 OK
  Invalid Data: 400 Bad Request

Scenario 5: Delete a comment
  Method: DELETE
  Endpoint: /api/comments/{id}
  Success: 204 No Content
  Not Found: 404 Not Found
```

---

## Level 5: Integration (Moderate)

**Challenge:** Design RESTful endpoints for a resource.

**Requirements:**
1. Design complete REST API for "Books" resource
2. Include nested resource for "Reviews"
3. Show all CRUD operations
4. Include query parameters for filtering

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              BOOKS API DESIGN                               ║
╠════════════════════════════════════════════════════════════╣

=== BOOKS RESOURCE ===

GET    /api/books
  Query: ?author=name&genre=fiction&page=1&limit=10
  Response: 200 OK - Array of books

GET    /api/books/{id}
  Response: 200 OK - Single book
  Error: 404 Not Found

POST   /api/books
  Body: { title, author, isbn, genre, price }
  Response: 201 Created - New book with id
  Error: 400 Bad Request - Validation failed

PUT    /api/books/{id}
  Body: { title, author, isbn, genre, price }
  Response: 200 OK - Updated book
  Error: 404 Not Found

DELETE /api/books/{id}
  Response: 204 No Content
  Error: 404 Not Found

=== REVIEWS (Nested Resource) ===

GET    /api/books/{bookId}/reviews
  Response: 200 OK - Array of reviews for book

GET    /api/books/{bookId}/reviews/{reviewId}
  Response: 200 OK - Single review
  Error: 404 Not Found

POST   /api/books/{bookId}/reviews
  Body: { rating, comment, userId }
  Response: 201 Created
  Error: 400 Bad Request

DELETE /api/books/{bookId}/reviews/{reviewId}
  Response: 204 No Content
```

---

## Level 6: Integration (Moderate)

**Challenge:** Simulate HTTP request/response handling.

**Requirements:**
1. Create classes: HttpRequest, HttpResponse
2. Simulate routing different paths to handlers
3. Return appropriate responses based on method
4. Handle 404 for unknown routes

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              HTTP REQUEST SIMULATOR                         ║
╠════════════════════════════════════════════════════════════╣

Processing Request 1:
  Request: GET /api/products
  Routing to: ProductsHandler
  Response: 200 OK
  Body: [{"id":1,"name":"Laptop"},{"id":2,"name":"Phone"}]

Processing Request 2:
  Request: POST /api/products
  Body: {"name":"Tablet","price":299}
  Routing to: ProductsHandler
  Response: 201 Created
  Location: /api/products/3
  Body: {"id":3,"name":"Tablet","price":299}

Processing Request 3:
  Request: GET /api/users/5
  Routing to: UsersHandler
  Response: 404 Not Found
  Body: {"error":"User with ID 5 not found"}

Processing Request 4:
  Request: DELETE /api/unknown
  No route found
  Response: 404 Not Found
  Body: {"error":"Endpoint not found"}

Requests processed: 4
Success: 2
Client errors: 2
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Build a complete HTTP message parser and builder.

**Requirements:**
1. Parse raw HTTP request strings into objects
2. Build HTTP response strings from objects
3. Handle headers, query strings, body
4. Support multiple content types

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   HTTP MESSAGE HANDLER                            ║
╠══════════════════════════════════════════════════════════════════╣

=== PARSING REQUEST ===

Raw Input:
POST /api/products?category=electronics HTTP/1.1
Host: api.example.com
Content-Type: application/json
Authorization: Bearer token123
Content-Length: 42

{"name":"Smart Watch","price":199.99}

Parsed Request Object:
  Method: POST
  Path: /api/products
  Query Parameters:
    - category: electronics
  HTTP Version: 1.1
  Headers (4):
    - Host: api.example.com
    - Content-Type: application/json
    - Authorization: Bearer token123
    - Content-Length: 42
  Body: {"name":"Smart Watch","price":199.99}
  Body Parsed: { name: "Smart Watch", price: 199.99 }

=== BUILDING RESPONSE ===

Response Object:
  Status: 201 Created
  Headers:
    - Content-Type: application/json
    - Location: /api/products/15
  Body: { id: 15, name: "Smart Watch", price: 199.99 }

Built HTTP Response:
HTTP/1.1 201 Created
Content-Type: application/json
Location: /api/products/15
Content-Length: 48

{"id":15,"name":"Smart Watch","price":199.99}

=== CONTENT TYPE HANDLING ===

JSON: application/json
  Body: {"key":"value"}
  Parsed: Dictionary with 1 entry

Form Data: application/x-www-form-urlencoded
  Body: name=John&age=30
  Parsed: name=John, age=30

Plain Text: text/plain
  Body: Hello, World!
  Parsed: Raw string
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Implement a mock API server with routing.

**Requirements:**
1. Create Router class that maps paths to handlers
2. Support path parameters (e.g., /users/{id})
3. Support different methods for same path
4. Return appropriate error responses
5. Log all requests

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      MOCK API SERVER                              ║
╠══════════════════════════════════════════════════════════════════╣

Server started. Registered routes:
  GET     /api/users           → GetAllUsers
  GET     /api/users/{id}      → GetUserById
  POST    /api/users           → CreateUser
  PUT     /api/users/{id}      → UpdateUser
  DELETE  /api/users/{id}      → DeleteUser
  GET     /api/products        → GetProducts

═══════════════════════════════════════════════════════════════════
                         REQUEST LOG
═══════════════════════════════════════════════════════════════════

[10:30:01] GET /api/users
  Route: GET /api/users → GetAllUsers
  Response: 200 OK
  Body: [{"id":1,"name":"Alice"},{"id":2,"name":"Bob"}]
  Time: 5ms

[10:30:02] GET /api/users/1
  Route: GET /api/users/{id} → GetUserById
  Path Params: { id: 1 }
  Response: 200 OK
  Body: {"id":1,"name":"Alice","email":"alice@test.com"}
  Time: 3ms

[10:30:03] POST /api/users
  Route: POST /api/users → CreateUser
  Request Body: {"name":"Carol","email":"carol@test.com"}
  Response: 201 Created
  Location: /api/users/3
  Body: {"id":3,"name":"Carol","email":"carol@test.com"}
  Time: 8ms

[10:30:04] GET /api/users/99
  Route: GET /api/users/{id} → GetUserById
  Path Params: { id: 99 }
  Response: 404 Not Found
  Body: {"error":"User not found","id":99}
  Time: 2ms

[10:30:05] PUT /api/users/1
  Route: PUT /api/users/{id} → UpdateUser
  Path Params: { id: 1 }
  Request Body: {"name":"Alice Updated"}
  Response: 200 OK
  Time: 4ms

[10:30:06] POST /api/unknown
  No matching route
  Response: 404 Not Found
  Body: {"error":"Endpoint not found"}
  Time: 1ms

═══════════════════════════════════════════════════════════════════
                          STATISTICS
═══════════════════════════════════════════════════════════════════
Total Requests: 6
  GET: 3
  POST: 2
  PUT: 1

Status Codes:
  200 OK: 3
  201 Created: 1
  404 Not Found: 2

Average Response Time: 3.8ms
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a comprehensive HTTP client/server simulation.

**Requirements:**
1. Full HTTP message parsing (request & response)
2. Complete routing system with middleware concept
3. Request validation and error handling
4. Response caching simulation
5. Rate limiting simulation
6. Request/Response logging
7. Support for all common HTTP methods

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    HTTP CLIENT/SERVER SIMULATION                          ║
╠══════════════════════════════════════════════════════════════════════════╣

=== SERVER CONFIGURATION ===

Middleware Pipeline:
  1. Logging Middleware
  2. Rate Limiter (100 req/min)
  3. Cache Middleware
  4. Authentication Middleware
  5. Router

Routes Registered: 8
Cache Enabled: Yes (TTL: 60s)
Rate Limit: 100 requests/minute

═══════════════════════════════════════════════════════════════════════════
                           REQUEST PROCESSING
═══════════════════════════════════════════════════════════════════════════

Request #1: GET /api/products
─────────────────────────────────────────────────────────────────────────────
[Logging] Incoming: GET /api/products
[RateLimiter] Allowed (1/100)
[Cache] MISS - Fetching from handler
[Auth] Public endpoint - No auth required
[Router] Matched: GET /api/products → ProductsController.GetAll

Response: 200 OK
Headers:
  Content-Type: application/json
  X-Cache: MISS
  X-RateLimit-Remaining: 99
Body: [{"id":1,"name":"Laptop"},{"id":2,"name":"Phone"}]
Time: 45ms

Request #2: GET /api/products (repeat)
─────────────────────────────────────────────────────────────────────────────
[Logging] Incoming: GET /api/products
[RateLimiter] Allowed (2/100)
[Cache] HIT - Returning cached response
[Auth] Skipped (cached)
[Router] Skipped (cached)

Response: 200 OK
Headers:
  Content-Type: application/json
  X-Cache: HIT
  X-Cache-Age: 5s
Body: [{"id":1,"name":"Laptop"},{"id":2,"name":"Phone"}]
Time: 2ms (cached)

Request #3: POST /api/products (no auth)
─────────────────────────────────────────────────────────────────────────────
[Logging] Incoming: POST /api/products
[RateLimiter] Allowed (3/100)
[Cache] SKIP - Non-GET request
[Auth] FAILED - Authorization header required

Response: 401 Unauthorized
Headers:
  Content-Type: application/json
  WWW-Authenticate: Bearer
Body: {"error":"Authentication required","code":"AUTH_MISSING"}
Time: 3ms

Request #4: POST /api/products (with auth)
─────────────────────────────────────────────────────────────────────────────
[Logging] Incoming: POST /api/products
[RateLimiter] Allowed (4/100)
[Cache] SKIP - Non-GET request
[Auth] Valid token for user: admin
[Router] Matched: POST /api/products → ProductsController.Create
[Validation] Body valid

Response: 201 Created
Headers:
  Content-Type: application/json
  Location: /api/products/3
Body: {"id":3,"name":"Tablet","price":299.99}
Time: 35ms

Request #5: GET /api/products/999
─────────────────────────────────────────────────────────────────────────────
[Logging] Incoming: GET /api/products/999
[RateLimiter] Allowed (5/100)
[Cache] MISS
[Auth] Public endpoint
[Router] Matched: GET /api/products/{id} → ProductsController.GetById
[Handler] Product 999 not found

Response: 404 Not Found
Headers:
  Content-Type: application/json
Body: {"error":"Product not found","productId":999}
Time: 8ms

Request #6-106: Rapid requests (rate limit test)
─────────────────────────────────────────────────────────────────────────────
[RateLimiter] Requests 6-100: Allowed
[RateLimiter] Requests 101-106: BLOCKED

Response: 429 Too Many Requests
Headers:
  Retry-After: 45
  X-RateLimit-Remaining: 0
Body: {"error":"Rate limit exceeded","retryAfter":45}

═══════════════════════════════════════════════════════════════════════════
                              STATISTICS
═══════════════════════════════════════════════════════════════════════════

Total Requests: 106

By Method:
  GET: 103
  POST: 3

By Status:
  200 OK: 98
  201 Created: 1
  401 Unauthorized: 1
  404 Not Found: 1
  429 Too Many Requests: 5

Cache Performance:
  Hits: 97
  Misses: 6
  Hit Rate: 94.2%

Rate Limiting:
  Allowed: 100
  Blocked: 6

Middleware Processing:
  Avg Logging Time: 0.1ms
  Avg Auth Time: 0.5ms
  Avg Cache Check: 0.2ms
  Avg Handler Time: 25ms

Authentication:
  Public Requests: 100
  Authenticated: 3
  Failed Auth: 3

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Implement complete HTTP message classes
- Build configurable middleware pipeline
- Implement route matching with path parameters
- Add caching with TTL
- Add rate limiting logic
- Comprehensive request/response logging
- Error handling for all scenarios

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: HTTP concepts applied properly?
2. **Methods/Status**: Appropriate codes used?
3. **Structure**: Proper request/response handling?
4. **Design**: RESTful principles followed?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of HTTP Fundamentals concepts.*
