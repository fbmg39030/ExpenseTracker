namespace Shop.API.Models.Request;

public class ProductAddOrUpdateRequest
{
    public Guid LogicalObjectId { get; set; }
    public string Name1 { get; set; }
    public string Description {  get; set; }
    public decimal Price { get; set; }
}
