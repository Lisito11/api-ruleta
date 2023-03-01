using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiRuleta.Helpers
{
	public static class ServiceExtensions
	{
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["Connection:RuletaDB"];
            services.AddDbContext<RuletaDBContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["Connection:RuletaDB"];
            var conStrBuilder = new SqlConnectionStringBuilder(connectionString);
            conStrBuilder.Password = config["Password"];
            var connection = conStrBuilder.ConnectionString;

            services.AddDbContext<RuletaDBContext>(options => options.UseNpgsql(connection));
        }
    }
} 

