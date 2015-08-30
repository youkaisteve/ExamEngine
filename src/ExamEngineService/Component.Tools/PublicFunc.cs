/**************************************************************************************
 *  类名：PublicFunc.cs
 *  描述：公共方法
 *  创建者：tangting
 *  创建时间：2013/10/18
 *  修改记录：
 *  
 * ************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using EmitMapper;

namespace Component.Tools
{
    public sealed class PublicFunc
    {
        /// <summary>
        ///     实体（模型）转换
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TResult EntityMap<TSource, TResult>(TSource source)
        {
            ObjectsMapper<TSource, TResult> mapper =
                ObjectMapperManager.DefaultInstance.GetMapper<TSource, TResult>();
            return mapper.Map(source);
        }

        /// <summary>
        ///     IList to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof (T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name,
                    (prop.PropertyType.IsGenericType &&
                     prop.PropertyType.GetGenericTypeDefinition() ==
                     typeof (Nullable<>))
                        ? Nullable.GetUnderlyingType(
                            prop.PropertyType)
                        : prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        ///     获取config文件的AppSettings配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigByKey_AppSettings(string key)
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings[key])
                ? string.Empty
                : ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        ///     获取config文件的connectionstring配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigByKey_ConnString(string key)
        {
            return string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[key].ConnectionString)
                ? string.Empty
                : ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}