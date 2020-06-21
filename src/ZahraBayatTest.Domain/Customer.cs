using AliBayatTest.Domain.SeedWorks;
using System;

namespace AliBayatTest.Domain
{
    public class Customer : Entity
    {
        public Customer(string firstName, string lastName, DateTime dateOfBirth, PhoneNumber phoneNumber, Email email, string bankAccountNumber) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
        protected Customer() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
