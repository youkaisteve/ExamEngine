﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;
using Exam.Model;
using Component.Tools.Exceptions;
using Component.Data;
using System;
using Component.Tools;

namespace Exam.Service.Implement
{
    [Export(typeof(IUserService))]
    public class UserService : ServiceBase, IUserService
    {
        [Import]
        private TeamRepository teamRepo;
        [Import]
        private UserRepository userRepo;
        [Import]
        private UserTeamRepository userTeamRepo;
        [Import]
        private IAdoNetWrapper adonetWrapper;

        protected override string ModuleName
        {
            get { return "User"; }
        }

        public List<User> GetUserByTeamName(string name)
        {
            return userRepo.GetUserByTeamName(name);
        }

        public List<dynamic> GetAllTeamsWithUser()
        {
            var query = from user in userRepo.Entities
                        join ut in userTeamRepo.Entities
                            on user.UserID equals ut.UserID
                        join t in teamRepo.Entities
                            on ut.TeamName equals t.TeamName
                        select new { t, user };
            var result = new List<dynamic>();
            query.ToList().ForEach(item =>
            {
                if (result.All(m => m.TeamSysNo != item.t.SysNo))
                {
                    result.Add(new
                    {
                        item.t.SysNo,
                        item.t.TeamName,
                        Users = new List<dynamic>()
                    });
                }
                else
                {
                    dynamic firstOrDefault = result.FirstOrDefault(m => m.TeamSysNo == item.t.SysNo);
                    if (firstOrDefault != null)
                        firstOrDefault.Users.Add(item.user);
                }
            });

            return result;
        }

        public void ImportTeamUser(List<TeamUserImportModel> data)
        {
            if (data == null)
            {
                throw new BusinessException("无数据");
            }

            string sqlStr = @" DELETE FROM dbo.Team;
                                    DELETE FROM dbo.User WHERE UserType = 0;
                                    DELETE FROM dbo.UserTeam";
            adonetWrapper.ExecuteSqlCommand(sqlStr);
            var pwd = PublicFunc.GetConfigByKey_AppSettings("DefaultPWD");

            var now = DateTime.Now;

            var groupList = data.GroupBy(m => m.TeamName);
            foreach (var group in groupList)
            {
                teamRepo.Insert(new Team() { TeamName = group.Key, InDate = now, InUser = "001" });
                foreach (var user in group.ToList())
                {
                    userRepo.Insert(new User() { UserID = user.UserId, UserName = user.UserName, Password = pwd, Status = 1, InUser = "001", InDate = now });
                    userTeamRepo.Insert(new UserTeam() { TeamName = user.TeamName, UserID = user.UserId, InDate = now, InUser = "001" });
                }
            }
        }
    }
}