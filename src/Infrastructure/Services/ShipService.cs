using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Services
{
    public class ShipService : IService<Ship>
    {
        private IBaseRepository<Ship> repository;
        private IApplicationLogger<ShipService> logger;

        public ShipService(IBaseRepository<Ship> repository, IApplicationLogger<ShipService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public bool Add(Ship item, int shipOwnerId)
        {
            try
            {
                repository.Add(item, shipOwnerId);
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
            throw new NotImplementedException();
        }

        public Ship Find(int id)
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

        public IQueryable<Ship> GetAll()
        {
            try
            {
                return repository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return new List<Ship>().AsQueryable();
        }

        public bool Update(Ship entity)
        {
            throw new NotImplementedException();
        }
    }
}
