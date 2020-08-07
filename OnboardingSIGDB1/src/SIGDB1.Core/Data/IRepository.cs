using SIGDB1.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGDB1.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<IEnumerable<T>> Get();

        Task<int> Create(T obj);

        Task Update(T obj);

        Task<bool> Delete(int id);
    }
}
