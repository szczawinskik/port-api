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
    public class ScheduleServiceTests
    {
        private ScheduleService service;
        private Mock<IBaseRepository<Schedule>> repositoryMock;
        private Mock<IApplicationLogger<ScheduleService>> loggerMock;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IBaseRepository<Schedule>>();
            loggerMock = new Mock<IApplicationLogger<ScheduleService>>();

            service = new ScheduleService(repositoryMock.Object, loggerMock.Object);
        }
        [Test]
        public void ShouldAddToRepositoryAndReturnTrue()
        {
            var entity = new Schedule();

            var result = service.Add(entity);

            Assert.IsTrue(result);
            repositoryMock.Verify(x => x.Add(entity), Times.Once());
        }

        [Test]
        public void ShouldReturnFalseAndLogErrorWhenRepositoryAddFails()
        {
            var entity = new Schedule();
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.Add(entity)).Throws(expectedException);

            var result = service.Add(entity);

            Assert.IsFalse(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }

        [Test]
        public void ShouldDeleteFromRepositoryAndReturnTrue()
        {
            var idToDelete = 1;

            var result = service.Delete(idToDelete);

            Assert.IsTrue(result);
            repositoryMock.Verify(x => x.Delete(idToDelete), Times.Once());
        }

        [Test]
        public void ShouldReturnFalseAndLogErrorWhenRepositoryDeleteFails()
        {
            var idToDelete = 1;
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
        public void ShouldReturnFalseAndLogErrorWhenRepositoryFindFails()
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
    }
}
