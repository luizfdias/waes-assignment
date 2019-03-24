using System.ComponentModel.DataAnnotations;

namespace Waes.Assignment.Application.Attributes
{
    public class ContentRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var content = value as byte[];

            return content != null && content.Length > 0 ? ValidationResult.Success : new ValidationResult("Content must not be null or empty.");
        }
    }
}
