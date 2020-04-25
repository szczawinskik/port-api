using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Configuration
{
    public static class ApplicationContextConfiguration
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection collecion)
        {
            collecion.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseInMemoryDatabase("test_db");
            });

            return collecion;
        }
    }
}
