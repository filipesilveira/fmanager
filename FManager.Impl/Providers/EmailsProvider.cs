namespace FManager.Impl.Services
{
    using FManager.Core.Providers;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    /// <summary>
    /// Emails Provider
    /// </summary>
    public class EmailsProvider : IEmailsProvider
    {
        private readonly string smtpHost = "xxxx";
        private readonly int smtpPort = 587;
        private readonly string mailAddress = "xxxx";
        private readonly string mailPassword = "xxxx";
        private readonly string mailDisplayName = "xxxx";

        /// <inheritdoc/>
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address.Trim().ToUpperInvariant() == email.Trim().ToUpperInvariant();
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task SendAsync(string subject, string body, params string[] mailDestinations)
        {
            await Task.Run(() =>
            {
                lock (this)
                {
                    var destinations = mailDestinations.Where(dest => this.IsValidEmail(dest));

                    if (!destinations.Any())
                    {
                        return;
                    }

                    var client = new SmtpClient(smtpHost, smtpPort)
                    {
                        Credentials = new NetworkCredential(mailAddress, mailPassword),
                        EnableSsl = true
                    };

                    var mailMessage = new MailMessage()
                    {
                        From = new MailAddress(mailAddress, mailDisplayName)
                    };

                    foreach (var destination in destinations)
                    {
                        mailMessage.To.Add(destination);
                    }

                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    client.Send(mailMessage);
                }
            });
        }
    }
}