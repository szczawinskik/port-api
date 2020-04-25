using ApplicationCore.Entities;
using Database.Context;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private Mock<ApplicationContext> contextMock;
        private ScheduleRepository repository;

        [SetUp]
        public void Setup()
        {
            contextMock = new Mock<ApplicationContext>();

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
            var order = 0;
            var entity = new Schedule { Id = 1 };
            var entityUpdate = new Schedule { Id = 1 };
            var dbEntities = new List<Schedule> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Schedules).Returns(dbSet);
            contextMock.Setup(x => x.Update(entityUpdate))
                .Callback(() => Assert.That(order++, Is.EqualTo(0)));
            contextMock.Setup(x => x.SaveChanges())
              .Callback(() => Assert.That(order++, Is.EqualTo(1)));

            var result = repository.Update(entityUpdate);

            Assert.AreEqual(entityUpdate, result);
            contextMock.Verify(x => x.Update(entityUpdate), Times.Once());
            contextMock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
