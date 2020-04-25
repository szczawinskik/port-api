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
            throw new NotImplementedException();
        }

        public IQueryable<Schedule> GetAll()
        {
            throw new NotImplementedException();
        }

        public Schedule Find(int id)
        {
            throw new NotImplementedException();
        }

        public Schedule Update(Schedule entity)
        {
            throw new NotImplementedException();
        }
    }
}
