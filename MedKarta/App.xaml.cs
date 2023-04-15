using MedKarta.Application;
using MedKarta.Application.DependencyInjection;
using MedKarta.Windows.Main;
using MedKarta.Windows.Main.Views.Start;
using MedKarta.Windows.Main.Views.Work;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Windows;

namespace MedKarta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application, IApp
    {
        /// <summary>
        /// https://learn.microsoft.com/dotnet/api/microsoft.extensions.hosting.ihost?view=dotnet-plat-ext-6.0
        /// https://adnanrafiq.com/blog/complete-guide-to-hosted-or-background-or-worker-services-in-dot-net-using-csharp/
        /// </summary>
        private readonly IHost AppHost;

        /// <summary>
        /// Можно сделать статическое поле и обращаться к нему.
        /// 
        /// </summary>
        private IServiceScope? AppScope;

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                })
                .ConfigureServices(ConfigureServices)
                .Build()
                ;
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services) => services
            .AddSingleton<IApp>(this)
            .AddView<StartView>()
            .AddView<WorkView>()
            .AddMainWindow()
            ;

        public (T ViewModel, object View) GetViewModel<T>() where T : class
            => AppScope!.ServiceProvider.GetViewModel<T>();

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            AppScope = AppHost.Services.CreateScope();

            MainWindow = AppScope.ServiceProvider.GetRequiredService<MainWindow>();

            MainWindow!.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}