
using System.Security.Cryptography;
namespace Radicitus.Web.Extensions
{
    public static class CryptographyExtensions
    {
        public static byte[] HashPasswordSha512(string password)
        {
            var cryptoProvider = new SHA512Managed();
            var hashedBytes = cryptoProvider
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return hashedBytes;
        }
    }
}
