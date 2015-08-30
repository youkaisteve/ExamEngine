// 源文件头信息：
// 文 件 名：SortCondition.cs
// 类    名：SortCondition
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-30 01:13:34

using System.ComponentModel;

namespace Component.Tools
{
    /// <summary>
    ///     属性排序条件信息类
    /// </summary>
    public class PropertySortCondition
    {
        /// <summary>
        ///     构造一个指定属性名称的升序排序的排序条件
        /// </summary>
        /// <param name="propertyName">排序属性名称</param>
        public PropertySortCondition(string propertyName)
            : this(propertyName, ListSortDirection.Ascending)
        {
        }

        /// <summary>
        ///     构造一个排序属性名称和排序方式的排序条件
        /// </summary>
        /// <param name="propertyName">排序属性名称</param>
        /// <param name="listSortDirection">排序方式</param>
        public PropertySortCondition(string propertyName, ListSortDirection listSortDirection)
        {
            PropertyName = propertyName;
            ListSortDirection = listSortDirection;
        }

        /// <summary>
        ///     获取或设置 排序属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        ///     获取或设置 排序方向
        /// </summary>
        public ListSortDirection ListSortDirection { get; set; }
    }
}