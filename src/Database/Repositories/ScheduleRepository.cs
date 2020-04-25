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
        private ApplicationContext context;

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
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
