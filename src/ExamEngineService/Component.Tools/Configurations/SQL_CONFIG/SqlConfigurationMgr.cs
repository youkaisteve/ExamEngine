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
        private static readonly object CmgrLocker = new object();
        private static SqlConfigurationMgr _instanse;

        private SqlConfigurationMgr()
        {
        }

        protected override string ConfigPath
        {
            get { return "SQL_CONFIG"; }
        }

        public static SqlConfigurationMgr Instanse
        {
            get
            {
                lock (CmgrLocker)
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
            var directoryInfo = new DirectoryInfo(Path.Combine(BaseFolder, ConfigPath));
            FileInfo[] files = directoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            if (files.Length <= 0)
            {
                return;
            }

            _Configurations = new List<Command>();

            foreach (FileInfo fileInfo in files)
            {
                var fs = new FileStream(fileInfo.FullName, FileMode.Open);
                var sc = (SqlConfiguration) serializer.Deserialize(fs);

                foreach (Command item in sc.Commands)
                {
                    _Configurations.Add(item);
                }
                fs.Dispose();
            }
        }

        #region 接口实现

        public string GetCommandTextByKey(string key)
        {
            Command config = _Configurations.FirstOrDefault(m => m.Name == key);
            if (config == null)
            {
                throw new BusinessException(string.Format("key:{0} is not found in SQL_CONFIG", key));
            }
            return config.CommandText;
        }

        #endregion
    }
}