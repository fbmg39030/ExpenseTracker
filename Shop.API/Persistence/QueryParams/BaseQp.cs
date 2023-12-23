using System.Text.Json.Serialization;

namespace Shop.API.Persistence.QueryParams;
public abstract class BaseQp
{
    [JsonIgnore] public Guid DatabaseId { get; set; } //only for internal query -> ignore for swagger + client gen
    [JsonIgnore] public IList<Guid> DbIdList { get; set; } //only for internal query -> ignore for swagger + client gen
    public Guid LogicalObjectId { get; set; }
    public IList<Guid> LoidList { get; set; }
}

