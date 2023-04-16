using MedKarta.Windows.Main;
using Microsoft.Extensions.DependencyInjection;

namespace MedKarta.Application.DependencyInjection
{
    internal static class AddWindowsExtensions
    {
        public static IServiceCollection AddMainWindow(this IServiceCollection services) => services
            .AddSingleton<MainModel>()
            .AddSingleton<MainWindowModel>()
            .AddSingleton((s) => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowModel>()
            })
            ;
    }
}