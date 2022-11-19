using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface ICustomerCartRepository
    {
        Task<CustomerCart> CreateAsync(CustomerCart customerCart, CancellationToken cancellationToken);
        Task<CustomerCart> UpdateAsync(CustomerCart customerCart, CancellationToken cancellationToken);
        Task<CustomerCart> GetAsync(Expression<Func<CustomerCart, bool>> expression, CancellationToken cancellationToken);
        Task<CustomerCart> GetById(string cartId, CancellationToken cancellationToken);
        Task<IList<CustomerCart>> GetAllCartAsync(CancellationToken cancellationToken);
        Task<IList<CustomerCart>> GetAllAsync(Expression<Func<CustomerCart, bool>> expression, CancellationToken cancellationToken);
    }
}
