# LINQ - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of LINQ concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Use basic Where and Select.

**Requirements:**
1. Create a list of integers 1-10
2. Use Where to filter numbers greater than 5
3. Use Select to double each number
4. Display original and results

**Expected Output:**
```
Original numbers: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10

Filtered (> 5): 6, 7, 8, 9, 10

Doubled: 12, 14, 16, 18, 20

LINQ made filtering and transforming easy!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Use First, Last, and Count.

**Requirements:**
1. Create list of names: Alice, Bob, Carol, David, Eve
2. Get first name
3. Get last name
4. Count names starting with vowel
5. Get first name with more than 4 characters

**Expected Output:**
```
Names: Alice, Bob, Carol, David, Eve

First name: Alice
Last name: Eve
Names starting with vowel: 2 (Alice, Eve)
First name > 4 chars: Alice

Single methods retrieve specific elements.
```

---

## Level 3: Application (Easy)

**Challenge:** Use OrderBy and chaining.

**Requirements:**
1. Create list of products with Name and Price
2. Sort by price ascending
3. Sort by price descending
4. Chain: filter price > 20, sort by name, take top 3

**Expected Output:**
```
Products:
  Apple: $15.00
  Banana: $8.00
  Cherry: $25.00
  Date: $30.00
  Elderberry: $22.00

Sorted by Price (Ascending):
  Banana: $8.00
  Apple: $15.00
  Elderberry: $22.00
  Cherry: $25.00
  Date: $30.00

Filtered, Sorted, Top 3:
  Cherry: $25.00
  Date: $30.00
  Elderberry: $22.00
```

---

## Level 4: Application (Easy)

**Challenge:** Use Sum, Average, Min, Max.

**Requirements:**
1. Create list of test scores
2. Calculate sum, average, min, max
3. Count passing (>= 70) and failing
4. Find the range (max - min)

**Expected Output:**
```
Test Scores: 85, 92, 78, 65, 88, 95, 72, 58

Statistics:
  Sum: 633
  Average: 79.13
  Min: 58
  Max: 95
  Range: 37

  Passing (>= 70): 6
  Failing (< 70): 2
  Pass Rate: 75%
```

---

## Level 5: Integration (Moderate)

**Challenge:** Use GroupBy for data analysis.

**Requirements:**
1. Create list of employees with Name, Department, Salary
2. Group by department
3. For each department: count, average salary, min, max
4. Find department with highest average salary

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              EMPLOYEE SALARY ANALYSIS                       ║
╠════════════════════════════════════════════════════════════╣

Employees:
  Alice - IT: $75,000
  Bob - HR: $55,000
  Carol - IT: $82,000
  David - Sales: $65,000
  Eve - HR: $58,000
  Frank - Sales: $70,000

By Department:
──────────────────────────────────────────────────────────────
Department │ Count │ Avg Salary │ Min      │ Max
───────────┼───────┼────────────┼──────────┼──────────
IT         │ 2     │ $78,500    │ $75,000  │ $82,000
HR         │ 2     │ $56,500    │ $55,000  │ $58,000
Sales      │ 2     │ $67,500    │ $65,000  │ $70,000

Highest Paying Department: IT ($78,500 avg)
```

---

## Level 6: Integration (Moderate)

**Challenge:** Use Join to combine related data.

**Requirements:**
1. Create list of Customers (Id, Name)
2. Create list of Orders (CustomerId, Product, Amount)
3. Join customers with their orders
4. Show each customer's orders and total spending
5. Find customer with most orders

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              CUSTOMER ORDER ANALYSIS                        ║
╠════════════════════════════════════════════════════════════╣

Customers and Orders:

Alice (ID: 1):
  - Laptop: $999.99
  - Mouse: $29.99
  - Keyboard: $79.99
  Total: $1,109.97

Bob (ID: 2):
  - Monitor: $299.99
  - Webcam: $49.99
  Total: $349.98

Carol (ID: 3):
  - Phone: $799.99
  - Case: $19.99
  - Charger: $24.99
  Total: $844.97

Customer with most orders: Alice (3 orders)
Highest spender: Alice ($1,109.97)
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Complex query with multiple operations.

**Requirements:**
1. Create list of Products (Id, Name, Category, Price, Stock)
2. Find products low in stock (< 10) per category
3. Calculate inventory value per category
4. Find best-selling category potential (price × stock)
5. Generate comprehensive inventory report

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   INVENTORY ANALYSIS                              ║
╠══════════════════════════════════════════════════════════════════╣

Products:
  1. Laptop (Electronics): $999 × 15
  2. Phone (Electronics): $599 × 25
  3. Shirt (Clothing): $29 × 50
  4. Jeans (Clothing): $59 × 8
  5. Apple (Food): $1 × 200
  6. Milk (Food): $3 × 5

Low Stock Alert (< 10):
  ⚠ Jeans (Clothing): 8 remaining
  ⚠ Milk (Food): 5 remaining

Category Analysis:
──────────────────────────────────────────────────────────────────
Category    │ Products │ Total Stock │ Inventory Value │ Potential
────────────┼──────────┼─────────────┼─────────────────┼──────────
Electronics │ 2        │ 40          │ $29,960         │ $29,960
Clothing    │ 2        │ 58          │ $1,922          │ $1,922
Food        │ 2        │ 205         │ $215            │ $215

Most Valuable Category: Electronics ($29,960)
Highest Potential: Electronics ($29,960)

Price Distribution:
  Under $10: 2 products
  $10-$100: 2 products
  Over $100: 2 products
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Build a sales reporting system.

**Requirements:**
1. Create Sale class (Id, Product, Category, Amount, Date, Region)
2. Generate 20+ sample sales across multiple dates/regions
3. Daily sales summary
4. Regional comparison
5. Category trends
6. Top performers

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                   SALES REPORTING SYSTEM                          ║
╠══════════════════════════════════════════════════════════════════╣

=== DAILY SUMMARY ===

Date       │ Transactions │ Total Sales │ Avg Sale
───────────┼──────────────┼─────────────┼──────────
2024-01-15 │ 8            │ $2,450      │ $306.25
2024-01-16 │ 7            │ $1,890      │ $270.00
2024-01-17 │ 6            │ $3,200      │ $533.33

Best Day: 2024-01-17 ($3,200)

=== REGIONAL COMPARISON ===

Region │ Sales │ Total    │ Avg     │ % of Total
───────┼───────┼──────────┼─────────┼───────────
North  │ 8     │ $2,800   │ $350.00 │ 37.1%
South  │ 7     │ $2,540   │ $362.86 │ 33.6%
East   │ 6     │ $2,200   │ $366.67 │ 29.1%

Top Region: East (highest avg sale)

=== CATEGORY PERFORMANCE ===

Category    │ Units │ Revenue  │ Best Seller
────────────┼───────┼──────────┼────────────
Electronics │ 12    │ $4,500   │ Laptop
Clothing    │ 15    │ $1,200   │ Shirt
Accessories │ 10    │ $1,840   │ Watch

=== TOP 5 SALES ===

1. Laptop - $999 (North, 2024-01-17)
2. Phone - $599 (South, 2024-01-15)
3. Watch - $450 (East, 2024-01-16)
4. Tablet - $399 (North, 2024-01-15)
5. Headphones - $299 (South, 2024-01-17)

Grand Total: $7,540 across 21 transactions
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete data analytics dashboard.

**Requirements:**
1. Student class (Id, Name, Major, GPA, Credits, EnrollmentYear)
2. Course class (Id, Name, Department, Credits, Instructor)
3. Enrollment class (StudentId, CourseId, Grade, Semester)
4. Use LINQ to generate:
   - Student performance by major
   - Course popularity rankings
   - GPA trends by enrollment year
   - Department workload analysis
   - At-risk students identification
   - Instructor effectiveness

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                    ACADEMIC ANALYTICS DASHBOARD                           ║
╠══════════════════════════════════════════════════════════════════════════╣

=== STUDENT PERFORMANCE BY MAJOR ===

Major           │ Students │ Avg GPA │ Honors (>3.5) │ At Risk (<2.0)
────────────────┼──────────┼─────────┼───────────────┼───────────────
Computer Sci    │ 25       │ 3.42    │ 12 (48%)      │ 2 (8%)
Engineering     │ 20       │ 3.28    │ 8 (40%)       │ 3 (15%)
Business        │ 30       │ 3.15    │ 10 (33%)      │ 4 (13%)
Biology         │ 15       │ 3.55    │ 9 (60%)       │ 1 (7%)

Top Major by GPA: Biology (3.55)

=== COURSE POPULARITY (Top 10) ===

Rank │ Course                  │ Dept    │ Enrolled │ Avg Grade
─────┼─────────────────────────┼─────────┼──────────┼──────────
1    │ Introduction to CS      │ CS      │ 45       │ 3.2
2    │ Calculus I              │ Math    │ 42       │ 2.8
3    │ Business Ethics         │ Bus     │ 38       │ 3.5
4    │ Organic Chemistry       │ Chem    │ 35       │ 2.6
5    │ Data Structures         │ CS      │ 32       │ 3.1

=== GPA TRENDS BY ENROLLMENT YEAR ===

Year │ Students │ Avg GPA │ Trend
─────┼──────────┼─────────┼─────────────
2021 │ 20       │ 3.45    │ ████████████████
2022 │ 25       │ 3.32    │ ██████████████
2023 │ 30       │ 3.28    │ ██████████████
2024 │ 15       │ 3.18    │ █████████████

Observation: Slight downward trend in GPA

=== DEPARTMENT WORKLOAD ===

Department      │ Courses │ Total Enrolled │ Avg Class Size │ Instructors
────────────────┼─────────┼────────────────┼────────────────┼────────────
Computer Sci    │ 12      │ 380            │ 31.7           │ 8
Mathematics     │ 10      │ 320            │ 32.0           │ 6
Business        │ 15      │ 450            │ 30.0           │ 10
Chemistry       │ 8       │ 200            │ 25.0           │ 5

Busiest Department: Business (450 enrollments)

=== AT-RISK STUDENTS ===

Students with GPA < 2.0 or failing 2+ courses:

ID    │ Name         │ Major       │ GPA  │ Failed Courses │ Action Needed
──────┼──────────────┼─────────────┼──────┼────────────────┼──────────────
S015  │ John Smith   │ Engineering │ 1.85 │ 2              │ Academic Probation
S023  │ Jane Doe     │ Business    │ 1.92 │ 1              │ Counseling
S041  │ Bob Wilson   │ Comp Sci    │ 1.78 │ 3              │ Immediate Review

Total at-risk: 10 students (11% of population)

=== INSTRUCTOR EFFECTIVENESS ===

Instructor      │ Courses │ Students │ Avg Grade │ Pass Rate │ Rating
────────────────┼─────────┼──────────┼───────────┼───────────┼────────
Dr. Anderson    │ 3       │ 90       │ 3.45      │ 95%       │ ★★★★★
Prof. Baker     │ 4       │ 120      │ 3.20      │ 88%       │ ★★★★
Dr. Chen        │ 2       │ 60       │ 3.55      │ 97%       │ ★★★★★
Prof. Davis     │ 3       │ 85       │ 2.80      │ 78%       │ ★★★

Top Instructor: Dr. Chen (highest avg grade + pass rate)

═══════════════════════════════════════════════════════════════════════════
                              SUMMARY
═══════════════════════════════════════════════════════════════════════════

LINQ Operations Used:
  - Where (filtering students, courses)
  - Select (projections, transformations)
  - GroupBy (by major, department, year)
  - Join (students with enrollments, courses)
  - OrderBy/OrderByDescending (rankings)
  - Sum, Average, Count (aggregations)
  - Take (top N results)
  - Any/All (at-risk identification)

Data Sources Queried: 3 (Students, Courses, Enrollments)
Total Records Processed: 500+
Query Execution Time: <50ms

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use all major LINQ methods (Where, Select, GroupBy, Join, OrderBy, etc.)
- Chain multiple operations efficiently
- Use anonymous types for projections
- Aggregate functions (Sum, Average, Count, Min, Max)
- Handle empty results gracefully
- Demonstrate deferred vs immediate execution understanding

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: LINQ queries produce correct results?
2. **Syntax**: Proper use of LINQ methods?
3. **Efficiency**: Appropriate chaining and execution?
4. **Readability**: Clean, understandable queries?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of LINQ concepts.*
