using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Logger
{
    public class ApplicationLoger<T> : IApplicationLogger<T>
    {
        private ILogger<T> logger;

        public ApplicationLoger(ILogger<T> logger)
        {
            this.logger = logger;
        }
        public void LogError(Exception e)
        {
            logger.LogError(e, e.Message);
        }
    }
}
