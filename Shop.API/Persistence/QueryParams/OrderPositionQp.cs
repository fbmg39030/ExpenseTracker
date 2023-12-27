using Shop.API.Models;

namespace Shop.API.Persistence.QueryParams;

public class OrderPositionQp : BaseQp
{
    public virtual Guid Product { get; set; }
    //public virtual int Quantity { get; set; }
    //public virtual decimal UnitPrice { get; set; }
}
