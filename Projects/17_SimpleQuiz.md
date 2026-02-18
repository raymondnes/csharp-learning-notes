# Project 17: Simple Quiz

## Difficulty: Beginner

## Concepts: Arrays, loops, scoring

## Requirements

Create a multiple-choice quiz application.

### Tasks:
1. Store questions and answers in arrays
2. Present questions one at a time
3. Check answers and keep score
4. Show results at end
5. Track time (bonus)

## Expected Output

```
═══════════════════════════════════════
          KNOWLEDGE QUIZ
═══════════════════════════════════════

Welcome! This quiz has 5 questions.
You'll get 20 points for each correct answer.

Press Enter to start...

───────────────────────────────────────
Question 1 of 5:

What is the capital of France?

A) London
B) Paris
C) Berlin
D) Madrid

Your answer: B

✓ Correct! +20 points
───────────────────────────────────────
Question 2 of 5:

Which planet is known as the Red Planet?

A) Venus
B) Jupiter
C) Mars
D) Saturn

Your answer: C

✓ Correct! +20 points
───────────────────────────────────────
Question 3 of 5:

What is 7 × 8?

A) 54
B) 56
C) 58
D) 62

Your answer: A

✗ Incorrect! The answer was B) 56
───────────────────────────────────────

... (continues)

═══════════════════════════════════════
            QUIZ RESULTS
═══════════════════════════════════════

Correct Answers: 4/5
Final Score: 80/100

Grade: B - Good Job!

Time taken: 1 minute 23 seconds
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("          KNOWLEDGE QUIZ");
        Console.WriteLine("═══════════════════════════════════════\n");

        // Questions array
        string[] questions = {
            "What is the capital of France?",
            "Which planet is known as the Red Planet?",
            "What is 7 × 8?",
            "Who wrote 'Romeo and Juliet'?",
            "What is the chemical symbol for water?"
        };

        // Options array (4 options per question)
        string[,] options = {
            {"London", "Paris", "Berlin", "Madrid"},
            {"Venus", "Jupiter", "Mars", "Saturn"},
            {"54", "56", "58", "62"},
            {"Dickens", "Shakespeare", "Austen", "Hemingway"},
            {"H2O", "CO2", "O2", "NaCl"}
        };

        // Correct answers (A=0, B=1, C=2, D=3)
        int[] correctAnswers = { 1, 2, 1, 1, 0 };

        int score = 0;
        int totalQuestions = questions.Length;

        Console.WriteLine($"Welcome! This quiz has {totalQuestions} questions.");
        Console.WriteLine("Press Enter to start...");
        Console.ReadLine();

        DateTime startTime = DateTime.Now;

        for (int i = 0; i < totalQuestions; i++)
        {
            Console.WriteLine("───────────────────────────────────────");
            Console.WriteLine($"Question {i + 1} of {totalQuestions}:\n");
            Console.WriteLine(questions[i] + "\n");

            // Display options
            Console.WriteLine($"A) {options[i, 0]}");
            Console.WriteLine($"B) {options[i, 1]}");
            Console.WriteLine($"C) {options[i, 2]}");
            Console.WriteLine($"D) {options[i, 3]}\n");

            Console.Write("Your answer: ");
            char answer = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("\n");

            // Convert A-D to 0-3
            int answerIndex = answer - 'A';

            // Check answer
            if (answerIndex == correctAnswers[i])
            {
                Console.WriteLine("✓ Correct! +20 points");
                score += 20;
            }
            else
            {
                char correctLetter = (char)('A' + correctAnswers[i]);
                Console.WriteLine($"✗ Incorrect! The answer was {correctLetter}) {options[i, correctAnswers[i]]}");
            }
        }

        // Calculate time and display results
        TimeSpan timeTaken = DateTime.Now - startTime;

        Console.WriteLine("\n═══════════════════════════════════════");
        Console.WriteLine("            QUIZ RESULTS");
        Console.WriteLine("═══════════════════════════════════════\n");

        // Display final results
    }
}
```
