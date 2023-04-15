using MedKarta.Core.Extensions.StringExtensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MedKarta.Core.Extensions.DependencyInjection
{
    public static class AddViewExtensions
    {
        public static IServiceCollection AddView<TView>(this IServiceCollection services) where TView : class
        {
            var viewType = typeof(TView);
            var viewTypeNamespace = viewType.Namespace;
            string viewTypeFullName = viewType.Name;


            Assembly assembly = Assembly.Load("MedKarta.UCL");
            //Type[] types = assembly.GetTypes();
            //string? nameBase = viewTypeFullName.SplitPascalCase();
            //Type? modelType = assembly.GetType(string.Format("{0}.{1}", viewTypeNamespace, $"{nameBase}Model"));

            //Type? modelType = assembly.GetType(string.Format("{0}.{1}", viewTypeNamespace, $"{viewTypeFullName}Model"));

            //if (modelType != null ) services.AddScoped(modelType);
            Type vType = assembly.GetType(string.Format("{0}.{1}", viewTypeNamespace, viewTypeFullName))!;
            Type viewModelType = assembly.GetType(string.Format("{0}.{1}", viewTypeNamespace, $"{viewTypeFullName}Model"))!;
            
            services.AddScoped(viewModelType);
            services.AddScoped(vType);

            return services;
        }

    }
}