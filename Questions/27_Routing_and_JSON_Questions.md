# Routing and JSON - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Routing and JSON concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create basic static routes.

**Requirements:**
1. Create routes for /, /about, /contact
2. Each returns appropriate text
3. Create /api/status that returns JSON

**Expected Output:**
```
Static Routes:

GET /        → "Welcome to the Home Page"
GET /about   → "About Us Page"
GET /contact → "Contact Us Page"
GET /api/status → {"status":"running","version":"1.0"}
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Use route parameters.

**Requirements:**
1. Create /users/{id} with int parameter
2. Create /greet/{name} with string parameter
3. Return parameter values in response

**Expected Output:**
```
Route Parameters:

GET /users/42
  Parameter: id = 42
  Response: "User ID: 42"

GET /greet/Alice
  Parameter: name = Alice
  Response: "Hello, Alice!"

GET /products/laptop/reviews/5
  Parameters: product = laptop, reviewId = 5
  Response: "Review 5 for laptop"
```

---

## Level 3: Application (Easy)

**Challenge:** Apply route constraints.

**Requirements:**
1. {id:int} - integer only
2. {name:alpha} - letters only
3. {rating:range(1,5)} - specific range
4. {code:length(3)} - exact length
5. Show what matches and what doesn't

**Expected Output:**
```
Route Constraints:

/users/{id:int}
  GET /users/123    → ✓ "User 123"
  GET /users/abc    → ✗ 404 Not Found

/category/{name:alpha}
  GET /category/electronics → ✓ "Category: electronics"
  GET /category/item123     → ✗ 404 Not Found

/rating/{value:range(1,5)}
  GET /rating/3     → ✓ "Rating: 3 stars"
  GET /rating/10    → ✗ 404 Not Found

/country/{code:length(2)}
  GET /country/US   → ✓ "Country: US"
  GET /country/USA  → ✗ 404 Not Found
```

---

## Level 4: Application (Easy)

**Challenge:** Work with query string parameters.

**Requirements:**
1. Required query parameter
2. Optional query parameters with defaults
3. Multiple query parameters
4. Parse and display all values

**Expected Output:**
```
Query Parameters:

GET /search?q=laptop
  Required: q = "laptop"
  Response: "Searching for: laptop"

GET /products?page=2&size=20
  page = 2 (default was 1)
  size = 20 (default was 10)
  Response: "Page 2, showing 20 items"

GET /products
  page = 1 (default)
  size = 10 (default)
  Response: "Page 1, showing 10 items"

GET /filter?category=electronics&minPrice=100&maxPrice=500&inStock=true
  category = electronics
  minPrice = 100
  maxPrice = 500
  inStock = true
  Response: Filtered products list
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a searchable product API.

**Requirements:**
1. GET /api/products - list all
2. GET /api/products/{id:int} - by ID
3. GET /api/products/category/{name} - by category
4. Query params: ?minPrice, ?maxPrice, ?sort
5. Return proper JSON responses

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              PRODUCT SEARCH API                             ║
╠════════════════════════════════════════════════════════════╣

GET /api/products
  Response: [
    {"id":1,"name":"Laptop","category":"Electronics","price":999.99},
    {"id":2,"name":"Shirt","category":"Clothing","price":29.99},
    {"id":3,"name":"Phone","category":"Electronics","price":599.99}
  ]

GET /api/products/1
  Response: {"id":1,"name":"Laptop","category":"Electronics","price":999.99}

GET /api/products/category/Electronics
  Response: [
    {"id":1,"name":"Laptop",...},
    {"id":3,"name":"Phone",...}
  ]

GET /api/products?minPrice=100&maxPrice=700&sort=price
  Response: [
    {"id":3,"name":"Phone","price":599.99},
    {"id":1,"name":"Laptop","price":999.99}  // excluded: > 700
  ]
  Actually: [{"id":3,"name":"Phone","price":599.99}]

GET /api/products/999
  Response: 404 Not Found
  {"error":"Product not found","id":999}
```

---

## Level 6: Integration (Moderate)

**Challenge:** Configure JSON serialization options.

**Requirements:**
1. Set up custom JSON options
2. Use PascalCase property names
3. Handle enums as strings
4. Format dates properly
5. Demonstrate attribute usage

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              JSON CONFIGURATION                             ║
╠════════════════════════════════════════════════════════════╣

Default JSON (camelCase):
{
  "id": 1,
  "productName": "laptop",
  "status": 1,
  "createdAt": "2024-01-15T10:30:00Z"
}

Configured JSON (PascalCase + Enums as strings):
{
  "Id": 1,
  "ProductName": "Laptop",
  "Status": "Active",
  "CreatedAt": "2024-01-15T10:30:00Z"
}

With Attributes:
{
  "product_id": 1,          // [JsonPropertyName]
  "name": "Laptop",
  "price": 999.99
  // internalCode omitted   // [JsonIgnore]
}

Pretty Printed:
{
  "Id": 1,
  "Name": "Laptop",
  "Price": 999.99,
  "Category": {
    "Id": 1,
    "Name": "Electronics"
  }
}
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create nested resource routes.

**Requirements:**
1. /api/customers/{id}/orders - customer's orders
2. /api/customers/{id}/orders/{orderId} - specific order
3. /api/categories/{id}/products - category products
4. /api/products/{id}/reviews - product reviews
5. Proper JSON responses with relationships

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   NESTED RESOURCE ROUTES                          ║
╠══════════════════════════════════════════════════════════════════╣

GET /api/customers/5/orders
  Route: customers/{customerId:int}/orders
  Parameters: customerId = 5
  Response:
  {
    "customer": {"id":5,"name":"John Doe"},
    "orders": [
      {"id":101,"total":150.00,"status":"shipped"},
      {"id":102,"total":299.99,"status":"pending"}
    ],
    "orderCount": 2
  }

GET /api/customers/5/orders/101
  Route: customers/{customerId}/orders/{orderId}
  Parameters: customerId = 5, orderId = 101
  Response:
  {
    "orderId": 101,
    "customer": {"id":5,"name":"John Doe"},
    "items": [
      {"productId":1,"name":"Laptop","quantity":1,"price":999.99}
    ],
    "total": 999.99,
    "status": "shipped"
  }

GET /api/categories/2/products
  Route: categories/{categoryId}/products
  Response:
  {
    "category": {"id":2,"name":"Electronics"},
    "products": [
      {"id":1,"name":"Laptop","price":999.99},
      {"id":3,"name":"Phone","price":599.99}
    ]
  }

POST /api/products/1/reviews
  Body: {"rating":5,"comment":"Great product!","userId":10}
  Response: 201 Created
  Location: /api/products/1/reviews/15
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Build an API with complete routing patterns.

**Requirements:**
1. Use route groups for organization
2. Support catch-all routes for file paths
3. Optional route segments
4. Multiple route constraints combined
5. API versioning through routes

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                 ADVANCED ROUTING PATTERNS                         ║
╠══════════════════════════════════════════════════════════════════╣

=== ROUTE GROUPS ===

/api/v1/products/*     (Version 1)
/api/v2/products/*     (Version 2)

GET /api/v1/products
  Response: v1 format (flat)
  [{"id":1,"name":"Laptop"}]

GET /api/v2/products
  Response: v2 format (detailed)
  {"data":[{"id":1,"name":"Laptop","metadata":{...}}],"apiVersion":"2.0"}

=== CATCH-ALL ROUTES ===

GET /files/{*path}
  /files/documents/report.pdf
  Path captured: "documents/report.pdf"
  Response: File info for documents/report.pdf

=== OPTIONAL SEGMENTS ===

GET /products/{category?}
  /products           → All products
  /products/electronics → Electronics only

=== COMBINED CONSTRAINTS ===

GET /users/{id:int:min(1):max(10000)}
  /users/0      → 404 (below min)
  /users/5000   → 200 OK
  /users/50000  → 404 (above max)

GET /code/{value:alpha:length(3,5)}
  /code/AB      → 404 (too short)
  /code/ABC     → 200 OK
  /code/ABCDEF  → 404 (too long)

=== ROUTE MATCHING ORDER ===

Routes registered:
  1. /api/products/featured      (literal)
  2. /api/products/{id:int}      (constrained)
  3. /api/products/{name}        (catch-all string)

GET /api/products/featured → Matches #1 (literal first)
GET /api/products/123      → Matches #2 (constraint)
GET /api/products/laptop   → Matches #3 (fallback)
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete REST API with advanced routing and JSON handling.

**Requirements:**
1. Full CRUD for multiple resources
2. Nested routes with relationships
3. Complex filtering via query strings
4. Pagination with metadata
5. Custom JSON formatting
6. API versioning
7. Route documentation

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    COMPLETE REST API DESIGN                               ║
╠══════════════════════════════════════════════════════════════════════════╣

=== API ROUTES ===

/api/v1
├── /products
│   ├── GET    /                     List (paginated, filterable)
│   ├── GET    /{id:int}             Get by ID
│   ├── GET    /{id:int}/reviews     Get product reviews
│   ├── POST   /                     Create
│   ├── PUT    /{id:int}             Update
│   ├── DELETE /{id:int}             Delete
│   └── GET    /search/{*query}      Full-text search
├── /categories
│   ├── GET    /                     List all
│   ├── GET    /{id:int}             Get by ID
│   ├── GET    /{id:int}/products    Get category products
│   └── GET    /{slug:alpha}         Get by slug
└── /orders
    ├── GET    /                     List (filterable by status, date)
    ├── GET    /{id:guid}            Get by GUID
    └── POST   /                     Create

=== FILTERING & PAGINATION ===

GET /api/v1/products?category=electronics&minPrice=100&maxPrice=1000&sort=price_desc&page=2&pageSize=20

Response:
{
  "data": [
    {"id":5,"name":"Tablet","price":499.99,...},
    {"id":8,"name":"Headphones","price":199.99,...}
  ],
  "pagination": {
    "page": 2,
    "pageSize": 20,
    "totalItems": 45,
    "totalPages": 3,
    "hasNext": true,
    "hasPrevious": true
  },
  "filters": {
    "category": "electronics",
    "priceRange": {"min":100,"max":1000}
  },
  "sort": {"field":"price","direction":"desc"},
  "links": {
    "self": "/api/v1/products?page=2&...",
    "first": "/api/v1/products?page=1&...",
    "prev": "/api/v1/products?page=1&...",
    "next": "/api/v1/products?page=3&...",
    "last": "/api/v1/products?page=3&..."
  }
}

=== NESTED RESOURCES ===

GET /api/v1/categories/electronics/products?inStock=true&sort=name

Response:
{
  "category": {
    "id": 1,
    "name": "Electronics",
    "slug": "electronics",
    "productCount": 25
  },
  "products": [...],
  "pagination": {...}
}

=== FULL-TEXT SEARCH ===

GET /api/v1/products/search/laptop%20gaming?fields=name,description

Response:
{
  "query": "laptop gaming",
  "searchFields": ["name", "description"],
  "results": [
    {
      "id": 12,
      "name": "Gaming Laptop Pro",
      "relevance": 0.95,
      "highlights": {
        "name": "<em>Gaming</em> <em>Laptop</em> Pro",
        "description": "High-performance <em>gaming</em> <em>laptop</em>..."
      }
    }
  ],
  "totalResults": 8,
  "searchTime": "45ms"
}

=== JSON FORMATTING ===

Standard Response Envelope:
{
  "success": true,
  "data": {...},
  "meta": {
    "apiVersion": "1.0",
    "timestamp": "2024-01-15T10:30:00Z",
    "requestId": "req-abc123"
  }
}

Error Response:
{
  "success": false,
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Invalid input data",
    "details": [
      {"field":"price","error":"Must be positive"},
      {"field":"name","error":"Required"}
    ]
  },
  "meta": {...}
}

=== API VERSIONING ===

/api/v1/products → Original format
/api/v2/products → Enhanced format with more data

Version differences:
  v1: {"id":1,"name":"Laptop","price":999}
  v2: {"id":1,"name":"Laptop","price":{"amount":999,"currency":"USD"},"metadata":{...}}

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Comprehensive route organization
- All route parameter types
- Complex query string handling
- Pagination implementation
- Consistent JSON envelope
- Error response formatting
- API versioning strategy

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Routes match correctly?
2. **Constraints**: Parameters validated?
3. **JSON**: Proper serialization?
4. **Design**: RESTful patterns followed?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Routing and JSON concepts.*
