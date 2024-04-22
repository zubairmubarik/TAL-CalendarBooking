using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.MSSQL
{
    public class MSDbContextFactory : IDesignTimeDbContextFactory<MSDbContext>
    {
        private const string ConnectionStringName = "SQLDBConnection";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public MSDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}..{0}Presentation{0}CalendarBooking-Console", Path.DirectorySeparatorChar);
            var environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);           
            var configuration = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.Local.json", optional: true)
               .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
               .AddEnvironmentVariables()
               .Build();

            var builder = new DbContextOptionsBuilder<MSDbContext>();
          
            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            builder.UseSqlServer(connectionString);
            return new MSDbContext(builder.Options);         
        }
    }
}
