# Project 26: Shopping Cart

## Difficulty: Intermediate

## Concepts: Objects, lists, calculations

## Requirements

Create a shopping cart system with products and cart management.

### Tasks:
1. Product class with properties
2. Cart class with items list
3. Add/remove items
4. Calculate totals with tax
5. Apply discounts

## Expected Output

```
═══════════════════════════════════════
         SHOPPING CART
═══════════════════════════════════════

Available Products:
───────────────────────────────────────
1. Laptop       $999.99
2. Phone        $599.99
3. Headphones   $149.99
4. Keyboard     $79.99
5. Mouse        $29.99
───────────────────────────────────────

1. Add to Cart
2. View Cart
3. Remove Item
4. Apply Coupon
5. Checkout
0. Exit

Select: 1
Product number: 1
Quantity: 1
✓ Added 1x Laptop to cart

Select: 1
Product number: 5
Quantity: 2
✓ Added 2x Mouse to cart

Select: 2

      YOUR CART
───────────────────────────────────────
1. Laptop (1x)         $999.99
2. Mouse (2x)          $59.98
───────────────────────────────────────
Subtotal:              $1,059.97
Tax (8%):              $84.80
───────────────────────────────────────
Total:                 $1,144.77
═══════════════════════════════════════

Select: 4
Enter coupon code: SAVE10

✓ Coupon applied! 10% discount

Select: 2

      YOUR CART
───────────────────────────────────────
1. Laptop (1x)         $999.99
2. Mouse (2x)          $59.98
───────────────────────────────────────
Subtotal:              $1,059.97
Discount (10%):        -$106.00
Tax (8%):              $76.32
───────────────────────────────────────
Total:                 $1,030.29
═══════════════════════════════════════
```

## Starter Code

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public decimal Total => Product.Price * Quantity;
}

class ShoppingCart
{
    private List<CartItem> _items = new List<CartItem>();
    private decimal _discountPercent = 0;
    private const decimal TAX_RATE = 0.08m;

    public void AddItem(Product product, int quantity)
    {
        var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem { Product = product, Quantity = quantity });
        }
    }

    public bool RemoveItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items.RemoveAt(index);
            return true;
        }
        return false;
    }

    public void ApplyDiscount(decimal percent)
    {
        _discountPercent = percent;
    }

    public decimal Subtotal => _items.Sum(i => i.Total);
    public decimal Discount => Subtotal * (_discountPercent / 100);
    public decimal TaxableAmount => Subtotal - Discount;
    public decimal Tax => TaxableAmount * TAX_RATE;
    public decimal Total => TaxableAmount + Tax;

    public void DisplayCart()
    {
        Console.WriteLine("\n      YOUR CART");
        Console.WriteLine("───────────────────────────────────────");

        if (_items.Count == 0)
        {
            Console.WriteLine("Cart is empty!");
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            Console.WriteLine($"{i + 1}. {item.Product.Name} ({item.Quantity}x)".PadRight(25) +
                $"{item.Total:C}");
        }

        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine($"{"Subtotal:",-25}{Subtotal:C}");

        if (_discountPercent > 0)
        {
            Console.WriteLine($"{"Discount (" + _discountPercent + "%):",-25}-{Discount:C}");
        }

        Console.WriteLine($"{"Tax (8%):",-25}{Tax:C}");
        Console.WriteLine("───────────────────────────────────────");
        Console.WriteLine($"{"Total:",-25}{Total:C}");
    }
}

class Program
{
    static List<Product> products = new List<Product>
    {
        new Product(1, "Laptop", 999.99m),
        new Product(2, "Phone", 599.99m),
        new Product(3, "Headphones", 149.99m),
        new Product(4, "Keyboard", 79.99m),
        new Product(5, "Mouse", 29.99m)
    };

    static ShoppingCart cart = new ShoppingCart();

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            DisplayProducts();
            DisplayMenu();

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: AddToCart(); break;
                case 2: cart.DisplayCart(); break;
                case 3: RemoveFromCart(); break;
                case 4: ApplyCoupon(); break;
                case 5: Checkout(); running = false; break;
                case 0: running = false; break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter...");
                Console.ReadLine();
            }
        }
    }

    static void DisplayProducts()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("         SHOPPING CART");
        Console.WriteLine("═══════════════════════════════════════\n");
        Console.WriteLine("Available Products:");
        Console.WriteLine("───────────────────────────────────────");

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id}. {p.Name,-15} {p.Price:C}");
        }
        Console.WriteLine("───────────────────────────────────────");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n1. Add to Cart");
        Console.WriteLine("2. View Cart");
        Console.WriteLine("3. Remove Item");
        Console.WriteLine("4. Apply Coupon");
        Console.WriteLine("5. Checkout");
        Console.WriteLine("0. Exit\n");
        Console.Write("Select: ");
    }

    static void AddToCart()
    {
        Console.Write("Product number: ");
        int productId = int.Parse(Console.ReadLine());
        Console.Write("Quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        var product = products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            cart.AddItem(product, quantity);
            Console.WriteLine($"✓ Added {quantity}x {product.Name} to cart");
        }
    }

    static void RemoveFromCart()
    {
        cart.DisplayCart();
        Console.Write("\nItem number to remove: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (cart.RemoveItem(index))
            Console.WriteLine("✓ Item removed");
        else
            Console.WriteLine("✗ Invalid item");
    }

    static void ApplyCoupon()
    {
        Console.Write("Enter coupon code: ");
        string code = Console.ReadLine().ToUpper();

        if (code == "SAVE10")
        {
            cart.ApplyDiscount(10);
            Console.WriteLine("✓ Coupon applied! 10% discount");
        }
        else if (code == "SAVE20")
        {
            cart.ApplyDiscount(20);
            Console.WriteLine("✓ Coupon applied! 20% discount");
        }
        else
        {
            Console.WriteLine("✗ Invalid coupon code");
        }
    }

    static void Checkout()
    {
        cart.DisplayCart();
        Console.WriteLine("\n✓ Order placed! Thank you for shopping!");
    }
}
```
