using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public interface IApplicationContext
    {
        DbSet<ShipOwner> ShipOwners { get; set; }
        DbSet<Ship> Ships { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        int SaveChanges();
    }
}
