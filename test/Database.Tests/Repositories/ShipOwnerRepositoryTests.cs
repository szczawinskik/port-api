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
    public class ShipOwnerRepositoryTests
    {
        private Mock<ApplicationContext> contextMock;
        private ShipOwnerRepository repository;

        [SetUp]
        public void Setup()
        {
            contextMock = new Mock<ApplicationContext>();

            repository = new ShipOwnerRepository(contextMock.Object);
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
        public void ShouldReturnEntityById()
        {
            var entity = new ShipOwner { Id = 1, Name = "test" };
            var dbEntities = new List<ShipOwner> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.ShipOwners).Returns(dbSet);

            var result = repository.Find(entity.Id);

            Assert.AreEqual(entity, result);
        }
    }
}
