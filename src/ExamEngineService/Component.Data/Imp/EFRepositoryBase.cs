using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Component.Data
{
    public abstract class EFRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        //<summary>
        //    获取 仓储上下文的实例
        //</summary>
        //[Import(typeof(IUnitOfWork))] tangt comment,在子类里实现此UnitOfWork的import，区别不同的context
        public virtual IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     获取 Entity Framework 仓储上下文实例
        /// </summary>
        protected UnitOfWorkContextBase EFContext
        {
            get { return UnitOfWork as UnitOfWorkContextBase; }
        }

        #region IRepository 接口实现

        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity, TKey>(); }
        }

        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterNew<TEntity, TKey>(entity);
            return isSave ? EFContext.Submit() : 0;
        }

        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            EFContext.RegisterNew<TEntity, TKey>(entities);
            return isSave ? EFContext.Submit() : 0;
        }

        public virtual int Delete(TKey key, bool isSave = true)
        {
            TEntity entity = EFContext.Set<TEntity, TKey>().Find(key);
            EFContext.RegisterDeleted<TEntity, TKey>(entity);
            return isSave ? EFContext.Submit() : 0;
        }

        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterDeleted<TEntity, TKey>(entity);
            return isSave ? EFContext.Submit() : 0;
        }

        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            EFContext.RegisterDeleted<TEntity, TKey>(entities);
            return isSave ? EFContext.Submit() : 0;
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> expression, bool isSave = true)
        {
            List<TEntity> entities = EFContext.Set<TEntity, TKey>().Where(expression).ToList();
            return entities.Count > 0 ? Delete(entities) : 0;
        }

        public virtual int Update(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterModified<TEntity, TKey>(entity);
            return isSave ? EFContext.Submit() : 0;
        }

        public virtual int Update(Expression<Func<TEntity, object>> expression, TEntity entity, bool isSave = true)
        {
            EFContext.RegisterModified<TEntity, TKey>(expression, entity);
            return isSave ? EFContext.Submit() : 0;
        }

        #endregion
    }
}