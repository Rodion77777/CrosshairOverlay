using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosshairOverlay
{
    internal class ConfigManager
    {
        private readonly string configDirectory;
        private const string localConfig = "config.json";

        public ConfigManager()
        {
            configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CrosshairOverlay", "Configs");

            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }
        }

        public string GetConfigDirectory() => configDirectory;

        public void SaveConfig(CrosshairConfig config)
        {
            try
            {
                File.WriteAllText(localConfig, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            catch
            {
                System.Windows.MessageBox.Show("Error when saving the configuration", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public void SaveConfig(CrosshairConfig config, string fileName)
        {
            string fullPath = Path.Combine(configDirectory, fileName + ".json");
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(fullPath, json);
        }

        public CrosshairConfig LoadConfig()
        {
            try
            {
                string json = File.ReadAllText(localConfig);
                return JsonConvert.DeserializeObject<CrosshairConfig>(json) ?? new CrosshairConfig();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error when loading the configuration: " + ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return new CrosshairConfig();
        }

        public CrosshairConfig LoadConfig(string fileName)
        {
            string fullPath = Path.Combine(configDirectory, fileName + ".json");
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Файл не найден", fullPath);

            string json = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<CrosshairConfig>(json);
        }

        public string[] ListConfigs()
        {
            return Directory.GetFiles(configDirectory, "*.json")
                            .Select(Path.GetFileNameWithoutExtension)
                            .ToArray();
        }

        public bool DeleteConfig(string fileName)
        {
            string fullPath = Path.Combine(configDirectory, fileName + ".json");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true; // Успешное удаление
            }

            return false; // Файл не найден
        }
    }
}
