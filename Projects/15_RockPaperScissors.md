# Project 15: Rock Paper Scissors

## Difficulty: Beginner

## Concepts: Random, switch, game loops

## Requirements

Create a Rock-Paper-Scissors game against the computer.

### Tasks:
1. Get player choice
2. Generate random computer choice
3. Determine winner
4. Track score across rounds
5. Best of N option

## Expected Output

```
═══════════════════════════════════════
      ROCK PAPER SCISSORS
═══════════════════════════════════════

Best of how many rounds? 3

══════════ ROUND 1 ══════════

Choose:
1. Rock 🪨
2. Paper 📄
3. Scissors ✂️

Your choice: 1

You chose: Rock 🪨
Computer chose: Scissors ✂️

🎉 You WIN this round!
───────────────────────────────────────
Score: You 1 - 0 Computer

══════════ ROUND 2 ══════════

Your choice: 2

You chose: Paper 📄
Computer chose: Rock 🪨

🎉 You WIN this round!
───────────────────────────────────────
Score: You 2 - 0 Computer

════════════════════════════════════════
         🏆 GAME OVER 🏆

Final Score: You 2 - 0 Computer

YOU ARE THE CHAMPION! 🎉
════════════════════════════════════════

Play again? (y/n):
```

## Starter Code

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("      ROCK PAPER SCISSORS");
        Console.WriteLine("═══════════════════════════════════════\n");

        Random random = new Random();
        int playerScore = 0;
        int computerScore = 0;

        Console.Write("Best of how many rounds? ");
        int rounds = int.Parse(Console.ReadLine());
        int roundsToWin = (rounds / 2) + 1;

        while (playerScore < roundsToWin && computerScore < roundsToWin)
        {
            int roundNumber = playerScore + computerScore + 1;
            Console.WriteLine($"\n══════════ ROUND {roundNumber} ══════════\n");

            Console.WriteLine("Choose:");
            Console.WriteLine("1. Rock 🪨");
            Console.WriteLine("2. Paper 📄");
            Console.WriteLine("3. Scissors ✂️\n");

            Console.Write("Your choice: ");
            int playerChoice = int.Parse(Console.ReadLine());

            int computerChoice = random.Next(1, 4);

            // Display choices
            string[] choices = { "", "Rock 🪨", "Paper 📄", "Scissors ✂️" };
            Console.WriteLine($"\nYou chose: {choices[playerChoice]}");
            Console.WriteLine($"Computer chose: {choices[computerChoice]}");

            // Determine winner
            string result = DetermineWinner(playerChoice, computerChoice);
            // Update scores based on result
        }

        // Display final results
    }

    static string DetermineWinner(int player, int computer)
    {
        if (player == computer) return "tie";

        // Rock beats Scissors, Scissors beats Paper, Paper beats Rock
        if ((player == 1 && computer == 3) ||
            (player == 2 && computer == 1) ||
            (player == 3 && computer == 2))
        {
            return "player";
        }
        return "computer";
    }
}
```
