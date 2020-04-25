using ApplicationCore.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection collection)
        {
            collection.AddScoped<IService<Schedule>, ScheduleService>();

            return collection;
        }
    }
}
