using System.Text.RegularExpressions;

namespace Waes.Diff.Core.Extensions
{
    /// <summary>
    /// Extensions for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Check if the given value is base64
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>true if it is a base64 string</returns>
        public static bool IsBase64String(this string value)
        {
            value = value.Trim();
            return (value.Length % 4 == 0) && Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}
