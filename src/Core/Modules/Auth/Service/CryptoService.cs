using System.Security.Cryptography;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class CryptoService
    {
        public static byte[] GetRandomBytes()
        {
            byte[] randomBytes = new byte[32];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(randomBytes);

            return randomBytes;
        }
    }
}
