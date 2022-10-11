using System.Text;
using XSystem.Security.Cryptography;

namespace Pharmacy.WebApi.Common.Security
{
    public class PasswordHasher
    {
        private const string _key = "2d0f1460-033f-43a1-88d7-cdf57898e23e";

        public static (string Hash, string Salt) Hash(string password)
        {
            string salt = GenerateSalt();

            string _password = salt + password + _key;

            var tmpSource = ASCIIEncoding.ASCII.GetBytes(_password);
            var _hash = Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(tmpSource));

            return (Hash: _hash, Salt: salt);
        }

        public static bool Verify(string password, string salt, string hash)
        {
            string _password = salt + password + _key;

            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(_password);
            var _hash = Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(tmpSource));

            return _hash == hash;
        }

        private static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
