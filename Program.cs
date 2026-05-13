using System;

class Product
{
    private int id;
    private string name;
    private string category;
    private double price;
    private int remainingStock;

    public void SetId(int id) { this.id = id; }
    public void SetName(string name) { this.name = name; }
    public void SetCategory(string category) { this.category = category; }
    public void SetPrice(double price) { this.price = price; }
    public void SetRemainingStock(int stock) { this.remainingStock = stock; }

    public int GetId() { return id; }
    public string GetName() { return name; }
    public string GetCategory() { return category; }
    public double GetPrice() { return price; }
    public int GetRemainingStock() { return remainingStock; }

    public void ReduceStock(int qty)
    {
        remainingStock -= qty;
    }

    public void DisplayProduct()
    {
        Console.WriteLine($"{id}. {name} ({category}) - P{price} (Stock: {remainingStock})");
    }
}

class CartItem
{
    private Product product;
    private int quantity;
    private double subtotal;

    public void SetProduct(Product product) { this.product = product; }
    public void SetQuantity(int quantity) { this.quantity = quantity; }
    public void SetSubtotal(double subtotal) { this.subtotal = subtotal; }

    public Product GetProduct() { return product; }
    public int GetQuantity() { return quantity; }
    public double GetSubtotal() { return subtotal; }
}




