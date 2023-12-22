namespace ExpenseTracker.API.Models;

public abstract class BaseDbo : ICloneable
{
    public virtual Guid DatabaseId { get; set; }
    public virtual Guid LogicalObjectId { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime LastModifiedAt { get; set; }

    //Create a Deep Copy of the object
    public virtual object Clone()
    {
        // https://stackoverflow.com/a/8651750/10308805
        // MemberwiseClone creates a new instance and copies scalar fields into corresponding memebers
        return this.MemberwiseClone();
    }
}
