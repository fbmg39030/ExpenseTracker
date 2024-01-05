using Shop.API.Models.Enum;

namespace Shop.API.Persistence.QueryParams;

public class ProductQp : BaseQp
{
    public virtual string Name1 { get; set; }
    public virtual string Description { get; set;}
    public virtual decimal Price { get; set; }
    public virtual ProductStatus[] Status { get; set; }
}
