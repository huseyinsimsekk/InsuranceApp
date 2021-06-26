using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Models;
using InsuranceApp.UnitTest.FakeServices;
using System;
using Xunit;

namespace InsuranceApp.UnitTest
{
    public class OuterInsuranceServiceTest
    {
        private readonly IOuterInsuranceService service;
        public OuterInsuranceServiceTest()
        {
            service = new OuterInsuranceFakeService();
        }
        [Fact]
        public void SendRequestToOfferAsync_ShouldReturnArgumentNullReferenceException_WhenArgumentIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.SendRequestToOfferAsync(null, null));
        }
        [Fact]
        public void SendRequestToOfferAsync_ShouldReturnException_WhenModelCannotDeseriliaze()
        {
            var testModel = new CarInsuranceModel();
            Assert.ThrowsAsync<Exception>(() => service.SendRequestToOfferAsync("test", testModel));
        }
        [Fact]
        public void SendRequestToOfferAsync_ShouldReturnModel_WhenExecute()
        {
            var testModel = new CarInsuranceModel();
            var result = service.SendRequestToOfferAsync("t", testModel).Result;
            Assert.NotNull(result);
            Assert.IsType<CompanyOfferModel>(result);
        }
    }
}
