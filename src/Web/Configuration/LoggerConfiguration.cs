using ApplicationCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Logger;

namespace Web.Configuration
{
    public static class LoggerConfiguration
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection collecion)
        {
            collecion.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLoger<>));

            return collecion;
        }
    }
}
