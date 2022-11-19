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
    public class CustomerCartRepository : ICustomerCartRepository
    {
        private readonly ApplicationContext _context;
        public CustomerCartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CustomerCart> CreateAsync(CustomerCart customerCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (customerCart == null) throw new ArgumentNullException(null);
            await _context.CustomerCarts.AddAsync(customerCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return customerCart;
        }

        public async Task<IList<CustomerCart>> GetAllCartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allCustomerCart = await _context.CustomerCarts
                .Include(sc => sc.ProductCustomerCarts)
                .ToListAsync(cancellationToken);
            return allCustomerCart;
        }
        public async Task<IList<CustomerCart>> GetAllAsync(Expression<Func<CustomerCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allCustomerCart = await _context.CustomerCarts
                .Include(sc => sc.ProductCustomerCarts)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return allCustomerCart;
        }

        public async Task<CustomerCart> GetAsync(Expression<Func<CustomerCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _context.CustomerCarts
                 .Include(sc => sc.ProductCustomerCarts)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return cart;
        }

        public async Task<CustomerCart> GetById(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(nameof(cartId));
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _context.CustomerCarts
                 .Include(sc => sc.ProductCustomerCarts)
                .SingleOrDefaultAsync(sc => sc.Id == cartId, cancellationToken);
            return cart;
        }

        public async Task<CustomerCart> UpdateAsync(CustomerCart customerCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (customerCart == null) throw new ArgumentNullException(null);
            _context.CustomerCarts.Update(customerCart);
            await _context.SaveChangesAsync(cancellationToken);
            return customerCart;
        }
    }
}
