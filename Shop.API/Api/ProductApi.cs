
using Shop.API.Application;
using Shop.API.Models.Dto;
using Shop.API.Models.Request;
using Shop.API.Persistence.QueryParams;

namespace Shop.API.Api;

public static class ProductApi
{
    private const string ClassName = nameof(ProductApi); //Later useful for logging
    
    public static void Setup(WebApplication application)
    {
        //application.MapPost("/api/Product/AddOrUpdate", AddOrUpdate)
        application.MapPost("/api/Product/Query", Query)
            .WithName("Product_Query")
            .WithTags("Product")
            .Produces(StatusCodes.Status200OK, typeof(List<ProductDto>));

        application.MapPost("/api/Product/AddOrUpdate", AddOrUpdate)
            .WithName("Product_AddOrUpdate")
            .WithTags("Product")
            .Produces(StatusCodes.Status200OK, typeof(ProductDto));
    }

    public static IResult Query(ProductQp qp)
    {
        var result = ProductManager.Query(qp);

        return Results.Json(result.ResponseBody, statusCode: (int)result.StatusCode);
    }

    public static IResult AddOrUpdate(ProductAddOrUpdateRequest request)
    {
        var result = ProductManager.AddOrUpdate(request);

        return Results.Json(result.ResponseBody, statusCode: (int)result.StatusCode);
    }
}
