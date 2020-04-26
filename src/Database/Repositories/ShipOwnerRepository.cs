using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    public class ShipOwnerRepository : IFindRepository<ShipOwner>
    {
        private readonly ApplicationContext context;
        public ShipOwnerRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public ShipOwner Find(int id)
        {
            return context.ShipOwners.First(x => x.Id == id);
        }
    }
}
