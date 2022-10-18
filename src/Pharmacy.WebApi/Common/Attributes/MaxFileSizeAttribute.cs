using Pharmacy.WebApi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.WebApi.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly bool _isFileNullable;

        public MaxFileSizeAttribute(int maxFileSize, bool isFileNullable = false)
        {
            _maxFileSize = maxFileSize;
            _isFileNullable = isFileNullable;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (value is null && _isFileNullable)
                return ValidationResult.Success;

            if (value is null) return new ValidationResult("Value cannot be null");

            var file = (IFormFile)value;

            return FileSizeHelper.ByteToMb(file.Length) > _maxFileSize
                ? new ValidationResult($"Image must be less than {_maxFileSize} MB")
                : ValidationResult.Success;
        }
    }
}
