using Gauss.Investment.Domain.Enums;
using Gauss.Investment.Domain.Repositories;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Infrastructure.Data;
using Gauss.Investment.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gauss.Investment.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dataBaseType = configuration.GetConnectionString("DatabaseType");
            var databaseTypeEnum = (DatabaseType)Enum.Parse(typeof(DatabaseType), dataBaseType!);

            if (databaseTypeEnum == DatabaseType.MySql)            
                AddDbContextMySql(services, configuration);            
            else
                AddDbContextSqlServer(services, configuration);

            AddRepositories(services);
        }

        private static void AddDbContextMySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionMySqlServer");
            var serverVersion = new MySqlServerVersion (new Version(8, 0, 35));

            services.AddDbContext<GaussInvestmentDbContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddDbContextSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSqlServer");

            services.AddDbContext<GaussInvestmentDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
