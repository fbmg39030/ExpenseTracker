using NHibernate.Criterion;
using Shop.API.Models.Dbo;
using Shop.API.Persistence.QueryParams;

namespace Shop.API.Persistence.Dao;

public class OrderPositionDao : BaseDao<OrderPositionDao, OrderPositionDbo, OrderPositionQp>, IBaseDao<OrderPositionDbo,OrderPositionQp>
{
    public IList<OrderPositionDbo> QueryByParams(OrderPositionQp qp)
    {
        using var session = DbSessionManager.OpenReadOnlySession();
        var criteria = session.CreateCriteria<OrderPositionDbo>();
        var hasDbIdList = qp.DbIdList != null;
        var hasLoidList = qp.LoidList != null;
        var hasDatabaseId = qp.DatabaseId != Guid.Empty;
        var hasLogicalObjectId = qp.LogicalObjectId != Guid.Empty;

        var hasOrderLoid = qp.Order != Guid.Empty;
        var hasProductLoid = qp.Product != Guid.Empty;

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

        if(hasOrderLoid)
        {
            criteria.CreateAlias($"{nameof(OrderPositionDbo.Order)}", "Order");
            criteria.Add(Restrictions.Eq($"Order.{nameof(OrderDbo.LogicalObjectId)}", qp.Order));
        }

        if (hasProductLoid)
        {
            criteria.CreateAlias($"{nameof(OrderPositionDbo.Product)}", "Product");
            criteria.Add(Restrictions.Eq($"Product.{nameof(ProductDbo.LogicalObjectId)}", qp.Product));
        }

        return criteria.List<OrderPositionDbo>();
    }
}
