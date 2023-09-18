using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RestaurantRatingApp_V2.Tools
{
    public class HashClass
    {
        public static string HashPassword(string password, out string salt)
        {
         
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);

            // Hash the password with the salt using PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); 
                return Convert.ToBase64String(hashBytes);
            }
        }

       
        public static bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {         
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);
            using (var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] inputHashBytes = pbkdf2.GetBytes(32);
                return CompareByteArrays(inputHashBytes, storedHashBytes);
            }
        }

     
        private static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}