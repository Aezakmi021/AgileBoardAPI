using AgileBoardAPI.DTO;
using System.Net.Mail;
using System.Net;
using AgileBoardAPI.Interfaces;

namespace AgileBoardAPI.Services
{
    public class MailService : IMailService
    {
        public async Task SendConfirmationMail(Mail mail)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("agile.boards@mail.com");
                message.To.Add(mail.Recipient);
                message.Subject = mail.Subject;
                message.Body = mail.Body;

                using (var smtpClient = new SmtpClient("your-smtp-server"))
                {
                    smtpClient.Port = 587; // Adjust the port accordingly
                    smtpClient.Credentials = new NetworkCredential("your-username", "your-password");
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(message);
                }
            }
        }
    }
}
