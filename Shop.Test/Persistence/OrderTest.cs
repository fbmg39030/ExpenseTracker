using Shop.API.Models;
using Shop.API.Models.Dbo;
using Shop.API.Persistence.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Test.Persistence;

[TestClass]
public class OrderTest
{
    [TestMethod]
    public void CreateOrder()
    {
        var loid = Guid.NewGuid();

        var orderDbo = new OrderDbo
        {
            LogicalObjectId = loid,
            UserId = "TestLoid" + loid,
            TotalAmount = 0,
        };
        OrderDao.SaveOrUpdate(orderDbo);

        var queriedOrderDbo = OrderDao.QueryByLogicalObjectId(loid);
        Assert.IsNotNull(queriedOrderDbo);
        Assert.AreEqual(queriedOrderDbo.UserId, "TestLoid" + loid);
    }

    [TestMethod]
    public void UpdateOrder()
    {
        var loid = Guid.NewGuid();

        var orderDbo = new OrderDbo
        {
            LogicalObjectId = loid,
            UserId = "TestLoid" + loid,
            TotalAmount = 0,
        };
        OrderDao.SaveOrUpdate(orderDbo);

        orderDbo.UserId = "TestLoid2" + loid;
        OrderDao.SaveOrUpdate(orderDbo);

        var queriedOrderDbo = OrderDao.QueryByLogicalObjectId(loid);
        Assert.IsNotNull(queriedOrderDbo);
        Assert.AreEqual(queriedOrderDbo.UserId, "TestLoid2" + loid);
    }

    [TestMethod]
    public void CreateOrderWithOrderPositions()
    {
        var productLoid = Guid.NewGuid();
        var productDbo = new ProductDbo
        {
            LogicalObjectId = productLoid,
            Name1 = "Bottle"+ productLoid,
            Price = 20.5m
        };
        ProductDao.SaveOrUpdate(productDbo);

        var productLoid2 = Guid.NewGuid();
        var productDbo2 = new ProductDbo
        {
            LogicalObjectId = productLoid2,
            Name1 = "Shoes" + productLoid2,
            Price = 40.5m
        };
        ProductDao.SaveOrUpdate(productDbo2);

        var orderPositionList = new List<OrderPositionDbo>();

        var orderPositionsLoid = Guid.NewGuid();
        var orderPositionsLoid2 = Guid.NewGuid();
        var orderPosition1 = new OrderPositionDbo
        {
            LogicalObjectId = orderPositionsLoid,
            Product = productDbo,
            Quantity = 2,
            UnitPrice = 18.5m
        };
        var orderPosition2 = new OrderPositionDbo
        {
            LogicalObjectId = orderPositionsLoid2,
            Product = productDbo2,
            Quantity = 2,
            UnitPrice = 30.5m
        };
        orderPositionList.Add(orderPosition1);
        orderPositionList.Add(orderPosition2);

        var loid = Guid.NewGuid();
        var orderDbo = new OrderDbo
        {
            LogicalObjectId = loid,
            UserId = "TestLoid" + loid,
            TotalAmount = 0,
            OrderPositionList = orderPositionList
        };
        OrderDao.SaveOrUpdate(orderDbo);

        var queriedOrderDbo = OrderDao.QueryByLogicalObjectId(orderDbo.LogicalObjectId);
        Assert.IsNotNull(queriedOrderDbo);
        Assert.IsTrue(queriedOrderDbo.OrderPositionList.Count == 2);
        Assert.AreEqual(queriedOrderDbo.OrderPositionList[0].Product.LogicalObjectId, productLoid);

    }
}
