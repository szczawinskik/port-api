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
using Web.ViewModels;

namespace Web.Tests.Controllers
{
    [TestFixture]
    public class ShipControllerTests
    {
        private Mock<IService<Ship>> serviceMock;
        private Mock<IMapper> mapperMock;
        private ShipController controller;

        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IService<Ship>>();
            mapperMock = new Mock<IMapper>();

            controller = new ShipController(serviceMock.Object, mapperMock.Object);
        }

        [Test]
        public void ShouldReturnAllSchedules()
        {
            var sourceCollectionMock = new Mock<IQueryable<Ship>>();
            var destinationCollection = new Mock<IQueryable<ShipAggregateViewModel>>();
            mapperMock.Setup(x => x.ProjectTo<ShipAggregateViewModel>(sourceCollectionMock.Object, null))
                .Returns(destinationCollection.Object);
            serviceMock.Setup(x => x.GetAll()).Returns(sourceCollectionMock.Object);

            var result = controller.GetAll();

            Assert.AreEqual(destinationCollection.Object, result);
            mapperMock.Verify(x => x.ProjectTo<ShipAggregateViewModel>(sourceCollectionMock.Object, null),
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

    }
}
