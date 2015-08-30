// 源文件头信息：
// 文 件 名：BaseEntity.cs
// 类    名：BaseEntity
// 所属工程：Component.Data
// 最后修改：游凯
// 最后修改：2013-09-10 05:38:01

using System.ComponentModel.DataAnnotations;

namespace Component.Data
{
    /// <summary>
    ///     领域模型实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityBase<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }
    }
}