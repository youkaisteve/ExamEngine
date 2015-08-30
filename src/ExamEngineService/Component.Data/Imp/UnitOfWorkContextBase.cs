// 源文件头信息：
// 文 件 名：UnitOfWorkContextBase.cs
// 类    名：UnitOfWorkContextBase
// 所属工程：Component.Data
// 最后修改：游凯
// 最后修改：2013-09-11 11:19:57

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Component.Data.Extensions;
using Component.Tools.Exceptions;

namespace Component.Data
{
    /// <summary>
    ///     单元操作实现
    /// </summary>
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        #region 属性

        /// <summary>
        ///     获取当前使用的数据访问上下文对象，由具体的数据访问上下文实现，比如EFDbContext
        /// </summary>
        protected abstract DbContext Context { get; }

        public DbContext DbContext
        {
            get { return Context; }
        }

        public bool IsCommited { get; private set; }

        #endregion

        #region IUnitOfWork实现——提交，回滚，释放

        public int Submit(bool validateOnSaveEnabled = true)
        {
            if (IsCommited)
            {
                return 0;
            }
            try
            {
                int result = Context.SaveChanges(validateOnSaveEnabled);
                IsCommited = true;
                return result;
            }
            catch (DbUpdateException exception)
            {
                if (exception.InnerException != null && exception.InnerException.InnerException is SqlException)
                {
                    var sqlEx = exception.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw new DataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
        }

        public void RollBack()
        {
            IsCommited = false;
        }

        public void Dispose()
        {
            if (!IsCommited)
            {
                Submit();
            }
            Context.Dispose();
        }

        #endregion

        public DbSet<TEntity> Set<TEntity, TKey>() where TEntity : EntityBase<TKey>
        {
            return Context.Set<TEntity>();
        }

        #region IUnitOfWorkContext实现 —— CRUD 操作

        public void RegisterNew<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommited = false;
        }

        public void RegisterNew<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<TKey>
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew<TEntity, TKey>(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RegisterModified<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>
        {
            Context.Update<TEntity, TKey>(entity);
            IsCommited = false;
        }

        public void RegisterModified<TEntity, TKey>(Expression<Func<TEntity, object>> expression, TEntity entity)
            where TEntity : EntityBase<TKey>
        {
            Context.Update<TEntity, TKey>(expression, entity);
            IsCommited = false;
        }

        public void RegisterDeleted<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommited = false;
        }

        public void RegisterDeleted<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<TKey>
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted<TEntity, TKey>(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        #endregion
    }
}