using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Validation.Interfaces;
using Web.Validation.Messages;
using Web.Validation.Validators;
using Web.ViewModels;

namespace Web.Tests.Validator
{
    [TestFixture]
    public class ShipViewModelValidatorTests
    {
        private Mock<IValidator<ScheduleViewModel>> scheduleViewModelValidator;
        private ShipViewModelValidator validator;

        [SetUp]
        public void Setup()
        {
            scheduleViewModelValidator = new Mock<IValidator<ScheduleViewModel>>();

            validator = new ShipViewModelValidator(scheduleViewModelValidator.Object);
        }
        [Test]
        public void ShouldNotValidateWhenScheduleInListIsNotValid()
        {
            var schedule = new ScheduleViewModel { };
            var invalidViewModel = new ShipViewModel
            {
                Schedules = new List<ScheduleViewModel>
                {
                   schedule
                }

            };
            var errors = new List<string> { "error"};
            scheduleViewModelValidator.Setup(x => x.IsValid(schedule)).Returns(false);
            scheduleViewModelValidator.SetupGet(x => x.ErrorList).Returns(errors);

            var result = validator.IsValid(invalidViewModel);

            Assert.IsFalse(result);
            Assert.AreEqual(errors, validator.ErrorList);
        }

        [Test]
        public void ShouldNotValidateWhenClosestScheduleIsNotValid()
        {
            var schedule = new ScheduleViewModel { };
            var closesSchedule = new ScheduleViewModel { };
            var invalidViewModel = new ShipViewModel
            {
                Schedules = new List<ScheduleViewModel>
                {
                    schedule
                },
                ClosestSchedule = closesSchedule

            };
            var errors = new List<string> { "error" };
            scheduleViewModelValidator.Setup(x => x.IsValid(schedule)).Returns(true);
            scheduleViewModelValidator.Setup(x => x.IsValid(closesSchedule)).Returns(true);
            scheduleViewModelValidator.SetupGet(x => x.ErrorList).Returns(errors);

            var result = validator.IsValid(invalidViewModel);

            Assert.IsFalse(result);
            Assert.AreEqual(errors, validator.ErrorList);
        }

        [Test]
        public void ShouldValidateWhenAllSchedulesAreValid()
        {
            var schedule = new ScheduleViewModel { };
            var closesSchedule = new ScheduleViewModel { };
            var invalidViewModel = new ShipViewModel
            {
                Schedules = new List<ScheduleViewModel>
                {
                    schedule
                },
                ClosestSchedule = closesSchedule

            };
            var errors = new List<string>();
            scheduleViewModelValidator.Setup(x => x.IsValid(schedule)).Returns(true);
            scheduleViewModelValidator.Setup(x => x.IsValid(closesSchedule)).Returns(true);
            scheduleViewModelValidator.SetupGet(x => x.ErrorList).Returns(errors);


            var result = validator.IsValid(invalidViewModel);

            Assert.IsTrue(result);
            Assert.AreEqual(errors, validator.ErrorList);
        }
    }
}
