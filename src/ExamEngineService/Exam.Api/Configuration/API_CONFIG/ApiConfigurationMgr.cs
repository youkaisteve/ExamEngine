using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Component.Tools.Configurations;
using Newtonsoft.Json;

namespace Exam.Api.Configuration.API_CONFIG
{
    public class ApiConfigurationMgr : ConfigurationBase<ApiConfiguration>, IApiConfiguration
    {
        private static readonly object apiConfigLocker = new object();
        private static ApiConfigurationMgr _instanse;

        protected override string ConfigPath
        {
            get { return "API_CONFIG"; }
        }

        protected override List<ApiConfiguration> Configurations { get; set; }

        public static ApiConfigurationMgr Instanse
        {
            get
            {
                lock (apiConfigLocker)
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
            return Configurations.Any(m => m.Name == apiName);
        }

        protected override void Init()
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) ==
                                Environment.CurrentDirectory.TrimEnd(new[] {'\\'})
                ? AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) + "\\" + BasePath
                : AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) + "\\bin\\" + BasePath;
            var directoryInfo = new DirectoryInfo(Path.Combine(baseFolder, ConfigPath));
            FileInfo[] files = directoryInfo.GetFiles("*.json", SearchOption.TopDirectoryOnly);
            if (files.Length <= 0)
            {
                return;
            }

            Configurations = JsonConvert.DeserializeObject<List<ApiConfiguration>>(
                File.ReadAllText(files[0].FullName));
        }
    }
}