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
}
