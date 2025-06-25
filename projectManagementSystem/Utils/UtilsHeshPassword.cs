using System.Security.Cryptography;
using System.Text;

namespace projectManagementSystem.Utils
{
    public static class UtilsHeshPassword
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32;  
        private const int Iterations = 100_000;
        public static string HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);

            
            var hashBytes = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
            Buffer.BlockCopy(key, 0, hashBytes, SaltSize, KeySize);

            return Convert.ToBase64String(hashBytes);
        }
        public static bool IsBase64String(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            input = input.Trim();
            if (input.Length % 4 != 0) return false;

            Span<byte> buffer = new byte[input.Length];
            return Convert.TryFromBase64String(input, buffer, out _);
        }
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (!IsBase64String(storedHash))
                return false;

            byte[] hashBytes = Convert.FromBase64String(storedHash);

            if (hashBytes.Length != SaltSize + KeySize)
                return false;

            byte[] salt = new byte[SaltSize];
            byte[] storedKey = new byte[KeySize];

            Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(hashBytes, SaltSize, storedKey, 0, KeySize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] newKey = pbkdf2.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(storedKey, newKey);
        }
    }
}
