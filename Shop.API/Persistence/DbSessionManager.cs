using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Id;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using ISession = NHibernate.ISession;

namespace Shop.API.Persistence;

public static class DbSessionManager
{
    private static ISessionFactory? s_sessionFactory;

    public static ISessionFactory SessionFactory => s_sessionFactory ??
                                                    throw new InvalidOperationException(
                                                        "Trying to access SessionFactory but DbSessionManager has not been Initialized");
    public static Configuration MappingConfig { get; private set; }

    public static SchemaExport InitializeSessionFactory(string connectionString)
    {
        var fluentCfg = Fluently.Configure()
           .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
           .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(DbSessionManager).Assembly))
           //.ExposeConfiguration(cfg => new SchemaUpdate(cfg));
           .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));

        MappingConfig = fluentCfg.BuildConfiguration();
        //MappingConfig.SetInterceptor(new Interceptor());

        var schema = new SchemaExport(MappingConfig);
        //schema.Drop(true, true);
        //schema.Create(true, true);

        s_sessionFactory = MappingConfig?.BuildSessionFactory();
        //Logger.LogInformation("Session Factory now ready");

        return schema;
    }

    public static ISession OpenSession() => SessionFactory.OpenSession();

    public static ISession OpenReadOnlySession()
    {
        var session = SessionFactory.WithOptions().FlushMode(FlushMode.Manual).OpenSession();
        session.BeginTransaction();
        return session;
    }

    public static ISession OpenSessionWithTransaction()
    {
        ISession session = SessionFactory.OpenSession();
        ITransaction transaction = session.BeginTransaction();
        return session;
    }
}