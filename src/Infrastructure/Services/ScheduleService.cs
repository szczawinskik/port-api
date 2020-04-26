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
        private readonly IScheduleRepository repository;
        private readonly IBaseRepository<Ship> shipRepository;
        private readonly IApplicationLogger<ScheduleService> logger;

        public ScheduleService(IScheduleRepository repository, IBaseRepository<Ship> shipRepository,
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
                ValidateSchedules(schedule, ship);
                UpdateClosestSchedule(schedule, ship);
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

        private void UpdateClosestSchedule(Schedule schedule, Ship ship)
        {
            if (ShouldUpdateClosesSchedule(schedule, ship))
            {
                ship.ClosestSchedule = schedule;
            }
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
                var schedule = repository.Find(entity.Id);
                var ship = shipRepository.Find(schedule.ShipId);
                ValidateSchedules(schedule, ship);
                if (entity.ArrivalSent && entity.DepartureSent)
                {
                    ChangeClosestSchedule(ship, schedule.Id);
                }
                else
                {
                    UpdateClosestSchedule(entity, schedule.Ship);
                }
                repository.Update(entity);

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }

        private void ValidateSchedules(Schedule schedule, Ship ship)
        {
            if(ship.Schedules == null || !ship.Schedules.Any())
            {
                return;
            }
            if(ship.Schedules.Any(x => (x.Arrival <= schedule.Arrival && schedule.Arrival < x.Departure)
                ||(x.Arrival >= schedule.Arrival && schedule.Arrival > x.Departure)))
            {
                throw new Exception("Schedules are interlocking");
            }
        }
        private void ChangeClosestSchedule(Ship ship, int scheduleId)
        {
            var closestSchedule = ship.Schedules
                .Where(x => x.Id != scheduleId)
                .OrderBy(x => x.Arrival)
                .FirstOrDefault();
            ship.ClosestSchedule = closestSchedule;
        }
    }
}
