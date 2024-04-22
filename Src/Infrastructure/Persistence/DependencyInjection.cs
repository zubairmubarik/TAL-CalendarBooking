//using Microsoft.Data.SqlClient;
using Persistence.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;


namespace Persistence
{
    public static class DependencyInjection
    {   
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {            
            var configurationString = configuration.GetConnectionString("SQLDBConnection");
            
            services.AddDbContext<MSDbContext>(options => options.UseSqlServer(configurationString));

            services.AddScoped<IMSDbContext>(provider => provider.GetService<MSDbContext>());

            return services;
        }
    }
}
