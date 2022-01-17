using System;

namespace Reflection.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TimeSpanFormatAttribute : System.Attribute
    {
        public TimeSpanFormatAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; }

        public string[] AlternativeFormats { get; set; }
    }
}
