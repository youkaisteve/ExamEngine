using System.ComponentModel.Composition;
using Component.Data;

namespace Exam.Repository.Repo
{
    public abstract class ExamRepositoryBase<TEntity, TKey> : EFRepositoryBase<TEntity, TKey>
        where TEntity : EntityBase<TKey>
    {
        [Import("Exam", typeof (IUnitOfWork))]
        public override IUnitOfWork UnitOfWork { get; set; }
    }
}