using Shop.API.Api;
using Shop.API.Models.Dto;
using Shop.API.Models.Request;
using Shop.API.Persistence.Dao;
using Shop.API.Persistence.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Test.API;

[TestClass]
public class ProductApiTest
{
    [TestMethod]
    public void AddProduct()
    {
        var testLoid = Guid.NewGuid(); 
        var request = new ProductAddOrUpdateRequest
        {
            Name1 = "Bottle"+ testLoid,
            Description = "This is obviously a bottle"+ testLoid,
            Price = 15.2m
        };

        var result = ProductApi.AddOrUpdate(request);
        var resultDto = result.GetType().GetProperty("Value")?.GetValue(result, null) as ProductDto;

        Assert.IsNotNull(resultDto);
    }

    [TestMethod]
    public void UpdateProduct() {
        var testLoid = Guid.NewGuid();

        var request = new ProductAddOrUpdateRequest
        {
            Name1 = "Bottle"+ testLoid,
            Description = "This is obviously a bottle"+ testLoid,
            Price = 15.2m
        };
        var resultDto = ProductApi.AddOrUpdate(request);
        Assert.IsNotNull(resultDto);

        request.Name1 = "NOT a bottle anymore"+ testLoid;
        request.Description = "This was a bottle years ago"+ testLoid;
        request.Price = 10.4m;

        var updateResult = ProductApi.AddOrUpdate(request);
        var updateResultDto = updateResult.GetType().GetProperty("Value")?.GetValue(updateResult, null) as ProductDto;

        Assert.IsNotNull(updateResultDto);
        Assert.IsTrue(updateResultDto.Name1 == "NOT a bottle anymore"+ testLoid);

    }

    [TestMethod]
    public void QueryProduct()
    {
        var testLoid = Guid.NewGuid();

        var request = new ProductAddOrUpdateRequest
        {
            Name1 = "Bottle"+ testLoid,
            Description = "This is obviously a bottle"+ testLoid,
            Price = 9.2m
        };

        var request2 = new ProductAddOrUpdateRequest
        {
            Name1 = "Bottle" + testLoid,
            Description = "This is obviously a bottle"+ testLoid,
            Price = 9.2m
        };

        ProductApi.AddOrUpdate(request);
        ProductApi.AddOrUpdate(request2);

        var result2 = ProductApi.Query(new ProductQp { Price = 9.2m });
        var resultDtoList = result2.GetType().GetProperty("Value")?.GetValue(result2, null) as List<ProductDto>;
        Assert.IsNotNull(resultDtoList);
        Assert.IsTrue(resultDtoList.Count == 2);

    }
}
