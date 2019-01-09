using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CAS.myUtilities
{
    public sealed class myConfig
    {
        private static volatile myConfig instance;
        private static object syncRoot = new object();

        string configFile = System.Reflection.Assembly.GetExecutingAssembly().Location;

        //Constructor
        public myConfig() { }

        //Properties

        //Methods
        public static myConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new myConfig();
                            
                        }
                    }
                }
                return instance;
            }
        }

        public string getAppSetting(string key)
        {
            string value;
            //Laden der AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(configFile);
            //Zurückgeben der dem Key zugehörigen Value
            try
            {
                value = config.AppSettings.Settings[key].Value;
            }
            catch
            {
                value = String.Empty;
            }

            return value;
        }

        public void setAppSetting(string key, string value)
        {
            //Laden der AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(configFile);
            //Überprüfen ob Key existiert
            if (config.AppSettings.Settings[key] != null)
            {
                //Key existiert. Löschen des Keys zum "überschreiben"
                config.AppSettings.Settings.Remove(key);
            }
            //Anlegen eines neuen KeyValue-Paars
            config.AppSettings.Settings.Add(key, value);
            //Speichern der aktualisierten AppSettings
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
