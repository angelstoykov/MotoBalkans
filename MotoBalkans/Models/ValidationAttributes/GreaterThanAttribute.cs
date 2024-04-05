using System;
using System.ComponentModel.DataAnnotations;

public class GreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public GreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (propertyInfo == null)
        {
            return new ValidationResult($"Unknown property: {_comparisonProperty}");
        }

        var comparisonValue = propertyInfo.GetValue(validationContext.ObjectInstance) as IComparable;
        var currentValue = value as IComparable;

        if (currentValue != null && comparisonValue != null && currentValue.CompareTo(comparisonValue) <= 0)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
