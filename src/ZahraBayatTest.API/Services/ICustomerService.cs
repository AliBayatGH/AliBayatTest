using AliBayatTest.API.ViewModels;
using AliBayatTest.Domain;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AliBayatTest.API.Services
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomerAsync(CustomerViewModel customerViewModel);
        Task<Result<Customer>> DeleteCustomerAsync(int id);
        Task<CustomerViewModel> GetCustomerAsync(int id);
        Task<List<CustomerViewModel>> GetCustomersAsync();
        Task<Result> UpdateCustomerAsync(int id, CustomerViewModel customerViewModel);
    }
}