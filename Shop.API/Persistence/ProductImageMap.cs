using NHibernate.Mapping;
using Shop.API.Models;

namespace Shop.API.Persistence;

public class ProductImageMap : BaseMap<ProductImageDbo>
{
    public const string Table = "ProductImages";

    public ProductImageMap() : base(Table)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Image).Length(int.MaxValue);
        References(x => x.Product).Column(nameof(ProductDbo) + Reference).Cascade.None().Not
            .LazyLoad();
    }
}
