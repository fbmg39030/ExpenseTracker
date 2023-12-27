namespace Shop.API.Models.Dbo;

public class OrderPositionDbo : BaseDbo
{
    public virtual ProductDbo Product { get; set; }
    public virtual int Quantity { get; set; }
    public virtual decimal UnitPrice { get; set; }
}
