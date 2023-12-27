using Shop.API.Models.Dbo;

namespace Shop.API.Models.Dto;

public class OrderDto : BaseDto<OrderDbo, OrderDto>
{
    public OrderDto(OrderDbo dbo) : base(dbo)
    {
        
    }
    public virtual string UserId { get; set; }
    public virtual decimal TotalAmount { get; set; }
    public virtual IList<OrderPositionDto> OrderPositionList { get; set; }


    public static OrderDto FromDbo(OrderDbo dbo, Dictionary<Guid, object> dtoReferences)
    {
        if (dtoReferences.ContainsKey(dbo.LogicalObjectId))
        {
            return dtoReferences[dbo.LogicalObjectId] as OrderDto;
        }

        var dto = new OrderDto(dbo)
        {
            UserId = dbo.UserId,
            TotalAmount = dbo.TotalAmount,
            OrderPositionList = OrderPositionDto.FromDboList(dbo.OrderPositionList, dtoReferences)
        };
        dtoReferences.Add(dto.LogicalObjectId, dto);
        return dto; 
    }

    public static List<OrderDto> FromDboList(IList<OrderDbo> dboList, Dictionary<Guid, object> dtoReferences)
    {
        return dboList?.Select(dbo => FromDbo(dbo, dtoReferences)).ToList();
    }
}
