using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Services;
using BusinessLogic.Services.Mapping;
using ComponentRegistrar.Settings;
using DataAccess.EntityFramework;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentRegistrar
{
    /// <summary>
    /// Регистратор сервиса
    /// </summary>
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();
            services.AddSingleton(applicationSettings);
            return services.AddSingleton((IConfigurationRoot)configuration)
                .InstallServices()
                .ConfigureContext(applicationSettings.ConnectionString)
                .InstallRepositories();
        }

        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICourseService, CourseService>()
                .AddTransient<ILessonService, LessonService>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICourseRepository, CourseRepository>()
                .AddTransient<ILessonRepository, LessonRepository>();
            return serviceCollection;
        }
    }
}