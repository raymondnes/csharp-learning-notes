# Project 25: Contact Manager

## Difficulty: Intermediate

## Concepts: List<T>, classes, searching

## Requirements

Create a contact manager using List<T> and a Contact class.

### Tasks:
1. Contact class with properties
2. Add, edit, delete contacts
3. Search by name or phone
4. Sort contacts
5. Save/display all

## Expected Output

```
═══════════════════════════════════════
        CONTACT MANAGER
═══════════════════════════════════════

1. Add Contact
2. View All
3. Search
4. Edit Contact
5. Delete Contact
6. Sort Contacts
0. Exit

Select: 1

Enter name: John Doe
Enter phone: 555-1234
Enter email: john@email.com

✓ Contact added!

Select: 1

Enter name: Alice Smith
Enter phone: 555-5678
Enter email: alice@email.com

✓ Contact added!

Select: 2

      ALL CONTACTS (2)
───────────────────────────────────────
1. John Doe
   📞 555-1234
   📧 john@email.com

2. Alice Smith
   📞 555-5678
   📧 alice@email.com
───────────────────────────────────────

Select: 3

Search by (1) Name or (2) Phone: 1
Enter search term: alice

Search Results:
───────────────────────────────────────
Alice Smith
📞 555-5678
📧 alice@email.com
───────────────────────────────────────
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    private static int _nextId = 1;

    public Contact(string name, string phone, string email)
    {
        Id = _nextId++;
        Name = name;
        Phone = phone;
        Email = email;
    }

    public void Display()
    {
        Console.WriteLine($"{Id}. {Name}");
        Console.WriteLine($"   📞 {Phone}");
        Console.WriteLine($"   📧 {Email}");
        Console.WriteLine();
    }
}

class ContactManager
{
    private List<Contact> _contacts = new List<Contact>();

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void ViewAll()
    {
        Console.WriteLine($"\n      ALL CONTACTS ({_contacts.Count})");
        Console.WriteLine("───────────────────────────────────────");

        if (_contacts.Count == 0)
        {
            Console.WriteLine("No contacts yet!");
            return;
        }

        foreach (var contact in _contacts)
        {
            contact.Display();
        }
    }

    public List<Contact> SearchByName(string name)
    {
        return _contacts.Where(c =>
            c.Name.ToLower().Contains(name.ToLower())).ToList();
    }

    public List<Contact> SearchByPhone(string phone)
    {
        return _contacts.Where(c =>
            c.Phone.Contains(phone)).ToList();
    }

    public bool DeleteContact(int id)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == id);
        if (contact != null)
        {
            _contacts.Remove(contact);
            return true;
        }
        return false;
    }

    public Contact GetById(int id)
    {
        return _contacts.FirstOrDefault(c => c.Id == id);
    }

    public void SortByName()
    {
        _contacts = _contacts.OrderBy(c => c.Name).ToList();
    }
}

class Program
{
    static ContactManager manager = new ContactManager();

    static void Main()
    {
        bool running = true;

        while (running)
        {
            DisplayMenu();
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: AddContact(); break;
                case 2: manager.ViewAll(); break;
                case 3: SearchContacts(); break;
                case 4: EditContact(); break;
                case 5: DeleteContact(); break;
                case 6: manager.SortByName(); Console.WriteLine("✓ Sorted!"); break;
                case 0: running = false; break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter...");
                Console.ReadLine();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("        CONTACT MANAGER");
        Console.WriteLine("═══════════════════════════════════════\n");
        Console.WriteLine("1. Add Contact");
        Console.WriteLine("2. View All");
        Console.WriteLine("3. Search");
        Console.WriteLine("4. Edit Contact");
        Console.WriteLine("5. Delete Contact");
        Console.WriteLine("6. Sort Contacts");
        Console.WriteLine("0. Exit\n");
        Console.Write("Select: ");
    }

    static void AddContact()
    {
        Console.Write("\nEnter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter phone: ");
        string phone = Console.ReadLine();
        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        manager.AddContact(new Contact(name, phone, email));
        Console.WriteLine("\n✓ Contact added!");
    }

    static void SearchContacts()
    {
        Console.Write("\nSearch by (1) Name or (2) Phone: ");
        int option = int.Parse(Console.ReadLine());

        Console.Write("Enter search term: ");
        string term = Console.ReadLine();

        var results = option == 1
            ? manager.SearchByName(term)
            : manager.SearchByPhone(term);

        Console.WriteLine("\nSearch Results:");
        Console.WriteLine("───────────────────────────────────────");

        if (results.Count == 0)
            Console.WriteLine("No contacts found.");
        else
            results.ForEach(c => c.Display());
    }

    static void EditContact()
    {
        // Implement edit functionality
    }

    static void DeleteContact()
    {
        manager.ViewAll();
        Console.Write("Enter ID to delete: ");
        int id = int.Parse(Console.ReadLine());

        if (manager.DeleteContact(id))
            Console.WriteLine("✓ Contact deleted!");
        else
            Console.WriteLine("✗ Contact not found.");
    }
}
```
