// 源文件头信息：
// 文 件 名：IUnitOfWorkContext.cs
// 接 口 名：IUnitOfWorkContext
// 所属工程：Component.Data
// 最后修改：游凯
// 最后修改：2013-09-11 11:14:45

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