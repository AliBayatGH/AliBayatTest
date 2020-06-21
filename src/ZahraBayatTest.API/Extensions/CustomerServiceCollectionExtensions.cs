using AliBayatTest.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AliBayatTest.API.Extensions
{
    public static class CustomerServiceCollectionExtensions
    {
        /// <summary>
        ///   Registers the given context as a service in the Microsoft.Extensions.DependencyInjection.IServiceCollection.
        ///   You use this method when using dependency injection in your application, such</summary>
        ///   as with ASP.NET. For more information on setting up dependency injection, see<param name="serviceCollection"></param>
        /// </summary>
        /// <param name="serviceCollection">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCustomerService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
