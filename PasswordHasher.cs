using System;
using System.Security.Cryptography;

namespace Personal_Expense_Tracker
{
    /// <summary>
    /// Password hashing with PBKDF2 (HMAC-SHA256, 100,000 rounds, random 16-byte salt).
    /// Stored format:  PBKDF2$iterations$salt(base64)$hash(base64)
    /// A password can never be read back from the database - it can only be checked.
    /// </summary>
    internal static class PasswordHasher
    {
        private const string Prefix = "PBKDF2";
        private const int Iterations = 100000;
        private const int SaltSize = 16;
        private const int KeySize = 32;

        public static bool IsHashed(string stored)
        {
            return stored != null && stored.StartsWith(Prefix + "$", StringComparison.Ordinal);
        }

        public static string Hash(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            byte[] key = Derive(password, salt, Iterations, KeySize);
            return Prefix + "$" + Iterations + "$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(key);
        }

        public static bool Verify(string password, string stored)
        {
            if (password == null || stored == null) return false;

            // Old plain-text row (only until the startup migration hashes it)
            if (!IsHashed(stored)) return string.Equals(password, stored, StringComparison.Ordinal);

            string[] parts = stored.Split('$');
            if (parts.Length != 4) return false;

            int iterations;
            if (!int.TryParse(parts[1], out iterations) || iterations < 1) return false;

            byte[] salt;
            byte[] expected;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }
            if (expected.Length == 0) return false;

            byte[] actual = Derive(password, salt, iterations, expected.Length);
            return ConstantTimeEquals(actual, expected);
        }

        private static byte[] Derive(string password, byte[] salt, int iterations, int length)
        {
            using (Rfc2898DeriveBytes kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(length);
            }
        }

        // Compares without stopping at the first difference (prevents timing attacks)
        private static bool ConstantTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
