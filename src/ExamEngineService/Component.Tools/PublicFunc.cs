﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Reflection;

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
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name,
                    (prop.PropertyType.IsGenericType &&
                     prop.PropertyType.GetGenericTypeDefinition() ==
                     typeof(Nullable<>))
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

        public static string GetCurrentDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] { '\\' }) ==
                    Environment.CurrentDirectory.TrimEnd(new[] { '\\' })
                    ? AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] { '\\' }) + "\\"
                    : AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] { '\\' }) + "\\bin\\";
        }

        public static string GetDeployDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static bool MatchType(Type sourceType, string destTypeName)
        {
            destTypeName = destTypeName.ToUpper();
            if (sourceType.GetTypeInfo().Name.ToUpper() == destTypeName)
            {
                return true;
            }

            var baseType = sourceType.GetTypeInfo().BaseType;
            if (baseType != null)
            {
                if (baseType.Name.ToUpper() == destTypeName)
                {
                    return true;
                }
                return MatchType(baseType, destTypeName);
            }
            return false;
        }
    }
}