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
        public virtual DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ship>()
                .HasMany(s => s.Schedules)
                .WithOne(sch => sch.Ship);

            modelBuilder.Entity<Ship>()
                .HasOne(s => s.ClosestSchedule);
        }
    }
}
