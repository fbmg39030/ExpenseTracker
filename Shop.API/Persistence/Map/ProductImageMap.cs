using Shop.API.Models.Dbo;

namespace Shop.API.Persistence.Map;

public class ProductImageMap : BaseMap<ProductImageDbo>
{
    const string TableName = "ProductImage";

    public ProductImageMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Name).Not.Nullable();
        Map(x => x.Version).Nullable();
        Map(x => x.MimeType).Not.Nullable();

        Map(x => x.Bytes).CustomSqlType("VARBINARY(MAX)").Length(160000);
    }
}
