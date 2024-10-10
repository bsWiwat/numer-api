
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Numer.Infrastructure {
    public static class InfraServiceRegistration {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration  configuration) {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //repository

            return services;
        }

        
    }
}
