using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ApiRuleta.Helpers
{
	public static class ServiceExtensions
	{
        public static void ConfigureContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("RuletaDB");

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);

            if (string.IsNullOrEmpty(databaseUrl)) {
                services.AddDbContext<RuletaDBContext>(options => options!.UseSqlServer(connection));
            } else {
                services.AddDbContext<RuletaDBContext>(options => options!.UseNpgsql(connection));
            }
        
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            return builder.ToString();
        }
    }
} 

