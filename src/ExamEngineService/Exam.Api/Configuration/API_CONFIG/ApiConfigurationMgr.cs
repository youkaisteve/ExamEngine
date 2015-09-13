using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Component.Tools;
using Component.Tools.Configurations;
using Newtonsoft.Json;

namespace Exam.Api.Configuration.API_CONFIG
{
    public class ApiConfigurationMgr : ConfigurationBase<ApiConfiguration>, IApiConfiguration
    {
        private static readonly object ApiConfigLocker = new object();
        private static ApiConfigurationMgr _instanse;

        protected override string ConfigPath
        {
            get { return "API_CONFIG"; }
        }

        public static ApiConfigurationMgr Instanse
        {
            get
            {
                lock (ApiConfigLocker)
                {
                    if (_instanse == null)
                    {
                        _instanse = new ApiConfigurationMgr();
                    }
                    return _instanse;
                }
            }
        }

        public bool HasApi(string apiName)
        {
            return _Configurations.Any(m => m.Name == apiName);
        }

        protected override void Init()
        {
            var directoryInfo = new DirectoryInfo(Path.Combine(BaseFolder, ConfigPath));
            FileInfo[] files = directoryInfo.GetFiles("*.json", SearchOption.TopDirectoryOnly);
            if (files.Length <= 0)
            {
                return;
            }

            _Configurations = JsonConvert.DeserializeObject<List<ApiConfiguration>>(
                File.ReadAllText(files[0].FullName));
        }
    }
}