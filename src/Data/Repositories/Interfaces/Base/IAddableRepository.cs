using System.Threading.Tasks;

namespace Data.Repositories.Interfaces.Base
{
    public interface IAddableRepository<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
    }
}