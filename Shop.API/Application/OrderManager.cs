using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;
using Shop.API.Models.Request;
using Shop.API.Models.Result;
using Shop.API.Persistence.Dao;
using Shop.API.Persistence.QueryParams;
using System.Net;

namespace Shop.API.Application;

public static class OrderManager
{
    private const string ClassName = nameof(OrderManager);

    public static OrderResult CreateOrUpdate(OrderCreateOrUpdateRequest request)
    {
        OrderDbo orderDbo; 

        List<OrderPositionDbo>OrderPositions= new List<OrderPositionDbo>();
        foreach (var item in request.OderPositionRequests)
        {
            var productDbo = ProductDao.QueryByLogicalObjectId(item.ProductLoid);
            if(productDbo == null)
            {
                return OrderResult.FromMessage(HttpStatusCode.NotFound, "Didn't find product with loid: " + item.ProductLoid);
            }

            var position = new OrderPositionDbo()
            {
                LogicalObjectId = Guid.NewGuid(),
                Product = productDbo,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice ?? productDbo.Price
            };
            OrderPositions.Add(position);
        }

        orderDbo = new OrderDbo()
        {
            LogicalObjectId = Guid.NewGuid(),
            OrderPositionList = OrderPositions,
            UserId = "UserID WILL BE READ FROM oAUTH",
            TotalAmount = OrderPositions.Sum(position => position.UnitPrice)
        };
        OrderDao.SaveOrUpdate(orderDbo);
        var orderDto = OrderDto.FromDbo(orderDbo, new());
        return OrderResult.FromDto(HttpStatusCode.OK, orderDto);
    }

    public static OrderResult Query(OrderQp qp)
    {
        var orderDboList = OrderDao.QueryByParameters(qp);
        var orderDtoList = OrderDto.FromDboList(orderDboList, new());
        return OrderResult.FromDtoList(HttpStatusCode.OK, orderDtoList);
    }
}
