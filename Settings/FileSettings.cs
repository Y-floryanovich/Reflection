using ConfigurationProvider.Enum;
using Reflection.Attribute;
using System;

namespace Reflection.Settings
{
    public class FileSettings : ConfigurationComponentBase
    {
        public FileSettings(IProviders providersFactory) : base(providersFactory)
        {
        }

        public FileSettings(string name, TimeSpan? duration, int? number, float? floatNumber, IProviders providers)
            : base(providers)
        {
            Name = name;
            Duration = duration;
            Number = number;
            FloatNumber = floatNumber;
        }

        [ConfigurationItem("Name", ProviderType.File)]
        public string Name { get; set; }

        [ConfigurationItem("Duration", ProviderType.File)]
        public TimeSpan? Duration { get; set; }

        [ConfigurationItem("Number", ProviderType.File)]
        public int? Number { get; set; }

        [ConfigurationItem("FloatNumber", ProviderType.File)]
        public float? FloatNumber { get; set; }
    }
}
