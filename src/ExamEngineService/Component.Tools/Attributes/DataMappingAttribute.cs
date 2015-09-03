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