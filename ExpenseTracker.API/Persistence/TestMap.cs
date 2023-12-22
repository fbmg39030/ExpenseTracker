using ExpenseTracker.API.Models;
using NHibernate.Mapping.ByCode;

namespace ExpenseTracker.API.Persistence;

public class TestMap: BaseMap<TestDbo>
{
    public const string TableName = "Test";

    public TestMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Name1);
        Map(x => x.Name2);
        Not.LazyLoad();
    }
}
