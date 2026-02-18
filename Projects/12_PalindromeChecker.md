# Project 12: Palindrome Checker

## Difficulty: Beginner

## Concepts: String manipulation, loops

## Requirements

Create a program that checks if words/phrases are palindromes.

### Tasks:
1. Check if a word is a palindrome
2. Ignore case sensitivity
3. Handle phrases (ignore spaces and punctuation)
4. Show reversed string for comparison

## Expected Output

```
═══════════════════════════════════════
       PALINDROME CHECKER
═══════════════════════════════════════

Enter a word or phrase: Racecar

Original:  Racecar
Reversed:  racecaR
Cleaned:   racecar

✓ "Racecar" is a PALINDROME!
═══════════════════════════════════════

Enter a word or phrase: A man a plan a canal Panama

Original:  A man a plan a canal Panama
Cleaned:   amanaplanacanalpanama
Reversed:  amanaplanacanalpanama

✓ This phrase is a PALINDROME!
═══════════════════════════════════════

Enter a word or phrase: Hello

Original:  Hello
Reversed:  olleH

✗ "Hello" is NOT a palindrome.
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
        Console.WriteLine("       PALINDROME CHECKER");
        Console.WriteLine("═══════════════════════════════════════\n");

        while (true)
        {
            Console.Write("Enter a word or phrase: ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) break;

            bool isPalindrome = CheckPalindrome(input);

            // Display results
        }
    }

    static bool CheckPalindrome(string text)
    {
        // Remove non-letter characters and convert to lowercase
        string cleaned = "";
        foreach (char c in text.ToLower())
        {
            if (char.IsLetter(c))
            {
                cleaned += c;
            }
        }

        // Reverse the string
        char[] charArray = cleaned.ToCharArray();
        Array.Reverse(charArray);
        string reversed = new string(charArray);

        return cleaned == reversed;
    }

    static string Reverse(string text)
    {
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
```

## Hints

- Use `char.IsLetter(c)` to check for letters only
- Use `ToLower()` for case-insensitive comparison
