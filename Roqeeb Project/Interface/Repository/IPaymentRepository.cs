using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
    }
}
