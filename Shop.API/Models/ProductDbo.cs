using System.Xml.Linq;

namespace Shop.API.Models;

public class ProductDbo : BaseDbo
{
    public virtual string Name1 { get; set; }
    public virtual string Description { get; set; }
    public virtual decimal Price { get; set; }
    //public virtual int StockQuantity { get; set; }
    public virtual List<string> ImageUrl { get; set; }
}
