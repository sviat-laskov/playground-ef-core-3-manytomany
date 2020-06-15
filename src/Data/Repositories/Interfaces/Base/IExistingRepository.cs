using System;
using System.Threading.Tasks;
using Common.Models.Base;

namespace Data.Repositories.Interfaces.Base
{
    public interface IExistingByIdRepository<TEntity> where TEntity : BaseGuid
    {
        Task<bool> DoesExist(Guid id);
    }
}