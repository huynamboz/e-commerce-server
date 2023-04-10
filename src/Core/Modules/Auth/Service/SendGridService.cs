using SendGrid.Helpers.Mail;
using SendGrid;
using e_commerce_server.src.Core.Env;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class SendGridService
    {
        private readonly SendGridClient _client;
        private readonly EmailAddress _from;
        public SendGridService()
        {
            _client = new SendGridClient(ENV.SENDGRID_API_KEY);
            _from = new EmailAddress(ENV.SENDGRID_EMAIL_ADDRESS, "Bad Supermarket");
        }
        public async void SendMail(string email, MailContent mailContent)
        {
            var to = new EmailAddress(email);

            var msg = MailHelper.CreateSingleEmail(_from, to, mailContent.subject, mailContent.plainTextContent, mailContent.htmlContent);

            var response = await _client.SendEmailAsync(msg);

            Console.WriteLine(response.StatusCode);
        }
    }
}
