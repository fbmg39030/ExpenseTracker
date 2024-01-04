using Shop.API.Models.Dbo;

namespace Shop.API.Models.Dto;

public class ProductImageDto : BaseDto<ProductImageDbo, ProductImageDto>
{
    public virtual string Name { get; set; }
    public virtual byte[] Bytes { get; set; }
    public virtual decimal Version { get; set; }
    public virtual string MimeType { get; set; }
    public ProductImageDto(ProductImageDbo dbo) : base(dbo)
    {
    }

    public static ProductImageDto FromDbo(ProductImageDbo dbo, Dictionary<Guid, object> dtoReferences)
    {
        if (dtoReferences.ContainsKey(dbo.LogicalObjectId))
        {
            return dtoReferences[dbo.LogicalObjectId] as ProductImageDto;
        }
        var dto = new ProductImageDto(dbo)
        {
            Name = dbo.Name,
            Bytes = dbo.Bytes,
            Version = dbo.Version,
            MimeType = dbo.MimeType,
        };
        dtoReferences.Add(dto.LogicalObjectId, dto);
        return dto;
    }

    public static List<ProductImageDto> FromDboList(IList<ProductImageDbo> dboList, Dictionary<Guid, object> dtoReferences)
    {
        return dboList.Select(dbo=> FromDbo(dbo, dtoReferences)).ToList();
    }
}
