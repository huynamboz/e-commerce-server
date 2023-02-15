namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class BCryptService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
