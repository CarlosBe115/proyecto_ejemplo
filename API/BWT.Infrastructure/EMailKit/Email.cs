using BWT.Core.Entities.Mail;
using System.Net.Mail;

namespace BWT.Infrastructure.EMailKit
{
    public class Email
    {
        public static void Send(ElectronicMail mail)
        {
            string origin = "";
            string destination = mail.To;
            string password = "";
            string url = "https://localhost:44383/api/access/finalpr/?token=" + mail.Token;

            MailMessage mailMessage = new MailMessage(
                origin,
                destination,
                mail.Title,
                "<p>Correo para recuperacion de contraseña</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>"
                );

            mailMessage.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(origin, password);

            client.Send(mailMessage);

            client.Dispose();
        }
    }
}
