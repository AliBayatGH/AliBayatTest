using AliBayatTest.API.CustomAttributes;
using System;

namespace AliBayatTest.API.ViewModels
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [PhoneNumber]
        public string PhoneNumber { get; set; }
        [Email]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}