using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace RealTimeChat.Domain.Shared.Handlers
{
    public class PasswordHandler
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            int hashIterations = 10000;
            byte[] salt;
            byte[] hash;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            hash = new Rfc2898DeriveBytes(password, salt).GetBytes(hashIterations);
            return (Convert.ToBase64String(hash).Substring(0, 200), Convert.ToBase64String(salt));
        }

        public static bool VerifyPassword(string hashString, string saltString, string password)
        {
            int hashIterations = 10000;
            byte[] hash;
            byte[] salt;
            salt = Convert.FromBase64String(saltString);

            hash = new Rfc2898DeriveBytes(password, salt).GetBytes(hashIterations);
            return Convert.ToBase64String(hash).Substring(0, 200) == hashString;
        }

        public static bool IsPasswordComplex(string Password)
        {
            var PassRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*_-]).{8,}$");
            if (PassRegex.IsMatch(Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
