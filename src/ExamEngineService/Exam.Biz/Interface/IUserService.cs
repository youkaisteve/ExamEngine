﻿using System.Collections.Generic;
using Exam.Model;
using Exam.Repository;

namespace Exam.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        ///     根据小组名获取用户
        /// </summary>
        /// <param name="name">小组名</param>
        /// <returns></returns>
        List<User> GetUserByTeamName(string name);

        /// <summary>
        ///     获取所有小组，包括用户信息
        /// </summary>
        /// <returns></returns>
        List<dynamic> GetAllTeamsWithUser();

        void ImportTeamUser(TramUserImportListModel data);

        dynamic GetAllProcess();
        dynamic GetScoreStatistics();

        /// <summary>
        /// 终止所有未结束的流程
        /// </summary>
        void TerminateAllUnFinishProcess();

        /// <summary>
        /// 获取已导入的学生
        /// </summary>
        /// <returns></returns>
        List<dynamic> GetAllStudent();
    }
}