﻿using Microsoft.Extensions.DependencyInjection;
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
            //Type modelType = Type.GetType(string.Format("{0}.{1}", viewTypeNamespace, "Model"))!;
            Type viewModelType = Type.GetType(string.Format("{0}.{1}", viewTypeNamespace, $"{viewTypeFullName}Model"))!;
            //services.AddSingleton(modelType);
            services.AddSingleton(viewModelType);
            services.AddSingleton(typeof(TView));

            return services;
        }

    }
}