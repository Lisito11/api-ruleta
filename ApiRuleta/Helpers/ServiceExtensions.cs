using System;
using Microsoft.EntityFrameworkCore;

namespace ApiRuleta.Helpers
{
	public static class ServiceExtensions
	{
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["SqlServerConnection:RuletaDB"];
            services.AddDbContext<RuletaDBContext>(options => options.UseSqlServer(connectionString));
        }
    }
}

