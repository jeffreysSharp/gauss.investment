using Gauss.Investment.Domain.Enums;
using Microsoft.Extensions.Configuration;


namespace Gauss.Investment.Infrastructure.Extensions
{
    public static class ConnectionConfigurationExtension
    {
        public static bool IsUnitTestEnvironment(this IConfiguration configuration)
        {
           return configuration.GetValue<bool>("InMemoryTest");
        }

        public static DatabaseType DatabaseType(this IConfiguration configuration)
        {
            var dataBaseType = configuration.GetConnectionString("DatabaseType");
            return (DatabaseType)Enum.Parse(typeof(DatabaseType), dataBaseType!);
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == Domain.Enums.DatabaseType.MySql)
                return configuration.GetConnectionString("ConnectionMySqlServer")!;
            else
                return configuration.GetConnectionString("ConnectionSqlServer")!;
        }
    }
}
