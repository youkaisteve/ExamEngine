// 源文件头信息：
// 文 件 名：Extensions.cs
// 类    名：Extensions
// 所属工程：Component.Data
// 最后修改：游凯
// 最后修改：2013-09-18 09:38:26

using System;
using System.ComponentModel;
using System.Reflection;

namespace Component.Data.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     获取枚举对象的Description特性的描述信息
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string GetDescription(this object obj)
        {
            return GetDescription(obj, false);
        }

        /// <summary>
        ///     获取枚举对象的Description特性的描述信息
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="isTop">是否是对类的描述</param>
        /// <returns></returns>
        public static string GetDescription(this object obj, bool isTop)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                Type _type = obj.GetType();

                DescriptionAttribute descriptionAttribute;
                if (isTop)
                {
                    descriptionAttribute =
                        (DescriptionAttribute) Attribute.GetCustomAttribute(_type, typeof (DescriptionAttribute));
                }
                else
                {
                    FieldInfo fieldInfo = _type.GetField(Enum.GetName(_type, obj));
                    descriptionAttribute =
                        (DescriptionAttribute)
                            Attribute.GetCustomAttribute(fieldInfo, typeof (DescriptionAttribute));
                }
                if (descriptionAttribute != null && !string.IsNullOrEmpty(descriptionAttribute.Description))
                {
                    return descriptionAttribute.Description;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return obj.ToString();
        }
    }
}