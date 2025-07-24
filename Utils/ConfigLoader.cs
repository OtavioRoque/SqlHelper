using System.Configuration;
using System.IO;

namespace SqlHelper.Utils
{
    public static class ConfigLoader
    {
        public static string? ObterConnectionString(string nome)
        {
            string localConfigPath = "App.Local.config";

            if (File.Exists(localConfigPath))
            {
                var map = new ExeConfigurationFileMap { ExeConfigFilename = localConfigPath };
                var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                var connectionString = config.ConnectionStrings.ConnectionStrings[nome];

                if (connectionString != null)
                    return connectionString.ConnectionString;
            }

            return ConfigurationManager.ConnectionStrings[nome]?.ConnectionString;
        }
    }
}
