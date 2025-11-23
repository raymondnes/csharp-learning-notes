# C# Lesson: 2025-11-23

## Concept: The "Hello, World!" Program

**Professor's Question:** In C#, if you wanted to display a message on the console, what kind of instruction do you think you would need to give it? What would be the key components of such an instruction?

**Student's Response:** it will need a type format, variable value and an output command

## Concept: Data Types

**Professor's Question 1:** You mentioned "type format." In C#, when we talk about "type," what do you imagine that refers to? What is its purpose in a programming language, and why might it be important for the computer to know the "type" of something?

**Student's Response 1:** type to me means something that represents the identity of a given value.

**Professor's Response & Question 2:** That's a fantastic way to put it! "The identity of a given value" is a very insightful definition. It gets to the heart of the matter.

In C#, a 	ype tells the computer both the *kind* of data it is (a number, text, a true/false value, etc.) and what you can *do* with that data. For example, you can perform division on two numbers, but you can't divide two pieces of text. The type defines the data's identity, just as you said, and its capabilities.

This brings us to our next question: If we want to store a piece of text like "Hello, World!", what 	ype do you think C# would use to represent it? And for a whole number, like 5, what 	ype might that be?

**Student's Response 2:** "hello world" would be a string type while 5 will be an integer type

**Professor's Response & Question 3:** Excellent! You've precisely identified the fundamental types C# uses for those values: string for "hello world" and int for 5. This shows a strong intuitive grasp of how data is categorized in programming.

Now, let's deepen our understanding of these.

When you declare a string or an int in C#, you are essentially telling the compiler, "This variable will hold this *kind* of data."

Consider this: Why is it beneficial for a programming language like C# to know in advance that "hello world" is a string and 5 is an int? What problems might arise if the language didn't enforce these types, or treated all values generically?

**Coding Task 1.1: Declare and Initialize**

1.  Create a new console application using the dotnet new console command (if you haven't already from the environment setup).
2.  Open the Program.cs file.
3.  Inside the Main method, declare a variable of type string and assign it the value "Hello, C# World!".
4.  Declare another variable of type int and assign it your favorite whole number.
5.  Use Console.WriteLine() to display both the string and the integer variables on separate lines.
