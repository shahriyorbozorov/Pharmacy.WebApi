using Org.BouncyCastle.Crypto.Generators;
using System.Text;

namespace Pharmacy.WebApi.Common.Security
{
    public class PasswordHasher
    {
        private const string _key = "2d0f1460-033f-43a1-88d7-cdf57898e23e";
        public static (string Hash, string Salt) Hash(string password)
        {
            string salt = GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(salt + password + _key);
            return (Hash: hash, Salt: salt);
        }

        public static bool Verify(string password, string salt, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(salt + password + _key, hash);
        }

        public static string ChangePassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(salt + password + _key);
        }

        private static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
