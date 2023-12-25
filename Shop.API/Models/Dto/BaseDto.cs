using Microsoft.AspNetCore.Http.HttpResults;
using Shop.API.Models.Dbo;

namespace Shop.API.Models.Dto;

public abstract class BaseDto<TDbo, TDto> where TDbo : BaseDbo where TDto : BaseDto<TDbo, TDto>
{
    protected BaseDto(TDbo dbo)
    {
        LogicalObjectId = dbo.LogicalObjectId;
        CreatedAt = dbo.CreatedAt;
        LastModifiedAt = dbo.LastModifiedAt;
    }
    public Guid LogicalObjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}
