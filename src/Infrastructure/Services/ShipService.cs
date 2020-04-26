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
        private readonly IBaseRepository<Ship> repository;
        private readonly IFindRepository<ShipOwner> shipOwnerRepository;
        private readonly IApplicationLogger<ShipService> logger;

        public ShipService(IBaseRepository<Ship> repository, IFindRepository<ShipOwner> shipOwnerRepository,
            IApplicationLogger<ShipService> logger)
        {
            this.repository = repository;
            this.shipOwnerRepository = shipOwnerRepository;
            this.logger = logger;
        }

        public bool Add(Ship ship, int shipOwnerId)
        {
            try
            {
                SetClosestSchedule(ship);
                var shipOwner = shipOwnerRepository.Find(shipOwnerId);
                ship.ShipOwner = shipOwner;
                repository.Add(ship);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
            return false;
        }

        private void SetClosestSchedule(Ship ship)
        {
            if (ship.Schedules.Any())
            {
                var now = DateTime.Now;
                if (ship.ClosestSchedule == null)
                {
                    ship.ClosestSchedule = ship.Schedules
                        .Where(x => x.Arrival >= now)
                        .OrderBy(x => x.Arrival)
                        .FirstOrDefault();
                }
                else
                {
                    ship.ClosestSchedule =
                        ship.Schedules.First(x => x.Arrival == ship.ClosestSchedule.Arrival
                        && x.Departure == ship.ClosestSchedule.Departure);
                }
            }
          
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
