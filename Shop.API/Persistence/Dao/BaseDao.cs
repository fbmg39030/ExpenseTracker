using Shop.API.Models.Dbo;
using Shop.API.Persistence.QueryParams;
using ISession = NHibernate.ISession;

namespace Shop.API.Persistence.Dao;

public abstract class BaseDao<TDao, TDbo, TQp> where TDao : IBaseDao<TDbo, TQp> where TDbo : BaseDbo where TQp : BaseQp, new()
{
    public static TDbo QueryByDatabaseId(Guid databaseId)
    {
        using var session = DbSessionManager.OpenReadOnlySession();
        return session.Query<TDbo>().SingleOrDefault(dbo => dbo.DatabaseId == databaseId);
    }

    public static TDbo QueryByLogicalObjectId(Guid logicalObjectId)
    {
        using var session = DbSessionManager.OpenReadOnlySession();
        return session.Query<TDbo>().SingleOrDefault(dbo => dbo.LogicalObjectId == logicalObjectId);
    }

    public static IList<TDbo> QueryByParameters(TQp qp) => TDao.QueryByParameters(qp);
    public static TDbo QueryFirstOrDefaultByParameters(TQp qp) => QueryByParameters(qp).FirstOrDefault();


    public static void SaveOrUpdateNoCommit(TDbo dbo, ISession session)
    {
        TDbo dbObject = null;
        var dbIdIsSet = dbo.DatabaseId != Guid.Empty;
        if (dbIdIsSet)
        {
            dbObject = QueryByDatabaseId(dbo.DatabaseId); //try DatabaseId
        }

        var loidIsSet = dbObject == null && dbo.LogicalObjectId != Guid.Empty;
        if (loidIsSet)
        {
            dbObject = QueryByLogicalObjectId(dbo.LogicalObjectId); // fallback to LogicalObjectId
        }

        var dbObjectIsSet = dbObject != null;
        if (dbObjectIsSet)
        {
            dbo.DatabaseId = dbObject.DatabaseId;
            dbo.LogicalObjectId = dbObject.LogicalObjectId;
        } // if found, set DatabaseId & LogicalObjectId of dbo

        session.SaveOrUpdate(dbo);
    }


    public static void SaveOrUpdate(TDbo dbo)
    {
        using var session = DbSessionManager.OpenSession();
        using var transaction = session.BeginTransaction();
        SaveOrUpdateNoCommit(dbo, session);
        transaction.Commit();
    }
}
