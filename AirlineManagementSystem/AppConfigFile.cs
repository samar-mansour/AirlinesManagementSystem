using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class AppConfigFile
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_file_name;

        private static readonly AppConfigFile Instance = new AppConfigFile();

        private JObject m_configRoot;

        public string ConnectionString { get; set; }
        public string AppName { get; set; }
        public float Version { get; set; }

        public static AppConfigFile GetInstance()
        {
            return Instance;
        }

        private AppConfigFile()
        {
            Init();
        }

        internal void Init(string file_name = "AppConfigFile.Config.json")
        {
            m_file_name = file_name;

            if (!File.Exists(m_file_name))
            {
                my_logger.Error($"File {m_file_name} not exist!");
                Console.WriteLine($"File {m_file_name} not exist!");
                Environment.Exit(-1);
            }

            var reader = File.OpenText(m_file_name);
            string json_string = reader.ReadToEnd();

            JObject jo = (JObject)JsonConvert.DeserializeObject(json_string);
            m_configRoot = (JObject)jo["CoreConfig"];
            ConnectionString = m_configRoot["ConnectionString"].Value<string>();
            AppName = m_configRoot["AppName"].Value<string>();
            Version = m_configRoot["Version"].Value<float>();

        }

    }
}
