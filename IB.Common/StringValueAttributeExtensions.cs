using System;
using System.Reflection;

namespace IB.Common
{
    public static class StringValueAttributeExtensions
    {
        public static string GetStringValue<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(StringValueAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((StringValueAttribute)attrs[0]).Value;
                }
            }

            return enumerationValue.ToString();
        }
    }
}
