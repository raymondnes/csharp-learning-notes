# Minimal API - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Minimal API concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a basic "Hello World" API.

**Requirements:**
1. Create minimal API project
2. Add root endpoint returning "Hello, World!"
3. Add /about endpoint returning app info
4. Run and test the endpoints

**Expected Output:**
```
Minimal API Running on http://localhost:5000

GET / → "Hello, World!"
GET /about → {"name":"My First API","version":"1.0"}

Both endpoints responding correctly!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Use different HTTP methods.

**Requirements:**
1. MapGet for retrieving data
2. MapPost for creating
3. MapPut for updating
4. MapDelete for removing
5. Each returns appropriate message

**Expected Output:**
```
Testing HTTP Methods:

GET  /items     → "Retrieved all items"
POST /items     → "Created new item"
PUT  /items/1   → "Updated item 1"
DELETE /items/1 → "Deleted item 1"

All HTTP methods working!
```

---

## Level 3: Application (Easy)

**Challenge:** Work with route and query parameters.

**Requirements:**
1. Route parameter: /users/{id}
2. Query parameter: /search?term=value
3. Optional query: /products?page=1&limit=10
4. Multiple route params: /orders/{userId}/{orderId}

**Expected Output:**
```
Parameter Handling:

GET /users/42
  Route param id: 42
  Response: "User ID: 42"

GET /search?term=laptop
  Query param term: laptop
  Response: "Searching for: laptop"

GET /products?page=2
  page: 2, limit: 10 (default)
  Response: "Page 2, showing 10 items"

GET /orders/5/100
  userId: 5, orderId: 100
  Response: "Order 100 for user 5"
```

---

## Level 4: Application (Easy)

**Challenge:** Return different response types.

**Requirements:**
1. Return plain text
2. Return JSON object
3. Use Results.Ok, Results.NotFound, Results.Created
4. Return list of objects

**Expected Output:**
```
Response Types Demo:

GET /text
  Content-Type: text/plain
  Body: "Plain text response"

GET /json
  Content-Type: application/json
  Body: {"message":"JSON response","timestamp":"2024-01-15T10:30:00"}

GET /product/1
  Status: 200 OK
  Body: {"id":1,"name":"Laptop","price":999.99}

GET /product/999
  Status: 404 Not Found
  Body: {"error":"Product not found"}

POST /products
  Status: 201 Created
  Location: /products/3
  Body: {"id":3,"name":"New Product"}

GET /products
  Status: 200 OK
  Body: [{"id":1,...},{"id":2,...}]
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a complete Todo API.

**Requirements:**
1. In-memory list of todos
2. GET all todos
3. GET todo by id
4. POST create todo
5. PUT update todo
6. DELETE todo
7. Filter todos by completion status

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║                    TODO API                                 ║
╠════════════════════════════════════════════════════════════╣

GET /api/todos
  Response: [
    {"id":1,"title":"Learn C#","completed":true},
    {"id":2,"title":"Build API","completed":false}
  ]

GET /api/todos/1
  Response: {"id":1,"title":"Learn C#","completed":true}

GET /api/todos?completed=false
  Response: [{"id":2,"title":"Build API","completed":false}]

POST /api/todos
  Body: {"title":"Deploy App"}
  Response: 201 Created
  {"id":3,"title":"Deploy App","completed":false}

PUT /api/todos/2
  Body: {"title":"Build REST API","completed":true}
  Response: 200 OK
  {"id":2,"title":"Build REST API","completed":true}

DELETE /api/todos/1
  Response: 204 No Content

GET /api/todos
  Response: [
    {"id":2,"title":"Build REST API","completed":true},
    {"id":3,"title":"Deploy App","completed":false}
  ]
```

---

## Level 6: Integration (Moderate)

**Challenge:** Use route groups and organize endpoints.

**Requirements:**
1. Create /api/v1/products group
2. Create /api/v1/categories group
3. Each group has CRUD endpoints
4. Add /api/v1 base group for shared configuration

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              ORGANIZED API ROUTES                           ║
╠════════════════════════════════════════════════════════════╣

Route Groups Configured:

/api/v1 (base group)
├── /products
│   ├── GET    /              → List products
│   ├── GET    /{id}          → Get product
│   ├── POST   /              → Create product
│   ├── PUT    /{id}          → Update product
│   └── DELETE /{id}          → Delete product
└── /categories
    ├── GET    /              → List categories
    ├── GET    /{id}          → Get category
    ├── GET    /{id}/products → Get products in category
    └── POST   /              → Create category

Testing Grouped Routes:

GET /api/v1/products
  → 200 OK: [{...}, {...}]

GET /api/v1/products/1
  → 200 OK: {"id":1,"name":"Laptop",...}

GET /api/v1/categories/2/products
  → 200 OK: Products in category 2
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement request body handling with validation.

**Requirements:**
1. Accept JSON body for create/update
2. Validate required fields
3. Validate data types and ranges
4. Return appropriate error messages
5. Handle malformed JSON

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   REQUEST VALIDATION API                          ║
╠══════════════════════════════════════════════════════════════════╣

=== VALID REQUEST ===

POST /api/products
Body: {"name":"Laptop","price":999.99,"stock":50}

Response: 201 Created
{
  "id": 1,
  "name": "Laptop",
  "price": 999.99,
  "stock": 50,
  "createdAt": "2024-01-15T10:30:00"
}

=== MISSING REQUIRED FIELD ===

POST /api/products
Body: {"price":99.99}

Response: 400 Bad Request
{
  "errors": [
    {"field": "name", "message": "Name is required"}
  ]
}

=== INVALID DATA TYPE ===

POST /api/products
Body: {"name":"Phone","price":"expensive","stock":10}

Response: 400 Bad Request
{
  "errors": [
    {"field": "price", "message": "Price must be a number"}
  ]
}

=== OUT OF RANGE ===

POST /api/products
Body: {"name":"Item","price":-50,"stock":-10}

Response: 400 Bad Request
{
  "errors": [
    {"field": "price", "message": "Price must be positive"},
    {"field": "stock", "message": "Stock cannot be negative"}
  ]
}

=== MALFORMED JSON ===

POST /api/products
Body: {name: "Missing quotes"}

Response: 400 Bad Request
{
  "error": "Invalid JSON format",
  "details": "Unexpected character at position 1"
}
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Add async operations and dependency injection.

**Requirements:**
1. Create IProductRepository interface
2. Implement with simulated async delay
3. Register in DI container
4. Inject into endpoints
5. Use async/await properly

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                ASYNC API WITH DEPENDENCY INJECTION                ║
╠══════════════════════════════════════════════════════════════════╣

DI Container Configuration:
  IProductRepository → ProductRepository (Singleton)
  IOrderRepository → OrderRepository (Scoped)

=== ASYNC OPERATIONS ===

GET /api/products (async)
  [Repository] Fetching products... (100ms delay)
  [Repository] Retrieved 5 products
  Response: 200 OK (105ms total)
  Body: [{...}, {...}, {...}, {...}, {...}]

GET /api/products/1 (async)
  [Repository] Finding product 1... (50ms delay)
  [Repository] Found product
  Response: 200 OK (53ms total)
  Body: {"id":1,"name":"Laptop",...}

POST /api/products (async)
  [Repository] Validating...
  [Repository] Saving product... (150ms delay)
  [Repository] Product saved with ID: 6
  Response: 201 Created (156ms total)

GET /api/products/999 (async)
  [Repository] Finding product 999... (50ms delay)
  [Repository] Product not found
  Response: 404 Not Found (52ms total)

=== CONCURRENT REQUESTS ===

Sending 5 requests simultaneously...

Request 1: GET /api/products/1   → 200 OK (52ms)
Request 2: GET /api/products/2   → 200 OK (51ms)
Request 3: GET /api/products/3   → 200 OK (53ms)
Request 4: GET /api/products/4   → 200 OK (50ms)
Request 5: GET /api/products/5   → 200 OK (52ms)

All requests processed concurrently!
Total time: 55ms (not 250ms if sequential)
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete e-commerce Minimal API.

**Requirements:**
1. Multiple resources: Products, Categories, Orders, Customers
2. Full CRUD for each resource
3. Relationships (orders have products, products have categories)
4. Filtering, sorting, pagination
5. Async operations with DI
6. Comprehensive error handling
7. Request/Response logging

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                     E-COMMERCE MINIMAL API                                ║
╠══════════════════════════════════════════════════════════════════════════╣

=== API DOCUMENTATION ===

Base URL: http://localhost:5000/api

PRODUCTS
  GET    /products                    List all (supports ?category=&minPrice=&sort=)
  GET    /products/{id}               Get by ID
  POST   /products                    Create new
  PUT    /products/{id}               Update
  DELETE /products/{id}               Delete

CATEGORIES
  GET    /categories                  List all
  GET    /categories/{id}             Get by ID
  GET    /categories/{id}/products    Get products in category
  POST   /categories                  Create new
  DELETE /categories/{id}             Delete (fails if has products)

CUSTOMERS
  GET    /customers                   List all
  GET    /customers/{id}              Get by ID
  GET    /customers/{id}/orders       Get customer's orders
  POST   /customers                   Create new
  PUT    /customers/{id}              Update

ORDERS
  GET    /orders                      List all (supports ?status=&customerId=)
  GET    /orders/{id}                 Get by ID (includes items)
  POST   /orders                      Create new order
  PUT    /orders/{id}/status          Update status
  DELETE /orders/{id}                 Cancel order

═══════════════════════════════════════════════════════════════════════════
                            DEMO REQUESTS
═══════════════════════════════════════════════════════════════════════════

[10:30:01] GET /api/products?category=electronics&minPrice=100&sort=price_desc
  [Log] Query: category=electronics, minPrice=100, sort=price_desc
  [Repository] Filtering products...
  [Repository] Found 8 matching products
  Response: 200 OK
  {
    "data": [
      {"id":1,"name":"Laptop","price":999.99,"category":"electronics"},
      {"id":2,"name":"Phone","price":599.99,"category":"electronics"}
    ],
    "total": 8,
    "page": 1,
    "pageSize": 10
  }

[10:30:02] POST /api/orders
  Body: {
    "customerId": 5,
    "items": [
      {"productId": 1, "quantity": 2},
      {"productId": 3, "quantity": 1}
    ]
  }
  [Log] Creating order for customer 5
  [Validation] Customer exists: ✓
  [Validation] Product 1 in stock: ✓
  [Validation] Product 3 in stock: ✓
  [Repository] Calculating totals...
  [Repository] Order created: ORD-001
  Response: 201 Created
  {
    "id": "ORD-001",
    "customerId": 5,
    "items": [...],
    "subtotal": 2149.97,
    "tax": 172.00,
    "total": 2321.97,
    "status": "pending",
    "createdAt": "2024-01-15T10:30:02"
  }

[10:30:03] DELETE /api/categories/3
  [Log] Attempting to delete category 3
  [Validation] Checking for products...
  [Validation] Category has 5 products!
  Response: 400 Bad Request
  {
    "error": "Cannot delete category with products",
    "productCount": 5,
    "suggestion": "Move or delete products first"
  }

[10:30:04] GET /api/customers/5/orders
  [Log] Fetching orders for customer 5
  [Repository] Found 3 orders
  Response: 200 OK
  {
    "customer": {"id":5,"name":"John Doe"},
    "orders": [
      {"id":"ORD-001","total":2321.97,"status":"pending"},
      {"id":"ORD-002","total":599.99,"status":"shipped"},
      {"id":"ORD-003","total":149.99,"status":"delivered"}
    ],
    "totalSpent": 3071.95
  }

═══════════════════════════════════════════════════════════════════════════
                              STATISTICS
═══════════════════════════════════════════════════════════════════════════

API Endpoints: 16
Resources: 4 (Products, Categories, Customers, Orders)

Request Counts:
  GET: 45
  POST: 12
  PUT: 8
  DELETE: 3

Response Codes:
  200 OK: 52
  201 Created: 12
  204 No Content: 2
  400 Bad Request: 3
  404 Not Found: 2

Average Response Time: 35ms

DI Services Registered:
  - IProductRepository (Singleton)
  - ICategoryRepository (Singleton)
  - ICustomerRepository (Singleton)
  - IOrderRepository (Singleton)
  - IOrderCalculator (Transient)

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Multiple resource endpoints with route groups
- Full CRUD operations for each resource
- Query parameter support for filtering/sorting
- Relationship handling between resources
- Comprehensive validation
- Async operations with proper DI
- Consistent error response format
- Request logging

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Endpoints work as expected?
2. **HTTP Methods**: Appropriate methods used?
3. **Responses**: Correct status codes and formats?
4. **Organization**: Well-structured route groups?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Minimal API concepts.*
