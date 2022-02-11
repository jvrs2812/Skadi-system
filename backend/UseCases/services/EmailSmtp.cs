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
        private EmailSend _emailsend;
        private WebClient _cliente;
        public EmailSmtp(EmailSend emailsend,WebClient client){
            this._emailsend = emailsend;
            this._cliente = client;
        }
        public async Task<bool> executeAsync(string toEmail, string subject)
        {
            try
            {
                var emailSend = new MimeMessage();
                emailSend.From.Add(MailboxAddress.Parse(_emailsend.email));
                emailSend.To.Add(MailboxAddress.Parse(toEmail));
                emailSend.Subject = subject;
                emailSend.Body = new TextPart(TextFormat.Html) { Text = getBodyConfirmationEmail(toEmail) };

                using var smtp = new SmtpClient();
                smtp.Connect(_emailsend.servidor, _emailsend.port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailsend.email, _emailsend.password);
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

        private string getBodyConfirmationEmail(string email)
        {
            string html = _cliente.DownloadString(Directory.GetCurrentDirectory() + "\\template\\email\\confirmation.html");

            html = html.Replace("Confirmation-Email-Link", "https://localhost:7146/ap1/confirmation-email/" + email);
            return html;
        }
    }
}