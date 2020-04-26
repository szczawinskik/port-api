using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IConfigurationRepository
    {
        string GetConfigurationValue(ConfigurationType configurationType);
        void SetConfigurationValue(ConfigurationType configurationType, string value);
    }
}
