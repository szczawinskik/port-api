﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infastructure.Tests.Services
{
    [TestFixture]
    public class ShipServiceTests
    {
        private ShipService service;
        private int shipOwnerId;
        private Mock<IBaseRepository<Ship>> repositoryMock;
        private Mock<IApplicationLogger<ShipService>> loggerMock;

        [SetUp]
        public void Setup()
        {
            shipOwnerId = 1;
            repositoryMock = new Mock<IBaseRepository<Ship>>();
            loggerMock = new Mock<IApplicationLogger<ShipService>>();

            service = new ShipService(repositoryMock.Object, loggerMock.Object);
        }
     
        [Test]
        public void ShouldReturnEntityIfItExists()
        {
            var idToFind = 1;
            var entity = new Ship();
            repositoryMock.Setup(x => x.Find(idToFind)).Returns(entity);

            var result = service.Find(idToFind);

            Assert.AreEqual(entity, result);
            repositoryMock.Verify(x => x.Find(idToFind), Times.Once());
        }

        [Test]
        public void ShouldReturnNullAndLogErrorWhenRepositoryFindFails()
        {
            var idToFind = 1;
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.Find(idToFind)).Throws(expectedException);

            var result = service.Find(idToFind);

            Assert.IsNull(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }

        [Test]
        public void ShouldReturnAllEntities()
        {
            var entityList = new List<Ship>().AsQueryable();
            repositoryMock.Setup(x => x.GetAll()).Returns(entityList);

            var result = service.GetAll();

            Assert.AreEqual(entityList, result);
            repositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void ShouldAddToRepositoryAndReturnTrue()
        {
            var entity = new Ship();

            var result = service.Add(entity, shipOwnerId);

            Assert.IsTrue(result);
            repositoryMock.Verify(x => x.Add(entity, shipOwnerId), Times.Once());
        }

        [Test]
        public void ShouldReturnFalseAndLogErrorWhenRepositoryAddFails()
        {
            var entity = new Ship();
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.Add(entity, shipOwnerId)).Throws(expectedException);

            var result = service.Add(entity, shipOwnerId);

            Assert.IsFalse(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }
    }
}
