using ApplicationCore.Entities;
using Database.Context;
using Database.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Tests.Repositories
{
    [TestFixture]
    public class ScheduleRepositoryTests
    {
        private Mock<IApplicationContext> contextMock;
        private ScheduleRepository repository;

        [SetUp]
        public void Setup()
        {
            contextMock = new Mock<IApplicationContext>();

            repository = new ScheduleRepository(contextMock.Object);
        }

        [Test]
        public void ShouldAddScheduleToDatabase()
        {
            var entity = new Schedule();
            var tempEntities = new List<Schedule>();
            var dbEntities = new List<Schedule>();
            contextMock.Setup(x => x.Schedules.Add(entity))
                .Callback<Schedule>(x => tempEntities.Add(entity));
            contextMock.Setup(x => x.SaveChanges())
                .Callback(() => dbEntities.AddRange(tempEntities));

            repository.Add(entity);

            Assert.AreEqual(1, dbEntities.Count);
        }
    }
}
