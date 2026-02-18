# Project 22: Bank Account

## Difficulty: Intermediate

## Concepts: Encapsulation, private fields, validation

## Requirements

Create a BankAccount class with proper encapsulation.

### Tasks:
1. Private balance field
2. Deposit with validation
3. Withdraw with balance check
4. Transaction history
5. Multiple account types (checking, savings)

## Expected Output

```
═══════════════════════════════════════
         BANK ACCOUNT SYSTEM
═══════════════════════════════════════

Creating accounts...

Checking Account (ACC-001):
  Owner: John Doe
  Balance: $1,000.00
  Type: Checking

Savings Account (ACC-002):
  Owner: Jane Doe
  Balance: $5,000.00
  Type: Savings
  Interest Rate: 2.5%

───────────────────────────────────────
Transactions:
───────────────────────────────────────

Depositing $500 to ACC-001...
✓ Deposit successful
New Balance: $1,500.00

Withdrawing $200 from ACC-001...
✓ Withdrawal successful
New Balance: $1,300.00

Withdrawing $10,000 from ACC-001...
✗ Insufficient funds!
Balance: $1,300.00

Applying interest to savings...
Interest earned: $125.00
New Balance: $5,125.00

───────────────────────────────────────
Transaction History (ACC-001):
───────────────────────────────────────
1. INITIAL    +$1,000.00  Balance: $1,000.00
2. DEPOSIT    +$500.00    Balance: $1,500.00
3. WITHDRAW   -$200.00    Balance: $1,300.00
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;
using System.Collections.Generic;

class BankAccount
{
    // Private fields
    private decimal _balance;
    private List<string> _transactions;
    private static int _nextAccountNumber = 1;

    // Properties
    public string AccountNumber { get; private set; }
    public string Owner { get; set; }
    public string AccountType { get; private set; }

    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    // Constructor
    public BankAccount(string owner, decimal initialDeposit, string accountType = "Checking")
    {
        AccountNumber = $"ACC-{_nextAccountNumber++:D3}";
        Owner = owner;
        AccountType = accountType;
        _balance = 0;
        _transactions = new List<string>();

        if (initialDeposit > 0)
        {
            Deposit(initialDeposit, "INITIAL");
        }
    }

    // Methods
    public bool Deposit(decimal amount, string type = "DEPOSIT")
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive.");
            return false;
        }

        _balance += amount;
        _transactions.Add($"{type,-10} +{amount:C}  Balance: {_balance:C}");
        return true;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be positive.");
            return false;
        }

        if (amount > _balance)
        {
            Console.WriteLine($"✗ Insufficient funds!");
            Console.WriteLine($"Balance: {_balance:C}");
            return false;
        }

        _balance -= amount;
        _transactions.Add($"{"WITHDRAW",-10} -{amount:C}  Balance: {_balance:C}");
        return true;
    }

    public void PrintTransactionHistory()
    {
        Console.WriteLine($"Transaction History ({AccountNumber}):");
        Console.WriteLine("───────────────────────────────────────");
        for (int i = 0; i < _transactions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_transactions[i]}");
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"{AccountType} Account ({AccountNumber}):");
        Console.WriteLine($"  Owner: {Owner}");
        Console.WriteLine($"  Balance: {_balance:C}");
        Console.WriteLine($"  Type: {AccountType}");
    }
}

class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingsAccount(string owner, decimal initialDeposit, decimal interestRate)
        : base(owner, initialDeposit, "Savings")
    {
        InterestRate = interestRate;
    }

    public decimal ApplyInterest()
    {
        decimal interest = Balance * (InterestRate / 100);
        Deposit(interest, "INTEREST");
        return interest;
    }
}

class Program
{
    static void Main()
    {
        // Create and test accounts
    }
}
```
