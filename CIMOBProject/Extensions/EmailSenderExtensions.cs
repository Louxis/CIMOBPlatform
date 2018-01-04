using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CIMOBProject.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirma o teu email!",
                $"Bem vindo ao CIMOB! Para teres acesso a todas as funcionalidades precisas" +
                $" de ativar a tua conta. <br>" +
                $"Por favor visita a seguinte ligação: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>.");
        }

        public static void SendMultipleEmail(this IEmailSender emailSender, List<string> emails, string title, string message) {
            foreach(string email in emails){
                emailSender.SendEmailAsync(email, title, message);
            }
        }
    }
}
