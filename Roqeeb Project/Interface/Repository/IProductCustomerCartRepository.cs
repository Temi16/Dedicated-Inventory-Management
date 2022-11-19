using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IProductCustomerCartRepository
    {
        Task<ProductCustomerCart> CreateAsync(ProductCustomerCart productCustomerCart, CancellationToken cancellationToken);
        Task<IList<ProductCustomerCart>> GetProductCustomerCartByCartId(string cartId, CancellationToken cancellationToken);
        Task<ProductCustomerCart> GetAsync(Expression<Func<ProductCustomerCart, bool>> expression, CancellationToken cancellationToken);
        Task<ProductCustomerCart> UpdateAsync(ProductCustomerCart productCustomerCart, CancellationToken cancellationToken);
        Task<ProductCustomerCart> DeleteAsync(ProductCustomerCart productCustomerCart, CancellationToken cancellationToken);
        Task<IList<ProductCustomerCart>> GetAllAsync(Expression<Func<ProductCustomerCart, bool>> expression, CancellationToken cancellationToken);
        Task<IList<ProductCustomerCart>> GetAll(CancellationToken cancellationToken);
    }
}
