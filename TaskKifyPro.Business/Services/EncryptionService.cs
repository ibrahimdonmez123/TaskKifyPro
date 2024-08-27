using TaskKifyPro.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.Entity.Concrete;
using TaskKifyPro.DataAccess.EntityFramework;
using System.Security.Cryptography;

namespace TaskKifyPro.Business.Services
{
    public static class EncryptionService
    {
        // Şifreyi kriptolamak için SHA256 algoritmasını kullanır
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2")); // Hexadecimal formatında dizeye dönüştürür
                }
                return sb.ToString();
            }
        }

        // Şifrenin doğruluğunu kontrol eder
        public static bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentException("Hashed password cannot be null or empty.", nameof(hashedPassword));
            }

            if (string.IsNullOrEmpty(plainPassword))
            {
                throw new ArgumentException("Plain password cannot be null or empty.", nameof(plainPassword));
            }

            string hashOfInput = HashPassword(plainPassword);
            return hashOfInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
