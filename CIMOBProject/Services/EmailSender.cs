using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        public Task Execute(string subject, string message, string email)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("dontreply@cimob.ips.pt", "CIMOB Support");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            return client.SendEmailAsync(msg);
        }
    }
}
