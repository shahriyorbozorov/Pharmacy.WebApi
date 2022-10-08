using Pharmacy.WebApi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.WebApi.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as FormFile;
            if (file is not null)
            {
                if (FileSizeHelper.ByteToMb(file.Length) > _maxFileSize)
                {
                    return new ValidationResult($"Image must be less than {_maxFileSize} Mb");
                }
                else
                    return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The file cannot be null!");
            }
        }
    }
}
