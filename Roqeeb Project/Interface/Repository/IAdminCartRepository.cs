using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IAdminCartRepository
    {
        Task<AdminCart> CreateAsync(AdminCart adminCart, CancellationToken cancellationToken);
        Task<AdminCart> UpdateAsync(AdminCart adminCart, CancellationToken cancellationToken);
        Task<AdminCart> GetAsync(Expression<Func<AdminCart, bool>> expression, CancellationToken cancellationToken);
        Task<AdminCart> GetById(string cartId, CancellationToken cancellationToken);
        Task<IList<AdminCart>> GetAllCartAsync(CancellationToken cancellationToken);
        Task<IList<AdminCart>> GetAllAsync(Expression<Func<AdminCart, bool>> expression, CancellationToken cancellationToken);
        Task<ProductAdminCart> AddProductToCart(string productId, string adminCartId, CancellationToken cancellationToken);
    }
}
