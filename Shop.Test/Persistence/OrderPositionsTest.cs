using Shop.API.Models;
using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;
using Shop.API.Persistence.Dao;
using Shop.API.Persistence.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Test.Persistence;

[TestClass]
public class OrderPositionsTest
{
    [TestMethod]
    public void CreateOrderPositions()
    {
        var productLoid2 = Guid.NewGuid();
        var productDbo = new ProductDbo
        {
            LogicalObjectId = productLoid2,
            Name1 = "Shoes",
            Price = 40.5m
        };
        ProductDao.SaveOrUpdate(productDbo);

        var orderPositionDbo = new OrderPositionDbo
        {
            LogicalObjectId = Guid.NewGuid(),
            Product = productDbo,
            Quantity = 2,
            UnitPrice = 18.5m
        };
        OrderPositionDao.SaveOrUpdate(orderPositionDbo);

        var queriedPosition = OrderPositionDao.QueryByLogicalObjectId(orderPositionDbo.LogicalObjectId);
        Assert.IsNotNull(queriedPosition);
        Assert.AreEqual(queriedPosition.LogicalObjectId, orderPositionDbo.LogicalObjectId);
    }

    [TestMethod]
    public void UpdateOrderPosition()
    {
        var productLoid = Guid.NewGuid();
        var productDbo = new ProductDbo
        {
            LogicalObjectId = productLoid,
            Name1 = "Shoes",
            Price = 40.5m
        };
        ProductDao.SaveOrUpdate(productDbo);

        var orderPositionDbo = new OrderPositionDbo
        {
            LogicalObjectId = Guid.NewGuid(),
            Product = productDbo,
            Quantity = 2,
            UnitPrice = 18.5m
        };
        OrderPositionDao.SaveOrUpdate(orderPositionDbo);

        //Update 
        productDbo.Name1 = "Bottle";
        productDbo.Price = 10.5m;
        ProductDao.SaveOrUpdate(productDbo);

        orderPositionDbo.Product = productDbo;
        orderPositionDbo.UnitPrice = 5.8m;
        OrderPositionDao.SaveOrUpdate(orderPositionDbo);

        var queriedPosition = OrderPositionDao.QueryByLogicalObjectId(orderPositionDbo.LogicalObjectId);
        Assert.IsNotNull(queriedPosition);
        Assert.AreEqual(queriedPosition.LogicalObjectId, orderPositionDbo.LogicalObjectId);
        Assert.AreEqual(queriedPosition.UnitPrice, 5.8m);
        Assert.AreEqual(queriedPosition.Product.Name1, "Bottle");
    }
}
