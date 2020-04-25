using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public ApplicationContext() { }
        public virtual DbSet<ShipOwner> ShipOwners { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }

    }
}
