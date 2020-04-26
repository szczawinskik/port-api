using ApplicationCore.Entities;
using Database.Context;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Tests.Repositories
{
    [TestFixture]
    public class ScheduleRepositoryTests
    {
        private int shipId;
        private Mock<ApplicationContext> contextMock;
        private ScheduleRepository repository;

        [SetUp]
        public void Setup()
        {
            shipId = 1;
            contextMock = new Mock<ApplicationContext>();

            repository = new ScheduleRepository(contextMock.Object);
        }

        [Test]
        public void ShouldAddScheduleToDatabase()
        {
            var entity = new Schedule { Arrival = DateTime.Now.AddMinutes(10) };
            var tempEntities = new List<Schedule>();
            var dbEntities = new List<Schedule>();
            contextMock.Setup(x => x.Schedules.Add(entity))
                .Callback<Schedule>(x => tempEntities.Add(entity));
            contextMock.Setup(x => x.SaveChanges())
                .Callback(() => dbEntities.AddRange(tempEntities));

            repository.Add(entity);

            Assert.AreEqual(1, dbEntities.Count);
        }

        [Test]
        public void ShouldDeleteEntityFromDatabase()
        {
            var entity = new Schedule { Id = 1 };
            Schedule entityToDelete = null;
            var dbEntities = new List<Schedule> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Schedules).Returns(dbSet);
            contextMock.Setup(x => x.Remove(entity))
                .Callback<Schedule>(x => entityToDelete = x);
            contextMock.Setup(x => x.SaveChanges())
              .Callback(() => dbEntities.Remove(entityToDelete));

            repository.Delete(entity.Id);

            Assert.AreEqual(0, dbEntities.Count);
            contextMock.Verify(x => x.Remove(entity), Times.Once());

        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        [Test]
        public void ShouldReturnAllEntities()
        {
            var entity = new Schedule { Id = 1 };
            var dbEntities = new List<Schedule> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Schedules).Returns(dbSet);

            var result = repository.GetAll();

            Assert.AreEqual(dbSet, result);
        }

        [Test]
        public void ShouldReturnEntityById()
        {
            var entity = new Schedule { Id = 1 };
            var dbEntities = new List<Schedule> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Schedules).Returns(dbSet);

            var result = repository.Find(entity.Id);

            Assert.AreEqual(entity, result);
        }

        [Test]
        public void ShouldUpdateEntity()
        {
            var entity = new Schedule
            {
                Id = 1,
                Arrival = new DateTime(2020, 10, 10),
                Departure = new DateTime(2020, 10, 11)
            };
            var existingEntity = new Schedule
            {
                Id = 1,
                Arrival = new DateTime(2020, 9, 10),
                Departure = new DateTime(2020, 9, 11)
            };
            var dbEntities = new List<Schedule> { existingEntity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Schedules).Returns(dbSet);

            repository.Update(entity);

            Assert.AreEqual(entity.Arrival, existingEntity.Arrival);
            Assert.AreEqual(entity.Departure, existingEntity.Departure);
            contextMock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
