using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface ISupplierRepository
    {
        Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellationToken);
        Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
        Task<Supplier> GetAsync(Expression<Func<Supplier, bool>> expression , CancellationToken cancellationToken);
        Task<IList<Supplier>> GetAllAsync(CancellationToken cancellationToken);
    }
}
