using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Repositories;
using Infrastructure.Wrappers.HttpClientWrapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Validation.Interfaces;
using Web.Validation.Validators;
using Web.ViewModels;

namespace Web.Configuration
{
    public static class WrappersConfiguration
    {
        public static IServiceCollection ConfigureWrappers(this IServiceCollection collection)
        {
            collection.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

            return collection;
        }
    }
}
