using ApplicationCore.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Web.Controllers;
using Web.Validation.Interfaces;
using Web.ViewModels;
using AutoMapper;

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
            var badReqest = result as BadRequestObjectResult;
            Assert.AreEqual(errorsList, badReqest.Value);
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

        [Test]
        public void ShouldReturnAllSchedules()
        {
            var sourceCollectionMock = new Mock<IQueryable<Schedule>>();
            var destinationCollection = new Mock<IQueryable<ScheduleViewModel>>();
            mapperMock.Setup(x => x.ProjectTo<ScheduleViewModel>(sourceCollectionMock.Object, null))
                .Returns(destinationCollection.Object);
            serviceMock.Setup(x => x.GetAll()).Returns(sourceCollectionMock.Object);

            var result = controller.GetAll();

            Assert.AreEqual(destinationCollection.Object, result);
            mapperMock.Verify(x => x.ProjectTo<ScheduleViewModel>(sourceCollectionMock.Object, null),
                Times.Once());
            serviceMock.Verify(x => x.GetAll(), Times.Once());
        }
        [Test]
        public void ShouldReturnBadRequestWhenDeletionFailed()
        {
            int idToDelete = 1;
            serviceMock.Setup(x => x.Delete(idToDelete)).Returns(false);

            var result = controller.Delete(idToDelete);

            serviceMock.Verify(x => x.Delete(idToDelete), Times.Once());
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldDeleteItemAndReturnOk()
        {
            int idToDelete = 1;
            serviceMock.Setup(x => x.Delete(idToDelete)).Returns(true);

            var result = controller.Delete(idToDelete);

            serviceMock.Verify(x => x.Delete(idToDelete), Times.Once());
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void ShouldReturnBadRequestWhenEntityToFindNotExists()
        {
            int idToFind = 1;
            Schedule emptySchedule = null;
            serviceMock.Setup(x => x.Find(idToFind)).Returns(emptySchedule);

            var result = controller.GetById(idToFind);

            serviceMock.Verify(x => x.Find(idToFind), Times.Once());
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldReturnModelWhenEntityExists()
        {
            int idToFind = 1;
            var entity = new Schedule();
            var model = new ScheduleViewModel();
            serviceMock.Setup(x => x.Find(idToFind)).Returns(entity);
            mapperMock.Setup(x => x.Map<ScheduleViewModel>(entity)).Returns(model);

            var result = controller.GetById(idToFind);

            serviceMock.Verify(x => x.Find(idToFind), Times.Once());
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(model, okResult.Value);
        }

        [Test]
        public void ShouldReturnBadRequestAndValidationErrorsWhenModelIsNotValidForUpdate()
        {
            var model = new ScheduleViewModel();
            validatorMock.Setup(x => x.IsValid(model)).Returns(false);
            var errorsList = new List<string>();
            validatorMock.SetupGet(x => x.ErrorList).Returns(errorsList);

            var result = controller.Update(model);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badReqest = result as BadRequestObjectResult;
            Assert.AreEqual(errorsList, badReqest.Value);
        }
        [Test]
        public void ShouldReturnBadRequestWhenEntityToUpdateNotExists()
        {
            var entity = new Schedule();
            var model = new ScheduleViewModel();
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            mapperMock.Setup(x => x.Map<Schedule>(model)).Returns(entity);
            serviceMock.Setup(x => x.Update(entity)).Returns(false);

            var result = controller.Update(model);

            serviceMock.Verify(x => x.Update(entity), Times.Once());
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldUpdateEntityWhenItExists()
        {
            var entity = new Schedule();
            var model = new ScheduleViewModel();
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            mapperMock.Setup(x => x.Map<Schedule>(model)).Returns(entity);
            serviceMock.Setup(x => x.Update(entity)).Returns(true);

            var result = controller.Update(model);

            serviceMock.Verify(x => x.Update(entity), Times.Once());
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
