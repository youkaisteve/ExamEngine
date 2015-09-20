using System.ComponentModel.Composition;
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

        public dynamic LoadForm(string formName)
        {
            return
                stAnswerRepo.Entities.Select(n => new { n.TemplateName, n.TemplateData, n.TemplateDesc })
                    .FirstOrDefault(m => m.TemplateName == formName);
        }
    }
}
