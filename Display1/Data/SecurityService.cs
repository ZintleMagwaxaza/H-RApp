using Display1.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Display1.Data
{
    public class SecurityService : ISecurityService
    {
        public string UserName { get; set; }

        public bool ValidateCredentials(string password, Password dbPassword)
        {
            bool valid = false;

            byte[] saltBytes = Convert.FromBase64String(dbPassword.PasswordSalt);
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] passwordHashBytes = Convert.FromBase64String(dbPassword.PasswordHash);
            byte[] passwordHashed = Hash(passwordBytes, saltBytes);
            byte[] dbPasswordHashed = Hash(passwordHashBytes, saltBytes);

            valid = dbPasswordHashed.SequenceEqual(passwordHashed);
            return valid;
        }

        private static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();
            return HashAlgorithm.Create("MD5").ComputeHash(saltedValue);
        }

    }
}
