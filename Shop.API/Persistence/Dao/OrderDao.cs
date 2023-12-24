using NHibernate.Criterion;
using Shop.API.Models;
using Shop.API.Persistence.QueryParams;

namespace Shop.API.Persistence.Dao;

public class OrderDao : BaseDao<OrderDao, OrderDbo, OrderQp>, IBaseDao<OrderDbo, OrderQp>
{
    public static IList<OrderDbo> QueryByParams(OrderQp qp)
    {
        using var session = DbSessionManager.OpenReadOnlySession();
        var criteria = session.CreateCriteria<OrderDbo>();

        var hasDbIdList = qp.DbIdList != null;
        var hasLoidList = qp.LoidList != null;
        var hasDatabaseId = qp.DatabaseId != Guid.Empty;
        var hasLogicalObjectId = qp.LogicalObjectId != Guid.Empty;
        var hasUserId = !string.IsNullOrEmpty(qp.UserId);

        if (hasDbIdList)
        {
            criteria.Add(Restrictions.In(nameof(BaseDbo.DatabaseId), qp.DbIdList.ToArray()));
        }

        if (hasLoidList)
        {
            criteria.Add(Restrictions.In(nameof(BaseDbo.LogicalObjectId), qp.LoidList.ToArray()));
        }

        if (hasDatabaseId)
        {
            criteria.Add(Restrictions.Eq(nameof(BaseDbo.DatabaseId), qp.DatabaseId));
        }

        if (hasLogicalObjectId)
        {
            criteria.Add(Restrictions.Eq(nameof(BaseDbo.LogicalObjectId), qp.LogicalObjectId));
        }

        return criteria.List<OrderDbo>();
    }
   
}
