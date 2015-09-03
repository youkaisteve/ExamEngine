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