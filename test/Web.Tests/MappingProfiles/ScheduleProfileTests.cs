using ApplicationCore.Entities;
using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Web.MappingProfiles;
using Web.ViewModels;

namespace Web.Tests.MappingProfiles
{
    [TestFixture]
    public class ScheduleProfileTests
    {
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var profile = new ScheduleProfile();
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(profile);
            });

            mapper = config.CreateMapper();
        }

        [Test]
        public void ShouldBeValid()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapViewModelToEntityWithoutSeconds()
        {
            var model = new ScheduleViewModel
            {
                Arrival = new DateTime(2020, 1, 2, 3, 4, 5, DateTimeKind.Local),
                Departure = new DateTime(2020, 6, 7, 8, 9, 10, DateTimeKind.Local),
                Id = 1,
            };
            var expectedArrivalDate = new DateTime(model.Arrival.Year, model.Arrival.Month, model.Arrival.Day,
                    model.Arrival.Hour, model.Arrival.Minute, 0, DateTimeKind.Local);
            var expectedDepartureDate = new DateTime(model.Departure.Year, model.Departure.Month,
                model.Departure.Day, model.Departure.Hour, model.Departure.Minute, 0, DateTimeKind.Local);

            var result = mapper.Map<Schedule>(model);

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(expectedArrivalDate, result.Arrival);
            Assert.AreEqual(expectedDepartureDate, result.Departure);
        }

        [Test]
        public void ShouldMapEntityToModel()
        {
            var entity = new Schedule
            {
                Arrival = new DateTime(2020, 1, 2, 3, 4, 0, DateTimeKind.Local),
                Departure = new DateTime(2020, 6, 7, 8, 9, 0, DateTimeKind.Local),
                Id = 1,
            };

            var result = mapper.Map<ScheduleViewModel>(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.Arrival, result.Arrival);
            Assert.AreEqual(entity.Departure, result.Departure);
        }
    }
}
