using Shop.API.Api;
using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;
using Shop.API.Models.Request;
using Shop.API.Persistence.Dao;
using Shop.API.Persistence.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Test.API;

[TestClass]
public class OrderApiTest
{
    [TestMethod]
    public void CreateOrder()
    {
        var productLoid = Guid.NewGuid();
        var productDbo = new ProductDbo()
        {
            LogicalObjectId = productLoid,
            Name1 = "Product1" + productLoid,
            Price = 20m
        };
        ProductDao.SaveOrUpdate(productDbo);

        var productLoid2 = Guid.NewGuid();
        var productDbo2 = new ProductDbo()
        {
            LogicalObjectId = productLoid2,
            Name1 = "Product2" + productLoid2,
            Price = 10m
        };
        ProductDao.SaveOrUpdate(productDbo2);


        var positionsList = new List<CreateOrderPositionRequest>();

        var position1 = new CreateOrderPositionRequest()
        {
            Quantity = 2,
            UnitPrice = 10,
            ProductLoid = productLoid
        };
        var position2 = new CreateOrderPositionRequest()
        {
            Quantity = 5,
            UnitPrice = 5m,
            ProductLoid = productLoid2
        };

        positionsList.Add(position1);
        positionsList.Add(position2);

        OrderCreateOrUpdateRequest request = new OrderCreateOrUpdateRequest()
        {
            OderPositionRequests = positionsList
        };
        
        var orderResult = OrderApi.CreateOrUpdate(request);
        var orderDto = orderResult.GetType().GetProperty("Value")?.GetValue(orderResult, null) as OrderDto;
        Assert.IsNotNull(orderDto);
        Assert.IsTrue(orderDto.OrderPositionList.Count == 2);
        Assert.IsTrue(orderDto.OrderPositionList[1].Quantity == 5);
    }

    [TestMethod]
    public void QueryOrder()
    {
        var productLoid = Guid.NewGuid();
        var productDbo = new ProductDbo()
        {
            LogicalObjectId = productLoid,
            Name1 = "Product1" + productLoid,
            Price = 20m
        };
        ProductDao.SaveOrUpdate(productDbo);

        var positionsList = new List<CreateOrderPositionRequest>();
        var position1 = new CreateOrderPositionRequest()
        {
            Quantity = 2,
            UnitPrice = 10,
            ProductLoid = productLoid
        };
        positionsList.Add(position1);

        OrderCreateOrUpdateRequest request = new OrderCreateOrUpdateRequest()
        {
            OderPositionRequests = positionsList
        };

        var orderResult = OrderApi.CreateOrUpdate(request);
        var orderDto = orderResult.GetType().GetProperty("Value")?.GetValue(orderResult, null) as OrderDto;
        Assert.IsNotNull(orderDto);


        //atm there are not real qp to query from --> will use empty, will search for all
        var queriedOrderResult = OrderApi.Query(new OrderQp() { });
        var queriedResultDto = queriedOrderResult.GetType().GetProperty("Value")?.GetValue(queriedOrderResult, null) as List<OrderDto>;  
        Assert.IsNotNull(queriedResultDto);
        Assert.IsTrue(queriedResultDto.FirstOrDefault().OrderPositionList.FirstOrDefault().Product.Name1 == "Product1" + productLoid);
    }
}

