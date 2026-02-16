# Environment Setup and Hello World

## Introduction

Before writing any C# code, you need to understand the environment in which C# programs run. This foundation is critical because it affects how you develop, test, and deploy applications throughout your career.

## The .NET Ecosystem

### What is .NET?

.NET is a free, open-source developer platform created by Microsoft for building many types of applications. Think of it as the engine that powers your C# code. When you write C# code, it doesn't run directly on your computer's processor. Instead, it runs on the .NET runtime, which translates your code into instructions your computer can understand.

### Key Components

1. **Common Language Runtime (CLR)**: The execution engine that handles running your code. It manages memory, handles exceptions, and provides services like garbage collection.

2. **.NET SDK (Software Development Kit)**: Contains everything you need to build .NET applications - the compiler, libraries, and command-line tools.

3. **BCL (Base Class Library)**: A massive collection of pre-written code that provides common functionality like file operations, network communication, and data structures.

## Setting Up Your Development Environment

### Installing the .NET SDK

The .NET SDK is the foundation of your development environment. It includes:

- The `dotnet` CLI (Command Line Interface)
- The C# compiler (Roslyn)
- Core libraries needed to build applications

**Installation Steps:**
1. Visit https://dotnet.microsoft.com/download
2. Download the latest .NET SDK (not just the runtime)
3. Run the installer and follow the prompts
4. Verify installation by opening a terminal and running `dotnet --version`

### The .NET CLI

The `dotnet` command is your primary tool for creating, building, and running C# projects. Key commands include:

| Command | Purpose |
|---------|---------|
| `dotnet new` | Create a new project from a template |
| `dotnet build` | Compile the project |
| `dotnet run` | Build and execute the project |
| `dotnet test` | Run unit tests |
| `dotnet publish` | Package for deployment |

## Your First C# Program

### Creating a Console Application

```bash
dotnet new console -n HelloWorld
cd HelloWorld
```

This creates a new console application with the following structure:

```
HelloWorld/
├── HelloWorld.csproj    # Project file (configuration)
├── Program.cs           # Your C# source code
└── obj/                 # Build artifacts (auto-generated)
```

### Understanding the Project File (.csproj)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

- **OutputType**: `Exe` means this creates an executable program
- **TargetFramework**: Specifies which version of .NET to use
- **ImplicitUsings**: Automatically includes common namespaces
- **Nullable**: Enables nullable reference type checking

### The Hello World Program

```csharp
// Program.cs
Console.WriteLine("Hello, World!");
```

In modern C# (with top-level statements), this single line is a complete program. Behind the scenes, the compiler wraps this in a class and Main method.

### Traditional vs. Modern Syntax

**Modern (Top-Level Statements - C# 9+):**
```csharp
Console.WriteLine("Hello, World!");
```

**Traditional:**
```csharp
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```

Both versions do exactly the same thing. Top-level statements reduce boilerplate for simple programs.

## Understanding the Console Class

The `Console` class is part of the `System` namespace and provides methods for interacting with the command-line interface.

### Key Methods

| Method | Purpose |
|--------|---------|
| `Console.WriteLine()` | Outputs text followed by a new line |
| `Console.Write()` | Outputs text without a new line |
| `Console.ReadLine()` | Reads a line of input from the user |
| `Console.ReadKey()` | Reads a single key press |
| `Console.Clear()` | Clears the console window |

### Examples

```csharp
// Output with new line
Console.WriteLine("First line");
Console.WriteLine("Second line");

// Output without new line
Console.Write("Hello ");
Console.Write("World");
// Output: Hello World (on one line)

// String interpolation
string name = "Developer";
Console.WriteLine($"Hello, {name}!");
// Output: Hello, Developer!

// Formatting
int age = 25;
Console.WriteLine("Name: {0}, Age: {1}", name, age);
// Output: Name: Developer, Age: 25
```

## Building and Running Your Program

### Using the CLI

```bash
# Build the project (compile to IL)
dotnet build

# Build and run
dotnet run

# Run with arguments
dotnet run -- arg1 arg2
```

### The Compilation Process

1. **Source Code** (Program.cs) →
2. **C# Compiler (Roslyn)** →
3. **Intermediate Language (IL)** →
4. **Just-In-Time (JIT) Compiler** →
5. **Machine Code** (runs on CPU)

This two-step compilation allows .NET to be platform-independent - the same IL can run on Windows, Linux, or macOS.

## Understanding Namespaces

Namespaces organize code and prevent naming conflicts. Think of them like folders for your code.

```csharp
using System;  // Import the System namespace

// Without using statement:
System.Console.WriteLine("Hello");

// With using statement:
Console.WriteLine("Hello");  // System is implied
```

## Common Beginner Mistakes

1. **Forgetting semicolons**: C# statements must end with `;`
2. **Case sensitivity**: `Console` is not the same as `console`
3. **Missing quotation marks**: Strings must be enclosed in `"double quotes"`
4. **Mismatched brackets**: Every `{` needs a closing `}`

## Best Practices

1. **Use meaningful project names**: Avoid generic names like "Test" or "Project1"
2. **Keep your code organized**: One concept per file as projects grow
3. **Learn keyboard shortcuts**: They speed up development significantly
4. **Read error messages carefully**: They usually tell you exactly what's wrong

## Summary

- .NET is the platform that runs C# code
- The .NET SDK provides tools for building applications
- The `dotnet` CLI is used to create, build, and run projects
- `Console.WriteLine()` outputs text to the terminal
- Modern C# uses top-level statements for simpler code
- The compilation process converts C# to IL, then to machine code

Understanding these fundamentals creates a solid foundation for everything that follows in your C# journey.
