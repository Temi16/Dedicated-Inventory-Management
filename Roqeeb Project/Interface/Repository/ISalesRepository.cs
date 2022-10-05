using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface ISalesRepository
    {
        Task<Sales> AddAsync(Sales sales, CancellationToken cancellationToken);
        Task<Sales> UpdateAsync(Sales sales, CancellationToken cancellationToken);
        Task<Sales> GetAsync(Expression<Func<Sales, bool>> expression, CancellationToken cancellationToken);
        Task<Sales> GetByDateAsync(DateTime date, CancellationToken cancellationToken);
        Task<IList<Sales>> GetAllAsync(Expression<Func<Sales, bool>> expression, CancellationToken cancellationToken);
        Task<IList<Sales>> GetAll(CancellationToken cancellationToken);
    }
}
