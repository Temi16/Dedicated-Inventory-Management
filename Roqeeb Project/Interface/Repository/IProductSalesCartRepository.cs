using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IProductSalesCartRepository
    {
        Task<ProductSalesCart> CreateAsync(ProductSalesCart productSalesCart, CancellationToken cancellationToken);
        Task<IList<ProductSalesCart>> GetProductSalesCartByCartId(string cartId, CancellationToken cancellationToken);
        Task<ProductSalesCart> GetAsync(Expression<Func<ProductSalesCart, bool>> expression, CancellationToken cancellationToken);
        Task<ProductSalesCart> UpdateAsync(ProductSalesCart productSalesCart, CancellationToken cancellationToken);
        Task<ProductSalesCart> DeleteAsync(ProductSalesCart productSalesCart, CancellationToken cancellationToken);
        Task<IList<ProductSalesCart>> GetAllAsync(Expression<Func<ProductSalesCart, bool>> expression, CancellationToken cancellationToken);
        Task<IList<ProductSalesCart>> GetAll(CancellationToken cancellationToken);
    }
}
