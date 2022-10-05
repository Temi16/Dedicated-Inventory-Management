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
                .Include(p => p.productSections)
                .ThenInclude(ps => ps.Section)
                .ThenInclude(se => se.Store)
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
                 .Include(p => p.productSections)
                .ThenInclude(ps => ps.Section)
                .ThenInclude(se => se.Store)
                .SingleOrDefaultAsync(p => p.Id == productId && p.IsDeleted == false, cancellationToken);
            return newProduct;
        }

        public async Task<Product> GetProductByNameAsync(string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            productName = productName.ToLower();
            if (productName == null) throw new ArgumentNullException(null);
            var newProduct = await _context.Products
                .Include(p => p.ProductPurchase)
                .ThenInclude(pp => pp.Purchase)
                .Include(p => p.ProductSales)
                .ThenInclude(ps => ps.Sales)
                .Include(p => p.productSections)
                .ThenInclude(ps => ps.Section)
                .ThenInclude(se => se.Store)
                .SingleOrDefaultAsync(p => p.ProductName.ToLower() == productName && p.IsDeleted == false, cancellationToken);
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
                .Include(p => p.productSections)
                .ThenInclude(ps => ps.Section)
                .ThenInclude(se => se.Store)
                .Where(p => p.IsDeleted == false)
                .ToListAsync(cancellationToken);
            return products;
        }
        public async Task<ProductSection> AddToSection(Product product,string sectionName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (product == null) throw new ArgumentNullException(null);
            sectionName = sectionName.ToLower();
            if(string.IsNullOrEmpty(sectionName)) throw new ArgumentNullException(nameof(sectionName));
            var section = await _context.Sections.SingleOrDefaultAsync(se => se.SectionName == sectionName, cancellationToken);
            if (section == null) throw new ArgumentException(null);
            var productSection = new ProductSection
            {
                ProductId = product.Id,
                SectionId = section.Id,
                IsDeleted = false
            };
            await _context.ProductSections.AddAsync(productSection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return productSection;

        }
        public async Task<IList<string>> GetAllProductNames(CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);
            var productNames = products.Select(p => p.ProductName).ToList();
            return productNames;
        }

      
    }
}
