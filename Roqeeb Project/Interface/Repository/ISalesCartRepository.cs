using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface ISalesCartRepository
    {
        Task<SalesCart> CreateAsync(SalesCart salesCart, CancellationToken cancellationToken);
        Task<SalesCart> UpdateAsync(SalesCart salesCart, CancellationToken cancellationToken);
        Task<SalesCart> GetAsync(Expression<Func<SalesCart, bool>> expression, CancellationToken cancellationToken);
        Task<SalesCart> GetById(string cartId, CancellationToken cancellationToken);
        Task<IList<SalesCart>> GetAllCartAsync(CancellationToken cancellationToken);
        Task<IList<SalesCart>> GetAllAsync(Expression<Func<SalesCart, bool>> expression, CancellationToken cancellationToken);
       
    }
}
