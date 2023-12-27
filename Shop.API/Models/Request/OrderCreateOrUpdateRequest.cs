namespace Shop.API.Models.Request;

public class OrderCreateOrUpdateRequest
{
    //public Guid LogicalObjectId { get; set; }
    public IList<CreateOrderPositionRequest> OderPositionRequests { get; set; }
}

public class CreateOrderPositionRequest
{
    public Guid ProductLoid { get; set; }
    public int Quantity { get; set; }
    public decimal? UnitPrice { get; set; }

}
