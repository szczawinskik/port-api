//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Web.Controllers;
//using Web.Validation.Interfaces;
//using Web.ViewModels;

//namespace Web.Tests.Controllers
//{
//    [TestFixture]
//    public class ScheduleControllerTests
//    {
//        private Mock<IValidator<ScheduleViewModel>> validatorMock;
//        private ScheduleController controller;

//        [SetUp]
//        public void Setup()
//        {
//            validatorMock = new Mock<IValidator<ScheduleViewModel>>();
//            controller = new ScheduleController(validatorMock.Object);
//        }
//        [Test]
//        public void ShouldReturnBadRequestAndValidationErrorsWhenModelIsNotValid()
//        {
//            var model = new ScheduleViewModel();
//            validatorMock.Setup(x => x.IsValid(model)).Returns(false);
//            var errorsList = new List<string>();
//            validatorMock.SetupGet(x => x.ErrorList).Returns(errorsList);

//            var result = controller.Add(model);

//            Assert.IsInstanceOf<BadRequestObjectResult>(result);
//            var badReqest = (BadRequestObjectResult)result;
//            Assert.AreEqual(badReqest.Value, errorsList);
//        }
//        [Test]
//        public void ShouldAddItemWhenModelIsValid()
//        {
//            var model = new ScheduleViewModel();
//            validatorMock.Setup(x => x.IsValid(model)).Returns(true);

//            var result = controller.Add(model);

//            Assert.IsInstanceOf<OkResult>(result);
//        }
//    }
//}
