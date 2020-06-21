using AliBayatTest.Domain;
using CSharpFunctionalExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AliBayatTest.API.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var phoneNumber = value as string;
            if (phoneNumber == null)
                return new ValidationResult("PhoneNumber is invalid!");

            Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(phoneNumber);

            if (phoneNumberResult.IsFailure)
                return new ValidationResult(phoneNumberResult.Error);

            return ValidationResult.Success;
        }
    }
}