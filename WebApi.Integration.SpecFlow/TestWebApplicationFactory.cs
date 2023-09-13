using System;
using System.Linq;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Integration.SpecFlow
{
    public class TestWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>, IDisposable where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<DatabaseContext>));

                services.Remove(descriptor);

                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryReviewProtocolSagaContext", builder => { });
                }, ServiceLifetime.Transient);
            });
        }

        public new void Dispose()
        {
        }
    }
}