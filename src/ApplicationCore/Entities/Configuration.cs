using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Configuration
    {
        [Key]
        public ConfigurationType ConfigurationType { get; set; }
        public string Value { get; set; }

    }
    public enum ConfigurationType
    {
        RemoteServiceAddress
    }

}
