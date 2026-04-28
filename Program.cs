using System;

class Product
{
    public int Id;
    public string Name;
    public string Category;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} ({Category}) - P{Price} (Stock: {RemainingStock})");
    }

class CartItem
{
    public Product product;
    public int quantity;
    public double subtotal;
}

class Order
{
    public int ReceiptNo;
    public double FinalTotal;
}

    
class Program
{
    static int receiptCounter = 1;
    
    static void Main()
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Monitor", Category="Electronics", Price = 5000, Stock = 10 },
            new Product { Id = 2, Name = "Keyboard", Category="Electronics", Price = 1000, Stock = 20 },
            new Product { Id = 3, Name = "Mouse", Category="Electronics", Price = 600, Stock = 25 },
            new Product { Id = 4, Name = "Shirt", Category="Clothing", Price = 800, Stock = 20 },
            new Product { Id = 5, Name = "Pants", Category="Clothing", Price = 500, Stock = 25 },
            new Product { Id = 6, Name = "Jacket", Category="Clothing", Price = 1200, Stock = 10 }
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        while (true)
        {
            Console.WriteLine("\n--- STORE MENU ---");
            foreach (var p in products)
                p.DisplayProduct();

            Console.Write("Enter product number: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > products.Length)
            {
                Console.WriteLine("Invalid product!");
                continue;
            }

            Product selected = products[choice - 1];

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                continue;
            }

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Out of stock!");
                continue;
            }

            if (!selected.HasEnoughStock(qty))
            {
                Console.WriteLine("Not enough stock!");
                continue;
            }

            
            bool found = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].product.Id == selected.Id)
                {
                    cart[i].quantity += qty;
                    cart[i].subtotal += selected.GetItemTotal(qty);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full!");
                    continue;
                }

                cart[cartCount] = new CartItem
                {
                    product = selected,
                    quantity = qty,
                    subtotal = selected.GetItemTotal(qty)
                };
                cartCount++;
            }

            selected.DeductStock(qty);
            
            Console.WriteLine("Added to cart!");

            Console.Write("Add more? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y")
                break;
        }

        
        double grandTotal = 0;
        Console.WriteLine("\n--- RECEIPT ---");
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cart[i].product.Name} x{cart[i].quantity} = P{cart[i].subtotal}");
            grandTotal += cart[i].subtotal;
        }

        Console.WriteLine($"Grand Total: P{grandTotal}");

        double discount = 0;
        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine($"Discount (10%): P{discount}");
        }

        Console.WriteLine($"Final Total: P{grandTotal - discount}");

        Console.WriteLine("\n--- UPDATED STOCK ---");
        foreach (var p in products)
            p.DisplayProduct();
    }
}
