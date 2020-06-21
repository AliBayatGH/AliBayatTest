using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AliBayatTest.API.Services;
using AliBayatTest.API.ViewModels;
using AliBayatTest.Domain;
using AliBayatTest.Infrastructure;

namespace AliBayatTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly ICustomerService _customerService;

        public CustomersController(ApplicationDbcontext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomers()
        {
            return await _customerService.GetCustomersAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomer(int id)
        {
            CustomerViewModel result = await _customerService.GetCustomerAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Customers/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerViewModel customerViewModel)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customerViewModel);

            if (result.IsFailure)
                return NotFound();

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerViewModel customerViewModel)
        {
            Customer customer = await _customerService.AddCustomerAsync(customerViewModel);

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);

            if (result.IsFailure)
                return NotFound();

            return result.Value;
        }
    }
}
