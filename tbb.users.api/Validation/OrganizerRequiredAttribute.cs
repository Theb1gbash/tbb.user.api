using System.ComponentModel.DataAnnotations;

public class OrganizerRequiredAttribute : ValidationAttribute
{
    private readonly string _userTypePropertyName;
    private readonly string _userType;

    public OrganizerRequiredAttribute(string userTypePropertyName, string userType)
    {
        _userTypePropertyName = userTypePropertyName;
        _userType = userType;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var userTypeProperty = validationContext.ObjectType.GetProperty(_userTypePropertyName);
        if (userTypeProperty == null)
        {
            return new ValidationResult($"Unknown property: {_userTypePropertyName}");
        }

        var userType = userTypeProperty.GetValue(validationContext.ObjectInstance)?.ToString();
        if (userType == _userType && string.IsNullOrEmpty(value?.ToString()))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
