using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using MediatR;
using System.Reflection;
using Application.Common.Helper;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // TODO: Scoped / Singleton / Transisient
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();            
            return services;
        }
    }
}
