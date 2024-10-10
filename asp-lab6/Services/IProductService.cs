using asp_lab6.Models;

namespace asp_lab6.Services;

public interface IProductService
{
    List<ProductModel> GetProducts();
    
    ProductModel FindProductById(int id);
}