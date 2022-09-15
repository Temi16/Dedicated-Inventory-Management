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
    public class AdminCartRepository : IAdminCartRepository
    {
        private readonly ApplicationContext _context;
        public AdminCartRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<AdminCart> CreateAsync(AdminCart adminCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (adminCart == null) throw new ArgumentNullException(null);
            await _context.AdminCarts.AddAsync(adminCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return adminCart;
        }

        public async Task<IList<AdminCart>> GetAllCartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allCart = await _context.AdminCarts
                .Include(ac => ac.ProductAdminsCart)
                .ThenInclude(pc => pc.Product)
                .ToListAsync(cancellationToken);
            return allCart;
        }
        public async Task<IList<AdminCart>> GetAllAsync(Expression<Func<AdminCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allCart = await _context.AdminCarts
                .Include(ac => ac.ProductAdminsCart)
                .ThenInclude(pc => pc.Product)
                .Where(expression)
                .ToListAsync(cancellationToken);
            return allCart;
        }

        public async Task<AdminCart> GetAsync(Expression<Func<AdminCart, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _context.AdminCarts
                .Include(ac => ac.ProductAdminsCart)
                .ThenInclude(pc => pc.Product)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return cart;
        }

        public async Task<AdminCart> GetById(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(nameof(cartId));   
            var cart = await _context.AdminCarts
                .Include(ac => ac.ProductAdminsCart)
                .ThenInclude(pc => pc.Product)
                .SingleOrDefaultAsync(ac => ac.Id == cartId, cancellationToken);
            return cart;
        }

        public async Task<AdminCart> UpdateAsync(AdminCart adminCart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (adminCart == null) throw new ArgumentNullException(null);
            _context.AdminCarts.Update(adminCart);
            await _context.SaveChangesAsync(cancellationToken);
            return adminCart;
        }
        public async Task<ProductAdminCart> AddProductToCart(string productId, string adminCartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(productId)) throw new ArgumentNullException(null);
            if(string.IsNullOrEmpty(productId)) throw new ArgumentNullException(null);
            var productCart = new ProductAdminCart
            {
                ProductId = productId,
                AdminCartId = adminCartId,
            };
            await _context.ProductAdminCarts.AddAsync(productCart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return productCart;
            
            

        }
    }
}
