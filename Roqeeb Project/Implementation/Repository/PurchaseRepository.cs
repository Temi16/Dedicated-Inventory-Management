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
    public class PurchaseRepository : IPurchaseRepository
    {
        public readonly ApplicationContext _context;
        public PurchaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Purchase> AddAsync(Purchase purchase, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (purchase == null) throw new ArgumentNullException(null);
            await _context.Purchases.AddAsync(purchase, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return purchase;
        }

        public async Task<IList<Purchase>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchases = await _context.Purchases
                .Include(p => p.AdminCart)
                .ThenInclude(ac => ac.productCarts)
                .ToListAsync(cancellationToken);
            return purchases;
        }

        public async Task<IList<Purchase>> GetAllAsync(Expression<Func<Purchase, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchases = await _context.Purchases
                .Include(p => p.AdminCart)
                .ThenInclude(ac => ac.productCarts)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return purchases;
        }

        public async Task<Purchase> GetAsync(Expression<Func<Purchase, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _context.Purchases
                .Include(p => p.AdminCart)
                .ThenInclude(ac => ac.productCarts)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return purchase;
        }

        public async Task<Purchase> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            date = date.ToUniversalTime();
            var purchase = await _context.Purchases
                .Include(p => p.AdminCart)
                .SingleOrDefaultAsync(p => p.CreatedOn == date, cancellationToken);
            return purchase;
        }

        public async Task<Purchase> UpdateAsync(Purchase purchase, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (purchase == null) throw new ArgumentNullException(null);
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync(cancellationToken);
            return purchase;
        }
    }
}
