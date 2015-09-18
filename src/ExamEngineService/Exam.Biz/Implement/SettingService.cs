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
    public class SettingService : ISettingService
    {
        private StAnswerRepository stAnswerRepo;
        public void SaveStandardAnswer(StandardAnwserModel model)
        {
            var data = new StandardAnwser()
            {
                TemplateData = model.TemplateData,
                InDate = DateTime.Now,
                InUser = model.User.UserID,
                TemplateName = model.TemplateName,
                TemplateDesc = model.TemplateName
            };
            stAnswerRepo.Insert(data);
        }
    }
}
