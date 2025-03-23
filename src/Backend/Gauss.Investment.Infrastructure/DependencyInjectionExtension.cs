using FluentMigrator.Runner;
using Gauss.Investment.Domain.Enums;
using Gauss.Investment.Domain.Repositories;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Infrastructure.Data;
using Gauss.Investment.Infrastructure.Data.Repositories;
using Gauss.Investment.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gauss.Investment.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);

            if (configuration.IsUnitTestEnvironment())
                return;

            var databaseType = configuration.DatabaseType();
            if (databaseType == DatabaseType.MySql)
            {
                AddDbContextMySql(services, configuration);
                AddFluentMIgrator_MySql(services, configuration);
            }
            else
            {
                AddDbContextSqlServer(services, configuration);
                AddFluentMIgrator_SqlServer(services, configuration);
            } 
        }

        private static void AddDbContextMySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            services.AddDbContext<GaussInvestmentDbContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddDbContextSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

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

        private static void AddFluentMIgrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                
                options
                .AddMySql5()
                .WithGlobalConnectionString(connectionsString)
                .ScanIn(Assembly.Load("Gauss.Investment.Infrastructure")).For.All();
            });
        }

        private static void AddFluentMIgrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {

                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionsString)
                .ScanIn(Assembly.Load("Gauss.Investment.Infrastructure")).For.All();
            });
        }
    }
}
