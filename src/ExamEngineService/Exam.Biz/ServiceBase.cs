using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Component.Data;
using Exam.Repository;

namespace Exam.Service
{
    public abstract class ServiceBase
    {
        [Import("Exam", typeof (IUnitOfWork))]
        protected IUnitOfWork UnitOfWork { get; set; }

        protected abstract string ModuleName { get; }

        /// <summary>
        ///     随机选择用户
        /// </summary>
        /// <param name="userList"></param>
        /// <param name="excepts"></param>
        /// <returns></returns>
        protected User GetRandomUserId(List<User> userList, List<string> excepts)
        {
            IEnumerable<User> tempList = userList.Where(m => !excepts.Contains(m.UserID));
            int randomIndex = new Random().Next(0, tempList.Count() - 1);
            return userList[randomIndex];
        }

        /// <summary>
        ///     随机选择用户
        /// </summary>
        /// <param name="userList"></param>
        /// <param name="excepts"></param>
        /// <returns></returns>
        protected User GetRandomUserId(List<User> userList)
        {
            int randomIndex = new Random().Next(0, userList.Count() - 1);
            return userList[randomIndex];
        }
    }
}