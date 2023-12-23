namespace Shop.Test;
using NHibernate.Tool.hbm2ddl;
using Shop.API.Persistence;

[TestClass]
public class AssemblyInitialize
{
    private static SchemaExport Schema { get; set; }

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_TEST");
        Schema = DbSessionManager.InitializeSessionFactory(connectionString);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup() => Schema.Drop(true, true);
}
