using asp_lab6.Models;

namespace asp_lab6.Services;

public class ProductService : IProductService
{
    private static List<ProductModel> _productStorage;

    static ProductService()
    {
        _productStorage = new List<ProductModel>
        {
            new ProductModel { id = 1, Name = "Margherita", Price = 8.99, AvailableQuantity = 20 },
            new ProductModel { id = 2, Name = "Pepperoni", Price = 10.99, AvailableQuantity = 15 },
            new ProductModel { id = 3, Name = "BBQ Chicken", Price = 12.49, AvailableQuantity = 10 },
            new ProductModel { id = 4, Name = "Hawaiian", Price = 11.99, AvailableQuantity = 12 },
            new ProductModel { id = 5, Name = "Veggie Supreme", Price = 9.49, AvailableQuantity = 18 }
        };
    }
    
    public List<ProductModel> GetProducts()
    {
        return new List<ProductModel>(_productStorage);
    }

    public ProductModel FindProductById(int id)
    {
        var foundProduct = _productStorage.FirstOrDefault(x => x.id == id);

        if (foundProduct == null)
        {
            throw new KeyNotFoundException("Product not found with given Id");
        }
        else
        {
            return foundProduct;
        }
    }
}