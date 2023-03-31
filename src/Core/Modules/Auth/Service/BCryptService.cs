namespace e_commerce_server.Src.Core.Modules.Auth.Service
{
    public class BCryptService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
