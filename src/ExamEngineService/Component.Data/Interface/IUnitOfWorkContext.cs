using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Component.Data
{
    /// <summary>
    ///     数据单元操作接口
    /// </summary>
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        void RegisterNew<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>;

        void RegisterNew<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<TKey>;

        void RegisterModified<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>;

        void RegisterModified<TEntity, TKey>(Expression<Func<TEntity, object>> expression, TEntity entity)
            where TEntity : EntityBase<TKey>;

        void RegisterDeleted<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>;

        void RegisterDeleted<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<TKey>;
    }
}