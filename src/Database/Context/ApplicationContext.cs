using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public DbSet<ShipOwner> ShipOwners { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
