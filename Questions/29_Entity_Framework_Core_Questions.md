# Entity Framework Core - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Entity Framework Core concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a simple entity and DbContext.

**Requirements:**
1. Create Product class with Id, Name, Price
2. Create AppDbContext with DbSet<Product>
3. Configure in-memory database
4. Add one product and retrieve it

**Expected Output:**
```
EF Core Basics:

Created Product entity:
  public class Product
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
  }

Created DbContext with DbSet<Product>

Adding product:
  db.Products.Add(new Product { Name = "Laptop", Price = 999.99m });
  db.SaveChanges();

Retrieving product:
  var product = db.Products.First();
  Output: Product { Id = 1, Name = "Laptop", Price = 999.99 }

EF Core working!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Perform basic CRUD operations.

**Requirements:**
1. Create - Add 3 products
2. Read - Get all, get by Id
3. Update - Change a product's price
4. Delete - Remove a product

**Expected Output:**
```
CRUD Operations:

CREATE:
  Added: Laptop ($999.99) - ID: 1
  Added: Phone ($599.99) - ID: 2
  Added: Tablet ($449.99) - ID: 3

READ ALL:
  1. Laptop - $999.99
  2. Phone - $599.99
  3. Tablet - $449.99

READ BY ID (2):
  Phone - $599.99

UPDATE (ID: 2, new price):
  Phone: $599.99 → $549.99
  Updated successfully

DELETE (ID: 3):
  Removed: Tablet
  Products remaining: 2
```

---

## Level 3: Application (Easy)

**Challenge:** Use LINQ queries with EF Core.

**Requirements:**
1. Filter products by price range
2. Order by name and price
3. Select specific columns
4. Use First, Count, Any

**Expected Output:**
```
LINQ Queries with EF Core:

Products over $500:
  - Laptop ($999.99)
  - Phone ($599.99)

Ordered by price (descending):
  1. Laptop ($999.99)
  2. Phone ($599.99)
  3. Tablet ($449.99)

Select only names:
  ["Laptop", "Phone", "Tablet"]

First product starting with 'P':
  Phone

Total count: 3
Any over $1000: false
```

---

## Level 4: Application (Easy)

**Challenge:** Create one-to-many relationship.

**Requirements:**
1. Create Category entity
2. Add CategoryId to Product
3. Configure relationship
4. Include related data in queries

**Expected Output:**
```
One-to-Many Relationship:

Entities:
  Category: Id, Name, Products[]
  Product: Id, Name, Price, CategoryId, Category

Seeded Data:
  Category: Electronics
    - Laptop ($999.99)
    - Phone ($599.99)
  Category: Accessories
    - Mouse ($29.99)

Query with Include:
  var products = db.Products.Include(p => p.Category).ToList();

Results:
  Laptop - Category: Electronics
  Phone - Category: Electronics
  Mouse - Category: Accessories
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a complete REST API with EF Core.

**Requirements:**
1. Products API with full CRUD
2. Categories API with full CRUD
3. Nested route: /categories/{id}/products
4. Proper error handling

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              REST API WITH EF CORE                          ║
╠════════════════════════════════════════════════════════════╣

GET /api/products
  [EF] SELECT * FROM Products
  Response: [{id:1,name:"Laptop",...}, {...}]

GET /api/products/1
  [EF] SELECT * FROM Products WHERE Id = 1
  Response: {id:1,name:"Laptop",price:999.99}

POST /api/products
  Body: {"name":"Tablet","price":449.99,"categoryId":1}
  [EF] INSERT INTO Products...
  Response: 201 Created

GET /api/categories/1/products
  [EF] SELECT * FROM Products WHERE CategoryId = 1
  Response: [{...}, {...}]

DELETE /api/products/999
  [EF] SELECT * FROM Products WHERE Id = 999
  Response: 404 Not Found

Endpoints working with database!
```

---

## Level 6: Integration (Moderate)

**Challenge:** Implement pagination and filtering.

**Requirements:**
1. Pagination: page, pageSize parameters
2. Filtering: by name, minPrice, maxPrice, categoryId
3. Sorting: by name or price, asc/desc
4. Return metadata with results

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              PAGINATION & FILTERING                         ║
╠════════════════════════════════════════════════════════════╣

GET /api/products?page=2&pageSize=5&minPrice=100&sort=price_desc

[EF] Query built:
  SELECT * FROM Products
  WHERE Price >= 100
  ORDER BY Price DESC
  OFFSET 5 ROWS FETCH NEXT 5 ROWS

Response:
{
  "data": [
    {"id":5,"name":"Headphones","price":199.99},
    {"id":8,"name":"Keyboard","price":149.99},
    ...
  ],
  "pagination": {
    "page": 2,
    "pageSize": 5,
    "totalItems": 23,
    "totalPages": 5,
    "hasNext": true,
    "hasPrevious": true
  }
}
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Handle complex relationships.

**Requirements:**
1. Many-to-many: Products and Tags
2. One-to-one: Product and ProductDetails
3. Include multiple levels of related data
4. Create, update, delete with relationships

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   COMPLEX RELATIONSHIPS                           ║
╠══════════════════════════════════════════════════════════════════╣

=== ENTITY STRUCTURE ===

Product (1) ←→ (1) ProductDetails
Product (N) ←→ (M) Tags (via ProductTags)
Product (N) ←→ (1) Category

=== QUERIES ===

Get product with all related data:
  var product = await db.Products
      .Include(p => p.Details)
      .Include(p => p.Category)
      .Include(p => p.ProductTags)
          .ThenInclude(pt => pt.Tag)
      .FirstOrDefaultAsync(p => p.Id == 1);

Result:
{
  "id": 1,
  "name": "Laptop",
  "details": {
    "description": "High-performance laptop",
    "specifications": "16GB RAM, 512GB SSD"
  },
  "category": { "id": 1, "name": "Electronics" },
  "tags": [
    { "id": 1, "name": "Premium" },
    { "id": 2, "name": "Bestseller" }
  ]
}

=== UPDATES ===

Adding tag to product:
  product.ProductTags.Add(new ProductTag { TagId = 3 });
  await db.SaveChangesAsync();
  → Tag "New" added to Laptop

Updating details:
  product.Details.Description = "Updated description";
  await db.SaveChangesAsync();
  → Details updated
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Implement repository pattern with EF Core.

**Requirements:**
1. Generic IRepository<T> interface
2. EfRepository<T> implementation
3. Unit of Work pattern
4. Service layer using repositories
5. Dependency injection setup

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   REPOSITORY PATTERN                              ║
╠══════════════════════════════════════════════════════════════════╣

=== INTERFACES ===

IRepository<T>:
  - GetAllAsync()
  - GetByIdAsync(id)
  - AddAsync(entity)
  - UpdateAsync(entity)
  - DeleteAsync(id)

IUnitOfWork:
  - Products: IRepository<Product>
  - Categories: IRepository<Category>
  - SaveChangesAsync()

=== DI REGISTRATION ===

services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IProductService, ProductService>();

=== SERVICE USAGE ===

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Product> CreateAsync(ProductDto dto)
    {
        var product = new Product { Name = dto.Name, Price = dto.Price };
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return product;
    }
}

=== REQUEST FLOW ===

POST /api/products
  [Endpoint] Calling IProductService.CreateAsync()
  [Service] Using IUnitOfWork.Products.AddAsync()
  [Repository] db.Set<Product>().AddAsync()
  [UnitOfWork] db.SaveChangesAsync()
  [Response] 201 Created
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete e-commerce data layer.

**Requirements:**
1. Multiple entities: Products, Categories, Orders, OrderItems, Customers
2. All relationship types demonstrated
3. Complex queries with aggregations
4. Transactions for order processing
5. Audit fields (CreatedAt, ModifiedAt)
6. Soft delete implementation
7. Query specifications pattern

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    E-COMMERCE DATA LAYER                                  ║
╠══════════════════════════════════════════════════════════════════════════╣

=== ENTITY DIAGRAM ===

Customer (1) ←── (N) Orders
Order (1) ←── (N) OrderItems
OrderItem (N) ──→ (1) Product
Product (N) ──→ (1) Category
Product (N) ←→ (M) Tags

All entities inherit from BaseEntity:
  - Id, CreatedAt, ModifiedAt, IsDeleted

=== COMPLEX QUERIES ===

Customer order history with totals:
  var customer = await db.Customers
      .Include(c => c.Orders)
          .ThenInclude(o => o.Items)
          .ThenInclude(i => i.Product)
      .Where(c => c.Id == customerId)
      .Select(c => new CustomerOrderHistoryDto
      {
          CustomerId = c.Id,
          CustomerName = c.Name,
          TotalOrders = c.Orders.Count,
          TotalSpent = c.Orders.Sum(o => o.Total),
          Orders = c.Orders.Select(o => new OrderSummaryDto {...})
      })
      .FirstOrDefaultAsync();

=== TRANSACTION EXAMPLE ===

Creating order with transaction:
  using var transaction = await db.Database.BeginTransactionAsync();
  try
  {
      // Create order
      var order = new Order { CustomerId = customerId };
      db.Orders.Add(order);
      await db.SaveChangesAsync();

      // Add items and update inventory
      foreach (var item in items)
      {
          db.OrderItems.Add(new OrderItem
          {
              OrderId = order.Id,
              ProductId = item.ProductId,
              Quantity = item.Quantity
          });

          var product = await db.Products.FindAsync(item.ProductId);
          product.Stock -= item.Quantity;
      }

      await db.SaveChangesAsync();
      await transaction.CommitAsync();
  }
  catch
  {
      await transaction.RollbackAsync();
      throw;
  }

=== SOFT DELETE ===

Deleting product (soft):
  product.IsDeleted = true;
  product.ModifiedAt = DateTime.UtcNow;
  await db.SaveChangesAsync();

Global query filter:
  modelBuilder.Entity<Product>()
      .HasQueryFilter(p => !p.IsDeleted);

=== AGGREGATION QUERIES ===

Sales report by category:
  var report = await db.OrderItems
      .Include(oi => oi.Product)
          .ThenInclude(p => p.Category)
      .GroupBy(oi => oi.Product.Category.Name)
      .Select(g => new CategorySalesDto
      {
          Category = g.Key,
          TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice),
          ItemsSold = g.Sum(oi => oi.Quantity),
          OrderCount = g.Select(oi => oi.OrderId).Distinct().Count()
      })
      .OrderByDescending(c => c.TotalSales)
      .ToListAsync();

Result:
  Category     │ Total Sales │ Items │ Orders
  ─────────────┼─────────────┼───────┼────────
  Electronics  │ $45,230.00  │ 156   │ 89
  Clothing     │ $12,450.00  │ 523   │ 234
  Accessories  │ $8,920.00   │ 892   │ 445

=== SPECIFICATION PATTERN ===

ProductsByCategory specification:
  var spec = new ProductsByCategorySpec(categoryId)
      .WithPriceRange(minPrice, maxPrice)
      .OrderByPriceDescending()
      .WithPagination(page, pageSize);

  var products = await _repository.ListAsync(spec);

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Multiple entity types with proper relationships
- BaseEntity with audit fields
- Soft delete with global query filter
- Transaction handling
- Complex LINQ queries with aggregations
- Repository and Unit of Work patterns
- Query specifications for reusable queries

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: EF Core operations work properly?
2. **Relationships**: Properly configured?
3. **Queries**: Efficient LINQ usage?
4. **Patterns**: Repository/UoW implemented correctly?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Entity Framework Core concepts.*
