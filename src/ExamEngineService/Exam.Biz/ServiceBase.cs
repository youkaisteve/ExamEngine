using System.ComponentModel.Composition;
using Component.Data;

namespace Exam.Service
{
    public abstract class ServiceBase
    {
        [Import("Exam", typeof(IUnitOfWork))]
        protected IUnitOfWork UnitOfWork { get; set; }

        protected abstract string ModuleName { get; }
    }
}