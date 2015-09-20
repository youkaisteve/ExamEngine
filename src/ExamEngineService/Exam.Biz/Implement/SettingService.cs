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

namespace Exam.Service.Implement
{
    [Export(typeof(ISettingService))]
    public class SettingService : ISettingService
    {
        [Import]
        private StAnswerRepository stAnswerRepo;
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
                    TemplateName = PublicFunc.GetConfigByKey_AppSettings("TemplatePrefix") + model.TemplateName,
                    TemplateDesc = model.TemplateDesc
                };
                stAnswerRepo.Insert(data);
            }

        }

        public dynamic LoadForm(string formName)
        {
            return
                stAnswerRepo.Entities.Select(n => new { n.TemplateName, n.TemplateData, n.TemplateDesc })
                    .FirstOrDefault(m => m.TemplateName == formName);
        }
    }
}
