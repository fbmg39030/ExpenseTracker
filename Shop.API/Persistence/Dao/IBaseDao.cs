using Shop.API.Models.Dbo;
using Shop.API.Persistence.QueryParams;

namespace Shop.API.Persistence.Dao;

public interface IBaseDao<TDbo, in TQp> where TDbo : BaseDbo where TQp : BaseQp
{
    static abstract IList<TDbo> QueryByParameters(TQp qp);
}


