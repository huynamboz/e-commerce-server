using e_commerce_server.src.Core.Env;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class MailContent
    {
        public readonly string subject;
        public readonly string plainTextContent;
        public readonly string htmlContent;
        private static readonly string _host = ENV.HOST;
        private MailContent(string subject, string plainTextContent, string htmlContent)
        {
            this.subject = subject;
            this.plainTextContent = plainTextContent;
            this.htmlContent = htmlContent;
        }
        public static MailContent REQUEST_RESET_PASSWORD(string token)
        {
            return new MailContent(
                "Password recovery",
                $"Click this link to reset your password: {_host}/password/reset?token={token}",
                $"Click this <a href=\"{_host}/password/reset?token={token}\">link</a> to reset your password."
            );
        }
    }
}
