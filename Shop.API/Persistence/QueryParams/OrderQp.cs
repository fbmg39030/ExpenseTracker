namespace Shop.API.Persistence.QueryParams;

public class OrderQp : BaseQp
{
    public virtual string UserId { get; set; }
    //public virtual decimal TotalAmount { get; set; } //ATM not needed...but for future filtering YES
}
