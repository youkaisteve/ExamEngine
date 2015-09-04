using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
namespace Exam.Api.Framework
{
    public class UserHelper
    {
        public static string CreateUserToken(string strUserName)
        {
            string text = string.Format("{0}:{1}", strUserName, DateTime.Now.AddDays(7).ToString("yyyy-M-d hh:mm:ss"));
            return SymmetricEncryption.Encrypt(text);
        }

        public static string[] GetCredentials(string token)
        {
            var str= SymmetricEncryption.Decrypt(token);
            return str.Split(':');
        }
    }
}