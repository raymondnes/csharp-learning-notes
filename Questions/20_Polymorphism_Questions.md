# Polymorphism - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Polymorphism concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a simple polymorphic hierarchy.

**Requirements:**
1. Create base `Animal` class with virtual `Speak()` method
2. Create `Dog` and `Cat` that override `Speak()`
3. Create an Animal variable that holds a Dog, call Speak()
4. Change it to hold a Cat, call Speak() again

**Expected Output:**
```
Animal reference holding Dog:
Woof! Woof!

Animal reference holding Cat:
Meow!

Same variable, different behavior - polymorphism!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Process a polymorphic array.

**Requirements:**
1. Create `Shape` with virtual `GetArea()` returning 0
2. Create `Circle` and `Square` overriding `GetArea()`
3. Create array of Shape containing different shapes
4. Loop through and calculate total area

**Expected Output:**
```
Shapes in array:
Circle (r=5): Area = 78.54
Square (s=4): Area = 16.00
Circle (r=3): Area = 28.27

Total area: 122.81
```

---

## Level 3: Application (Easy)

**Challenge:** Use base keyword to extend behavior.

**Requirements:**
1. Create `Vehicle` with virtual `Start()` that prints "Engine starting..."
2. Create `Car` that overrides `Start()`, calls base, then adds own message
3. Create `ElectricCar` extending Car, overrides Start() differently
4. Demonstrate the chain of calls

**Expected Output:**
```
Regular Vehicle:
  Engine starting...

Car (extends Vehicle):
  Engine starting...
  Car systems check complete.
  Ready to drive!

Electric Car (extends Car):
  Battery check...
  Silent start - Electric motor ready!
```

---

## Level 4: Application (Easy)

**Challenge:** Demonstrate method hiding vs overriding.

**Requirements:**
1. Create class with virtual method and one with `new` keyword
2. Show how behavior differs when accessed through base vs derived reference
3. Explain why override is preferred

**Expected Output:**
```
=== OVERRIDE (Correct Polymorphism) ===
Derived reference: Derived.VirtualMethod()
Base reference:    Derived.VirtualMethod()  <- Same!

=== NEW/HIDE (Broken Polymorphism) ===
Derived reference: Derived.HiddenMethod()
Base reference:    Base.HiddenMethod()      <- Different!

Conclusion: 'new' breaks polymorphism. Use virtual/override.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Build a notification system using polymorphism.

**Requirements:**
1. Abstract `Notification` class with abstract `Send()` method
2. `EmailNotification`, `SMSNotification`, `PushNotification` implementations
3. Each has different sending logic
4. Process list of notifications polymorphically

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              NOTIFICATION SYSTEM                            ║
╠════════════════════════════════════════════════════════════╣

Processing notifications...

[EMAIL] Sending to: user@example.com
  Subject: Welcome!
  Status: Sent via SMTP server

[SMS] Sending to: +1-555-1234
  Message: Your code is 123456
  Status: Sent via SMS gateway

[PUSH] Sending to device: iPhone-ABC123
  Title: New Message
  Status: Sent via APNs

═══════════════════════════════════════════════════════════════
Summary: 3 notifications sent successfully
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a document export system.

**Requirements:**
1. Base `Document` with virtual `Export()` returning string
2. `PDFDocument`, `WordDocument`, `HTMLDocument` with different exports
3. Virtual `GetMetadata()` method
4. Show polymorphic export of multiple documents

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║              DOCUMENT EXPORT SYSTEM                         ║
╠════════════════════════════════════════════════════════════╣

Exporting documents...

Document 1: Report.pdf
  Type: PDF Document
  Metadata: {pages: 10, author: "John"}
  Export: Generating PDF with compression...
  Output: report_20240115.pdf ✓

Document 2: Letter.docx
  Type: Word Document
  Metadata: {words: 500, template: "formal"}
  Export: Creating DOCX with formatting...
  Output: letter_20240115.docx ✓

Document 3: Page.html
  Type: HTML Document
  Metadata: {tags: 50, css: "styles.css"}
  Export: Rendering HTML with styles...
  Output: page_20240115.html ✓

All documents exported successfully.
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement a plugin system using polymorphism.

**Requirements:**
1. Abstract `Plugin` base with `Initialize()`, `Execute()`, `Cleanup()`
2. Create 3 different plugin types
3. Plugin manager loads and runs plugins polymorphically
4. Handle plugin errors gracefully

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      PLUGIN SYSTEM                                ║
╠══════════════════════════════════════════════════════════════════╣

Loading plugins...

[PluginManager] Discovered 3 plugins

Initializing plugins:
  [LoggingPlugin] Initializing... OK
  [CachePlugin] Initializing... OK
  [AuthPlugin] Initializing... OK

Executing plugins:
  [LoggingPlugin] Setting up file logger...
    → Log file created: app.log
  [CachePlugin] Initializing cache...
    → Cache size: 100MB
  [AuthPlugin] Loading auth providers...
    → OAuth2 configured

Simulating error in plugin:
  [FaultyPlugin] Initializing... FAILED
  [PluginManager] Plugin failed, continuing with others

Cleanup:
  [LoggingPlugin] Flushing logs... Done
  [CachePlugin] Clearing cache... Done
  [AuthPlugin] Revoking tokens... Done

═══════════════════════════════════════════════════════════════════
Plugins loaded: 3
Plugins failed: 1
System status: Operational
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a tax calculation system with regional variations.

**Requirements:**
1. Base `TaxCalculator` with virtual methods for different tax types
2. `USTaxCalculator`, `UKTaxCalculator`, `EUTaxCalculator`
3. Each overrides: `CalculateIncomeTax()`, `CalculateSalesTax()`, `GetTaxBrackets()`
4. Process same income through different calculators

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════╗
║                    TAX CALCULATION SYSTEM                             ║
╠══════════════════════════════════════════════════════════════════════╣

Annual Income: $75,000

═══ US TAX CALCULATOR ═══
Tax Brackets:
  $0 - $10,000: 10%
  $10,001 - $40,000: 12%
  $40,001 - $85,000: 22%

Income Tax Calculation:
  First $10,000 × 10% = $1,000
  Next $30,000 × 12% = $3,600
  Remaining $35,000 × 22% = $7,700
  Total Income Tax: $12,300

Sales Tax Rate: 7.25% (varies by state)

═══ UK TAX CALCULATOR ═══
Tax Brackets:
  £0 - £12,570: 0% (Personal Allowance)
  £12,571 - £50,270: 20% (Basic Rate)
  £50,271 - £125,140: 40% (Higher Rate)

Income Tax: £12,172
National Insurance: £4,500
Total: £16,672

VAT Rate: 20%

═══ COMPARISON ═══
Country    │ Income Tax │ Effective Rate
───────────┼────────────┼───────────────
USA        │ $12,300    │ 16.4%
UK         │ £16,672    │ 22.2%

(Polymorphism allows same calculation interface, different implementations)
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Build a complete rendering engine with polymorphic shapes and effects.

**Requirements:**
1. Abstract `Renderable` with virtual `Render()`, `ApplyEffect()`, `GetBounds()`
2. Shape hierarchy: `Shape` → `Circle`, `Rectangle`, `Polygon`, `Group` (contains other shapes)
3. Effect hierarchy: `Effect` → `Shadow`, `Blur`, `Glow`
4. Composite pattern: Groups can contain shapes and other groups
5. Render scene graph with transformations

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════════╗
║                        RENDERING ENGINE                                   ║
╠══════════════════════════════════════════════════════════════════════════╣

Building scene graph...

Scene
├── Background (Rectangle 800x600)
│   └── Effect: Gradient
├── Logo Group
│   ├── Circle (r=50) at (100, 100)
│   │   └── Effect: Shadow (offset: 5, blur: 10)
│   └── Text "ACME" at (80, 180)
│       └── Effect: Glow (color: gold)
└── Button Group
    ├── Rectangle (120x40) at (400, 500)
    │   └── Effect: Rounded corners
    └── Text "Click Me" at (420, 515)

═══════════════════════════════════════════════════════════════════════════
                            RENDERING
═══════════════════════════════════════════════════════════════════════════

[Renderer] Starting render pass...

Rendering: Background
  Type: Rectangle
  Bounds: (0, 0, 800, 600)
  Applying effect: Gradient
  → Rendered 480,000 pixels

Rendering: Logo Group (2 children)
  Entering group transform...

  Rendering: Circle
    Center: (100, 100), Radius: 50
    Applying effect: Shadow
    → Shadow rendered at offset (5, 5)
    → Circle rendered (7,854 pixels)

  Rendering: Text "ACME"
    Position: (80, 180)
    Applying effect: Glow
    → Glow rendered (color: #FFD700)
    → Text rendered

  Exiting group transform...

Rendering: Button Group (2 children)
  Entering group transform...

  Rendering: Rectangle
    Bounds: (400, 500, 120, 40)
    Applying effect: Rounded corners (radius: 5)
    → Rectangle rendered (4,800 pixels)

  Rendering: Text "Click Me"
    Position: (420, 515)
    → Text rendered

  Exiting group transform...

═══════════════════════════════════════════════════════════════════════════
                          RENDER STATISTICS
═══════════════════════════════════════════════════════════════════════════
Total objects rendered: 6
  Shapes: 4 (1 circle, 2 rectangles, 2 text)
  Groups: 2
  Effects applied: 4

Polymorphic calls made:
  Render(): 6 calls (different implementations)
  ApplyEffect(): 4 calls
  GetBounds(): 6 calls

Composite pattern: Groups rendered children recursively

Render time: 16ms (60 FPS capable)

╚══════════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use abstract base classes appropriately
- Implement composite pattern for groups
- Virtual methods with meaningful overrides
- Demonstrate polymorphic iteration
- Show effect chain application
- Clean architecture with separation of concerns

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does polymorphism work as expected?
2. **Virtual/Override**: Proper use of keywords?
3. **Base Calls**: Appropriate use of base keyword?
4. **Design**: Good polymorphic architecture?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Polymorphism concepts.*
