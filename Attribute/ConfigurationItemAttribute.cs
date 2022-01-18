using ConfigurationProvider.Enum;
using System;

namespace Reflection.Attribute
{

        [AttributeUsage(AttributeTargets.Property)]
        internal class ConfigurationItemAttribute : System.Attribute
    {
            public ConfigurationItemAttribute(string settingName, ProviderType providerType)
            {
                SettingName = settingName;
                ProviderType = providerType;
            }

            public string SettingName { get; }

            public ProviderType ProviderType { get; }
        }
}
