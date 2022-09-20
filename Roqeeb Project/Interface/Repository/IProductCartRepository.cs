using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IProductCartRepository
    {
        Task<ProductCart> CreateAsync(ProductCart productCart, CancellationToken cancellationToken);
        Task<IList<ProductCart>> GetProductCartByCartId(string cartId, CancellationToken cancellationToken);
        Task<ProductCart> GetAsync(Expression<Func<ProductCart, bool>> expression, CancellationToken cancellationToken);
        Task<ProductCart> UpdateAsync(ProductCart productCart, CancellationToken cancellationToken);
        Task<ProductCart> DeleteAsync(ProductCart productCart, CancellationToken cancellationToken);
    }
}
