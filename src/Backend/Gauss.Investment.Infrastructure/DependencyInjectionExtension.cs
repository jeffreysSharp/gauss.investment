using FluentMigrator.Runner;
using Gauss.Investment.Domain.Enums;
using Gauss.Investment.Domain.Repositories;
using Gauss.Investment.Domain.Repositories.User;
using Gauss.Investment.Domain.Security.Cryptography;
using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Domain.Services.LoggedUser;
using Gauss.Investment.Infrastructure.Data;
using Gauss.Investment.Infrastructure.Data.Repositories;
using Gauss.Investment.Infrastructure.Extensions;
using Gauss.Investment.Infrastructure.Security.Access.Generator;
using Gauss.Investment.Infrastructure.Security.Access.Validator;
using Gauss.Investment.Infrastructure.Security.Cryptography;
using Gauss.Investment.Infrastructure.Services.LoggedUser;
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
            AddPasswordEncripter(services, configuration);
            AddRepositories(services);
            AddLoggedUser(services);
            AddTokens(services, configuration); 

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
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
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
    
        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }

        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
            services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionalKey!));
        }
    }
}
