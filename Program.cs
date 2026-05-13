using System;
using System.Collections.Generic;

class Product
{
    private int id;
    private string name;
    private string category;
    private double price;
    private int remainingStock;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    public double Price
    {
        get { return price; }
        set
        {
            if (value >= 0)
                price = value;
        }
    }

    public int RemainingStock
    {
        get { return remainingStock; }
        set
        {
            if (value >= 0)
                remainingStock = value;
        }
    }

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} ({Category}) - P{Price} (Stock: {RemainingStock})");
    }
}

class CartItem
{
    private Product product;
    private int quantity;
    private double subtotal;

    public Product Product
    {
        get { return product; }
        set { product = value; }
    }

    public int Quantity
    {
        get { return quantity; }
        set
        {
            if (value >= 0)
                quantity = value;
        }
    }

    public double Subtotal
    {
        get { return subtotal; }
        set
        {
            if (value >= 0)
                subtotal = value;
        }
    }
}

class Order
{
    private int receiptNo;
    private double finalTotal;

    public int ReceiptNo
    {
        get { return receiptNo; }
        set { receiptNo = value; }
    }

    public double FinalTotal
    {
        get { return finalTotal; }
        set
        {
            if (value >= 0)
                finalTotal = value;
        }
    }
}

class Program
{
    static int receiptCounter = 1;

    static void Main()
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Monitor", Category="Electronics", Price = 5000, RemainingStock = 10 },
            new Product { Id = 2, Name = "Keyboard", Category="Electronics", Price = 1000, RemainingStock = 20 },
            new Product { Id = 3, Name = "Mouse", Category="Electronics", Price = 600, RemainingStock = 25 },
            new Product { Id = 4, Name = "Shirt", Category="Clothing", Price = 800, RemainingStock = 20 },
            new Product { Id = 5, Name = "Pants", Category="Clothing", Price = 500, RemainingStock = 25 },
            new Product { Id = 6, Name = "Jacket", Category="Clothing", Price = 1200, RemainingStock = 10 }
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        Order[] history = new Order[10];
        int historyCount = 0;

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

                foreach (var p in products)
                    p.DisplayProduct();

                Console.Write("Enter product #: ");

                int pnum;

                if (!int.TryParse(Console.ReadLine(), out pnum) ||
                    pnum < 1 || pnum > products.Length)
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

                if (qty > selected.RemainingStock)
                {
                    Console.WriteLine("Not enough stock!");
                    continue;
                }

                bool found = false;

                for (int i = 0; i < cartCount; i++)
                {
                    if (cart[i].Product.Id == selected.Id)
                    {
                        cart[i].Quantity += qty;
                        cart[i].Subtotal += qty * selected.Price;

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    cart[cartCount] = new CartItem
                    {
                        Product = selected,
                        Quantity = qty,
                        Subtotal = qty * selected.Price
                    };

                    cartCount++;
                }

                selected.RemainingStock -= qty;

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
                        Console.WriteLine(
                            $"{i + 1}. " +
                            $"{cart[i].Product.Name} x{cart[i].Quantity} = P{cart[i].Subtotal}"
                        );

                        total += cart[i].Subtotal;
                    }

                    Console.WriteLine("Total: P" + total);

                    Console.WriteLine("\n1. Remove Item");
                    Console.WriteLine("2. Update Quantity");
                    Console.WriteLine("3. Clear Cart");
                    Console.WriteLine("4. Checkout");
                    Console.WriteLine("5. Back");

                    Console.Write("Choice: ");

                    int c;

                    if (!int.TryParse(Console.ReadLine(), out c))
                        continue;

                    if (c == 1)
                    {
                        Console.Write("Enter item #: ");

                        int i = int.Parse(Console.ReadLine()) - 1;

                        if (i >= 0 && i < cartCount)
                        {
                            cart[i].Product.RemainingStock += cart[i].Quantity;

                            cart[i] = cart[cartCount - 1];

                            cartCount--;

                            Console.WriteLine("Item removed!");
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
                            cart[i].Product.RemainingStock += cart[i].Quantity;

                            if (newQty > cart[i].Product.RemainingStock)
                            {
                                Console.WriteLine("Not enough stock!");

                                cart[i].Product.RemainingStock -= cart[i].Quantity;

                                continue;
                            }

                            cart[i].Quantity = newQty;

                            cart[i].Subtotal =
                                newQty * cart[i].Product.Price;

                            cart[i].Product.RemainingStock -= newQty;

                            Console.WriteLine("Quantity updated!");
                        }
                    }

                    else if (c == 3)
                    {
                        for (int i = 0; i < cartCount; i++)
                        {
                            cart[i].Product.RemainingStock += cart[i].Quantity;
                        }

                        cartCount = 0;

                        Console.WriteLine("Cart cleared!");
                    }

                    else if (c == 4)
                    {
                        double discount =
                            total >= 5000 ? total * 0.10 : 0;

                        double finalTotal = total - discount;

                        double payment;

                        while (true)
                        {
                            Console.Write("Enter payment: ");

                            if (!double.TryParse(Console.ReadLine(), out payment)
                                || payment < finalTotal)
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
                            if (p.RemainingStock <= 5)
                            {
                                Console.WriteLine(
                                    p.Name + " has only " +
                                    p.RemainingStock + " left."
                                );
                            }
                        }

                        break;
                    }

                    else if (c == 5)
                    {
                        break;
                    }
                }
            }

            else if (choice == 3)
            {
                Console.Write("Search name: ");

                string search = Console.ReadLine().ToLower();

                foreach (var p in products)
                {
                    if (p.Name.ToLower().Contains(search))
                    {
                        p.DisplayProduct();
                    }
                }
            }

            else if (choice == 4)
            {
                Console.WriteLine("\n--- ORDER HISTORY ---");

                for (int i = 0; i < historyCount; i++)
                {
                    Console.WriteLine(
                        $"Receipt #{history[i].ReceiptNo} - P{history[i].FinalTotal}"
                    );
                }
            }

            else if (choice == 5)
            {
                break;
            }
        }
    }
}
