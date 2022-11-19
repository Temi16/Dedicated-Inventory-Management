using System;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;

namespace Roqeeb_Project.Implementation.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationContext _context;
        public PaymentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return payment;
        }
    }
}
