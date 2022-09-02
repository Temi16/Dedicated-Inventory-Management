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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(product == null) throw new ArgumentNullException(nameof(product));
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _context.Products
                .Include(p => p.ProductPurchase)
                .ThenInclude(pp => pp.Purchase)
                .Include(p => p.ProductSales)
                .ThenInclude(ps => ps.Sales)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return product;
        }

        public async Task<Product> GetProductByIdAsync(string productId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productId == null) throw new ArgumentNullException(null);
            var newProduct = await _context.Products
                .Include(p => p.ProductPurchase)
                .ThenInclude(pp => pp.Purchase)
                .Include(p => p.ProductSales)
                .ThenInclude(ps => ps.Sales)
                .SingleOrDefaultAsync(p => p.Id == productId, cancellationToken);
            return newProduct;
        }

        public async Task<Product> GetProductByNameAsync(string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (productName == null) throw new ArgumentNullException(null);
            var newProduct = await _context.Products
                .Include(p => p.ProductPurchase)
                .ThenInclude(pp => pp.Purchase)
                .Include(p => p.ProductSales)
                .ThenInclude(ps => ps.Sales)
                .SingleOrDefaultAsync(p => p.ProductName == productName, cancellationToken);
            return newProduct;
        }


        public async Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (product == null) throw new ArgumentNullException(nameof(product));
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<IList<Product>> ViewProductsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var products = await _context.Products
                .Include(p => p.ProductPurchase)
                .ThenInclude(pp => pp.Purchase)
                .Include(p => p.ProductSales)
                .ThenInclude(ps => ps.Sales)
                .ToListAsync(cancellationToken);
            return products;
        }
    }
}
