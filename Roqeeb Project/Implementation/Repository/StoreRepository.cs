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
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationContext _context;
        public StoreRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Store> CreateAsync(Store store, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (store == null) throw new ArgumentNullException(nameof(store));
            await _context.Stores.AddAsync(store, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return store;
        }

        public async Task<Store> GetAsync(Expression<Func<Store, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var store = await _context.Stores
                .Include(st => st.Sections)
                .ThenInclude(se => se.SectionName)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return store;
        }

        public async Task<Store> GetByNameAsync(string storeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (storeId == null) throw new ArgumentNullException(null);
            var newStore = await _context.Stores
                 .Include(st => st.Sections)
                .ThenInclude(se => se.SectionName)
                .SingleOrDefaultAsync(s => s.Id == storeId, cancellationToken);
            return newStore;
        }

        public Task<Store> UpdateAsync(Store store, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
