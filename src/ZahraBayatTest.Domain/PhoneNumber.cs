using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AliBayatTest.Domain
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var number = phoneNumberUtil.Parse(phoneNumber, null);

            if (!phoneNumberUtil.IsValidNumber(number))
                return Result.Failure<PhoneNumber>("PhoneNumber is invalid");

            return Result.Success(new PhoneNumber(phoneNumber));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(PhoneNumber phoneNumber)
        {
            return phoneNumber.Value;
        }
    }
}
