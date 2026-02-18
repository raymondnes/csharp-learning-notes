# Project 14: Simple ATM

## Difficulty: Beginner

## Concepts: Methods, if-else, loops, state management

## Requirements

Create a simple ATM simulation with basic banking operations.

### Tasks:
1. Set initial balance
2. Menu: Check Balance, Deposit, Withdraw, Exit
3. Validate withdrawal (sufficient funds)
4. Track transaction history
5. PIN verification (bonus)

## Expected Output

```
═══════════════════════════════════════
          SIMPLE ATM
═══════════════════════════════════════

Enter PIN: ****

✓ PIN Accepted. Welcome!

Current Balance: $1,000.00

═══════════════════════════════════════
1. Check Balance
2. Deposit
3. Withdraw
4. Transaction History
5. Exit
═══════════════════════════════════════

Select option: 2

Enter deposit amount: $500

✓ Deposited: $500.00
New Balance: $1,500.00
───────────────────────────────────────

Select option: 3

Enter withdrawal amount: $200

✓ Withdrawn: $200.00
New Balance: $1,300.00
───────────────────────────────────────

Select option: 3

Enter withdrawal amount: $2000

✗ Insufficient funds!
Your balance: $1,300.00
───────────────────────────────────────

Select option: 4

Transaction History:
───────────────────────────────────────
1. DEPOSIT   +$500.00   Balance: $1,500.00
2. WITHDRAW  -$200.00   Balance: $1,300.00
───────────────────────────────────────

Select option: 5

Thank you for using Simple ATM!
Final Balance: $1,300.00
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static decimal balance = 1000.00m;
    static List<string> transactions = new List<string>();

    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("          SIMPLE ATM");
        Console.WriteLine("═══════════════════════════════════════\n");

        // Optional: PIN verification
        if (!VerifyPIN())
        {
            Console.WriteLine("Too many failed attempts. Goodbye.");
            return;
        }

        bool running = true;
        while (running)
        {
            DisplayMenu();
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: CheckBalance(); break;
                case 2: Deposit(); break;
                case 3: Withdraw(); break;
                case 4: ShowHistory(); break;
                case 5: running = false; break;
            }
        }

        Console.WriteLine($"\nFinal Balance: {balance:C}");
    }

    static void CheckBalance()
    {
        Console.WriteLine($"\nCurrent Balance: {balance:C}\n");
    }

    static void Deposit()
    {
        Console.Write("\nEnter deposit amount: $");
        // Implement deposit
    }

    static void Withdraw()
    {
        Console.Write("\nEnter withdrawal amount: $");
        // Implement withdrawal with validation
    }

    static void ShowHistory()
    {
        // Display transaction history
    }

    static bool VerifyPIN()
    {
        string correctPIN = "1234";
        int attempts = 3;
        // Implement PIN verification
        return true;
    }

    static void DisplayMenu()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Transaction History");
        Console.WriteLine("5. Exit");
        Console.WriteLine("═══════════════════════════════════════");
        Console.Write("\nSelect option: ");
    }
}
```
