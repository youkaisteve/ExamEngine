// 源文件头信息：
// 文 件 名：DataMappingAttribute.cs
// 类    名：DataMappingAttribute
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-10-14 02:06:25

using System;
using System.Data;

namespace Component.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataMappingAttribute : Attribute
    {
        private readonly string columnName;
        private readonly DbType dbType;

        public DataMappingAttribute(string columnName, DbType dbType)
        {
            this.columnName = columnName;
            this.dbType = dbType;
        }

        public string ColumnName
        {
            get { return columnName; }
        }

        public DbType DbType
        {
            get { return dbType; }
        }
    }
}