namespace Shop.API.Models.Dbo;

public class ProductImageDbo : BaseDbo
{
    public virtual string Name { get; set; }
    public virtual byte[] Bytes { get; set; }
    public virtual decimal Version { get; set; }
    public virtual string MimeType { get; set; }
}
