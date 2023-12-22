namespace Shop.API.Models;

public class ProductImageDbo : BaseDbo
{
    public virtual byte[] Image { get; set; }
    public virtual ProductDbo Product { get; set; }
}
