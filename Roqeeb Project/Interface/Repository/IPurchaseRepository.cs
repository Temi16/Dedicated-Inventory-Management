using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IPurchaseRepository
    {
        Task<Purchase> AddAsync(Purchase purchase, CancellationToken cancellationToken);
        Task<Purchase> GetAsync(Expression<Func<Purchase, bool>> expression, CancellationToken cancellationToken);
       
    }
}
