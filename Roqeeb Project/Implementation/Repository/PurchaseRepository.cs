using System;
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
            await _context.AddAsync(purchase, cancellationToken);
            await _context.SaveChangesAsync();
            return purchase;
        }



        public async Task<Purchase> GetAsync(Expression<Func<Purchase, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _context.Purchases
                .Include(p => p.AdminCart)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return purchase;
        }
    }
}
