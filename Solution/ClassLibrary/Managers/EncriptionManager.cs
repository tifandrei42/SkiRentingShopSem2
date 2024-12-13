using BCrypt.Net;

namespace BusinessLogic.Managers
{
    public class EncriptionManager
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }
        public bool CheckCredentials(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
