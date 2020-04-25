using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Services
{
    public class ScheduleService : IService<Schedule>
    {
        private IBaseRepository<Schedule> repository;
        private IApplicationLogger<ScheduleService> logger;

        public ScheduleService(IBaseRepository<Schedule> repository, IApplicationLogger<ScheduleService> logger)
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
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }

        public Schedule Find(int id)
        {
            try
            {
                return repository.Find(id);
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return null;
        }

        public IQueryable<Schedule> GetAll()
        {
            try
            {
                return repository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return new List<Schedule>().AsQueryable();
        }

        public bool Update(Schedule entity)
        {
            try
            {
                repository.Update(entity);
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
