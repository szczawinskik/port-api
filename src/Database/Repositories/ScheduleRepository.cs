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
    public class ShipRepository : IBaseRepository<Ship>
    {
        private ApplicationContext context;

        public ShipRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Ship entity)
        {
            context.Ships.Add(entity);
            context.SaveChanges();
        }

        public IQueryable<Ship> GetAll()
        {
            return context.Ships;
        }

        public Ship Find(int id)
        {
            return context.Ships
                .Include(x => x.Schedules)
                .Include(x => x.ShipOwner)
                .First(x => x.Id == id);
        }

        public void Update(Ship entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
