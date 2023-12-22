using Shop.API.Models;
using NHibernate.Mapping.ByCode;

namespace Shop.API.Persistence;

public class ProductMap: BaseMap<ProductDbo>
{
    public const string TableName = "Product";

    public ProductMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Name1);
        Map(x => x.Description);
        Map(x => x.Price);
        Map(x => x.StockQuantity);
        Map(x => x.ImageUrl);
        Not.LazyLoad();
    }
}
