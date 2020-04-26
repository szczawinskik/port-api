using ApplicationCore.Entities;
using Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Seed
{
    public class DbSeedInitializer
    {
        public static void SeedDatabase(ApplicationContext context)
        {
            if(!context.Configurations.Any(x => x.ConfigurationType == ConfigurationType.RemoteServiceAddress))
            {
                context.Configurations.Add(new Configuration
                {
                    ConfigurationType = ConfigurationType.RemoteServiceAddress,
                    Value = "http://localhost:4444/Port/ComplexMessage"
                });
            }
            if (context.ShipOwners.Any())
            {
                return;
            }

            var alabama = CreateShip("Alabama");
            var california = CreateShip("California");
            var mississippi = CreateShip("Mississippi");
            var queenElizabeth = CreateShip("Queen Elizabeth");
            var kingGeorge = CreateShip("King George V");
            var nagato = CreateShip("Nagato");
            var yamato = CreateShip("Yamato");

            var usNavy = new ShipOwner
            {
                Name = "United States Navy",
                Ships = new List<Ship> { alabama, california, mississippi }
            };

            var royalNavy = new ShipOwner
            {
                Name = "Royal Navy",
                Ships = new List<Ship> { queenElizabeth, kingGeorge }
            };

            var japaneseNavy = new ShipOwner
            {
                Name = "Imperial Japanese Navy",
                Ships = new List<Ship> { nagato, yamato }
            };

            var ships = new List<Ship> { alabama, california, mississippi, queenElizabeth,
                kingGeorge, nagato, yamato};

            AddRandomSchedules(ships);

            context.ShipOwners.Add(usNavy);
            context.ShipOwners.Add(royalNavy);
            context.ShipOwners.Add(japaneseNavy);

            context.SaveChanges();
        }

        private static Ship CreateShip(string name)
        {
            return new Ship
            {
                Name = name,
            };
        }

        private static void AddRandomSchedules(List<Ship> ships)
        {
            var rand = new Random();
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);
            foreach (var ship in ships)
            {
                var schedulesNumber = rand.Next() % 4 + 1;
                var schedules = new List<Schedule>();
                for (var i = 0; i < schedulesNumber; i++)
                {
                    var hours = rand.Next() % 10 + 1;
                    var days = rand.Next() % 2;
                    var minutes = rand.Next() % 60;
                    var hoursToDeparture = rand.Next() % 5 + 3;
                    schedules.Add(new Schedule
                    {
                        Arrival = now.AddDays(days).AddHours(hours).AddMinutes(minutes),
                        Departure = now.AddDays(days).AddHours(hours + hoursToDeparture).AddMinutes(minutes)
                    });
                    ship.Schedules = schedules;
                }
                ship.ClosestSchedule = schedules.OrderBy(x => x.Arrival).First();
            }
        }
    }
}
