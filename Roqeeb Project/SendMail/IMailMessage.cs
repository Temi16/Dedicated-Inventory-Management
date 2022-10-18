using System.Threading;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.SendMail
{
    public interface IMailMessage
    {
        void VerifyMail(Customer customer, CancellationToken cancellationToken);
    }
}
