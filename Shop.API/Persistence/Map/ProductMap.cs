using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using Shop.API.Models.Dbo;
using Shop.API.Models.Enum;
using System.Configuration;

namespace Shop.API.Persistence.Map;

public class ProductMap : BaseMap<ProductDbo>
{
    public const string TableName = "Product";

    public ProductMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Name1).Not.Nullable();
        Map(x => x.Description).Length(4096);
        Map(x => x.Price);
        Map(x => x.Tag);
        Map(x => x.Status).CustomType<ProductStatus>();
        //Map(x => x.StockQuantity);
        Map(x => x.ImageUrl).Nullable();

        HasMany(x => x.TechnicalDetails).Table("Dictionary_table")
            .KeyColumn(nameof(TableName)+Reference).AsMap<string>("string")
            .Element("value").Not.LazyLoad();
    }
}
