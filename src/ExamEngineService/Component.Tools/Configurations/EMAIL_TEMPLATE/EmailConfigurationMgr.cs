﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Component.Tools.Configurations.EMAIL_TEMPLATE
{
    public class EmailConfigurationMgr : ConfigurationBase<Email>, IEmailConfiguration
    {
        private static readonly object emailMgrLocker = new object();
        private static EmailConfigurationMgr _instanse;

        private EmailConfigurationMgr()
        {
        }

        protected override string ConfigPath
        {
            get { return "MAIL_TEMPLATE"; }
        }

        public static EmailConfigurationMgr Instanse
        {
            get
            {
                lock (emailMgrLocker)
                {
                    if (_instanse == null)
                    {
                        _instanse = new EmailConfigurationMgr();
                    }
                    return _instanse;
                }
            }
        }

        protected override List<Email> Configurations { get; set; }

        public Email GetMail(string mailType)
        {
            return Configurations.FirstOrDefault(m => m.Type == mailType);
        }

        protected override void Init()
        {
            var serializer = new XmlSerializer(typeof (MailTemplate));
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) ==
                                Environment.CurrentDirectory.TrimEnd(new[] {'\\'})
                ? AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) + "\\" + BasePath
                : AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] {'\\'}) + "\\bin\\" + BasePath;
            var directoryInfo = new DirectoryInfo(Path.Combine(baseFolder, ConfigPath));
            FileInfo[] files = directoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            if (files.Length <= 0)
            {
                return;
            }

            Configurations = new List<Email>();


            foreach (FileInfo fileInfo in files)
            {
                using (var fs = new FileStream(fileInfo.FullName, FileMode.Open))
                {
                    var sc = (MailTemplate) serializer.Deserialize(fs);

                    foreach (Email item in sc.Emails)
                    {
                        Configurations.Add(item);
                    }
                }
            }
        }
    }
}