using ExpenseTracker.API.Models;
using FluentNHibernate.Mapping;

namespace ExpenseTracker.API.Persistence;

public class BaseMap<T> : ClassMap<T> where T : BaseDbo
{
    public const string ForeignKey = "FK_";
    public const string Reference = "_DBID";

    protected BaseMap(string tableName)
    {
        Table(tableName);
        Id(x => x.DatabaseId).GeneratedBy.GuidComb();
        Map(x => x.CreatedAt).Not.Nullable().Generated.Always()
           .Default("getdate()"); //will always be set by database - can not be overwritten
        Map(x => x.LastModifiedAt).Not.Nullable().Generated.Insert()
            .Default("getdate()"); //will always be set by database - can be overwritten afterwards
        UseUnionSubclassForInheritanceMapping();
    }
}
