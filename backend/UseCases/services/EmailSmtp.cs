using System.Net;
using backend.UseCases.model;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace backend.UseCases.services
{
    public class EmailSmtp
    {
        public async Task<bool> executeAsync(EmailSend emailDados, string toEmail, string subject)
        {
            try
            {
                var emailSend = new MimeMessage();
                emailSend.From.Add(MailboxAddress.Parse(emailDados.email));
                emailSend.To.Add(MailboxAddress.Parse(toEmail));
                emailSend.Subject = subject;
                emailSend.Body = new TextPart(TextFormat.Html) { Text = getBodyConfirmationEmail(toEmail) };

                using var smtp = new SmtpClient();
                smtp.Connect(emailDados.servidor, emailDados.port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailDados.email, emailDados.password);
                smtp.Send(emailSend);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        private static string getBodyConfirmationEmail(string email)
        {
            var client = new WebClient();

            string html = client.DownloadString(Directory.GetCurrentDirectory() + "\\template\\email\\confirmation.html");

            html = html.Replace("Confirmation-Email-Link", "https://localhost:7146/ap1/confirmation-email/" + email);
            return html;
        }
    }
}