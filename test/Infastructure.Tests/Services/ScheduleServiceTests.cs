using ApplicationCore.Entities;
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
    public class ScheduleServiceTests
    {
        private ScheduleService service;
        private int shipId;
        private Mock<IScheduleRepository> repositoryMock;
        private Mock<IBaseRepository<Ship>> shipRepositoryMock;
        private Mock<IApplicationLogger<ScheduleService>> loggerMock;

        [SetUp]
        public void Setup()
        {
            shipId = 1;
            repositoryMock = new Mock<IScheduleRepository>();
            shipRepositoryMock = new Mock<IBaseRepository<Ship>>();
            loggerMock = new Mock<IApplicationLogger<ScheduleService>>();

            service = new ScheduleService(repositoryMock.Object, shipRepositoryMock.Object, loggerMock.Object);
        }
        [Test]
        public void ShouldAddToRepositoryAndReturnTrue()
        {
            var ship = new Ship();
            shipRepositoryMock.Setup(x => x.Find(shipId)).Returns(ship);
            var entity = new Schedule();

            var result = service.Add(entity, shipId);

            Assert.IsTrue(result);
            Assert.AreEqual(ship, entity.Ship);
            repositoryMock.Verify(x => x.Add(entity), Times.Once());
            shipRepositoryMock.Verify(x => x.Find(shipId), Times.Once());
        }

        [Test]
        public void ShouldReturnFalseAndLogErrorWhenRepositoryAddFails()
        {
            var entity = new Schedule();
            var expectedException = new Exception();
            shipRepositoryMock.Setup(x => x.Find(shipId)).Returns(new Ship());
            repositoryMock.Setup(x => x.Add(entity)).Throws(expectedException);

            var result = service.Add(entity, shipId);

            Assert.IsFalse(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }

        [Test]
        public void ShouldDeleteFromRepositoryAndReturnTrue()
        {
            var idToDelete = 1;
            var entity = new Schedule { Id = idToDelete, ShipId = shipId };
            shipRepositoryMock.Setup(x => x.Find(shipId)).Returns(new Ship { Schedules = new List<Schedule>()});
            repositoryMock.Setup(x => x.Find(idToDelete)).Returns(entity);
            repositoryMock.Setup(x => x.Update(entity));

            var result = service.Delete(idToDelete);

            Assert.IsTrue(result);
            repositoryMock.Verify(x => x.Delete(idToDelete), Times.Once());
        }

        [Test]
        public void ShouldReturnFalseAndLogErrorWhenRepositoryDeleteFails()
        {
            var idToDelete = 1;
            var entity = new Schedule { Id = idToDelete, ShipId = shipId };
            shipRepositoryMock.Setup(x => x.Find(shipId)).Returns(new Ship { Schedules = new List<Schedule>() });
            repositoryMock.Setup(x => x.Find(idToDelete)).Returns(entity);
            repositoryMock.Setup(x => x.Update(entity));
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.Delete(idToDelete)).Throws(expectedException);

            var result = service.Delete(idToDelete);

            Assert.IsFalse(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }

        [Test]
        public void ShouldReturnEntityIfItExists()
        {
            var idToFind = 1;
            var entity = new Schedule();
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
            var entityList = new List<Schedule>().AsQueryable();
            repositoryMock.Setup(x => x.GetAll()).Returns(entityList);

            var result = service.GetAll();

            Assert.AreEqual(entityList, result);
            repositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void ShouldReturnEmptyListAndLogErrorWhenFetchFails()
        {
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.GetAll()).Throws(expectedException);

            var result = service.GetAll();

            Assert.AreEqual(0, result.Count());
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }

        [Test]
        public void ShouldUpdateEntity()
        {
            var entity = new Schedule { ShipId = shipId, Id = 1 };
            shipRepositoryMock.Setup(x => x.Find(shipId)).Returns(new Ship());
            repositoryMock.Setup(x => x.Find(entity.Id)).Returns(entity);
            repositoryMock.Setup(x => x.Update(entity));

            var result = service.Update(entity);

            repositoryMock.Verify(x => x.Update(entity), Times.Once());
        }

        [Test]
        public void ShouldReturnNullAndLogErrorWhenFetchFails()
        {
            var expectedException = new Exception();
            var entity = new Schedule { ShipId = shipId, Id = 1 };
            shipRepositoryMock.Setup(x => x.Find(shipId)).Returns(new Ship());
            repositoryMock.Setup(x => x.Update(entity)).Throws(expectedException); 
            repositoryMock.Setup(x => x.Find(entity.Id)).Returns(entity); 

            var result = service.Update(entity);

            Assert.IsFalse(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }
    }
}
