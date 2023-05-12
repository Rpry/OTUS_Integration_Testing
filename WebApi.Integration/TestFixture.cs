using System;
using Microsoft.Extensions.Configuration;

namespace WebApi.Integration
{
    public class TestFixture : IDisposable
    {
        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Выполняется перед запуском тестов
        /// </summary>
        public TestFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json").Build();
        }

        public void Dispose()
        {
        }
    }
}
