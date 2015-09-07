using System.Collections.Generic;

namespace Exam.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        ///     根据小组sysno获取用户
        /// </summary>
        /// <param name="sysNo">小组sysno</param>
        /// <returns></returns>
        List<dynamic> GetUserByTeamSysNo(int sysNo);

        /// <summary>
        ///     获取所有小组，包括用户信息
        /// </summary>
        /// <returns></returns>
        List<dynamic> GetAllTeamsWithUser();
    }
}