﻿using Reflection.Attribute;
using Reflection.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class ConfigurationComponentBase
    {
        private readonly IProviders _providersFactory;

        protected ConfigurationComponentBase(IProviders providersFactory)
        {
            _providersFactory = providersFactory;
        }

        protected void LoadSettings()
        {
            
            var type = GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attr = property.GetCustomAttribute<ConfigurationItemAttribute>();
                var provider = _providersFactory.GetProvider(attr.ProviderType);
                if (provider != null)
                { 
                    property.SetValue(this, provider.LoadSettings(property, attr.SettingName));
                }
            }
        }


        protected void SaveSetting()
        {
            var type = GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attr = property.GetCustomAttribute<ConfigurationItemAttribute>();
                var provider = _providersFactory.GetProvider(attr.ProviderType);
                var value = property.GetValue(this);
                if (provider != null)
                {
                    provider.SaveSettings(attr.SettingName, value);
                }
            }
        }

    }
}
