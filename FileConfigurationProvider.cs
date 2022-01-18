using Reflection.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    internal class FileConfigurationProvider : IConfigurationProvider
    {
        public string FilePath { get; set; }

        public ProviderType ProviderType => ProviderType.File;

        public object LoadSettings(PropertyInfo info, string settingName)
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                using (StreamReader textReader = new StreamReader(fs))
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        string key = line.Trim();
                        string value = textReader.ReadLine();
                        data.Add(key, value);
                    }
                }
            }
                    foreach (var item in data)
                    {
                        if (settingName.Contains(item.Key))
                        {
                             return TryToParse(info, item.Value);
                        }
                    }

            return null;
        }

        public void SaveSettings(string key, object value)
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                using (StreamReader textReader = new StreamReader(fs))
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        string name = line.Trim();
                        string val = textReader.ReadLine();
                        data.Add(name, val);
                    }
                }
            }
            foreach (var item in data)
            {
                if (key.Contains(item.Key))
                {
                    data[item.Key] = value.ToString();
                }
            }
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                using (StreamWriter textWriter = new StreamWriter(fs))
                {
                    foreach (var item in data)
                    {
                        textWriter.WriteLine(item.Key);
                        textWriter.WriteLine(item.Value);
                    }
                }
            }

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
