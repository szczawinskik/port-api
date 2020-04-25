using ApplicationCore.Entities;
using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.MappingProfiles;
using Web.ViewModels;

namespace Web.Tests.MappingProfiles
{
    [TestFixture]
    public class ShipProfileTests
    {
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var shipProfile = new ShipProfile();
            var scheduleProfile = new ScheduleProfile();
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(scheduleProfile);
                opt.AddProfile(shipProfile);
            });

            mapper = config.CreateMapper();
        }

        [Test]
        public void ShouldBeValid()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapEntityToAggregateViewModel()
        {
            var model = new Ship
            {
                Id = 1,
                Name = "ship_name",
                ClosestSchedule = new Schedule
                {
                    Arrival = new DateTime(2020, 10, 10),
                    Departure = new DateTime(2020, 10, 9)
                },
                ShipOwner = new ShipOwner
                {
                    Name = "test"
                }
            };

            var result = mapper.Map<ShipAggregateViewModel>(model);

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreEqual(model.ShipOwner.Name, result.ShipOwnerName);
            Assert.AreEqual(model.ClosestSchedule.Arrival, result.ClosestSchedule.Arrival);
            Assert.AreEqual(model.ClosestSchedule.Departure, result.ClosestSchedule.Departure);
        }


        [Test]
        public void ShouldMapEntityToViewModel()
        {
            var model = new Ship
            {
                Id = 1,
                Name = "ship_name",
                ClosestSchedule = new Schedule
                {
                    Arrival = new DateTime(2020, 10, 10),
                    Departure = new DateTime(2020, 10, 9)
                },
                ShipOwner = new ShipOwner
                {
                    Name = "test"
                },
                Schedules = new List<Schedule>
                {
                    new Schedule{Id = 1, Arrival = new DateTime(2020, 1, 1), Departure = new DateTime(2020, 2, 2)},
                    new Schedule{Id = 2, Arrival = new DateTime(2020, 1, 2), Departure = new DateTime(2020, 2, 3)}
                }
            };

            var result = mapper.Map<ShipViewModel>(model);

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreEqual(model.ShipOwner.Name, result.ShipOwnerName);
            Assert.AreEqual(model.ClosestSchedule.Arrival, result.ClosestSchedule.Arrival);
            Assert.AreEqual(model.ClosestSchedule.Departure, result.ClosestSchedule.Departure);
            Assert.AreEqual(model.Schedules.Count(), result.Schedules.Count);
            Assert.AreEqual(model.Schedules.ElementAt(0).Id, result.Schedules[0].Id);
            Assert.AreEqual(model.Schedules.ElementAt(0).Arrival, result.Schedules[0].Arrival);
            Assert.AreEqual(model.Schedules.ElementAt(0).Departure, result.Schedules[0].Departure);
            Assert.AreEqual(model.Schedules.ElementAt(1).Id, result.Schedules[1].Id);
            Assert.AreEqual(model.Schedules.ElementAt(1).Arrival, result.Schedules[1].Arrival);
            Assert.AreEqual(model.Schedules.ElementAt(1).Departure, result.Schedules[1].Departure);
        }

        [Test]
        public void ShouldMapViewModelToEntity()
        {
            var model = new ShipViewModel
            {
                Id = 1,
                Name = "ship_name",
                ClosestSchedule = new ScheduleViewModel
                {
                    Arrival = new DateTime(2020, 10, 10),
                    Departure = new DateTime(2020, 10, 9)
                },
                Schedules = new List<ScheduleViewModel>
                {
                    new ScheduleViewModel{Id = 1, Arrival = new DateTime(2020, 1, 1), Departure = new DateTime(2020, 2, 2)},
                    new ScheduleViewModel{Id = 2, Arrival = new DateTime(2020, 1, 2), Departure = new DateTime(2020, 2, 3)}
                }
            };

            var result = mapper.Map<ShipViewModel>(model);

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreEqual(model.ClosestSchedule.Arrival, result.ClosestSchedule.Arrival);
            Assert.AreEqual(model.ClosestSchedule.Departure, result.ClosestSchedule.Departure);
            Assert.AreEqual(model.Schedules.Count(), result.Schedules.Count);
            Assert.AreEqual(model.Schedules.ElementAt(0).Id, result.Schedules[0].Id);
            Assert.AreEqual(model.Schedules.ElementAt(0).Arrival, result.Schedules[0].Arrival);
            Assert.AreEqual(model.Schedules.ElementAt(0).Departure, result.Schedules[0].Departure);
            Assert.AreEqual(model.Schedules.ElementAt(1).Id, result.Schedules[1].Id);
            Assert.AreEqual(model.Schedules.ElementAt(1).Arrival, result.Schedules[1].Arrival);
            Assert.AreEqual(model.Schedules.ElementAt(1).Departure, result.Schedules[1].Departure);
        }
    }
}
