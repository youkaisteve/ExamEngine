using System;
using System.Collections;

namespace Exam.Api.Utility
{
    public static class UserHelper
    {
        private static readonly Hashtable _hashtable = new Hashtable();

        public static bool IsValid(string sUserKey)
        {
            return _hashtable.Contains(sUserKey);
        }

        public static void Remove(string sKey)
        {
            _hashtable.Remove(sKey);
        }

        public static string CreateUserToken(string sUser, string sPassword)
        {
            string sUserKey = string.Format("{0}", Guid.NewGuid().ToString().Replace("-", ""));
            string sUserInfo = sUser;

            //验证用户名密码
            //...

            AddUser(sUserKey, sUserInfo);

            return sUserKey;
        }

        private static void AddUser(string sKey, string sUserInfo)
        {
            if (_hashtable.Contains(sKey))
            {
                _hashtable[sKey] = sUserInfo;
            }
            else
            {
                _hashtable.Add(sKey, sUserInfo);
            }
        }
    }
}