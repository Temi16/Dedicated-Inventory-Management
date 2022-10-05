using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string productId, CancellationToken cancellationToken);
        Task<Product> GetProductAsync(Expression<Func<Product, bool>> expression, CancellationToken cancellationToken);
        Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken);
        Task<Product> GetProductByNameAsync(string productName, CancellationToken cancellationToken);
        Task<IList<Product>> ViewProductsAsync(CancellationToken cancellationToken);
        Task<ProductSection> AddToSection(Product product, string sectionName, CancellationToken cancellationToken);
        Task<IList<string>> GetAllProductNames(CancellationToken cancellationToken);


    }
}
