using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces.Base
{
    public interface IGettableRepository<TEntity>
    {
        Task<IQueryable<TEntity>> Get();
    }
}