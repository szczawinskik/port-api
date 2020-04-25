using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Repositories;
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
    public static class ValidatorsConfiguration
    {
        public static IServiceCollection ConfigureValidators(this IServiceCollection collection)
        {
            collection.AddScoped<IValidator<ScheduleViewModel>, ScheduleViewModelValidator>();
            collection.AddScoped<IValidator<ShipViewModel>, ShipViewModelValidator>();

            return collection;
        }
    }
}
