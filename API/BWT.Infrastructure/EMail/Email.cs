using BWT.Core.Entities.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace BWT.Infrastructure.EMail
{
    public class Email
    {
        public static bool Send(Core.Entities.Mail.EMail mail)
        {
            string origin = "";
            string password = "";
            string url = "https://localhost:44383/api/access/finalpr/?token=" + mail.Token;

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("System Help", origin));
            message.To.Add(MailboxAddress.Parse(mail.To));
            message.Subject = mail.Title;
            message.Body = new TextPart("html") { Text =
                "<p>Correo para recuperacion de contraseña</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a><br>" +
                "Solicitado el '" + DateTime.Now.ToString("dd MMM yyyy") + "'<br>"+
                "A las '"+ DateTime.Now.ToString("HH:mm:ss tt") + "'"
            };

            SmtpClient client = new SmtpClient();
            try
            {

                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(origin, password);
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
