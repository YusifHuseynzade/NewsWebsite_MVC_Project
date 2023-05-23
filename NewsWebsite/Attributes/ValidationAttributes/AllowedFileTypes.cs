using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Attributes.ValidationAttributes
{
    public class AllowedFileTypes:ValidationAttribute
    {
        string[] _fileTypes;
        public AllowedFileTypes(params string[] fileTypes)
        {
            _fileTypes = fileTypes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as IFormFile;
            if(files != null)
            {
                if (!_fileTypes.Contains(files.ContentType))
                    return new ValidationResult("File Content type must be one of these types: " + String.Join(", ", _fileTypes));
            }

            return ValidationResult.Success;
        }

    }
}
