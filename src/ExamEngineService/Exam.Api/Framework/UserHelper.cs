using System.Web;
using Component.Tools;
using Exam.Model;
using Component.Tools.Cache;

namespace Exam.Api.Framework
{
    public class UserHelper
    {
        private static GlobalCacheWrapper _userCache = new GlobalCacheWrapper();
        public static string CreateUserToken(string userId, string password)
        {
            string text = string.Format("{0}:{1}:{2}", userId, password,
                PublicFunc.GetConfigByKey_AppSettings("EncryptKey"));
            return SymmetricEncryption.Encrypt(text);
        }

        public static void SetUserSession(UserInfo user)
        {
            _userCache.AddToCache(user.UserID, user, MyCachePriority.Default, user.ExpiredDate);
            //if (HttpContext.Current.Session[user.UserID] == null)
            //{
            //    HttpContext.Current.Session.Add(user.UserID, user);
            //}
            //else
            //{
            //    HttpContext.Current.Session[user.UserID] = user;
            //}
        }

        public static UserInfo GetUserSession(string userId)
        {
            return _userCache.GetMyCachedItem(userId) as UserInfo;
        }

        public static string[] GetCredentials(string token)
        {
            string str = SymmetricEncryption.Decrypt(token);
            return str.Split(':');
        }
    }
}