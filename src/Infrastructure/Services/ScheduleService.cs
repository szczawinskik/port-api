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
        private readonly IBaseRepository<Schedule> repository;
        private readonly IBaseRepository<Ship> shipRepository;
        private readonly IApplicationLogger<ScheduleService> logger;

        public ScheduleService(IBaseRepository<Schedule> repository, IBaseRepository<Ship> shipRepository,
            IApplicationLogger<ScheduleService> logger)
        {
            this.repository = repository;
            this.shipRepository = shipRepository;
            this.logger = logger;
        }
        public bool Add(Schedule schedule, int shipId)
        {
            try
            {
                var ship = shipRepository.Find(shipId);
                if(ShouldUpdateClosesSchedule(schedule, ship))
                {
                    ship.ClosestSchedule = schedule;
                }
                schedule.Ship = ship;
                repository.Add(schedule);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }

        private bool ShouldUpdateClosesSchedule(Schedule entity, Ship ship)
        {
            return DateTime.Now < entity.Arrival
                            && (ship.ClosestSchedule == null || ship.ClosestSchedule.Arrival > entity.Arrival);
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
