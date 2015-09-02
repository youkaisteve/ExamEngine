using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Exam.Api.Utility
{
    public static class HashHelper
    {
        public static string ToMd5Hash(this byte[] bytes)
        {
            var hash = new StringBuilder();
            MD5 md5 = MD5.Create();

            md5.ComputeHash(bytes)
                .ToList()
                .ForEach(b => hash.AppendFormat("{0:x2}", b));

            return hash.ToString();
        }

        public static string ToMd5Hash(this string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString).ToMd5Hash();
        }
    }
}