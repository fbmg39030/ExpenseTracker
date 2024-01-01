using Shop.API.Models;
using Shop.API.Models.Dbo;
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
        //CRUD --> CREATE; READ; 
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
            Assert.AreEqual(queriedProductDbo.Name1, "Name_" + productLoid);
            Assert.AreEqual(queriedProductDbo.Description, "Description_" + productLoid);
            Assert.AreEqual(queriedProductDbo.Price, 20.2m);
        }

        [TestMethod]
        //! UPDATE; DELETE --> SETTING PRODUCT TO INACTIVE --> STATUS WILL BE ADDED LATER
        public void UpdateProduct()
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

            productDbo.Price = 120.20m;
            productDbo.Name1 = "Name2_" + productLoid;

            ProductDao.SaveOrUpdate(productDbo);

            var queriedProductDbo = ProductDao.QueryByLogicalObjectId(productLoid);
            Assert.IsNotNull(queriedProductDbo);
            Assert.AreEqual(queriedProductDbo.Name1, "Name2_" + productLoid);
            Assert.AreEqual(queriedProductDbo.Description, "Description_" + productLoid);
            Assert.AreEqual(queriedProductDbo.Price, 120.20m);
        }

        [TestMethod]
        public void UpdateProductStatus()
        {
            var productLoid = Guid.NewGuid();
            var productDbo = new ProductDbo
            {
                LogicalObjectId = productLoid,
                Name1 = "Name_" + productLoid,
                Description = "Description_" + productLoid,
                Price = 20.2m, 
                Status = ProductStatus.INACTIVE
            };

            ProductDao.SaveOrUpdate(productDbo);

            productDbo.Price = 120.20m;
            productDbo.Name1 = "Name2_" + productLoid;
            productDbo.Status = ProductStatus.INSTOCK; 

            ProductDao.SaveOrUpdate(productDbo);

            var queriedProductDbo = ProductDao.QueryByLogicalObjectId(productLoid);
            Assert.IsNotNull(queriedProductDbo);
            Assert.AreEqual(queriedProductDbo.Status, ProductStatus.INSTOCK);
        }
    }
}
