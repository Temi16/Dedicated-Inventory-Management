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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }

       
        public async Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return customer;
        }
        public async Task<IList<Customer>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var customers = await _context.Customers
                .Include(c => c.User)
                .ToListAsync(cancellationToken);
            return customers;
        }

        public async Task<Customer> GetCustomerAsync(Expression<Func<Customer, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var customer = await _context.Customers
                .Include(c => c.User)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return customer;
        }

        public async Task<Customer> GetCustomerByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            userName = userName.ToLower();
            var customer = await _context.Customers
                .Include(c => c.User)
                .SingleOrDefaultAsync(e => e.User.Username.ToLower() == userName, cancellationToken);
            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return customer;
        }
    }
}
