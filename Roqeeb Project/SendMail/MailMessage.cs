using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using static Roqeeb_Project.SendMail.EmailDTO;

namespace Roqeeb_Project.SendMail
{
    public class MailMessage : IMailMessage
    {
        public async Task<bool> SendEmail(EmailRequestModel email)
        {

            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Golden Inventory Management System";
            string SenderEmail = "raufroqeeb123@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = email.ReceiverEmail;
            string ToName = email.ReceiverName;
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            string BccName = "Janice Doe";
            string BccEmail = "example2@example2.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>();
            Bcc.Add(BccData);
            string CcName = "John Doe";
            string CcEmail = "example3@example2.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);
            string mail = email.ReceiverEmail;
            string link = $"http://127.0.0.1:5500/Employee/Employee%20Registration/EployeeRegistration.html?{mail}";
            string HtmlContent = $"<html><body><h4>{email.Message}</h4><a href={link}>Complete your Registration</a></body></html>";
            string TextContent = null;
            string Subject = email.Subject;
            string ReplyToName = "Golden Inventory Management System";
            string ReplyToEmail = "raufroqeeb123@gmail.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);
            string AttachmentUrl = null;
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            Attachment.Add(AttachmentContent);
            JObject Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            long? TemplateId = null;
            JObject Params = new JObject();
            Params.Add("parameter", "My param value");
            Params.Add("subject", "New Subject");
            List<string> Tags = new List<string>();
            Tags.Add("mytag");
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            To1.Add(smtpEmailTo1);
            var g = Guid.NewGuid().ToString();
            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add(g, Params);
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, Bcc, Cc, ReplyTo1, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
            messageVersiopns.Add(messageVersion);

            var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, HtmlContent, TextContent, Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Configuration.Default.ApiKey.Clear();
            return true;
            // Debug.WriteLine(result.ToJson());
            // Console.WriteLine(result.ToJson());
            // Console.ReadLine();


        }
    }
}
