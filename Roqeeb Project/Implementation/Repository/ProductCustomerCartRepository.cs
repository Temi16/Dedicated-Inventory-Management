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
    public class ProductCustomerCartRepository : IProductCustomerCartRepository
    {
        private readonly ApplicationContext _context;
        public ProductCustomerCartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductCustomerCart> CreateAsync(ProductCustomerCart productCustomerCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productCustomerCart == null) throw new ArgumentNullException(null);
            await _context.ProductCustomerCarts.AddAsync(productCustomerCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return productCustomerCart;
        }

        public async Task<ProductCustomerCart> GetAsync(Expression<Func<ProductCustomerCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productSalesCart = await _context.ProductCustomerCarts
                .Include(pcc => pcc.CustomerCart)
                .FirstOrDefaultAsync(expression, cancellationToken);
            return productSalesCart;
        }
        public async Task<IList<ProductCustomerCart>> GetAllAsync(Expression<Func<ProductCustomerCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productCustomerCarts = await _context.ProductCustomerCarts
                .Include(pcc => pcc.CustomerCart)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return productCustomerCarts;
        }
        public async Task<IList<ProductCustomerCart>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productSalesCarts = await _context.ProductCustomerCarts
                .Include(psc => psc.CustomerCart)
                .ToListAsync(cancellationToken);
            return productSalesCarts;
        }

        public async Task<IList<ProductCustomerCart>> GetProductCustomerCartByCartId(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(nameof(cartId));
            var productCustomerCarts = await _context.ProductCustomerCarts
                .Include(pcc => pcc.CustomerCart)
                .Where(Pcc => Pcc.CustomerCartId == cartId)
                .ToListAsync(cancellationToken);
            return productCustomerCarts;
        }

        public async Task<ProductCustomerCart> UpdateAsync(ProductCustomerCart productCustomerCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productCustomerCart == null) throw new ArgumentNullException(null);
            _context.ProductCustomerCarts.Update(productCustomerCart);
            await _context.SaveChangesAsync(cancellationToken);
            return productCustomerCart;
        }
        public async Task<ProductCustomerCart> DeleteAsync(ProductCustomerCart productCustomerCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productCustomerCart == null) throw new ArgumentNullException(null);
            _context.ProductCustomerCarts.Remove(productCustomerCart);
            await _context.SaveChangesAsync(cancellationToken);
            return productCustomerCart;
        }
    }
}
