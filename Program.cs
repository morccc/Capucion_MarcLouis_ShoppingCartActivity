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
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. Add to Cart");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. View Order History");
            Console.WriteLine("5. Exit");

            Console.Write("Choice: ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (choice == 1)
            {
                Console.WriteLine("\n--- PRODUCTS ---");
                foreach (var p in products) p.Display();

                Console.Write("Enter product #: ");
                int pnum;
                if (!int.TryParse(Console.ReadLine(), out pnum) || pnum < 1 || pnum > products.Length)
                {
                    Console.WriteLine("Invalid product!");
                    continue;
                }

                Product selected = products[pnum - 1];

                Console.Write("Enter quantity: ");
                int qty;
                if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
                {
                    Console.WriteLine("Invalid quantity!");
                    continue;
                }

                if (qty > selected.Stock)
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
                        cart[i].subtotal += qty * selected.Price;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    cart[cartCount] = new CartItem
                    {
                        product = selected,
                        quantity = qty,
                        subtotal = qty * selected.Price
                    };
                    cartCount++;
                }

                selected.Stock -= qty;
                Console.WriteLine("Added to cart!");
            }

            else if (choice == 2)
              {
                while (true)
                {
                    Console.WriteLine("\n--- CART ---");
                    double total = 0;

                    for (int i = 0; i < cartCount; i++)
                    {
                        Console.WriteLine($"{i+1}. {cart[i].product.Name} x{cart[i].quantity} = P{cart[i].subtotal}");
                        total += cart[i].subtotal;
                    }

                    Console.WriteLine("Total: P" + total);

                    Console.WriteLine("\n1. Remove Item");
                    Console.WriteLine("2. Update Quantity");
                    Console.WriteLine("3. Clear Cart");
                    Console.WriteLine("4. Checkout");
                    Console.WriteLine("5. Back");

                    Console.Write("Choice: ");
                    int c;
                    if (!int.TryParse(Console.ReadLine(), out c)) continue;

                    if (c == 1)
                    {
                        Console.Write("Enter item #: ");
                        int i = int.Parse(Console.ReadLine()) - 1;
                        if (i >= 0 && i < cartCount)
                        {
                            cart[i] = cart[cartCount - 1];
                            cartCount--;
                        }
                    }

                    else if (c == 2)
                    {
                        Console.Write("Item #: ");
                        int i = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("New qty: ");
                        int newQty = int.Parse(Console.ReadLine());

                        if (i >= 0 && i < cartCount)
                        {
                            cart[i].quantity = newQty;
                            cart[i].subtotal = newQty * cart[i].product.Price;
                        }
                    }
                
                    else if (c == 3)
                    {
                        cartCount = 0;
                        Console.WriteLine("Cart cleared!");
                    }

                    else if (c == 4)
                    {
                        double discount = total >= 5000 ? total * 0.10 : 0;
                        double finalTotal = total - discount;

                        double payment;
                        while (true)
                        {
                            Console.Write("Enter payment: ");
                            if (!double.TryParse(Console.ReadLine(), out payment) || payment < finalTotal)
                            {
                                Console.WriteLine("Invalid or insufficient!");
                            }
                            else break;
                        }

                        double change = payment - finalTotal;

                        Console.WriteLine("\n--- RECEIPT ---");
                        Console.WriteLine("Receipt No: " + receiptCounter);
                        Console.WriteLine("Date: " + DateTime.Now);

                        Console.WriteLine("Total: P" + total);
                        Console.WriteLine("Discount: P" + discount);
                        Console.WriteLine("Final: P" + finalTotal);
                        Console.WriteLine("Payment: P" + payment);
                        Console.WriteLine("Change: P" + change);

                        history[historyCount++] = new Order
                        {
                            ReceiptNo = receiptCounter,
                            FinalTotal = finalTotal
                        };

                        receiptCounter++;
                        cartCount = 0;

                        Console.WriteLine("\nLOW STOCK ALERT:");
                        foreach (var p in products)
                        {
                            if (p.Stock <= 5)
                                Console.WriteLine(p.Name + " has only " + p.Stock + " left.");
                        }

                        break;
                    }

                    else break;
                }
            }

           // else if (choice == 3)


                
            
            //else if (choice == 4)
                


                
            else if (choice == 5)
            break
        }
    }
}
