using Shop.API.Models.Dbo;
using Shop.API.Models.Enum;

namespace Shop.API.Models.Dto;

public class ProductDto : BaseDto<ProductDbo, ProductDto>
{
    public ProductDto(ProductDbo dbo) : base(dbo)
    {
        
    }
    public string Name1 { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public string Tag { get; set; }
    public IDictionary<string, string> TechDetails { get; set; }
    public List<ProductImageDto> Images { get; set; }

    public static ProductDto FromDbo(ProductDbo dbo, Dictionary<Guid, object> dtoReferences)
    {
        if (dtoReferences.ContainsKey(dbo.LogicalObjectId))
        {
            return dtoReferences[dbo.LogicalObjectId] as ProductDto;
        }
        var dto = new ProductDto(dbo)
        {
            Name1 = dbo.Name1,
            Description = dbo.Description,
            Price = dbo.Price,
            Status = dbo.Status,
            Tag = dbo.Tag,
            Images = ProductImageDto.FromDboList(dbo.Images, new()),          
            TechDetails = dbo.TechnicalDetails ?? new Dictionary<string, string>()            
        };
        dtoReferences.Add(dto.LogicalObjectId, dto);
        return dto;
    }

    public static List<ProductDto> FromDboList(IEnumerable<ProductDbo> dbos, Dictionary<Guid, object> dtoReferences)
    {
        return dbos?.Select(dbo => FromDbo(dbo, dtoReferences)).ToList();
    }
}
