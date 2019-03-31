using System;
using System.Linq;
using System.Reflection;
using Waes.Assignment.Common.Attributes;

namespace Waes.Assignment.Common.Extensions
{
    public static class EnumExtensions
    {
        public static (string Code, string Message) GetDescription(this Enum value)
        {
            var attribute = value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<CodeMessageAttribute>();

            return attribute == null ? (null, null) : (attribute.Code, attribute.Description);
        }   
    }
}
