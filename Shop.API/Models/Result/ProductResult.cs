using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;

namespace Shop.API.Models.Result;

public class ProductResult : AResult<ProductDbo, ProductDto, ProductResult>
{
}
