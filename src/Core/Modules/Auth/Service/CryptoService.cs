using System.Security.Cryptography;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class CryptoService
    {
        public static byte[] GetRandomBytes()
        {
            byte[] randomBytes = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
