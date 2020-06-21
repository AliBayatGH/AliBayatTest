using AliBayatTest.Domain;
using CSharpFunctionalExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace AliBayatTest.API.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var email = value as string;
            if (email == null)
                return new ValidationResult("Email is invalid!");

            Result<Email> emailResult = Email.Create(email);

            if (emailResult.IsFailure)
                return new ValidationResult(emailResult.Error);

            return ValidationResult.Success;
        }
    }
}