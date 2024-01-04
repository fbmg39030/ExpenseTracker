namespace Shop.API.Models.Request;

public class AddProductImageRequest
{
    public virtual string Name { get; set; }
    public virtual byte[] Bytes { get; set; }
    public virtual decimal Version { get; set; }
    public virtual string MimeType { get; set; }
}
