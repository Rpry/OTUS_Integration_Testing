using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString)
        {
            services
                .AddDbContext<DatabaseContext>(o => o
                    .UseSqlite(connectionString));
                    //.UseSqlServer(connectionString));
            return services;
        }
    }
}