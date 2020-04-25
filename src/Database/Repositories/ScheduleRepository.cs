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
        private IApplicationContext context;

        public ScheduleRepository(IApplicationContext context)
        {
            this.context = context;
        }
        public Schedule Add(Schedule entity)
        {
            context.Schedules.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public void Delete(Schedule entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Schedule> GetAll()
        {
            throw new NotImplementedException();
        }

        public Schedule GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Schedule Update(Schedule entity)
        {
            throw new NotImplementedException();
        }
    }
}
