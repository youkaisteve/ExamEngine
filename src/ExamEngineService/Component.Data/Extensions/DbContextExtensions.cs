using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Component.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static int SaveChanges(this DbContext context, bool validateOnSaveEnabled)
        {
            bool isSame = context.Configuration.ValidateOnSaveEnabled == validateOnSaveEnabled;
            try
            {
                context.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                return context.SaveChanges();
            }
            finally
            {
                if (!isSame)
                {
                    context.Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }

        public static void Update<TEntity, TKey>(this DbContext context, TEntity entity)
            where TEntity : EntityBase<TKey>
        {
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            try
            {
                DbEntityEntry<TEntity> entry = context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    context.Set<TEntity>().Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (InvalidOperationException)
            {
                TEntity oldEntity = dbSet.Find(entity.Id);
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
            }
        }

        public static void Update<TEntity, TKey>(this DbContext dbContext,
            Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities)
            where TEntity : EntityBase<TKey>
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic) propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    //entry.State = EntityState.Unchanged;
                    foreach (MemberInfo memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity originalEntity = dbSet.Local.Single(m => Equals(m.Id, entity.Id));
                    ObjectContext objectContext = ((IObjectContextAdapter) dbContext).ObjectContext;
                    ObjectStateEntry objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
                    objectEntry.ApplyCurrentValues(entity);
                    objectEntry.ChangeState(EntityState.Unchanged);
                    foreach (MemberInfo memberInfo in memberInfos)
                    {
                        objectEntry.SetModifiedProperty(memberInfo.Name);
                    }
                }
            }
        }
    }
}