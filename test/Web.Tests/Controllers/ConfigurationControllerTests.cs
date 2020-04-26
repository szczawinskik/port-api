using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Validation.Interfaces;
using Web.ViewModels;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Tests.Controllers
{
    [TestFixture]
    public class ConfigurationControllerTests
    {
        private Mock<IConfigurationService> serviceMock;
        private Mock<IValidator<RemoteServiceAddressViewModel>> validatorMock;
        private ConfigurationController controller;

        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IConfigurationService>();
            validatorMock = new Mock<IValidator<RemoteServiceAddressViewModel>>();

            controller = new ConfigurationController(serviceMock.Object, validatorMock.Object);
        }

        [Test]
        public void ShouldReturnRemoteServiceAddress()
        {
            var address = "address";
            serviceMock.Setup(x => x.GetConfigurationValue(ConfigurationType.RemoteServiceAddress))
                .Returns(address);

            var result = controller.RemoteServiceAddress();

            Assert.AreEqual(address, result);
            serviceMock.Verify(x => x.GetConfigurationValue(ConfigurationType.RemoteServiceAddress),
                Times.Once());
        }

        [Test]
        public void ShouldReturnBadRequestAndValidationErrorsWhenModelIsNotValid()
        {
            var model = new RemoteServiceAddressViewModel();
            validatorMock.Setup(x => x.IsValid(model)).Returns(false);
            var errorsList = new List<string>();
            validatorMock.SetupGet(x => x.ErrorList).Returns(errorsList);

            var result = controller.RemoteServiceAddress(model);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badReqest = result as BadRequestObjectResult;
            Assert.AreEqual(errorsList, badReqest.Value);
        }
        [Test]
        public void ShouldAddItemWhenModelIsValid()
        {
            var model = new RemoteServiceAddressViewModel {Value = "address" };
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            serviceMock.Setup(x => x.SetConfigurationValue(ConfigurationType.RemoteServiceAddress, model.Value))
                .Returns(true);

            var result = controller.RemoteServiceAddress(model);

            Assert.IsInstanceOf<OkResult>(result);
            serviceMock.Verify(x => x.SetConfigurationValue(ConfigurationType.RemoteServiceAddress, model.Value),
                Times.Once());
        }

        [Test]
        public void ShouldReturnBadRequestWhenAddFails()
        {
            var model = new RemoteServiceAddressViewModel {Value = "address" };
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            serviceMock.Setup(x => x.SetConfigurationValue(ConfigurationType.RemoteServiceAddress, model.Value))
              .Returns(false);

            var result = controller.RemoteServiceAddress(model);

            Assert.IsInstanceOf<BadRequestResult>(result);
            serviceMock.Verify(x => x.SetConfigurationValue(ConfigurationType.RemoteServiceAddress, model.Value),
               Times.Once());
        }
    }
}
