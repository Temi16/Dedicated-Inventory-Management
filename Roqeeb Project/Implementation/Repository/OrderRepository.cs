using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;

namespace Roqeeb_Project.Implementation.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (order == null) throw new ArgumentNullException(null);
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task<IList<Order>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _context.Orders
                .Include(s => s.CustomerCart)
                .ThenInclude(s => s.ProductCustomerCarts)
                .Include(or => or.User)
                .ToListAsync(cancellationToken);
            return order;
        }

        public async Task<IList<Order>> GetAllAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _context.Orders
                .Include(s => s.CustomerCart)
                .ThenInclude(s => s.ProductCustomerCarts)
                .Include(or => or.User)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return order;
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _context.Orders
              .Include(s => s.CustomerCart)
              .ThenInclude(s => s.ProductCustomerCarts)
              .Include(or => or.User)
              .SingleOrDefaultAsync(expression, cancellationToken);
            return order;
        }

        public async Task<Order> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            date = date.ToUniversalTime();
            var order = await _context.Orders
                .Include(s => s.CustomerCart)
                .ThenInclude(s => s.ProductCustomerCarts)
                .Include(or => or.User)
                .SingleOrDefaultAsync(p => p.CreatedOn == date, cancellationToken);
            return order;
        }

        public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (order == null) throw new ArgumentNullException(null);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}
