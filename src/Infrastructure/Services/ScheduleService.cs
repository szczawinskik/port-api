using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class ScheduleService : IService<Schedule>
    {
        private IBaseRepository<Schedule> repository;
        private ILogger<ScheduleService> logger;

        public ScheduleService(IBaseRepository<Schedule> repository, ILogger<ScheduleService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        public bool Add(Schedule item)
        {
            try
            {
                repository.Add(item);
                return true;
            }
            catch(Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }
    }
}
