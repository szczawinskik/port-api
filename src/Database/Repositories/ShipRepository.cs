using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    public class ScheduleRepository : IBaseRepository<Schedule>
    {
        private readonly ApplicationContext context;

        public ScheduleRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Schedule entity, int shipId)
        {
            var ship = context.Ships.First(x => x.Id == shipId);
            entity.Ship = ship;
            if (ShouldUpdateClosesSchedule(entity, ship))
            {
                ship.ClosestSchedule = entity;
            }
            context.Schedules.Add(entity);
            context.SaveChanges();
        }

        private static bool ShouldUpdateClosesSchedule(Schedule entity, Ship ship)
        {
            return DateTime.Now < entity.Arrival
                            && (ship.ClosestSchedule == null || ship.ClosestSchedule.Arrival > entity.Arrival);
        }

        public void Delete(int id)
        {
            var schedule = context.Schedules.First(x => x.Id == id);
            context.Remove(schedule);
            context.SaveChanges();
        }

        public IQueryable<Schedule> GetAll()
        {
            return context.Schedules;
        }

        public Schedule Find(int id)
        {
            return context.Schedules.First(x => x.Id == id);
        }

        public void Update(Schedule entity)
        {
            var existingEntity = context.Schedules.First(x => x.Id == entity.Id);
            if (existingEntity.Arrival != entity.Arrival)
            {
                existingEntity.Arrival = entity.Arrival;
            }
            if (existingEntity.Departure != entity.Departure)
            {
                existingEntity.Departure = entity.Departure;
            }
            context.SaveChanges();
        }
    }
}
