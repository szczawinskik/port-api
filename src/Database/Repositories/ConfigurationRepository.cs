using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ApplicationContext context;
        public ConfigurationRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public string GetConfigurationValue(ConfigurationType configurationType)
        {
            return context.Configurations.First(x => x.ConfigurationType == configurationType).Value;
        }

        public void SetConfigurationValue(ConfigurationType configurationType, string value)
        {
            context.Configurations.First(x => x.ConfigurationType == configurationType).Value = value;
            context.SaveChanges();
        }
    }
}
