using System.Collections.Generic;

namespace Component.Data
{
    public interface IAdoNetWrapper
    {
        IEnumerable<TEntity> Query<TEntity>(string sql, params object[] parameters);

        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}