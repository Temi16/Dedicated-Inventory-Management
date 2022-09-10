using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IStoreRepository
    {
        Task<Store> CreateAsync(Store store, CancellationToken cancellationToken);
        Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken);
        Task<Store> GetByNameAsync(string storeName, CancellationToken cancellationToken);
        Task<Store> GetAsync(Expression<Func<Store, bool>> expression, CancellationToken cancellationToken);
        Task<IList<Store>> GetAllAsync(CancellationToken cancellationToken);
    }
}
