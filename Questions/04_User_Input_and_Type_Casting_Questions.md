# User Input and Type Casting - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of User Input and Type Casting concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a program that asks for the user's name and greets them.

**Requirements:**
1. Prompt the user to enter their name
2. Read the input using `Console.ReadLine()`
3. Display a personalized greeting using the name they entered

**Example Interaction:**
```
Enter your name: Alice
Hello, Alice! Welcome to C#!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Create a program that reads two numbers and displays their sum.

**Requirements:**
1. Prompt for and read two numbers from the user
2. Convert the string inputs to integers using `int.Parse()`
3. Calculate and display the sum

**Example Interaction:**
```
Enter first number: 15
Enter second number: 27
The sum of 15 and 27 is 42
```

---

## Level 3: Application (Easy)

**Challenge:** Demonstrate implicit and explicit type conversions with user input.

**Requirements:**
1. Ask the user to enter a decimal number (e.g., 7.89)
2. Parse it as a `double`
3. Show the original value
4. Convert to `int` using explicit casting (show truncation)
5. Convert to `int` using `Convert.ToInt32()` (show rounding)
6. Explain the difference in comments

**Example Interaction:**
```
Enter a decimal number: 7.89
Original value (double): 7.89
Explicit cast to int:    7 (truncated)
Convert.ToInt32:         8 (rounded)
```

---

## Level 4: Application (Easy)

**Challenge:** Create a safe input program using TryParse.

**Requirements:**
1. Ask the user to enter their age
2. Use `int.TryParse()` to validate the input
3. If valid, display their age and calculate their birth year
4. If invalid, display an error message
5. Handle both cases without crashing

**Example Interaction (Valid):**
```
Enter your age: 25
Your age is 25
You were born around 2001
```

**Example Interaction (Invalid):**
```
Enter your age: twenty-five
Invalid input! Please enter a numeric age.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a temperature converter with full input validation.

**Requirements:**
1. Ask the user to enter a temperature in Celsius
2. Validate the input is a valid number
3. Convert to Fahrenheit using the formula: F = (C × 9/5) + 32
4. Display both values with 2 decimal places
5. Keep asking until valid input is provided

**Formula:** F = (C × 9/5) + 32

**Example Interaction:**
```
Enter temperature in Celsius: abc
Invalid input. Please enter a number.
Enter temperature in Celsius: 25.5
25.50°C = 77.90°F
```

---

## Level 6: Integration (Moderate)

**Challenge:** Create a multi-type input collector that demonstrates various conversions.

**Requirements:**
1. Collect the following from the user:
   - Name (string)
   - Age (int)
   - Height in meters (double)
   - Account balance (decimal)
   - Is member? (bool - accept "true" or "false")
2. Use appropriate parsing methods for each type
3. Validate all numeric inputs with TryParse
4. Display all information in a formatted summary
5. Show each value with its type name using `.GetType().Name`

**Example Interaction:**
```
Enter your name: John
Enter your age: 30
Enter your height (m): 1.75
Enter account balance: 1500.50
Are you a member? (true/false): true

=== USER PROFILE ===
Name: John (String)
Age: 30 (Int32)
Height: 1.75 m (Double)
Balance: $1,500.50 (Decimal)
Member: True (Boolean)
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a robust calculator that handles all edge cases.

**Requirements:**
1. Prompt for two numbers and an operation (+, -, *, /)
2. Validate both numbers using TryParse (handle decimals)
3. Validate the operation is one of the four options
4. Handle division by zero gracefully
5. Display results with appropriate precision
6. Include an input loop - keep asking until all inputs are valid
7. Show the complete expression and result

**Example Interaction:**
```
=== CALCULATOR ===
Enter first number: abc
Invalid number. Try again.
Enter first number: 10.5
Enter second number: 3
Enter operation (+, -, *, /): %
Invalid operation. Use +, -, *, or /
Enter operation (+, -, *, /): /
Result: 10.5 / 3 = 3.5
```

**Edge Case - Division by Zero:**
```
Enter first number: 10
Enter second number: 0
Enter operation: /
Error: Cannot divide by zero!
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a number base converter with comprehensive error handling.

**Requirements:**
1. Ask the user to enter a decimal (base 10) integer
2. Validate the input
3. Convert and display the number in:
   - Binary (base 2)
   - Octal (base 8)
   - Hexadecimal (base 16)
4. Also accept negative numbers and show two's complement concept for binary
5. Include input range validation (must fit in int)
6. Format output in a clear table

**Hint:** Use `Convert.ToString(number, base)` for base conversion.

**Example Interaction:**
```
Enter a decimal number: 255

╔═══════════════════════════════════════╗
║        NUMBER BASE CONVERTER          ║
╠═══════════════════════════════════════╣
║ Decimal:     255                      ║
║ Binary:      11111111                 ║
║ Octal:       377                      ║
║ Hexadecimal: FF                       ║
╚═══════════════════════════════════════╝
```

**Negative number example:**
```
Enter a decimal number: -1

╔═══════════════════════════════════════╗
║        NUMBER BASE CONVERTER          ║
╠═══════════════════════════════════════╣
║ Decimal:     -1                       ║
║ Binary:      11111111111111111111111111111111 ║
║ Octal:       37777777777              ║
║ Hexadecimal: FFFFFFFF                 ║
╚═══════════════════════════════════════╝
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive "Smart Input System" with retry logic, type detection, and conversion.

**Requirements:**
1. Create a system that accepts any input and attempts to determine its type
2. The system should detect and convert:
   - Integer (e.g., "42")
   - Double (e.g., "3.14")
   - Boolean (e.g., "true", "false", "yes", "no")
   - Date (e.g., "2024-01-15")
   - String (anything else)
3. For each input, display:
   - Original input
   - Detected type
   - Converted value
   - Alternative interpretations (if any)
4. Allow multiple inputs in a loop until user types "exit"
5. Track statistics: how many of each type were entered
6. Handle culture-invariant number parsing
7. Display a summary at the end

**Example Session:**
```
╔══════════════════════════════════════════════════════════════════╗
║                    SMART INPUT SYSTEM                             ║
║         Enter values to analyze (type 'exit' to quit)            ║
╚══════════════════════════════════════════════════════════════════╝

Enter value: 42
┌─────────────────────────────────────────┐
│ Input: "42"                             │
│ Detected Type: Integer                  │
│ Value: 42                               │
│ Alternatives:                           │
│   - As Double: 42.0                     │
│   - As String: "42"                     │
│   - As Boolean: True (non-zero)         │
└─────────────────────────────────────────┘

Enter value: 3.14159
┌─────────────────────────────────────────┐
│ Input: "3.14159"                        │
│ Detected Type: Double                   │
│ Value: 3.14159                          │
│ Alternatives:                           │
│   - As Integer: 3 (truncated)           │
│   - As Decimal: 3.14159                 │
└─────────────────────────────────────────┘

Enter value: yes
┌─────────────────────────────────────────┐
│ Input: "yes"                            │
│ Detected Type: Boolean                  │
│ Value: True                             │
│ Note: Interpreted as affirmative        │
└─────────────────────────────────────────┘

Enter value: 2024-01-15
┌─────────────────────────────────────────┐
│ Input: "2024-01-15"                     │
│ Detected Type: Date                     │
│ Value: January 15, 2024                 │
│ Alternatives:                           │
│   - Days from today: 365                │
│   - Day of week: Monday                 │
└─────────────────────────────────────────┘

Enter value: Hello World
┌─────────────────────────────────────────┐
│ Input: "Hello World"                    │
│ Detected Type: String                   │
│ Value: "Hello World"                    │
│ Properties:                             │
│   - Length: 11 characters               │
│   - Words: 2                            │
└─────────────────────────────────────────┘

Enter value: exit

╔══════════════════════════════════════════════════════════════════╗
║                         SESSION SUMMARY                           ║
╠══════════════════════════════════════════════════════════════════╣
║ Total inputs processed: 5                                         ║
╠══════════════════════════════════════════════════════════════════╣
║ Type Distribution:                                                ║
║   Integers: 1 (20%)                                               ║
║   Doubles:  1 (20%)                                               ║
║   Booleans: 1 (20%)                                               ║
║   Dates:    1 (20%)                                               ║
║   Strings:  1 (20%)                                               ║
╚══════════════════════════════════════════════════════════════════╝

Thank you for using Smart Input System!
```

**Additional Requirements:**
- Use `CultureInfo.InvariantCulture` for parsing numbers
- Accept multiple date formats (ISO, US, etc.)
- Boolean should accept: true/false, yes/no, 1/0
- Code must be well-organized with helper methods
- Include comments explaining detection logic

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does it handle input and conversion properly?
2. **Validation**: Are invalid inputs handled gracefully?
3. **Type Safety**: Are appropriate conversion methods used?
4. **User Experience**: Are prompts clear and errors helpful?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of User Input and Type Casting concepts.*
