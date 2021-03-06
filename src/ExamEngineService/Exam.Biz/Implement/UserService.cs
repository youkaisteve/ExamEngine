﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Component.Data;
using Component.Tools;
using Component.Tools.Exceptions;
using Exam.Model;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;
using WorkflowCallWapper;

namespace Exam.Service.Implement
{
    [Export(typeof(IUserService))]
    public class UserService : ServiceBase, IUserService
    {
        [Import("Exam")]
        private IAdoNetWrapper adonetWrapper;
        [Import]
        private TeamRepository teamRepo;
        [Import]
        private UserRepository userRepo;
        [Import]
        private UserTeamRepository userTeamRepo;

        [Import]
        private RoleUserRepository roleUserRepo;

        [Import]
        private UserAnswerRepository userAnswerRepository;

        [Import]
        private StandardAnwserRepository standardAnwserRepository;

        protected override string ModuleName
        {
            get { return "User"; }
        }

        public List<User> GetUserByTeamName(string name)
        {
            return userRepo.GetUserByTeamName(name);
        }

        public dynamic GetAllProcess()
        {
            var proxy = new WorkflowProxy();
            var allProcess = proxy.GetAllProcessDefinitions();

            var result = allProcess.Select(p => new ProcessWithNodeModel
            {
                ProcessName = p.ProcessName,
                Tasks = proxy.GetProcessAllTask(p.ProcessName)
            }).ToList();

            return new { AllProcess = result, Teams = teamRepo.Entities.Select(m => m.TeamName).ToList() };
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

        public void ImportTeamUser(TramUserImportListModel data)
        {
            if (data == null)
            {
                throw new BusinessException("无数据");
            }

            string sqlStr = @" DELETE FROM dbo.Team;
                                DELETE A FROM dbo.RoleUser A INNER JOIN dbo.[User] B ON A.UserID = B.UserID WHERE B.UserType = 0;
                                    DELETE FROM dbo.[User] WHERE UserType = 0;
                                    DELETE FROM dbo.UserTeam;";
            adonetWrapper.ExecuteSqlCommand(sqlStr);
            string pwd = PublicFunc.GetConfigByKey_AppSettings("DefaultPWD");

            DateTime now = DateTime.Now;
            int studentRoleSysNo = int.Parse(PublicFunc.GetConfigByKey_AppSettings("StudentRoleSysNo"));
            IEnumerable<IGrouping<string, TeamUserImportModel>> groupList = data.Lists.GroupBy(m => m.TeamName);
            foreach (var group in groupList)
            {
                teamRepo.Insert(new Team { TeamName = group.Key, InDate = now, InUser = data.User.UserID });
                foreach (TeamUserImportModel user in group.ToList())
                {
                    userRepo.Insert(new User
                    {
                        UserID = user.UserID,
                        UserName = user.UserName,
                        Password = pwd,
                        Status = 1,
                        InUser = data.User.UserID,
                        InDate = now
                    });
                    userTeamRepo.Insert(new UserTeam
                    {
                        TeamName = user.TeamName,
                        UserID = user.UserID,
                        InDate = now,
                        InUser = data.User.UserID
                    });

                    roleUserRepo.Insert(new RoleUser()
                    {
                        InDate = now,
                        InUser = data.User.UserID,
                        Status = 1,
                        UserID = user.UserID,
                        RoleSysNo = studentRoleSysNo
                    });
                }
            }
        }

        public dynamic GetScoreStatistics()
        {
            var userAnswer = from user in userRepo.Entities
                             join answer in userAnswerRepository.Entities
                                 on user.UserID equals answer.UserID
                             group new { user, answer } by new { user.UserID, user.UserName }
                                 into g
                                 select new
                                 {
                                     User = new
                                     {
                                         g.Key.UserID,
                                         g.Key.UserName,
                                         Answer = g.Where(r => r.user.UserID == g.Key.UserID).Select(t => new { t.answer.TemplateName, t.answer.TemplateData })
                                     },
                                 };

            var standardAnswer = standardAnwserRepository.Entities.Select(m => new { m.TemplateName, m.TemplateData });

            return new { UserAnswers = userAnswer.ToList(), StandardAnswers = standardAnswer.ToList() };
        }

        public void TerminateAllUnFinishProcess()
        {
            var proxy = new WorkflowProxy();
            proxy.SetFinishProcess();
        }

        public List<dynamic> GetAllStudent()
        {
            var result = new List<dynamic>();
            var query = from user in userRepo.Entities
                        join userTeam in userTeamRepo.Entities
                        on user.UserID equals userTeam.UserID
                        where user.UserType == 0 && user.Status == 1
                        select new
                        {
                            userTeam.TeamName,
                            user.UserID,
                            user.UserName
                        };
            query.ToList().ForEach(item =>
            {
                result.Add(item);
            });

            return result;
        }
    }
}