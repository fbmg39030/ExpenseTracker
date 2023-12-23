using Shop.API.Models;
using Shop.API.Persistence.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Test.Persistence
{
    [TestClass]
    public class ProductTest
    {
        //CRUD --> CREATE; READ; UPDATE; DELETE
        [TestMethod]
        public void CreateProduct()
        {
            var productLoid = Guid.NewGuid();
            var productDbo = new ProductDbo
            {
                LogicalObjectId = productLoid,
                Name1 = "Name_" + productLoid,
                Description = "Description_" + productLoid,
                Price = 20.2m
            };

            ProductDao.SaveOrUpdate(productDbo);

            var queriedProductDbo = ProductDao.QueryByLogicalObjectId(productLoid);
            Assert.IsNotNull(queriedProductDbo);

        }
    }
}
