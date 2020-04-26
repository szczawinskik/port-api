using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Controllers;
using Web.Validation.Interfaces;
using Web.ViewModels;

namespace Web.Tests.Controllers
{
    [TestFixture]
    public class ShipControllerTests
    {
        private int shipOwnerId;
        private Mock<IService<Ship>> serviceMock;
        private Mock<IMapper> mapperMock;
        private Mock<IValidator<ShipViewModel>> validatorMock;
        private ShipController controller;

        [SetUp]
        public void Setup()
        {
            shipOwnerId = 1;
            serviceMock = new Mock<IService<Ship>>();
            mapperMock = new Mock<IMapper>();
            validatorMock = new Mock<IValidator<ShipViewModel>>();

            controller = new ShipController(serviceMock.Object, mapperMock.Object, validatorMock.Object);
        }

        [Test]
        public void ShouldReturnAllShips()
        {
            var sourceCollectionMock = new List<Ship>().AsQueryable();
            var destinationCollection = new Mock<IQueryable<ShipAggregateViewModel>>();
            mapperMock.Setup(x => x.ProjectTo<ShipAggregateViewModel>(sourceCollectionMock, null))
                .Returns(destinationCollection.Object);
            serviceMock.Setup(x => x.GetAll()).Returns(sourceCollectionMock);

            var result = controller.GetAll();

            Assert.AreEqual(destinationCollection.Object, result);
            mapperMock.Verify(x => x.ProjectTo<ShipAggregateViewModel>(sourceCollectionMock, null),
                Times.Once());
            serviceMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void ShouldReturnBadRequestWhenEntityToFindNotExists()
        {
            int idToFind = 1;
            Ship emptyShip = null;
            serviceMock.Setup(x => x.Find(idToFind)).Returns(emptyShip);

            var result = controller.GetById(idToFind);

            serviceMock.Verify(x => x.Find(idToFind), Times.Once());
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ShouldReturnModelWhenEntityExists()
        {
            int idToFind = 1;
            var entity = new Ship();
            var model = new ShipViewModel();
            serviceMock.Setup(x => x.Find(idToFind)).Returns(entity);
            mapperMock.Setup(x => x.Map<ShipViewModel>(entity)).Returns(model);

            var result = controller.GetById(idToFind);

            serviceMock.Verify(x => x.Find(idToFind), Times.Once());
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(model, okResult.Value);
        }
        [Test]
        public void ShouldReturnBadRequestAndValidationErrorsWhenModelIsNotValid()
        {
            var model = new ShipViewModel();
            validatorMock.Setup(x => x.IsValid(model)).Returns(false);
            var errorsList = new List<string>();
            validatorMock.SetupGet(x => x.ErrorList).Returns(errorsList);

            var result = controller.Add(model, shipOwnerId);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badReqest = result as BadRequestObjectResult;
            Assert.AreEqual(errorsList, badReqest.Value);
        }
        [Test]
        public void ShouldAddItemWhenModelIsValid()
        {
            var model = new ShipViewModel();
            var entity = new Ship();
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            mapperMock.Setup(x => x.Map<Ship>(model)).Returns(entity);
            serviceMock.Setup(x => x.Add(entity, shipOwnerId)).Returns(true);

            var result = controller.Add(model, shipOwnerId);

            Assert.IsInstanceOf<OkResult>(result);
            mapperMock.Verify(x => x.Map<Ship>(model));
            serviceMock.Verify(x => x.Add(entity, shipOwnerId), Times.Once());
        }

        [Test]
        public void ShouldReturnBadRequestWhenAddFails()
        {
            var model = new ShipViewModel();
            var entity = new Ship();
            validatorMock.Setup(x => x.IsValid(model)).Returns(true);
            mapperMock.Setup(x => x.Map<Ship>(model)).Returns(entity);
            serviceMock.Setup(x => x.Add(entity, shipOwnerId)).Returns(false);

            var result = controller.Add(model, shipOwnerId);

            Assert.IsInstanceOf<BadRequestResult>(result);
            mapperMock.Verify(x => x.Map<Ship>(model));
            serviceMock.Verify(x => x.Add(entity, shipOwnerId), Times.Once());
        }
    }
}
