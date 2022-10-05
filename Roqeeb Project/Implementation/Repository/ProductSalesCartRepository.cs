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
    public class ProductSalesCartRepository : IProductSalesCartRepository
    {
        private readonly ApplicationContext _context;
        public ProductSalesCartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductSalesCart> CreateAsync(ProductSalesCart productSalesCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productSalesCart == null) throw new ArgumentNullException(null);
            await _context.ProductSalesCarts.AddAsync(productSalesCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return productSalesCart;
        }

        public async Task<ProductSalesCart> GetAsync(Expression<Func<ProductSalesCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productSalesCart = await _context.ProductSalesCarts
                .Include(psc => psc.SalesCart)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return productSalesCart;
        }
        public async Task<IList<ProductSalesCart>> GetAllAsync(Expression<Func<ProductSalesCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productSalesCarts = await _context.ProductSalesCarts
                .Include(psc => psc.SalesCart)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return productSalesCarts;
        }
        public async Task<IList<ProductSalesCart>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productSalesCarts = await _context.ProductSalesCarts
                .Include(psc => psc.SalesCart)
                .ToListAsync(cancellationToken);
            return productSalesCarts;
        }

        public async Task<IList<ProductSalesCart>> GetProductSalesCartByCartId(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(nameof(cartId));
            var productSalesCarts = await _context.ProductSalesCarts
                .Include(psc => psc.SalesCart)
                .Where(Psc => Psc.SalesCartId == cartId)
                .ToListAsync(cancellationToken);
            return productSalesCarts;
        }

        public async Task<ProductSalesCart> UpdateAsync(ProductSalesCart productSalesCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productSalesCart == null) throw new ArgumentNullException(null);
            _context.ProductSalesCarts.Update(productSalesCart);
            await _context.SaveChangesAsync(cancellationToken);
            return productSalesCart;
        }
        public async Task<ProductSalesCart> DeleteAsync(ProductSalesCart productSalesCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productSalesCart == null) throw new ArgumentNullException(null);
            _context.ProductSalesCarts.Remove(productSalesCart);
            await _context.SaveChangesAsync(cancellationToken);
            return productSalesCart;
        }
    }
}
