using System.ComponentModel.Composition;

namespace Exam.Repository
{
    [Export("Exam", typeof (ExamSystemEntities))]
    public partial class ExamSystemEntities
    {
    }
}