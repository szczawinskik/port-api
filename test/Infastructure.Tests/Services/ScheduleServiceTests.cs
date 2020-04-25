using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Tests.Services
{
    [TestFixture]
    public class ScheduleServiceTests
    {
        private ScheduleService service;
        private Mock<IBaseRepository<Schedule>> repositoryMock;
        private Mock<ILogger<ScheduleService>> loggerMock;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IBaseRepository<Schedule>>();
            loggerMock = new Mock<ILogger<ScheduleService>>();

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
    }
}
