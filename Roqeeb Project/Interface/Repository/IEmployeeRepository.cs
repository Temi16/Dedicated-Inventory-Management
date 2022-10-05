using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreayeAsync(Employee employee, CancellationToken cancellationToken);
        Task<Employee> GetEmployeeAsync(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken);
        Task<Employee> UpdateEmployee(Employee employee, CancellationToken cancellationToken);
        Task<Employee> GetEmployeeByUserNameAsync(string userName, CancellationToken cancellationToken);
        Task<IList<Employee>> GetAll(CancellationToken cancellationToken);
        
    }
}
