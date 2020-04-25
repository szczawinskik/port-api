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
    public class ShipProfileTests
    {
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var profile = new ShipProfile();
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
            var model = new ShipViewModel
            {
                Id = 1,
                Name = "ship_name"
            };

            var result = mapper.Map<Ship>(model);

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.Name, result.Name);
        }

        [Test]
        public void ShouldMapEntityToModel()
        {
            var model = new Ship
            {
                Id = 1,
                Name = "ship_name"
            };

            var result = mapper.Map<ShipViewModel>(model);

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.Name, result.Name);
        }
    }
}
