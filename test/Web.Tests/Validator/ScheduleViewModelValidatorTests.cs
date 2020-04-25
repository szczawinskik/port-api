using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Validation.Messages;
using Web.Validation.Validators;
using Web.ViewModels;

namespace Web.Tests.Validator
{
    [TestFixture]
    public class ScheduleViewModelValidatorTests
    {
        private ScheduleViewModelValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new ScheduleViewModelValidator();
        }
        [Test]
        public void ShouldNotValidateWhenArrivalIsLaterThanDeparture()
        {
            var invalidViewModel = new ScheduleViewModel
            {
                Arrival = new DateTime(2020, 10, 10),
                Departure = new DateTime(2020, 10, 9)
            };

            var result = validator.IsValid(invalidViewModel);

            Assert.IsFalse(result);
            Assert.AreEqual(1, validator.ErrorList.Count());
            Assert.AreEqual(ScheduleViewModelMessages.ArrivalLaterThanDeparture,
                validator.ErrorList.ElementAt(0));
        }

        [Test]
        public void ShouldValidateWhenArrivalIsEarlierOrEqualToDeparture()
        {
            var invalidViewModel = new ScheduleViewModel
            {
                Arrival = new DateTime(2020, 10, 10),
                Departure = new DateTime(2020, 10, 10)
            };

            var result = validator.IsValid(invalidViewModel);

            Assert.IsTrue(result);
            Assert.AreEqual(0, validator.ErrorList.Count());
        }
    }
}
