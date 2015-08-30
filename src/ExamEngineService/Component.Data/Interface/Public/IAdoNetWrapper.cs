// 源文件头信息：
// 文 件 名：IAdoNetWrapper.cs
// 接 口 名：IAdoNetWrapper
// 所属工程：Component.Data
// 最后修改：游凯
// 最后修改：2013-09-17 04:09:11

using System.Collections.Generic;

namespace Component.Data
{
    public interface IAdoNetWrapper
    {
        IEnumerable<TEntity> Query<TEntity>(string sql, params object[] parameters);

        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}