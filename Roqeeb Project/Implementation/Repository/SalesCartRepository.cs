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
    public class SalesCartRepository : ISalesCartRepository
    {
        private readonly ApplicationContext _context;
        public SalesCartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<SalesCart> CreateAsync(SalesCart salesCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (salesCart == null) throw new ArgumentNullException(null);
            await _context.SalesCarts.AddAsync(salesCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return salesCart;
        }

        public async Task<IList<SalesCart>> GetAllCartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allSalesCart = await _context.SalesCarts
                .Include(sc => sc.ProductSalesCarts)
                .ToListAsync(cancellationToken);
            return allSalesCart;
        }
        public async Task<IList<SalesCart>> GetAllAsync(Expression<Func<SalesCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allSalesCart = await _context.SalesCarts
                .Include(sc => sc.ProductSalesCarts)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return allSalesCart;
        }

        public async Task<SalesCart> GetAsync(Expression<Func<SalesCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _context.SalesCarts
                 .Include(sc => sc.ProductSalesCarts)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return cart;
        }

        public async Task<SalesCart> GetById(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(nameof(cartId));
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _context.SalesCarts
                 .Include(sc => sc.ProductSalesCarts)
                .SingleOrDefaultAsync(sc => sc.Id == cartId, cancellationToken);
            return cart;
        }

        public async Task<SalesCart> UpdateAsync(SalesCart salesCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (salesCart == null) throw new ArgumentNullException(null);
            _context.SalesCarts.Update(salesCart);
            await _context.SaveChangesAsync(cancellationToken);
            return salesCart;
        }
    }
}
