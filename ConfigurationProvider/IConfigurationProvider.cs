using ConfigurationProvider.Enum;
using System.Reflection;

namespace ConfigurationProvider
{
    public interface IConfigurationProvider
    {
       ProviderType ProviderType { get; }

       string FilePath { get; set; }

       object LoadSettings(PropertyInfo info, string settingName);

       void SaveSettings(string key, object value);
    }
}
