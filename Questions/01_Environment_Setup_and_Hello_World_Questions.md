# Environment Setup and Hello World - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of Environment Setup and Hello World concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a new console application using the .NET CLI and make it output "Hello, C#!" to the console.

**Requirements:**
1. Use the `dotnet new console` command to create the project
2. Modify `Program.cs` to output "Hello, C#!" (exactly as written)
3. Build and run the project successfully

**Expected Output:**
```
Hello, C#!
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Write a program that outputs your name and your favorite programming language on two separate lines.

**Requirements:**
1. Use `Console.WriteLine()` for both outputs
2. First line should say: "My name is [YourName]"
3. Second line should say: "My favorite language is C#"
4. Replace [YourName] with an actual name

**Expected Output Example:**
```
My name is Alex
My favorite language is C#
```

---

## Level 3: Application (Easy)

**Challenge:** Create a program that displays a simple ASCII art box with a welcome message inside.

**Requirements:**
1. Use multiple `Console.WriteLine()` calls
2. Create a border using characters like `+`, `-`, and `|`
3. The message "Welcome to C#" should be centered inside the box
4. The box should be at least 20 characters wide

**Expected Output:**
```
+--------------------+
|   Welcome to C#    |
+--------------------+
```

---

## Level 4: Application (Easy)

**Challenge:** Write a program that demonstrates the difference between `Console.Write()` and `Console.WriteLine()`.

**Requirements:**
1. Use `Console.Write()` to output three words on the same line separated by spaces
2. Use `Console.WriteLine()` to move to a new line
3. Use `Console.WriteLine()` to output a summary sentence
4. Include a comment explaining the difference between the two methods

**Expected Output:**
```
C# is awesome
This sentence is on a new line.
```

---

## Level 5: Integration (Moderate)

**Challenge:** Create a program that displays a formatted "business card" with multiple pieces of information.

**Requirements:**
1. Store at least 4 pieces of information in variables (name, title, email, phone)
2. Use string interpolation (`$"..."`) to format the output
3. Include decorative borders
4. Display each piece of information on its own line

**Expected Output Example:**
```
================================
        BUSINESS CARD
================================
Name:   John Developer
Title:  Software Engineer
Email:  john@example.com
Phone:  555-1234
================================
```

---

## Level 6: Integration (Moderate)

**Challenge:** Write a program that demonstrates escape sequences and special characters in C# strings.

**Requirements:**
1. Display a file path using the backslash escape sequence (`\\`)
2. Display a quote within a string using the quote escape sequence (`\"`)
3. Display a multi-line message using the newline escape sequence (`\n`)
4. Display text with a tab character (`\t`) for alignment
5. Add comments explaining each escape sequence used

**Expected Output:**
```
File Path: C:\Users\Documents\file.txt
She said: "Hello, World!"
Line 1
Line 2
Line 3
Column1		Column2		Column3
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Create a program that displays a formatted table showing .NET CLI commands and their descriptions.

**Requirements:**
1. Create a properly aligned table with headers
2. Include at least 5 different `dotnet` commands
3. Use string formatting or padding to ensure columns are aligned
4. Include table borders using ASCII characters
5. Use variables to store the data before displaying

**Expected Output Example:**
```
+------------------+----------------------------------------+
| Command          | Description                            |
+------------------+----------------------------------------+
| dotnet new       | Create a new .NET project              |
| dotnet build     | Build the project                      |
| dotnet run       | Run the application                    |
| dotnet test      | Execute unit tests                     |
| dotnet publish   | Package for deployment                 |
+------------------+----------------------------------------+
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Create a program that generates a dynamic welcome banner that adapts to different message lengths.

**Requirements:**
1. Store the welcome message in a variable
2. Calculate the appropriate border width based on message length
3. Use `String.PadLeft()` or `String.PadRight()` for centering
4. Generate the top and bottom borders dynamically using `new string(char, count)`
5. The border should have 4 characters of padding on each side of the message
6. Test with different messages to ensure it works dynamically

**Expected Output Example (for message "Welcome!"):**
```
+----------------+
|    Welcome!    |
+----------------+
```

**Expected Output Example (for message "Hello, C# Developer!"):**
```
+----------------------------+
|    Hello, C# Developer!    |
+----------------------------+
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a comprehensive console application that serves as a "Development Environment Information" tool.

**Requirements:**
1. Display the current date and time using `DateTime.Now`
2. Display the operating system information using `Environment.OSVersion`
3. Display the .NET runtime version using `Environment.Version`
4. Display the current working directory using `Environment.CurrentDirectory`
5. Display the machine name using `Environment.MachineName`
6. Display the number of processors using `Environment.ProcessorCount`
7. Format all output in a professional, bordered table
8. Use proper string formatting and alignment
9. Include section headers and separators
10. Handle the display elegantly regardless of value lengths

**Expected Output Example:**
```
╔══════════════════════════════════════════════════════════════╗
║           DEVELOPMENT ENVIRONMENT INFORMATION                ║
╠══════════════════════════════════════════════════════════════╣
║ Property              │ Value                                ║
╠═══════════════════════╪══════════════════════════════════════╣
║ Date & Time           │ 2024-01-15 14:30:45                  ║
║ Operating System      │ Microsoft Windows NT 10.0.22621.0    ║
║ .NET Version          │ 8.0.0                                ║
║ Working Directory     │ C:\Projects\HelloWorld               ║
║ Machine Name          │ DEV-WORKSTATION                      ║
║ Processor Count       │ 8                                    ║
╚═══════════════════════╧══════════════════════════════════════╝

Press any key to exit...
```

**Additional Requirements for Level 9:**
- Use `Console.ReadKey()` to pause before exiting
- Use Unicode box-drawing characters for a professional appearance
- Ensure proper alignment even with varying value lengths
- Include appropriate comments explaining the code

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Does it produce the expected output?
2. **Requirements**: Are all stated requirements met?
3. **Syntax**: Is the code syntactically correct C#?
4. **Best Practices**: Does it follow C# conventions?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of Environment Setup and Hello World concepts.*
