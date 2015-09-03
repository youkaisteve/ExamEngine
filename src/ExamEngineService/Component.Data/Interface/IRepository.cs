using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Component.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        IQueryable<TEntity> Entities { get; }

        #region 方法

        int Insert(TEntity entity, bool isSave = true);

        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        int Delete(TKey key, bool isSave = true);

        int Delete(TEntity entity, bool isSave = true);

        int Delete(IEnumerable<TEntity> entities, bool isSave = true);

        int Delete(Expression<Func<TEntity, bool>> expression, bool isSave = true);

        int Update(TEntity entity, bool isSave = true);

        int Update(Expression<Func<TEntity, object>> expression, TEntity entity, bool isSave = true);

        #endregion
    }
}