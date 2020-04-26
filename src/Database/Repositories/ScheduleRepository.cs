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
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationContext context;

        public ScheduleRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Schedule entity)
        {                   
            context.Schedules.Add(entity);
            context.SaveChanges();
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
            if (!existingEntity.ArrivalSent && entity.ArrivalSent)
            {
                existingEntity.ArrivalSent = true;
            }
            if (!existingEntity.DepartureSent && entity.DepartureSent)
            {
                existingEntity.DepartureSent = true;
            }
            context.SaveChanges();
        }

        public IQueryable<Schedule> GetAllWithShips()
        {
            return GetAll().Include(x => x.Ship);
        }
    }
}
