using Microsoft.AspNetCore.Components.Server.Circuits;
using NHibernate.Criterion;
using Shop.API.Models.Dbo;
using Shop.API.Persistence.QueryParams;
using System.Security.Policy;

namespace Shop.API.Persistence.Dao;

public class ProductDao : BaseDao<ProductDao, ProductDbo, ProductQp>, IBaseDao<ProductDbo, ProductQp>
{
    public static new IList<ProductDbo> QueryByParameters(ProductQp qp)
    {
        using var session = DbSessionManager.OpenReadOnlySession();
        var criteria = session.CreateCriteria<ProductDbo>();
        var hasDbIdList = qp.DbIdList != null;
        var hasLoidList = qp.LoidList != null;
        var hasDatabaseId = qp.DatabaseId != Guid.Empty;
        var hasLogicalObjectId = qp.LogicalObjectId != Guid.Empty;
        var hasName = !string.IsNullOrEmpty(qp.Name1);
        var hasDescription = !string.IsNullOrEmpty(qp.Description);
        var hasPrice = qp.Price > 0;

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

        if (hasName)
        {
            criteria.Add(Restrictions.Eq(nameof(ProductDbo.Name1), qp.Name1));
        }

        if (hasDescription)
        {
            criteria.Add(Restrictions.Eq(nameof(ProductDbo.Description), qp.Description));
        }

        if (hasPrice)
        {
            criteria.Add(Restrictions.Eq(nameof(ProductDbo.Price), qp.Price));
        }

        return criteria.List<ProductDbo>();
    }
}
