using AdvantTest.Client.Services;
using AdvantTest.Client.ViewModels;
using AdvantTest.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdvantTest.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IConfiguration _configuration;
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddTransient<IEmployeeServices>(s => new EmployeeService(_configuration));
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _configuration = CreateConfiguration();
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.DataContext = serviceProvider.GetService<MainViewModel>();
            mainWindow.Show();
        }

        public static IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

    }
}
