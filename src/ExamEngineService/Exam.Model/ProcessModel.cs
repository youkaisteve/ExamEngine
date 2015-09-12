using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class ProcessModel : ModelBase
    {
        /// <summary>
        /// 等于ProcessID
        /// </summary>
        public string InstanceId { get; set; }
        /// <summary>
        /// 等于taskid
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// 等于ProcessName
        /// </summary>
        public string DefineName { get; set; }

        public string TokenName { get; set; }

        /// <summary>
        /// 等于ActionName
        /// </summary>
        public string TransitionName { get; set; }

        public string TemplateName { get; set; }
        public string TemplateData { get; set; }
    }
}
