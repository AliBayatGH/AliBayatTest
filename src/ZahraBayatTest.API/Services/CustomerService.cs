using AliBayatTest.API.ViewModels;
using AliBayatTest.Domain;
using AliBayatTest.Infrastructure;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AliBayatTest.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbcontext _context;

        public CustomerService(ApplicationDbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all cutomers
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerViewModel>> GetCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();

            var result = customers.Select(x => new CustomerViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                BankAccountNumber = x.BankAccountNumber
            }).ToList();

            return result;
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>A Customer</returns>
        public async Task<CustomerViewModel> GetCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            // ToDo: Use Automapper
            var result = new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                BankAccountNumber = customer.BankAccountNumber
            };

            return result;
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="customerViewModel"></param>
        /// <returns>Rerurn success or failure</returns>
        public async Task<Result> UpdateCustomerAsync(int id, CustomerViewModel customerViewModel)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                return Result.Failure("NotFound");
            }

            // ToDo: Use Automapper
            customer.FirstName = customerViewModel.FirstName;
            customer.LastName = customerViewModel.LastName;
            customer.DateOfBirth = customerViewModel.DateOfBirth;
            customer.PhoneNumber = PhoneNumber.Create(customerViewModel.PhoneNumber).Value;
            customer.Email = Email.Create(customerViewModel.Email).Value;
            customer.BankAccountNumber = customerViewModel.BankAccountNumber;

            await _context.SaveChangesAsync();

            return Result.Success();
        }

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomerAsync(CustomerViewModel customerViewModel)
        {
            var customer = new Customer(
                firstName: customerViewModel.FirstName,
                lastName: customerViewModel.LastName,
                dateOfBirth: customerViewModel.DateOfBirth,
                phoneNumber: PhoneNumber.Create(customerViewModel.PhoneNumber).Value,
                email: Email.Create(customerViewModel.Email).Value,
                bankAccountNumber: customerViewModel.BankAccountNumber
                );

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        /// <summary>
        /// Remove customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns></returns>
        public async Task<Result<Customer>> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return Result.Failure<Customer>("NotFound");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
