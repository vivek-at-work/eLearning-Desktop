using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
namespace Coneixement.Infrastructure.Helpers.Configuration
{
   public class ConeixementConfigurations
    {
        string ParentDirectoryPath
        {
            get;
            set;
        }
        string ConfigurationsPath
        {
            get;
            set;
        }
        XElement xelement 
        {
            get;set;
        }
        IEnumerable<XElement> Settings
        {
            get;
            set;
        }
        private  ConeixementConfigurations()
        {
            ParentDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            ConfigurationsPath = Path.Combine(ParentDirectoryPath, "Settings", "Configurations.cnx");
            xelement = XElement.Load(ConfigurationsPath);
            Settings = xelement.Elements();
        }
        static ConeixementConfigurations _instance;
        public static ConeixementConfigurations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConeixementConfigurations();
                }
                return _instance;
            }
        set{
            _instance = value;
        }
        }
        public string ApplicationName
        {
            get
            {
                return (Settings.FirstOrDefault() as XElement).Element("ApplicationName").Value;
            }
        }
        public string ApplicationVersion
        {
            get
            {
                return (Settings.FirstOrDefault() as XElement).Element("ApplicationVersion").Value;
            }
        }
    }
}
