using MedKarta.Core.Extensions.StringExtensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MedKarta.Core.Extensions.DependencyInjection
{
    public static class AddViewExtensions
    {
        public static IServiceCollection AddView<TView>(this IServiceCollection services) where TView : class
        {
            var viewType = typeof(TView);
            var viewTypeNamespace = viewType.Namespace;
            string viewTypeFullName = viewType.Name;

            string? nameBase = viewTypeFullName.SplitPascalCase();            
            Type? modelType = Type.GetType(string.Format("{0}.{1}", viewTypeNamespace, $"{nameBase}Model"));
            if (modelType != null ) services.AddSingleton(modelType);

            Type viewModelType = Type.GetType(string.Format("{0}.{1}", viewTypeNamespace, $"{viewTypeFullName}Model"))!;
            
            services.AddSingleton(viewModelType);
            services.AddSingleton(typeof(TView));

            return services;
        }

    }
}