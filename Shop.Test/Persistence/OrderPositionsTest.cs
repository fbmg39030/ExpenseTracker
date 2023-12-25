using Shop.API.Models;
using Shop.API.Models.Dbo;
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
        var loid = Guid.NewGuid();
        var orderDbo = new OrderDbo
        {
            LogicalObjectId = loid,
            UserId = "TestLoid" + loid,
            TotalAmount = 0,
        };
        OrderDao.SaveOrUpdate(orderDbo);

        var productLoid = Guid.NewGuid();   
        var productDbo = new ProductDbo
        {
            LogicalObjectId = productLoid,
            Name1 = "Bottle",
            Price = 20.5m
        };
        ProductDao.SaveOrUpdate(productDbo);

        var orderPositionsLoid = Guid.NewGuid();
        var orderPosition1 = new OrderPositionDbo
        {
            LogicalObjectId = orderPositionsLoid,
            Order = orderDbo,
            Product = productDbo,
            Quantity = 2,
            UnitPrice = 18.5m
        };
        OrderPositionDao.SaveOrUpdate(orderPosition1);

        var queriedPosition = OrderPositionDao.QueryByLogicalObjectId(orderPositionsLoid);
        Assert.IsNotNull(queriedPosition);
        Assert.AreEqual(queriedPosition.Order.LogicalObjectId, orderDbo.LogicalObjectId);
        Assert.AreEqual(queriedPosition.Product.LogicalObjectId, productLoid);

    }

    [TestMethod]
    public void FindOrderPositionsForOrder()
    {
        var orderLoid = Guid.NewGuid();
        var orderDbo = new OrderDbo
        {
            LogicalObjectId = orderLoid,
            UserId = "TestLoid" + orderLoid,
            TotalAmount = 0,
        };
        OrderDao.SaveOrUpdate(orderDbo);

        var productLoid = Guid.NewGuid();
        var productDbo = new ProductDbo
        {
            LogicalObjectId = productLoid,
            Name1 = "Bottle",
            Price = 20.5m
        };
        var productLoid2 = Guid.NewGuid();
        var productDbo2 = new ProductDbo
        {
            LogicalObjectId = productLoid2,
            Name1 = "Mouse",
            Price = 10m
        };
        ProductDao.SaveOrUpdate(productDbo);
        ProductDao.SaveOrUpdate(productDbo2);

        var orderPositionsLoid = Guid.NewGuid();
        var orderPositionsLoid2 = Guid.NewGuid();
        var orderPosition1 = new OrderPositionDbo
        {
            LogicalObjectId = orderPositionsLoid,
            Order = orderDbo,
            Product = productDbo,
            Quantity = 2,
            UnitPrice = 18.5m
        };
        OrderPositionDao.SaveOrUpdate(orderPosition1);
        var orderPosition2 = new OrderPositionDbo
        {
            LogicalObjectId = orderPositionsLoid2,
            Order = orderDbo,
            Product = productDbo2,
            Quantity = 1,
            UnitPrice = 18.5m
        };
        OrderPositionDao.SaveOrUpdate(orderPosition2);


        var queriedPositionsList = OrderPositionDao.QueryByParameters(new OrderPositionQp { Order = orderLoid });
        Assert.IsNotNull(queriedPositionsList);
        Assert.IsTrue(queriedPositionsList.Count == 2);

        var queriedPositionsList2 = OrderPositionDao.QueryByParameters(new OrderPositionQp { Product = productLoid });
        Assert.IsNotNull(queriedPositionsList2);
        Assert.IsTrue(queriedPositionsList2.Count == 1);
    }
}
