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

        string GetProcessImage(string processName);

        string GetCurrentTokenImage(string instanceId, string tokenId);
        /// <summary>
        /// 清洗数据
        /// </summary>
        void CleanData();
    }
}
