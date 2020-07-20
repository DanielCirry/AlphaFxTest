using AlphaFx.Domain.Contracts;
using AlphaFx.Domain.Services;
using AlphaFx.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AlphaFx
{
    class Program
    {
        public static IConfigurationRoot configuration;

        private static IServiceCollection ConfigureService()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IAlphaFxService, AlphaFxService>();
            services.AddSingleton<IConfigurationRoot>(configuration);

            services.AddTransient<AlphaFxApplication>();
            return services;
        }
        public static void Main(string[] args)
        {
            var services = ConfigureService();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<AlphaFxApplication>().Run();
        }
    }
}
