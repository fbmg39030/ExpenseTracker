namespace Shop.API.Models.Dbo;

public class OrderDbo : BaseDbo
{
    public virtual string UserId { get; set; }
    public virtual decimal TotalAmount { get; set; }

    public virtual IList<OrderPositionDbo> OrderPositionList { get; set; }
}
