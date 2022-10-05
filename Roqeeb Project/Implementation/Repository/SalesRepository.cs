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
    public class SalesRepository : ISalesRepository
    {
        public readonly ApplicationContext _context;
        public SalesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Sales> AddAsync(Sales sales, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (sales == null) throw new ArgumentNullException(null);
            await _context.Sales.AddAsync(sales, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sales;
        }

        public async Task<IList<Sales>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sales = await _context.Sales
                .Include(s => s.SalesCart)
                .ThenInclude(s => s.ProductSalesCarts)
                .ToListAsync(cancellationToken);
            return sales;
        }

        public async Task<IList<Sales>> GetAllAsync(Expression<Func<Sales, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sales = await _context.Sales
                .Include(s => s.SalesCart)
                .ThenInclude(s => s.ProductSalesCarts)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return sales;
        }

        public async Task<Sales> GetAsync(Expression<Func<Sales, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sales = await _context.Sales
              .Include(s => s.SalesCart)
              .ThenInclude(s => s.ProductSalesCarts)
              .SingleOrDefaultAsync(expression, cancellationToken);
            return sales;
        }

        public async Task<Sales> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            date = date.ToUniversalTime();
            var sales = await _context.Sales
                .Include(s => s.SalesCart)
                .ThenInclude(s => s.ProductSalesCarts)
                .SingleOrDefaultAsync(p => p.CreatedOn == date, cancellationToken);
            return sales;
        }

        public async Task<Sales> UpdateAsync(Sales sales, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (sales == null) throw new ArgumentNullException(null);
            _context.Sales.Update(sales);
            await _context.SaveChangesAsync(cancellationToken);
            return sales;
        }
    }
}
