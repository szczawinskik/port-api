using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Tests.Controllers
{
    [TestFixture]
    public class ScheduleControllerTests
    {
        private Mock<IValidator<ScheduleViewModel>> validatorMock;
        private Mock<IService<Schedule>> serviceMock;
        private Mock<IMapper> mapperMock;
        private ScheduleController controller;

        [SetUp]
        public void Setup()
        {
            validatorMock = new Mock<IValidator<ScheduleViewModel>>();
            serviceMock = new Mock<IService<Schedule>>();
            mapperMock = new Mock<IMapper>();

            controller = new ScheduleController(validatorMock.Object, serviceMock.Object,
                mapperMock.Object);
        }
        [Test]
        public void ShouldReturnBadRequestAndValidationErrorsWhenModelIsNotValid()
        {
            var model = new ScheduleViewModel();
            validatorMock.Setup(x => x.IsValid(model)).Returns(false);
            var errorsList = new List<string>();
            validatorMock.SetupGet(x => x.ErrorList).Returns(errorsList);

            var result = controller.Add(model);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badReqest = (BadRequestObjectResult)result;
            Assert.AreEqual(badReqest.Value, errorsList);
        }
        [Test]
        public void ShouldAddItemWhenModelIsValid()
        {
            var model = new ScheduleViewModel();
            var entity = new Schedule();
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            mapperMock.Setup(x => x.Map<Schedule>(model)).Returns(entity);
            serviceMock.Setup(x => x.Add(entity)).Returns(true);

            var result = controller.Add(model);

            Assert.IsInstanceOf<OkResult>(result);
            mapperMock.Verify(x => x.Map<Schedule>(model));
            serviceMock.Verify(x => x.Add(entity), Times.Once());
        }

        [Test]
        public void ShouldReturnBadRequestWhenAddFails()
        {
            var model = new ScheduleViewModel();
            var entity = new Schedule();
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            mapperMock.Setup(x => x.Map<Schedule>(model)).Returns(entity);
            serviceMock.Setup(x => x.Add(entity)).Returns(false);

            var result = controller.Add(model);

            Assert.IsInstanceOf<BadRequestResult>(result);
            mapperMock.Verify(x => x.Map<Schedule>(model));
            serviceMock.Verify(x => x.Add(entity), Times.Once());
        }
    }
}
