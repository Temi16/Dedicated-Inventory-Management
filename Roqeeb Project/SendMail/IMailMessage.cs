using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;
using static Roqeeb_Project.SendMail.EmailDTO;

namespace Roqeeb_Project.SendMail
{
    public interface IMailMessage
    {
        Task<bool> SendEmail(EmailRequestModel email);
    }
}
