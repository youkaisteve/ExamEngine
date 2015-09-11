using Component.Tools;
using Exam.Model;
using System;
using System.Web;

namespace Exam.Api.Framework
{
    public class UserHelper
    {
        public static string CreateUserToken(string userId, string password)
        {
            string text = string.Format("{0}:{1}:{2}", userId, password, PublicFunc.GetConfigByKey_AppSettings("EncryptKey"));
            return SymmetricEncryption.Encrypt(text);
        }

        public static void SetUserSession(UserInfo user)
        {
            if (HttpContext.Current.Session[user.UserID] == null)
            {
                HttpContext.Current.Session.Add(user.UserID, user);
            }
            else
            {
                HttpContext.Current.Session[user.UserID] = user;
            }
        }

        public static UserInfo GetUserSession(string userId)
        {
            return HttpContext.Current.Session[userId] as UserInfo;
        }

        public static string[] GetCredentials(string token)
        {
            string str = SymmetricEncryption.Decrypt(token);
            return str.Split(':');
        }
    }
}