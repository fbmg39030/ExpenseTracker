using NHibernate.Type;
using Shop.API.Models.Enum;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Shop.API.Models.Dbo;

public class ProductDbo : BaseDbo
{
    public virtual string Name1 { get; set; }
    public virtual string Description { get; set; }
    public virtual decimal Price { get; set; }
    public virtual ProductStatus Status { get; set; }
    public virtual string Tag { get; set; }
    public virtual IList<ProductImageDbo> Images { get; set; }
    public virtual IDictionary<string, string>? TechnicalDetails { get; set; }

}
