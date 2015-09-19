using Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam.Service.Interfave
{
    public interface ISettingService
    {
        void SaveStandardAnswer(StandardAnwserModel model);

        dynamic LoadForm(string formName);
    }
}
