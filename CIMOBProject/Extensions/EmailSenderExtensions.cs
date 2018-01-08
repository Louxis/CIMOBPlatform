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

        public static void SendStateEmail(this IEmailSender emailSender, int statId, string email) {
            string message = "";
            string title = "Atualização na sua candidatura!";
            switch (statId) {
                case 2: message = "Saudações, foi realizada uma atualização na sua candidatura e esta encontra-se em avaliação.";
                    break;
                case 3: message = "Saudações, foi realizada uma atualização na sua candidatura e esta foi avaliada, será anunciada" +
                        " a nota quando sair a seriação!";
                    break;
                case 4: message = "Saudações, a serialização foi finalizada e a sua candidatura foi aprovada para o seu destino!";
                    break;
                case 5: message = "Saudações, a serialização foi finalizada e a sua candidatura não foi aprovada para o seu destino," +
                        " poderá volta a candidatar-se no próximo semestre.";
                    break;
                default: message = "Saudações, existiu uma atualização na sua candidatura, poderá consultar o seu perfil para saber mais.";
                    break;
            }
            emailSender.SendEmailAsync(email, title, message);
        }
    }
}
