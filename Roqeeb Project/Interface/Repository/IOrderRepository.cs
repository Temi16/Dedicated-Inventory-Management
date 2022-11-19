using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order, CancellationToken cancellationToken);
        Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken);
        Task<Order> GetAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken);
        Task<Order> GetByDateAsync(DateTime date, CancellationToken cancellationToken);
        Task<IList<Order>> GetAllAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken);
        Task<IList<Order>> GetAll(CancellationToken cancellationToken);
    }
}
