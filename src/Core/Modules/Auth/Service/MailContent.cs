namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class MailContent
    {
        public readonly string subject;
        public readonly string plainTextContent;
        public readonly string htmlContent;
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
                $"Click this link to reset your password: http://localhost:5000/api/v1/auth/reset-password/{token}",
                $"Click this <a href=\"http://localhost:5000/api/v1/auth/reset-password/{token}\">link</a> to reset your password."
            );
        }
    }
}
