using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class ProcessModel : ModelBase
    {
        public string InstanceId { get; set; }
        public string TokenId { get; set; }

        public string TransitionName { get; set; }

        public string TemplateName { get; set; }
        public string TemplateData { get; set; }
    }
}
