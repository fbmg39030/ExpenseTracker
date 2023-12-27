using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;

namespace Shop.API.Models.Result;

public class OrderResult : AResult<OrderDbo, OrderDto, OrderResult>
{
}
