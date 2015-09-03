using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Component.Data;

namespace Exam.Repository
{
    /// <summary>
    ///     封装EF中使用ADO.NET的功能
    /// </summary>
    [Export("Exam", typeof (IAdoNetWrapper))]
    public class ExamAdoNetWrapper : IAdoNetWrapper
    {
        [Import("Exam", typeof(ExamSystemEntities))]
        private Lazy<ExamSystemEntities> _examContext;

        /// <summary>
        ///     查询并封装成对象返回
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query<TEntity>(string sql, params object[] parameters)
        {
            return _examContext.Value.Database.SqlQuery<TEntity>(sql, parameters);
        }

        /// <summary>
        ///     执行Sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _examContext.Value.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}