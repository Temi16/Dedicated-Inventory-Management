using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task<Customer> GetCustomerAsync(Expression<Func<Customer, bool>> expression, CancellationToken cancellationToken);
        Task<Customer> UpdateCustomer(Customer customer, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByUserNameAsync(string userName, CancellationToken cancellationToken);
        Task<IList<Customer>> GetAll(CancellationToken cancellationToken);
    }
}
