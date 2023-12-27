
using Shop.API.Application;
using Shop.API.Models.Dto;
using Shop.API.Models.Request;
using Shop.API.Persistence.QueryParams;

namespace Shop.API.Api;

public static class OrderApi
{
    private const string ClassName = nameof(OrderApi);

    public static void Setup(WebApplication application)
    {
        application.MapPost("/api/Order/CreateOrUpdate", CreateOrUpdate)
            .WithName("Order_CreateOrUpdate")
            .WithTags("Order")
            .Produces(StatusCodes.Status200OK, typeof(OrderDto));

        application.MapPost("/api/Order/Query", Query)
            .WithName("Order_Query")
            .WithTags("Order")
            .Produces(StatusCodes.Status200OK, typeof(OrderDto));
    }

    public static IResult CreateOrUpdate(OrderCreateOrUpdateRequest request)
    {
        var result = OrderManager.CreateOrUpdate(request);
        return Results.Json(result.ResponseBody, statusCode: (int)result.StatusCode);
    }

    public static IResult Query(OrderQp qp)
    {
        var result = OrderManager.Query(qp);
        return Results.Json(result.ResponseBody, statusCode: (int)result.StatusCode);
    }
}
