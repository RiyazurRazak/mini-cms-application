using BCrypt.Net;

namespace cms_api.Utils
{
    public class CryptoHelper
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool ValidatePassword(string userPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userPassword, hashedPassword);
        }

    }
}
