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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;
        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreayeAsync(Employee employee, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<IList<Employee>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employees = await _context.Employees
                .Include(e => e.User)
                .ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<Employee> GetEmployeeAsync(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _context.Employees
                .Include(e => e.User)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return employee;
        }

        public async Task<Employee> GetEmployeeByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            userName = userName.ToLower();
            var employee = await _context.Employees
                .Include(e => e.User)
                .SingleOrDefaultAsync(e => e.User.Username.ToLower() == userName, cancellationToken);
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }
    }
}
