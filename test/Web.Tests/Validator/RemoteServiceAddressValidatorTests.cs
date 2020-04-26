using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Validation.Validators;
using Web.ViewModels;

namespace Web.Tests.Validator
{
    [TestFixture]
    public class RemoteServiceAddressValidatorTests
    {
        private RemoteServiceAddressValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new RemoteServiceAddressValidator();
        }
        [Test]
        public void ShouldNotValidateInvalidAddress()
        {
            var invalidAddres = new RemoteServiceAddressViewModel {Value = "12345" } ;

            var result = validator.IsValid(invalidAddres);

            Assert.IsFalse(result);
            Assert.AreEqual(1, validator.ErrorList.Count);
        }

        [Test]
        public void ShouldValidateValidAddress()
        {
            var invalidAddres = new RemoteServiceAddressViewModel { Value = "http://validAdress.pl/controller/action" };

            var result = validator.IsValid(invalidAddres);

            Assert.IsTrue(result);
            Assert.AreEqual(0, validator.ErrorList.Count);
        }
    }
}
