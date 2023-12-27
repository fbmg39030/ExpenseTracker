using Shop.API.Models.Dbo;

namespace Shop.API.Models.Dto;

public class OrderPositionDto : BaseDto<OrderPositionDbo, OrderPositionDto>
{
    public OrderPositionDto(OrderPositionDbo dbo): base(dbo)
    {
        
    }
    public virtual ProductDto Product { get; set; }
    public virtual int Quantity { get; set; }
    public virtual decimal UnitPrice { get; set; }

    public static OrderPositionDto FromDbo(OrderPositionDbo dbo, Dictionary<Guid, object> dtoReferences)
    {
        if (dtoReferences.ContainsKey(dbo.LogicalObjectId))
        {
            return dtoReferences[dbo.LogicalObjectId] as OrderPositionDto;
        }

        var dto = new OrderPositionDto(dbo)
        {
            Product = ProductDto.FromDbo(dbo.Product, dtoReferences),
            Quantity = dbo.Quantity,
            UnitPrice = dbo.UnitPrice,
        };
        return dto;
    }

    public static IList<OrderPositionDto> FromDboList(IList<OrderPositionDbo> dboList, Dictionary<Guid, object> dtoReferences) {
        return dboList?.Select(dbo => FromDbo(dbo, dtoReferences)).ToList();
    }
}
