# Project 16: Word Counter

## Difficulty: Beginner

## Concepts: String methods, loops, arrays

## Requirements

Create a text analyzer that counts words, characters, and sentences.

### Tasks:
1. Count total words
2. Count total characters (with/without spaces)
3. Count sentences
4. Find longest/shortest word
5. Calculate average word length

## Expected Output

```
═══════════════════════════════════════
         WORD COUNTER
═══════════════════════════════════════

Enter your text (press Enter twice to finish):

The quick brown fox jumps over the lazy dog.
This is a sample text for testing.


═══════════════════════════════════════
         ANALYSIS RESULTS
═══════════════════════════════════════

Characters (with spaces):    84
Characters (without spaces): 71
Words:                       15
Sentences:                   2
Paragraphs:                  1

Word Statistics:
───────────────────────────────────────
Longest word:  "testing" (7 letters)
Shortest word: "a" (1 letter)
Average length: 4.7 letters

Word Frequency:
───────────────────────────────────────
the: 2
quick: 1
brown: 1
fox: 1
...
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("         WORD COUNTER");
        Console.WriteLine("═══════════════════════════════════════\n");

        Console.WriteLine("Enter your text (press Enter twice to finish):\n");

        string text = "";
        string line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            text += line + " ";
        }

        // Count characters
        int charsWithSpaces = text.Length;
        int charsWithoutSpaces = text.Replace(" ", "").Length;

        // Count words
        string[] words = text.Split(new char[] { ' ', '\t', '\n' },
            StringSplitOptions.RemoveEmptyEntries);
        int wordCount = words.Length;

        // Count sentences (., !, ?)
        int sentenceCount = 0;
        foreach (char c in text)
        {
            if (c == '.' || c == '!' || c == '?')
                sentenceCount++;
        }

        // Find longest and shortest word
        string longest = "";
        string shortest = words.Length > 0 ? words[0] : "";

        foreach (string word in words)
        {
            string cleanWord = CleanWord(word);
            if (cleanWord.Length > longest.Length)
                longest = cleanWord;
            if (cleanWord.Length < shortest.Length && cleanWord.Length > 0)
                shortest = cleanWord;
        }

        // Calculate average word length

        // Word frequency using Dictionary

        // Display results
    }

    static string CleanWord(string word)
    {
        // Remove punctuation from word
        return word.Trim('.', ',', '!', '?', ';', ':');
    }
}
```
