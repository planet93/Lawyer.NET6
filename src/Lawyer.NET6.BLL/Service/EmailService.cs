using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Lawyer.NET6.BLL.Service
{
    public class EmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _fromName;
        private readonly string _fromEmail;
        private readonly string _password;

        public EmailService(IConfiguration cfg)
        {
            _host = cfg["mail_host"];
            _port = Convert.ToInt32(cfg["mail_port"]);
            _ssl = Convert.ToBoolean(cfg["mail_ssl"]);
            _fromName = cfg["mail_fromName"];
            _fromEmail = cfg["mail_fromEmail"];
            _password = cfg["mail_password"];
        }

        public async Task SendMessage(string name, string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_fromName, _fromEmail));
            emailMessage.To.Add(new MailboxAddress(name, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            await Send(emailMessage);
        }
        private async Task Send(MimeMessage m)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_host, _port, false);
                await client.AuthenticateAsync(_fromEmail, _password);
                await client.SendAsync(m);

                await client.DisconnectAsync(true);
            }
        }

    }
}
