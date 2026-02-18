# Project 24: Todo List (Array)

## Difficulty: Intermediate

## Concepts: Arrays, CRUD operations, menu system

## Requirements

Create a todo list manager using arrays.

### Tasks:
1. Add todos (max capacity)
2. View all todos
3. Mark as complete
4. Delete todo
5. Search todos

## Expected Output

```
═══════════════════════════════════════
         TODO LIST MANAGER
═══════════════════════════════════════

      TODO LIST MENU
───────────────────────────────────────
  1. Add Todo
  2. View All
  3. Mark Complete
  4. Delete Todo
  5. Search
  0. Exit
───────────────────────────────────────

Select: 1

Enter todo: Buy groceries
✓ Todo added!

Select: 1

Enter todo: Finish homework
✓ Todo added!

Select: 1

Enter todo: Call mom
✓ Todo added!

Select: 2

      YOUR TODOS (3/10)
───────────────────────────────────────
  1. [ ] Buy groceries
  2. [ ] Finish homework
  3. [ ] Call mom
───────────────────────────────────────

Select: 3

Mark which todo as complete? 1

✓ Marked "Buy groceries" as complete!

Select: 2

      YOUR TODOS (3/10)
───────────────────────────────────────
  1. [✓] Buy groceries
  2. [ ] Finish homework
  3. [ ] Call mom
───────────────────────────────────────
Completed: 1/3

Select: 4

Delete which todo? 1

✓ Deleted "Buy groceries"

Select: 5

Search for: home

Search Results:
  2. [ ] Finish homework
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;

class Program
{
    const int MAX_TODOS = 10;
    static string[] todos = new string[MAX_TODOS];
    static bool[] completed = new bool[MAX_TODOS];
    static int todoCount = 0;

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("         TODO LIST MANAGER");
            Console.WriteLine("═══════════════════════════════════════\n");

            DisplayMenu();
            Console.Write("\nSelect: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: AddTodo(); break;
                case 2: ViewAll(); break;
                case 3: MarkComplete(); break;
                case 4: DeleteTodo(); break;
                case 5: SearchTodos(); break;
                case 0: running = false; break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("      TODO LIST MENU");
        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine("  1. Add Todo");
        Console.WriteLine("  2. View All");
        Console.WriteLine("  3. Mark Complete");
        Console.WriteLine("  4. Delete Todo");
        Console.WriteLine("  5. Search");
        Console.WriteLine("  0. Exit");
        Console.WriteLine("───────────────────────────────────────");
    }

    static void AddTodo()
    {
        if (todoCount >= MAX_TODOS)
        {
            Console.WriteLine("\n✗ Todo list is full!");
            return;
        }

        Console.Write("\nEnter todo: ");
        string todo = Console.ReadLine();

        todos[todoCount] = todo;
        completed[todoCount] = false;
        todoCount++;

        Console.WriteLine("✓ Todo added!");
    }

    static void ViewAll()
    {
        Console.WriteLine($"\n      YOUR TODOS ({todoCount}/{MAX_TODOS})");
        Console.WriteLine("───────────────────────────────────────");

        if (todoCount == 0)
        {
            Console.WriteLine("  No todos yet!");
            return;
        }

        int completedCount = 0;
        for (int i = 0; i < todoCount; i++)
        {
            string status = completed[i] ? "✓" : " ";
            Console.WriteLine($"  {i + 1}. [{status}] {todos[i]}");
            if (completed[i]) completedCount++;
        }

        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine($"Completed: {completedCount}/{todoCount}");
    }

    static void MarkComplete()
    {
        ViewAll();
        Console.Write("\nMark which todo as complete? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < todoCount)
        {
            completed[index] = true;
            Console.WriteLine($"\n✓ Marked \"{todos[index]}\" as complete!");
        }
    }

    static void DeleteTodo()
    {
        ViewAll();
        Console.Write("\nDelete which todo? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < todoCount)
        {
            string deleted = todos[index];

            // Shift elements
            for (int i = index; i < todoCount - 1; i++)
            {
                todos[i] = todos[i + 1];
                completed[i] = completed[i + 1];
            }
            todoCount--;

            Console.WriteLine($"\n✓ Deleted \"{deleted}\"");
        }
    }

    static void SearchTodos()
    {
        Console.Write("\nSearch for: ");
        string query = Console.ReadLine().ToLower();

        Console.WriteLine("\nSearch Results:");
        bool found = false;

        for (int i = 0; i < todoCount; i++)
        {
            if (todos[i].ToLower().Contains(query))
            {
                string status = completed[i] ? "✓" : " ";
                Console.WriteLine($"  {i + 1}. [{status}] {todos[i]}");
                found = true;
            }
        }

        if (!found) Console.WriteLine("  No matches found.");
    }
}
```
