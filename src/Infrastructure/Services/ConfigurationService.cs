using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository repository;
        private readonly IApplicationLogger<ConfigurationService> logger;

        public ConfigurationService(IConfigurationRepository repository, IApplicationLogger<ConfigurationService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        public string GetConfigurationValue(ConfigurationType configurationType)
        {
            try
            {
                return repository.GetConfigurationValue(configurationType);
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return null;
        }

        public bool SetConfigurationValue(ConfigurationType configurationType, string value)
        {
            try
            {
                repository.SetConfigurationValue(configurationType, value);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }
    }
}
