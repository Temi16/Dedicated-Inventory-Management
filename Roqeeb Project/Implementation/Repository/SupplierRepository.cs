using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;

namespace Roqeeb_Project.Implementation.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationContext _context;
        public SupplierRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (supplier == null) throw new ArgumentNullException(nameof(supplier));
            await _context.Suppliers.AddAsync(supplier, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return supplier;
        }

        public async Task<IList<Supplier>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var suppliers = await _context.Suppliers
                .Include(s => s.Purchases)
                .ToListAsync(cancellationToken);
            return suppliers;
        }

        public async Task<Supplier> GetAsync(Expression<Func<Supplier, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var supplier = await _context.Suppliers
                .Include(s => s.Purchases)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (supplier == null) throw new ArgumentNullException(nameof(supplier));
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(cancellationToken);
            return supplier;
        }
    }
}
