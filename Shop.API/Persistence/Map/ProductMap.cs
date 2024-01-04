using FluentNHibernate.Mapping;
using NHibernate.Mapping;
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
        Map(x => x.Description).CustomSqlType("nvarchar(max)").Length(Int32.MaxValue).Nullable(); ;
        Map(x => x.Price);
        Map(x => x.Tag);
        Map(x => x.Status).CustomType<ProductStatus>();

        //HasMany(x => x.Images).Table("ProductImage")
        //    .KeyColumn(nameof(TableName) + Reference).AsList()
        //    .Element("value").Not.LazyLoad();
        HasMany(x => x.Images).Cascade.AllDeleteOrphan().Not.LazyLoad().KeyColumn(TableName + Reference);

        HasMany(x => x.TechnicalDetails).Table("ProductTechnicalDetails")
            .KeyColumn(nameof(TableName)+Reference).AsMap<string>("string")
            .Element("value").Not.LazyLoad();
    }
}
