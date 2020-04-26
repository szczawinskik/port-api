using ApplicationCore.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService configurationService;
        private readonly IValidator<RemoteServiceAddressViewModel> validator;

        public ConfigurationController(IConfigurationService configurationService,
            IValidator<RemoteServiceAddressViewModel> validator)
        {
            this.configurationService = configurationService;
            this.validator = validator;
        }
        [HttpGet("[action]")]
        public string RemoteServiceAddress()
        {
            return configurationService.GetConfigurationValue(ConfigurationType.RemoteServiceAddress);
        }

        [HttpPost("[action]")]
        public IActionResult RemoteServiceAddress(RemoteServiceAddressViewModel model)
        {
            if (validator.IsValid(model))
            {
                if (configurationService.SetConfigurationValue(ConfigurationType.RemoteServiceAddress, model.Value))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(validator.ErrorList);

        }
    }
}
