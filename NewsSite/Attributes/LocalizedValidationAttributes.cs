using Microsoft.Extensions.Localization;
using NewsSite.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsSite.Attributes
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        private readonly string _fieldKey;

        public LocalizedRequiredAttribute(string fieldKey)
        {
            _fieldKey = fieldKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            var localizer = (IStringLocalizer)validationContext.GetService(typeof(IStringLocalizer<News>));
            var fieldName = localizer[_fieldKey];
            var errorMessage = localizer["RequiredAttribute_ValidationError", fieldName];

            return new ValidationResult(errorMessage);
        }
    }

    public class LocalizedStringLengthAttribute : StringLengthAttribute
    {
        private readonly string _fieldKey;

        public LocalizedStringLengthAttribute(int maximumLength, string fieldKey) : base(maximumLength)
        {
            _fieldKey = fieldKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            if (value.ToString().Length <= MaximumLength) return ValidationResult.Success;

            var localizer = (IStringLocalizer)validationContext.GetService(typeof(IStringLocalizer<News>));
            var fieldName = localizer[_fieldKey];
            var errorMessage = localizer["StringLengthAttribute_ValidationError", fieldName, MaximumLength];

            return new ValidationResult(errorMessage);
        }
    }

    public class LocalizedMinLengthAttribute : MinLengthAttribute
    {
        private readonly string _fieldKey;

        public LocalizedMinLengthAttribute(int length, string fieldKey) : base(length)
        {
            _fieldKey = fieldKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            if (value.ToString().Length >= Length) return ValidationResult.Success;

            var localizer = (IStringLocalizer)validationContext.GetService(typeof(IStringLocalizer<News>));
            var fieldName = localizer[_fieldKey];
            var errorMessage = localizer["MinLengthAttribute_ValidationError", fieldName, Length];

            return new ValidationResult(errorMessage);
        }
    }
}
