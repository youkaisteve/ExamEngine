using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using Component.Data;

namespace Exam.Repository
{
    [Export("Exam", typeof (IUnitOfWork))]
    public class ExamUnitOfWorkContext : UnitOfWorkContextBase
    {
        protected override DbContext Context
        {
            get { return ExamContext.Value; }
        }

        [Import("Exam", typeof (ExamSystemEntities))]
        public Lazy<ExamSystemEntities> ExamContext { get; set; }
    }
}