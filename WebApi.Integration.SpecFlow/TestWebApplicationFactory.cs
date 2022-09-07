using System;
using System.Linq;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Integration.SpecFlow.Data;

namespace WebApi.Integration.SpecFlow
{
    public class TestWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>, IDisposable where TStartup : class
    {
        private IServiceProvider ServiceProvider { get; set; }

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

                var sp = services.BuildServiceProvider();
                ServiceProvider = sp;
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<DatabaseContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<TestWebApplicationFactory<TStartup>>>();

                try
                {
                    new EfTestDbInitializer(dbContext).InitializeDb();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Проблема во время заполнения тестовой базы. " +
                                        "Ошибка: {Message}", ex.Message);
                }
            });
        }

        public new void Dispose()
        {
            new EfTestDbInitializer(ServiceProvider.GetRequiredService<DatabaseContext>()).CleanDb();
        }
    }
}