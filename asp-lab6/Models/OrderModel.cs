namespace asp_lab6.Models;

public class OrderModel
{
    public Guid Id {get; set;}
    public UserModel Customer { get; set; }
    public List<OrderItemModel> Products { get; set; } = new List<OrderItemModel>();

    public OrderModel()
    {
        Id = Guid.NewGuid();
    }
}