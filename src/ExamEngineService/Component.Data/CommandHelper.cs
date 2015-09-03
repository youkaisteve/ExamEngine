using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Component.Tools.Attributes;

namespace Component.Data
{
    public class CommandHelper
    {
        public static void Setting<T>(QueryCondition condition, ref string commandSql, List<SqlParameter> parameters)
        {
            string aliasName = string.IsNullOrEmpty(condition.AliaseTableName) ? "" : condition.AliaseTableName + ".";
            string conditionString = ConvertCondition<T>(aliasName, condition.Conditions);
            string orderByString = ConvertOrderBy(aliasName, condition.OrderBys);
            if (commandSql.Contains("@param"))
            {
                commandSql = commandSql.Replace("@param", conditionString);
            }
            else
            {
                commandSql += conditionString;
            }

            if (commandSql.Contains("@OrderBy"))
            {
                commandSql = commandSql.Replace("@OrderBy", orderByString);
            }

            foreach (QueryProperty item in condition.Conditions)
            {
                if (!item.IsQueryString)
                {
                    var parameter = new SqlParameter();
                    parameter.DbType = item.PropertyType;
                    parameter.ParameterName = "@" + item.ParameterName;
                    parameter.Value = item.PropertyValue;
                    parameter.Direction = ParameterDirection.Input;

                    parameters.Add(parameter);
                }
            }

            if (condition.PageInfo != null && condition.PageInfo.PageSize > 0)
            {
                //设置分页参数和输出参数
                var begin = new SqlParameter();
                begin.DbType = DbType.Int32;
                begin.ParameterName = "@BeginNum";
                begin.Direction = ParameterDirection.Input;
                begin.Value = (condition.PageInfo.PageIndex - 1)*condition.PageInfo.PageSize;
                parameters.Add(begin);

                var end = new SqlParameter();
                end.DbType = DbType.Int32;
                end.ParameterName = "@EndNum";
                end.Direction = ParameterDirection.Input;
                end.Value = condition.PageInfo.PageIndex*condition.PageInfo.PageSize;
                parameters.Add(end);

                //设置输出参数
                var total = new SqlParameter();
                total.DbType = DbType.Int32;
                total.ParameterName = "@TotalCount";
                total.Direction = ParameterDirection.Output;
                parameters.Add(total);
            }
        }

        private static string ConvertCondition<T>(string aliasTableName, List<QueryProperty> conditions)
        {
            string result = string.Empty;
            string aliasName = string.Empty;

            if (conditions.Count > 0)
            {
                //上一个Group的值
                string oriGroup = string.Empty;
                var builder = new StringBuilder();
                Type targetModelType = typeof (T);
                PropertyInfo[] properties = targetModelType.GetProperties();
                var attributes = new object[0];

                //分组的条件
                var groupConditions = new Dictionary<string, List<QueryProperty>>();

                //为分组的条件
                var noneGroupConditions = new List<QueryProperty>();

                #region 将分组和未分组的条件分离开

                foreach (QueryProperty condition in conditions)
                {
                    //如果有Group，则进入分组逻辑
                    if (!string.IsNullOrEmpty(condition.Group))
                    {
                        if (condition.Group != oriGroup)
                        {
                            var tempProperties = new List<QueryProperty>();
                            tempProperties.AddRange(conditions.Where(m => m.Group == condition.Group));
                            groupConditions.Add(condition.Group, tempProperties);
                            oriGroup = condition.Group;
                        }
                    }
                    else
                    {
                        //添加未分组条件
                        noneGroupConditions.Add(condition);
                    }
                }

                #endregion

                #region 组装未分组条件

                foreach (QueryProperty condition in noneGroupConditions)
                {
                    attributes = new object[0];
                    //找到对应的属性名字，根据conditon设置的PropertyName
                    IEnumerable<PropertyInfo> found = from temp in properties
                        where temp.Name == condition.PropertyName
                        select temp;
                    //属性只能唯一，不会存在重名的属性名
                    if (found.Count() == 1)
                    {
                        attributes = found.FirstOrDefault().GetCustomAttributes(typeof (DataMappingAttribute), true);
                        //属性对应的DataMappingAttribute，拼接字符串
                        if (attributes.Length == 1 && attributes[0] is DataMappingAttribute)
                        {
                            aliasName = string.IsNullOrEmpty(condition.AliaName)
                                ? aliasTableName
                                : condition.AliaName + ".";
                            //添加连接符：AND 或者 OR
                            builder.Append(" " + condition.ConnectType + " ");

                            //单独处理 in 和 not in
                            if (condition.IsQueryString)
                            {
                                builder.Append(aliasName + (attributes[0] as DataMappingAttribute).ColumnName + " " +
                                               condition.Comparesymbol + condition.PropertyValue);
                            }
                            else
                            {
                                builder.Append(aliasName + (attributes[0] as DataMappingAttribute).ColumnName + " " +
                                               condition.Comparesymbol + " @" + condition.ParameterName);
                                //condition.PropertyType = (attributes[0] as DataMappingAttribute).DbType;
                            }
                        }
                    }
                }

                #endregion

                #region 组装分组条件

                foreach (string groupKey in groupConditions.Keys)
                {
                    List<QueryProperty> groupList = groupConditions[groupKey];
                    groupList.Sort();
                    foreach (QueryProperty condition in groupList)
                    {
                        attributes = new object[0];
                        //找到对应的属性名字，根据conditon设置的PropertyName
                        IEnumerable<PropertyInfo> found = from temp in properties
                            where temp.Name == condition.PropertyName
                            select temp;
                        //属性只能唯一，不会存在重名的属性名
                        if (found.Count() == 1)
                        {
                            attributes = found.FirstOrDefault().GetCustomAttributes(typeof (DataMappingAttribute), true);
                            //属性对应的DataMappingAttribute，拼接字符串
                            if (attributes.Length == 1 && attributes[0] is DataMappingAttribute)
                            {
                                aliasName = string.IsNullOrEmpty(condition.AliaName)
                                    ? aliasTableName
                                    : condition.AliaName + ".";
                                //添加连接符：AND 或者 OR
                                builder.Append(" " + condition.ConnectType + " ");

                                //如果是第一个条件，在最前面添加左括号
                                if (condition.IsFirstInGroup)
                                {
                                    builder.Append("(");
                                }

                                //单独处理 in 和 not in
                                if (condition.IsQueryString)
                                {
                                    builder.Append(aliasName + (attributes[0] as DataMappingAttribute).ColumnName + " " +
                                                   condition.Comparesymbol + condition.PropertyValue);
                                }
                                else
                                {
                                    builder.Append(aliasName + (attributes[0] as DataMappingAttribute).ColumnName + " " +
                                                   condition.Comparesymbol + " @" + condition.ParameterName);
                                    //condition.PropertyType = (attributes[0] as DataMappingAttribute).DbType;
                                }

                                //如果是最后一个条件，在最后面添加左括号
                                if (condition.IsLastInGroup)
                                {
                                    builder.Append(")");
                                }
                            }
                        }
                    }
                }

                #endregion

                result = builder.ToString();
            }
            return result;
        }

        private static string ConvertOrderBy(string aliasTableName, List<OrderByProperty> orderByProperties)
        {
            string result = string.Empty;
            if (orderByProperties.Count > 0)
            {
                foreach (OrderByProperty orderByProperty in orderByProperties)
                {
                    string tempName = string.IsNullOrEmpty(orderByProperty.AliaName)
                        ? aliasTableName
                        : orderByProperty.AliaName + ".";
                    result += tempName + orderByProperty.OrderProperty + " " + orderByProperty.OrderbyType + ",";
                }

                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }

    #region 实体

    public class PageInfo
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }
    }

    public class QueryResult<T> where T : class
    {
        public QueryResult()
        {
            Page = new PageInfo();
            Result = new List<T>();
        }

        public PageInfo Page { get; set; }

        public IEnumerable<T> Result { get; set; }
    }

    /// <summary>
    ///     定义查询条件的参数的值与预定义的满足需求值的比较
    /// </summary>
    public class QueryProperty : IComparable<QueryProperty>
    {
        //PropertyValue
        //PropertyName 
        //Comparesymbol

        private ConnectTypes connectType = ConnectTypes.And;
        private string m_ParameterName;

        /// <summary>
        ///     属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        ///     属性类型
        /// </summary>
        public DbType PropertyType { get; set; }

        /// <summary>
        ///     用于比较的属性值
        /// </summary>
        public object PropertyValue { get; set; }

        /// <summary>
        ///     字段所属表在sql中的别名
        /// </summary>
        public string AliaName { get; set; }

        /// <summary>
        ///     比较符号：小于、大于、等于、不等于等等比较符
        /// </summary>
        public string Comparesymbol { get; set; }

        /// <summary>
        ///     参数名
        /// </summary>
        public string ParameterName
        {
            get
            {
                if (string.IsNullOrEmpty(m_ParameterName))
                {
                    return PropertyName;
                }
                return m_ParameterName;
            }
            set { m_ParameterName = value; }
        }

        /// <summary>
        ///     是否是查询字符串（如果为true，只拼sql，不设置参数）
        /// </summary>
        public bool IsQueryString { get; set; }

        /// <summary>
        ///     分组，用于合并条件组，将同组的条件使用（）放在一起，用以适应 or 的情况
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///     是否是分组的第一个，不可与IsLastInGroup同时为true
        /// </summary>
        public bool IsFirstInGroup { get; set; }

        /// <summary>
        ///     是否是分组的最后一个，不可与IsFirstInGroup同时为true
        /// </summary>
        public bool IsLastInGroup { get; set; }

        /// <summary>
        ///     条件连接符，默认为AND，注意：如果是分组条件的第一个，该值默认为AND
        /// </summary>
        public ConnectTypes ConnectType
        {
            get { return connectType; }
            set { connectType = value; }
        }


        public int CompareTo(QueryProperty other)
        {
            if (other == null)
            {
                return 0;
            }

            if (other.IsFirstInGroup)
            {
                return 1;
            }

            if (other.IsLastInGroup)
            {
                return -1;
            }

            return 0;
        }
    }

    /// <summary>
    ///     定义排序，可多个
    /// </summary>
    public class OrderByProperty
    {
        public string OrderProperty { get; set; }

        public OrderByTypes OrderbyType { get; set; }

        /// <summary>
        ///     排序字段别名
        /// </summary>
        public string AliaName { get; set; }
    }

    /// <summary>
    ///     升序还是降序
    /// </summary>
    public enum OrderByTypes
    {
        /// <summary>
        ///     升序
        /// </summary>
        Asc = 0,

        /// <summary>
        ///     降序
        /// </summary>
        Desc = 2
    }

    public enum ConnectTypes
    {
        /// <summary>
        ///     AND连接条件
        /// </summary>
        And = 0,

        /// <summary>
        ///     OR连接条件
        /// </summary>
        Or = 1
    }

    /// <summary>
    ///     包含分页信息的实体
    /// </summary>
    public class QueryCondition
    {
        public QueryCondition()
        {
            Conditions = new List<QueryProperty>();
            OrderBys = new List<OrderByProperty>();
            PageInfo = new PageInfo();
        }

        public PageInfo PageInfo { get; set; }

        public List<QueryProperty> Conditions { get; set; }

        public List<OrderByProperty> OrderBys { get; set; }

        /// <summary>
        ///     表别名。用于组合查询时使用
        /// </summary>
        public string AliaseTableName { get; set; }
    }

    #endregion
}