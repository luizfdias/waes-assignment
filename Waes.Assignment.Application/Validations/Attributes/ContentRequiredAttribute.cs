using System.ComponentModel.DataAnnotations;

namespace Waes.Assignment.Application.Validations.Attributes
{
    /// <summary>
    /// Validation attribute to validates the content of the payload
    /// </summary>
    public class ContentRequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// It validates the bytes content of the payload
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>True if content is not null or empty</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var content = value as byte[];

            return content != null && content.Length > 0 ? ValidationResult.Success : new ValidationResult("Content must not be null or empty.");
        }
    }
}
