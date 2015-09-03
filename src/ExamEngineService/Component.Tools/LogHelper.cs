using System;
using Component.Tools.Exceptions;
using log4net;
using log4net.Config;

namespace Component.Tools
{
    public class LogHelper
    {
        private static readonly object logLocker = new object();
        private static LogHelper _instanse;

        private readonly ILog _logger;

        private LogHelper()
        {
            XmlConfigurator.Configure();
            string loggerConfig = PublicFunc.GetConfigByKey_AppSettings("Logger");
            if (string.IsNullOrEmpty(loggerConfig))
            {
                throw new ConfigurationException("请在appSetting中配置 Logger节点，指定log4net对应的logger名称");
            }
            _logger = LogManager.GetLogger(loggerConfig);
        }

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