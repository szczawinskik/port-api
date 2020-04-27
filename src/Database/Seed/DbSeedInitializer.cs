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
        private static Random rand = new Random();
        public static void SeedDatabase(ApplicationContext context)
        {
            if (!context.Configurations.Any(x => x.ConfigurationType == ConfigurationType.RemoteServiceAddress))
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

            var shipAlreadyInPort = CreateShip("Alabama");
            var shipThatWillArrive = CreateShip("California");
            var shipWithoutArrival = CreateShip("Mississippi");
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);


            var scheduleInMinute = new Schedule
            {
                Arrival = now.AddHours(-1),
                Departure = now.AddMinutes(2),
                ArrivalSent = true,
            };

            var oldSchedule = new Schedule
            {
                Arrival = now.AddDays(-1),
                Departure = now.AddHours(-2),
                ArrivalSent = true,
                DepartureSent = true
            };

            var schedule2 = new Schedule
            {
                Arrival = now.AddMinutes(3),
                Departure = now.AddMinutes(8),
                ArrivalSent = false,
                DepartureSent = false
            };

            var futureSchedule = new Schedule
            {
                Arrival = now.AddHours(10),
                Departure = now.AddHours(15),
                ArrivalSent = false,
            };

            var schedule3 = new Schedule
            {
                Arrival = now.AddHours(20),
                Departure = now.AddMinutes(30),
                ArrivalSent = false,
            };

            var oldSchedules = new List<Schedule>();
            oldSchedules.AddRange(CreateSchedulesTwoWeeksBefore(now));
            oldSchedules.AddRange(CreateSchedulesWeekBefore(now));
            shipWithoutArrival.Schedules = oldSchedules;

            shipAlreadyInPort.Schedules = new List<Schedule> { scheduleInMinute, futureSchedule, schedule3 };
            shipAlreadyInPort.ClosestSchedule = scheduleInMinute;

            shipThatWillArrive.Schedules = new List<Schedule> { schedule2 };
            shipThatWillArrive.ClosestSchedule = schedule2;
            var shipOwner = new ShipOwner
            {
                Ships = new List<Ship> { shipThatWillArrive, shipAlreadyInPort, shipWithoutArrival },
                Name = "United States Navy"
            };
            context.ShipOwners.Add(shipOwner);

            context.SaveChanges();
        }

        private static Ship CreateShip(string name)
        {
            return new Ship
            {
                Name = name,
            };
        }

        private static List<Schedule> CreateSchedulesTwoWeeksBefore(DateTime now)
        {
            return new List<Schedule>
           {
              new Schedule
              {
                  Arrival = now.AddDays(-14).AddHours(1).AddMinutes(rand.Next() % 100),
                  Departure = now.AddDays(-13).AddHours(2).AddMinutes(rand.Next() % 100),
                  ArrivalSent = true,
                  DepartureSent = true
              },
               new Schedule
              {
                  Arrival = now.AddDays(-12).AddHours(-2).AddMinutes(rand.Next() % 100),
                  Departure = now.AddDays(-11).AddHours(-5).AddMinutes(rand.Next() % 100),
                  ArrivalSent = true,
                  DepartureSent = true
              }
              ,
               new Schedule
              {
                  Arrival = now.AddDays(-10).AddHours(1).AddMinutes(rand.Next() % 100),
                  Departure = now.AddDays(-9).AddHours(5).AddMinutes(rand.Next() % 100),
                  ArrivalSent = true,
                  DepartureSent = true,
              }
           };
        }

        private static List<Schedule> CreateSchedulesWeekBefore(DateTime now)
        {
            return new List<Schedule>
           {
              new Schedule
              {
                  Arrival = now.AddDays(-5).AddHours(-3).AddMinutes(rand.Next() % 100),
                  Departure = now.AddDays(-4).AddHours(-2).AddMinutes(rand.Next() % 100),
                  ArrivalSent = true,
                  DepartureSent = true
              }
              ,
               new Schedule
              {
                  Arrival = now.AddDays(-2).AddHours(1).AddMinutes(rand.Next() % 100),
                  Departure = now.AddHours(-3).AddMinutes(rand.Next() % 100),
                  ArrivalSent = true,
                  DepartureSent = true,
              }
           };

        }
    }
}
