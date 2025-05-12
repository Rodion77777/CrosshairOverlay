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

        public ConfigManager()
        {
            configDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CrosshairOverlay", "Configs");

            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }
        }

        public string GetConfigDirectory() => configDirectory;

        public void SaveConfig(CrosshairConfig config, string fileName)
        {
            string fullPath = Path.Combine(configDirectory, fileName + ".json");
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(fullPath, json);
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
    }
}
