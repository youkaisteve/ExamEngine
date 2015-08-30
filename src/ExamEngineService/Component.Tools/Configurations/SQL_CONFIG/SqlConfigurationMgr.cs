// 源文件头信息：
// 文 件 名：ConfigurationMgr.cs
// 类    名：ConfigurationMgr
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-29 02:09:34

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Component.Tools.Exceptions;

namespace Component.Tools.Configurations
{
    /// <summary>
    ///     配置管理类
    /// </summary>
    //[Export(typeof(ISqlConfiguration))]
    //[PartCreationPolicy(CreationPolicy.Shared)]
    public class SqlConfigurationMgr : ConfigurationBase<Command>, ISqlConfiguration
    {
        private static readonly object cmgrLocker = new object();
        private static SqlConfigurationMgr _instanse;

        private SqlConfigurationMgr()
        {
        }

        protected override string ConfigPath
        {
            get { return "SQL_CONFIG"; }
        }

        protected override List<Command> Configurations { get; set; }

        public static SqlConfigurationMgr Instanse
        {
            get
            {
                lock (cmgrLocker)
                {
                    if (_instanse == null)
                    {
                        _instanse = new SqlConfigurationMgr();
                    }
                    return _instanse;
                }
            }
        }

        /// <summary>
        ///     初始化配置
        /// </summary>
        protected override void Init()
        {
            var serializer = new XmlSerializer(typeof (SqlConfiguration));
            string baseFolder;
            baseFolder = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) ==
                         Environment.CurrentDirectory.TrimEnd(new[] {'\\'})
                ? AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) + "\\" + BasePath
                : AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) + "\\bin\\" + BasePath;
            var directoryInfo = new DirectoryInfo(Path.Combine(baseFolder, ConfigPath));
            FileInfo[] files = directoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            if (files.Length <= 0)
            {
                return;
            }

            Configurations = new List<Command>();

            foreach (FileInfo fileInfo in files)
            {
                var fs = new FileStream(fileInfo.FullName, FileMode.Open);
                var sc = (SqlConfiguration) serializer.Deserialize(fs);

                foreach (Command item in sc.Commands)
                {
                    Configurations.Add(item);
                }
                fs.Dispose();
            }
        }

        #region 接口实现

        public Command GetByKey(string key)
        {
            return Configurations.FirstOrDefault(m => m.Name == key);
        }

        public List<Command> GetAll()
        {
            return Configurations;
        }

        public string GetCommandTextByKey(string key)
        {
            Command config = Configurations.FirstOrDefault(m => m.Name == key);
            if (config == null)
            {
                throw new BusinessException(string.Format("key:{0} is not found in SQL_CONFIG", key));
            }
            return config.CommandText;
        }

        #endregion
    }
}