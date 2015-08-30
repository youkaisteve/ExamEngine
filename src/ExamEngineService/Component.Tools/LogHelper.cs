// 源文件头信息：
// 文 件 名：LogHelper.cs
// 类    名：LogHelper
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2014-04-22 04:30:27

using System;
using log4net;
using Component.Tools.Exceptions;

namespace Component.Tools
{
    public class LogHelper
    {
        private static readonly object logLocker = new object();
        private static LogHelper _instanse;

        private ILog _logger;

        public static LogHelper Instanse
        {
            get
            {
                lock (logLocker)
                {
                    if (_instanse == null)
                    {
                        _instanse = new LogHelper();
                    }
                    return _instanse;
                }
            }
        }

        private LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            string loggerConfig = PublicFunc.GetConfigByKey_AppSettings("Logger");
            if (string.IsNullOrEmpty(loggerConfig))
            {
                throw new ConfigurationException("请在appSetting中配置 Logger节点，指定log4net对应的logger名称");
            }
            _logger = LogManager.GetLogger(loggerConfig);
        }

        public void WriteError(string message, Exception ex)
        {
            _logger.Error(message, ex);
        }

        public void WriteError(string message)
        {
            _logger.Error(message);
        }

        public void WriteInfo(string message)
        {
            _logger.Info(message);
        }

        public void WriteWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}