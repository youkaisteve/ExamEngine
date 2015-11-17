using System.ComponentModel.Composition;
using Component.Tools;
using Exam.Model;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interfave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCallWapper;
using Component.Data;

namespace Exam.Service.Implement
{
    [Export(typeof(ISettingService))]
    public class SettingService : ISettingService
    {
        [Import]
        private StAnswerRepository stAnswerRepo;

        [Import("Exam")]
        private IAdoNetWrapper adonetWrapper;

        public void SaveStandardAnswer(StandardAnwserModel model)
        {
            var existData = stAnswerRepo.Entities.FirstOrDefault(m => m.TemplateName == model.TemplateName);
            if (existData != null)
            {
                existData.TemplateData = model.TemplateData;
                existData.TemplateDesc = model.TemplateDesc;
                stAnswerRepo.Update(existData);
            }
            else
            {
                var data = new StandardAnwser()
                {
                    TemplateData = model.TemplateData,
                    InDate = DateTime.Now,
                    InUser = model.User.UserID,
                    TemplateName = model.TemplateName,
                    TemplateDesc = model.TemplateDesc
                };
                stAnswerRepo.Insert(data);
            }

        }

        public dynamic LoadForm(string formName)
        {
            formName = PublicFunc.GetConfigByKey_AppSettings("TemplatePrefix") + formName;
            return
                stAnswerRepo.Entities.Select(n => new { n.TemplateName, n.TemplateData, n.TemplateDesc })
                    .FirstOrDefault(m => m.TemplateName == formName);
        }

        public string GetProcessImage(string processName)
        {
            var proxy = new WorkflowProxy();
            var imageData = proxy.GetProcessPictureToByte(processName);
            return Convert.ToBase64String(imageData);
        }

        public string GetCurrentTokenImage(string instanceId, string tokenId)
        {
            var proxy = new WorkflowProxy();
            var imageData = proxy.GetCurrentTokenPictureToByte(instanceId, tokenId);
            return Convert.ToBase64String(imageData);
        }

        public void CleanData()
        {
            string sql = @"
                            DELETE A
                            FROM dbo.RoleUser A
                            INNER JOIN dbo.Role B
                            ON A.RoleSysNo = B.SysNo
                            WHERE B.RoleName = 'Student'

                            DELETE FROM dbo.UserAnwser
                            DELETE FROM dbo.Team
                            DELETE FROM dbo.UserTeam
                            DELETE FROM dbo.WorkflowTeamRelation
                            DELETE FROM dbo.[User] WHERE UserType = 0
                            DELETE FROM dbo.AssignedUser
                            ";
            adonetWrapper.ExecuteSqlCommand(sql);
        }
    }
}
