using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    class AppConfigFile
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_file_name;

        internal static readonly AppConfigFile Instance = new AppConfigFile();

        public string ConnectionString { get; set; }
        public string AppName { get; set; }
        public float Version { get; set; }

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
        }

    }
}
