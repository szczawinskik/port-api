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
    public class ConfigurationServiceTests
    {
        private Mock<IConfigurationRepository> repositoryMock;
        private Mock<IApplicationLogger<ConfigurationService>> loggerMock;
        private ConfigurationService service;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IConfigurationRepository>();
            loggerMock = new Mock<IApplicationLogger<ConfigurationService>>();

            service = new ConfigurationService(repositoryMock.Object, loggerMock.Object);
        }

        [Test]
        public void ShoulReturnConfigurationValue()
        {
            var configurationType = ConfigurationType.RemoteServiceAddress;
            var configValue = "aaa";
            repositoryMock.Setup(x => x.GetConfigurationValue(configurationType))
                .Returns(configValue);

            var result = service.GetConfigurationValue(configurationType);

            Assert.AreEqual(configValue, result);
            repositoryMock.Verify(x => x.GetConfigurationValue(configurationType), Times.Once());
        }

        [Test]
        public void ShoulReturnNullAndCatchExceptionWhenGetFails()
        {
            var configurationType = ConfigurationType.RemoteServiceAddress;
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.GetConfigurationValue(configurationType))
                .Throws(expectedException);

            var result = service.GetConfigurationValue(configurationType);

            Assert.IsNull(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }
        [Test]
        public void ShoulSetConfigurationValue()
        {
            var configurationType = ConfigurationType.RemoteServiceAddress;
            var configValue = "aaa";

            var result = service.SetConfigurationValue(configurationType, configValue);

            Assert.IsTrue(result);
            repositoryMock.Verify(x => x.SetConfigurationValue(configurationType, configValue), Times.Once());
        }

        [Test]
        public void ShoulReturnFalseAndCatchExceptionWhenSetFails()
        {
            var configurationType = ConfigurationType.RemoteServiceAddress;
            var configValue = "aaa";
            var expectedException = new Exception();
            repositoryMock.Setup(x => x.SetConfigurationValue(configurationType, configValue))
                .Throws(expectedException);

            var result = service.SetConfigurationValue(configurationType, configValue);

            Assert.IsFalse(result);
            loggerMock.Verify(x => x.LogError(expectedException), Times.Once());
        }
    }
}
