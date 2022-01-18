using Reflection.Attribute;
using Reflection.Enum;
using System;
using System.Configuration;
using System.Reflection;

namespace Reflection
{
    internal class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public ProviderType ProviderType => ProviderType.ConfigurationManager;

        public string FilePath { get; set; }

        public object LoadSettings(PropertyInfo info, string settingName)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            var type = GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                    foreach (var item in settings.AllKeys)
                    {
                        if (settingName.Contains(item))
                        {
                            return TryToParse(info, item);
                        }
                    }
            }
            return null;
        }

        public void SaveSettings(string key, object value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if (settings[key] == null)
            {
                settings.Add(key, value.ToString());
            }
            else
            {
                settings[key].Value = value.ToString();
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        public object TryToParse(PropertyInfo info, string value)
        {
            var propertyType = info.PropertyType;
            switch (propertyType.Name)
            {
                case "Int32":
                    return int.Parse(value);
                case "Single":
                    return float.Parse(value);
                case "Timespan":
                    return TimeSpan.Parse(value);
                case "String":
                    return value;
                default:
                    Console.WriteLine("Unknown type.");
                    break;
            }
            return null;
        }
    }
}
