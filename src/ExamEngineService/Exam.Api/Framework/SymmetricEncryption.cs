using System;
using System.Security.Cryptography;
using System.Text;

namespace Exam.Api.Framework
{
    public static class SymmetricEncryption
    {
        private static string key = "sfdjf48mdfdf3054";

        public static string Encrypt(String plainText)
        {
            using (var hashmd5 = new MD5CryptoServiceProvider())
            using (var tdesProvider = new TripleDESCryptoServiceProvider())
            {
                tdesProvider.Key = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                ;
                tdesProvider.Mode = CipherMode.ECB;

                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                return
                    Convert.ToBase64String(tdesProvider.CreateEncryptor()
                        .TransformFinalBlock(inputBytes, 0, inputBytes.Length));
            }
        }

        public static String Decrypt(string encryptedString)
        {
            using (var hashmd5 = new MD5CryptoServiceProvider())
            using (var tdesProvider = new TripleDESCryptoServiceProvider())
            {
                tdesProvider.Key = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                tdesProvider.Mode = CipherMode.ECB;

                byte[] inputBytes = Convert.FromBase64String(encryptedString);
                return
                    Encoding.UTF8.GetString(tdesProvider.CreateDecryptor()
                        .TransformFinalBlock(inputBytes, 0, inputBytes.Length));
            }
        }
    }
}