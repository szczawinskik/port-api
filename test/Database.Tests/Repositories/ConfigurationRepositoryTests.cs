using ApplicationCore.Entities;
using Database.Context;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Tests.Repositories
{
    [TestFixture]
    public class ConfigurationRepositoryTests
    {
        private Mock<ApplicationContext> contextMock;
        private ConfigurationRepository repository;

        [SetUp]
        public void Setup()
        {
            contextMock = new Mock<ApplicationContext>();

            repository = new ConfigurationRepository(contextMock.Object);
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
        public void ShouldReturnConfigurationValue()
        {
            var value = "aaa";
            var entity = new Configuration { ConfigurationType = ConfigurationType.RemoteServiceAddress, Value = value };
            var dbEntities = new List<Configuration> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Configurations).Returns(dbSet);

            var result = repository.GetConfigurationValue(ConfigurationType.RemoteServiceAddress);

            Assert.AreEqual(value, result);
        }

        [Test]
        public void ShouldSetConfigurationValue()
        {
            var value = "aaa";
            var newValue = "bbb";
            var callback = 0;
            var entity = new Configuration { ConfigurationType = ConfigurationType.RemoteServiceAddress, Value = value };
            var dbEntities = new List<Configuration> { entity };
            var dbSet = GetQueryableMockDbSet(dbEntities);
            contextMock.SetupGet(x => x.Configurations)
                .Callback(() => Assert.AreEqual(0, callback++))
                .Returns(dbSet);
            contextMock.Setup(x => x.SaveChanges())
                 .Callback(() => Assert.AreEqual(1, callback++));

            repository.SetConfigurationValue(ConfigurationType.RemoteServiceAddress, newValue);

            Assert.AreEqual(newValue, entity.Value);
            contextMock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
