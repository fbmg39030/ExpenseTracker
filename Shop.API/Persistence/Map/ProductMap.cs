using NHibernate.Mapping.ByCode;
using Shop.API.Models.Dbo;

namespace Shop.API.Persistence.Map;

public class ProductMap : BaseMap<ProductDbo>
{
    public const string TableName = "Product";

    public ProductMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Name1).Not.Nullable();
        Map(x => x.Description);
        Map(x => x.Price);
        Map(x => x.Tag);
        Map(x => x.Status).CustomType<ProductStatus>();
        //Map(x => x.StockQuantity);
        Map(x => x.ImageUrl).Nullable();
        Not.LazyLoad();
    }
}
