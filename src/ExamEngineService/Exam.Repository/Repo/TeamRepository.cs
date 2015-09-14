﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(TeamRepository))]
    public class TeamRepository : ExamRepositoryBase<Team, int>
    {
        [Import]
        private UserRepository userRepo;

        [Import]
        private UserTeamRepository userTeamRepo;

        [Import]
        private WorkflowTeamRepository workflowTeamRepo;

        [Import]
        private AssignedUserRepository assignedUserRepo;

        [Import]
        private RoleUserRepository roleUserRepository;

        [Import]
        private RoleRepository roleRepository;

        public List<User> GetUsersByNodeName(string processName, string nodeName)
        {
            var query = from user in userRepo.Entities
                join userTeam in userTeamRepo.Entities
                    on user.UserID equals userTeam.UserID
                join wTeam in workflowTeamRepo.Entities
                    on userTeam.TeamName equals wTeam.TeamName
                //join roleUser in roleUserRepository.Entities
                //on user.UserID equals roleUser.UserID
                //join role in roleRepository.Entities
                //on roleUser.RoleSysNo equals role.SysNo
                where wTeam.NodeName == nodeName && wTeam.ProcessName == processName
                select user;

            var choosedUserIds = from assign in assignedUserRepo.Entities
                where assign.ProcessName == processName && assign.Nodename == nodeName
                select assign.UserID;

            return query.Where(m => !choosedUserIds.Contains(m.UserID)).Distinct().ToList();
        }
    }
}
