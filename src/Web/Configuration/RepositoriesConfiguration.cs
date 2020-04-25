using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection ConfigureAppRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IBaseRepository<Schedule>, ScheduleRepository>();

            return collection;
        }
    }
}
