using AliBayatTest.Domain;
using System;
using Xunit;

namespace AliBayatTest.Tests
{
    public class CustomerTest
    {
        [Fact]
        public void Create_customer_success()
        {
            // Arrange
            var firstName = "Ali";
            var lastName = "Bayat";
            var dateOfBirth = new DateTime(1988, 2, 20);
            var phoneNumber = PhoneNumber.Create("+989391818607").Value;
            var email = Email.Create("ali.bayat.gh@gamil.com").Value;
            var bankAccountNumber = "123456";

            // Act
            var fakeCustomer = new Customer(
                            firstName: firstName,
                            lastName: lastName,
                            dateOfBirth: dateOfBirth,
                            phoneNumber: phoneNumber,
                            email: email,
                            bankAccountNumber: bankAccountNumber
                            );
            // Assert
            Assert.NotNull(fakeCustomer);
        }

        [Fact]
        public void Create_Email_Success()
        {
            // Arrange
            var email = Email.Create("ali.bayat.gh@gamil.com");

            // Act - Assert
            Assert.True(email.IsSuccess);
        }

        [Fact]
        public void Create_PhoneNumber_Success()
        {
            // Arrange
            var email = PhoneNumber.Create("+989391818607");

            // Act - Assert
            Assert.True(email.IsSuccess);
        }

        [Fact]
        public void Invalid_Email()
        {
            // Arrange
            var email = Email.Create("ali.bayat.ghgamil.com");

            // Act - Assert
            Assert.True(email.IsFailure);
        }

        [Fact]
        public void Invalid_PhoneNumber()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+98920");

            // Act - Assert
            Assert.True(phoneNumber.IsFailure);
        }
    }
}