using ASP_HuynhNha_2122110299.Model;

public class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
}
