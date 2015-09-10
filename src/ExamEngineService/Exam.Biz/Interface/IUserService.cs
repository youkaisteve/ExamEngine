using System.Collections.Generic;
using Exam.Model;
using Exam.Repository;

namespace Exam.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        ///     根据小组sysno获取用户
        /// </summary>
        /// <param name="sysNo">小组sysno</param>
        /// <returns></returns>
        List<User> GetUserByTeamSysNo(int sysNo);

        /// <summary>
        ///     获取所有小组，包括用户信息
        /// </summary>
        /// <returns></returns>
        List<dynamic> GetAllTeamsWithUser();

        void ImportTeamUser(List<TeamUserImportModel> data);
    }
}