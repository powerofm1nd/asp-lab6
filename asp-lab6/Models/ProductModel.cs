namespace asp_lab6.Models;

public class ProductModel
{
    public int id {get; set;}
    public string Name {get; set;}
    public double Price { get; set;}
    public int AvailableQuantity { get; set;}

    public List<ProductModel> GetProducts()
    {
        return ProductModel.products;
    }
    
    private static List<ProductModel> products = new List<ProductModel>();

    static ProductModel()
    {
        products = new List<ProductModel>
        {
            new ProductModel { id = 1, Name = "Margherita", Price = 8.99, AvailableQuantity = 20 },
            new ProductModel { id = 2, Name = "Pepperoni", Price = 10.99, AvailableQuantity = 15 },
            new ProductModel { id = 3, Name = "BBQ Chicken", Price = 12.49, AvailableQuantity = 10 },
            new ProductModel { id = 4, Name = "Hawaiian", Price = 11.99, AvailableQuantity = 12 },
            new ProductModel { id = 5, Name = "Veggie Supreme", Price = 9.49, AvailableQuantity = 18 }
        };
    }
}