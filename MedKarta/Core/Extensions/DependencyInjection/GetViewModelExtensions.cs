using MedKarta.Core.Models;
using MedKarta.UCL.Models;
using System;
using System.Reflection;
using System.Windows;

// namespace Microsoft.Extensions.DependencyInjection;
namespace MedKarta.Core.Extensions.DependencyInjection
{
    internal static class GetViewModelExtensions
    {
        /// <summary>
        /// Расширение получает представление из преобразованного имени MyViewModel.
        /// </summary>
        /// <typeparam name="T">object MyViewModel.</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns>Возвращает MyViewModel, MyView.</returns>
        /// <remarks>Вызов расширения разместить в блоке try, catch.</remarks>
        /// <exception cref="ArgumentNullException" />
        internal static (T BaseViewModel, object View) GetViewModel<T>(this IServiceProvider serviceProvider) where T : BaseViewModel
        {
            var vmType = typeof(T);
            var viewTypeNamespace = vmType.Namespace?.Replace("Model", "");
            string? viewTypeFullName = vmType?.Name.Replace("Model", "");
            Assembly assembly = Assembly.Load("MedKarta.UCL");
            Type? viewType = assembly.GetType(string.Format("{0}.{1}", viewTypeNamespace, viewTypeFullName));
            //Type? viewType = Type.GetType(string.Format("{0}.{1}", viewTypeNamespace, viewTypeFullName));
            //FrameworkElement view = serviceProvider.GetService(viewType!) as FrameworkElement ?? throw new ArgumentNullException(null, nameof(viewType));
            // https://learn.microsoft.com/dotnet/api/system.argumentnullexception.throwifnull?view=net-6.0
            ArgumentNullException.ThrowIfNull(viewType, $"Для {viewTypeFullName}Model Path: {viewTypeNamespace}. Не найдено представление: {viewTypeFullName}");
            var vs = serviceProvider.GetService(viewType);
            ArgumentNullException.ThrowIfNull(vs, $"Dependency Injection: Не удалось получить представление {viewTypeFullName}");
            FrameworkElement view = (FrameworkElement)vs;
            view.DataContext = serviceProvider.GetService(vmType!);
            return (view.DataContext as T, view)!;
        }
    }
}