using System.Threading;
using MimeKit;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.SendMail
{
    public class MailMessage : IMailMessage
    {
        public void VerifyMail(Customer customer, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //MailMessage reminder = new MailMessage();
            //reminder.From.Add(new MailboxAddress("Admin", "raufroqeeb123@gmail.com"));
        }
    }
}
