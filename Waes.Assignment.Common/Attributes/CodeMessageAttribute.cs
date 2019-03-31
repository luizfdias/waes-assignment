using System.ComponentModel;

namespace Waes.Assignment.Common.Attributes
{
    public class CodeMessageAttribute : DescriptionAttribute
    {
        public string Code { get; }

        public CodeMessageAttribute(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
