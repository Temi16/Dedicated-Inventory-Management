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
    public class ProductCartRepository : IProductCartRepository
    {
        private readonly ApplicationContext _context;
        public ProductCartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductCart> CreateAsync(ProductCart productCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productCart == null) throw new ArgumentNullException(null);
            await _context.ProductsCarts.AddAsync(productCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return productCart;
        }

        public async Task<ProductCart> GetAsync(Expression<Func<ProductCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var productCart = await _context.ProductsCarts
                .Include(pc => pc.AdminCart)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return productCart;
        }

        public async Task<IList<ProductCart>> GetProductCartByCartId(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(nameof(cartId));
            var productCarts = await _context.ProductsCarts
                .Include(Pc => Pc.AdminCart)
                .Where(Pc => Pc.AdminCartId == cartId)
                .ToListAsync(cancellationToken);
            return productCarts;
        }

        public async Task<ProductCart> UpdateAsync(ProductCart productCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productCart == null) throw new ArgumentNullException(null);
            _context.ProductsCarts.Update(productCart);
            await _context.SaveChangesAsync(cancellationToken);
            return productCart;
        }
        public async Task<ProductCart> DeleteAsync(ProductCart productCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productCart == null) throw new ArgumentNullException(null);
            _context.ProductsCarts.Remove(productCart);
            await _context.SaveChangesAsync(cancellationToken);
            return productCart;
        }
    }
}
