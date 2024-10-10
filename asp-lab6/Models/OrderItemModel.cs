namespace asp_lab6.Models;

public class OrderItemModel
{
    public Guid Id { get; set; }
    public ProductModel Product { get; set; }
    public int Quantity { get; set; }
}