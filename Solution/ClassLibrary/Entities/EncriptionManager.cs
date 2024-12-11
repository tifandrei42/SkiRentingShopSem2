using BCrypt.Net;

namespace BusinessLogic.Entities
{
    public class EncriptionManager
    {
        /// <summary>
        /// Generates a bcrypt hash from a plain text password.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>The hashed password.</returns>
        public string HashPassword(string password)
        {
            // Adjust the work factor as needed. A typical value is 10 or 12.
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }

        /// <summary>
        /// Verifies that the provided password matches the given hashed password.
        /// </summary>
        /// <param name="hashedPassword">The previously hashed password (stored value).</param>
        /// <param name="providedPassword">The plain text password to verify.</param>
        /// <returns>True if the passwords match; otherwise, false.</returns>
        public bool CheckCredentials(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
