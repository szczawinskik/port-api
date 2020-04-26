using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IConfigurationService
    {
        string GetConfigurationValue(ConfigurationType configurationType);
        bool SetConfigurationValue(ConfigurationType configurationType, string value);

    }
}
