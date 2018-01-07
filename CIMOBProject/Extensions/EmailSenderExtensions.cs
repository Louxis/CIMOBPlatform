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
                $"Por favor visita a seguinte liga��o: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>.");
        }

        public static void SendMultipleEmail(this IEmailSender emailSender, List<string> emails, string title, string message) {
            foreach(string email in emails){
                emailSender.SendEmailAsync(email, title, message);
            }
        }

        public static void SendStateEmail(this IEmailSender emailSender, int statId, string email) {
            string message = "";
            string title = "Atualiza��o na sua candidatura!";
            switch (statId) {
                case 2: message = "Sauda��es, foi realizada uma atualiza��o na sua candidatura e esta encontra-se em avalia��o.";
                    break;
                case 3: message = "Sauda��es, foi realizada uma atualiza��o na sua candidatura e esta foi avaliada, ser� anunciada" +
                        " a nota quando sair a seria��o!";
                    break;
                case 4: message = "Sauda��es, a serializa��o foi finalizada e a sua candidatura foi aprovada para o seu destino!";
                    break;
                case 5: message = "Sauda��es, a serializa��o foi finalizada e a sua candidatura n�o foi aprovada para o seu destino," +
                        " poder� volta a candidatar-se no pr�ximo semestre.";
                    break;
                default: message = "Sauda��es, existiu uma atualiza��o na sua candidatura, poder� consultar o seu perfil para saber mais.";
                    break;
            }
            emailSender.SendEmailAsync(email, title, message);
        }
    }
}
