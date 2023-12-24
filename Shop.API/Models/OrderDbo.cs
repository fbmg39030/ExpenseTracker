namespace Shop.API.Models;

public class OrderDbo : BaseDbo
{
    public virtual string UserId { get; set; }
    public virtual decimal TotalAmount { get; set; }
}
