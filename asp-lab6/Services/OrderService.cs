using asp_lab6.Models;

namespace asp_lab6.Services;

public class OrderService : IOrderService
{
    static List<OrderModel> orders = new List<OrderModel>();

    IUserService _userService;
    
    public OrderService(IUserService userService)
    {
        _userService = userService;
    }
    
    public OrderModel CreateOrder(Guid customerGuid)
    {
        var newOrder = new OrderModel();
        newOrder.Customer = _userService.GetUser(customerGuid);
        orders.Add(newOrder);
        return newOrder;
    }

    public OrderModel GetOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public OrderModel GetOrder(Guid orderId)
    {
        return orders.SingleOrDefault(o => o.Id == orderId);
    }

    public bool AddProduct(OrderModel order, ProductModel product, int quantity)
    {
        var _order = GetOrder(order.Id);
        
        if (_order != null)
        {
            var existingProduct = _order.Products.SingleOrDefault(item => item.Product.id == product.id);

            if (existingProduct != null)
            {
                existingProduct.Quantity += quantity;
            }
            else
            {
                _order.Products.Add(new OrderItemModel { Id = Guid.NewGuid(), Quantity = quantity, Product = product });
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}