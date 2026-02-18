# Project 08: Number Guessing Game

## Difficulty: Beginner

## Concepts: Loops, random numbers, conditionals

## Requirements

Create a game where the user guesses a random number.

### Tasks:
1. Generate random number between 1-100
2. Let user guess with unlimited attempts
3. Give "higher" or "lower" hints
4. Track number of attempts
5. Display success message with attempt count

## Expected Output

```
═══════════════════════════════════════
      NUMBER GUESSING GAME
═══════════════════════════════════════

I'm thinking of a number between 1 and 100.
Can you guess it?

Guess #1: 50
Too low! Try higher.

Guess #2: 75
Too high! Try lower.

Guess #3: 62
Too low! Try higher.

Guess #4: 68
Too high! Try lower.

Guess #5: 65
🎉 CORRECT!

You guessed it in 5 attempts!

Rating: Great job! 🌟🌟🌟🌟
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
        Console.WriteLine("      NUMBER GUESSING GAME");
        Console.WriteLine("═══════════════════════════════════════\n");

        Random random = new Random();
        int secretNumber = random.Next(1, 101); // 1 to 100
        int attempts = 0;
        int guess = 0;

        Console.WriteLine("I'm thinking of a number between 1 and 100.");
        Console.WriteLine("Can you guess it?\n");

        while (guess != secretNumber)
        {
            attempts++;
            Console.Write($"Guess #{attempts}: ");
            guess = int.Parse(Console.ReadLine());

            if (guess < secretNumber)
            {
                Console.WriteLine("Too low! Try higher.\n");
            }
            else if (guess > secretNumber)
            {
                Console.WriteLine("Too high! Try lower.\n");
            }
        }

        // Display success message with rating based on attempts
    }
}
```

## Bonus Challenge

Add difficulty levels (easy: 1-50, medium: 1-100, hard: 1-500)
