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

namespace Database.Tests.TestDoubles
{
    [TestFixture]
    public class ShipRepositoryTests
    {
        private int shipOwnerId;
        private Mock<ApplicationContext> contextMock;
        private ShipRepository repository;

        [SetUp]
        public void Setup()
        {
            shipOwnerId = 1;
            contextMock = new Mock<ApplicationContext>();

            repository = new ShipRepository(contextMock.Object);
        }

        [Test]
        public void ShouldAddShipToDatabase()
        {
            var entity = new Ship { };
            var tempEntities = new List<Ship>();
            var dbEntities = new List<Ship>();
            var shipOwner = new ShipOwner { Id = shipOwnerId };
            var shipOwnerEntities = new List<ShipOwner> { shipOwner };
            var dbSet = GetQueryableMockDbSet(shipOwnerEntities);
            contextMock.SetupGet(x => x.ShipOwners).Returns(dbSet);
            contextMock.Setup(x => x.Ships.Add(entity))
                .Callback<Ship>(x => tempEntities.Add(entity));
            contextMock.Setup(x => x.SaveChanges())
                .Callback(() => dbEntities.AddRange(tempEntities));

            repository.Add(entity, shipOwnerId);

            Assert.AreEqual(1, dbEntities.Count);
            Assert.AreEqual(entity.ShipOwner, shipOwner);
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
            var entity = new Ship { Id = 1 };
            var dbEntities = new List<Ship> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Ships).Returns(dbSet);

            var result = repository.GetAll();

            Assert.AreEqual(dbSet, result);
        }

        [Test]
        public void ShouldReturnEntityById()
        {
            var entity = new Ship { Id = 1, ClosestSchedule = new Schedule { Arrival = DateTime.Now } };
            var dbEntities = new List<Ship> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Ships).Returns(dbSet);

            var result = repository.Find(entity.Id);

            Assert.AreEqual(entity, result);
        }
    }
}
