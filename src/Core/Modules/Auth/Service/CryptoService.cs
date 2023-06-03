namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class CryptoService
    {
        public static string GetRandomString()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, 64).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
