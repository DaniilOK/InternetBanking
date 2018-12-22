using System;

namespace IB.Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }
    }
}
