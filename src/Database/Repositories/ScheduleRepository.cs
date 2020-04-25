using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Context;
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
        public Schedule Add(Schedule entity)
        {
            context.Schedules.Add(entity);
            context.SaveChanges();

            return entity;
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

        public Schedule Update(Schedule entity)
        {
            context.Update(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
