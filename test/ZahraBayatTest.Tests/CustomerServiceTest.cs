using AliBayatTest.API.Services;
using AliBayatTest.API.ViewModels;
using AliBayatTest.Domain;
using AliBayatTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AliBayatTest.UnitTests
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerService> _customerServiceMock;

        public CustomerServiceTest()
        {
            _customerServiceMock = new Mock<ICustomerService>();
        }

        [Fact]
        public async Task GetCustomers_Success()
        {
            // Arrange
            var fakeCustomers = new List<CustomerViewModel> { new CustomerViewModel { } };
            _customerServiceMock.Setup(x => x.GetCustomersAsync())
            .Returns(Task.FromResult(fakeCustomers));

            // Act
            var customers = await _customerServiceMock.Object.GetCustomersAsync();

            // Assert
            Assert.NotEmpty(customers);
        }

        [Fact]
        public async Task GetCustomerById_Returns_Null_if_Customer_Is_Not_Persisted()
        {
            // Arrange
            var fakeCustomer =
                new CustomerViewModel
                {
                    FirstName = "Ali",
                    LastName = "Bayat",
                    DateOfBirth = new DateTime(1988, 2, 20),
                    Email = "ali.bayat.gh@gmail.com",
                    PhoneNumber = "+9391818607",
                    BankAccountNumber = "123456"
                };
            _customerServiceMock.Setup(x => x.GetCustomerAsync(0))
            .Returns(Task.FromResult(fakeCustomer));

            // Act
            var customer = await _customerServiceMock.Object.GetCustomersAsync();

            // Assert
            Assert.Null(customer);
        }

        [Fact]
        public async Task Combination_of_FirstName_LastName_and_DOB_must_be_unique()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbcontext>()
               .UseInMemoryDatabase(databaseName: "Database")
               .Options;

            var email = Email.Create("ali.bayat.gh@gmail.com").Value;
            var phoneNumber = PhoneNumber.Create("+989391818607").Value;

            var newCustomer =
                new Customer
                (
                    firstName: "Ali",
                    lastName: "Bayat",
                    dateOfBirth: new DateTime(1988, 2, 20),
                    email: email,
                    phoneNumber: phoneNumber,
                    bankAccountNumber: "123456"
                );

            using (var context = new ApplicationDbcontext(options))
            {
                context.Customers.Add(newCustomer);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbcontext(options))
            {
                context.Customers.Add(newCustomer);

                Assert.Throws<ArgumentException>(() => { context.SaveChangesAsync().GetAwaiter().GetResult(); });
            }
        }

        [Fact]
        public void Email_Must_Be_Unique()
        {
            // Arrange
            var email = Email.Create("ali.bayat.gh@gmail.com").Value;
            var phoneNumber = PhoneNumber.Create("+989391818607").Value;

            var customer1 = new Customer
                (
                    firstName: "Ali",
                    lastName: "Bayat",
                    dateOfBirth: new DateTime(1988, 2, 20),
                    email: email,
                    phoneNumber: phoneNumber,
                    bankAccountNumber: "123456"
                );

            var customer2 = new Customer
            (
                firstName: "Mohammad",
                lastName: "Najafi",
                dateOfBirth: new DateTime(1985, 2, 20),
                email: email,
                phoneNumber: phoneNumber,
                bankAccountNumber: "123456"
            );

            // Act - Assert
            using (var context = CreateDbContext())
            {
                context.Database.EnsureCreated();
                context.Customers.Add(customer1);

                context.SaveChanges();
            }

            using (var context = CreateDbContext())
            {
                context.Database.EnsureCreated();

                context.Customers.Add(customer2);
                Action saveDuplicate = () => context.SaveChanges();

                Assert.Throws<DbUpdateException>(saveDuplicate);

                context.Database.EnsureDeleted();
            }
        }

        public ApplicationDbcontext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbcontext>()
               .UseSqlServer("Server=.; Database=AliBayatTestDb.Test; Integrated Security=SSPI;MultipleActiveResultSets=true;TRUSTED_CONNECTION = TRUE")
               .Options;

            return new ApplicationDbcontext(options);
        }
    }
}