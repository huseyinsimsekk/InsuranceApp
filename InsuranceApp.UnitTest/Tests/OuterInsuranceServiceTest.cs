using Castle.Core.Logging;
using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using InsuranceApp.UnitTest.FakeServices;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace InsuranceApp.UnitTest
{
    public class OuterInsuranceServiceTest
    {
        private readonly IOuterInsuranceService service;
        private readonly Mock<ILogger<OuterInsuranceFakeService>> loggerMock;
        public OuterInsuranceServiceTest()
        {
            loggerMock = new Mock<ILogger<OuterInsuranceFakeService>>();
            service = new OuterInsuranceFakeService(loggerMock.Object);
        }
        [Fact]
        public void SendRequestToAllServices_ShouldReturnArgumentNullReferenceException_WhenArgumentIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => service.SendRequestToAllServices(null, null));
        }
        [Fact]
        public void SendRequestToOfferAsync_ShouldReturnException_WhenModelCannotDeseriliaze()
        {
            var testUrl = new List<string>() { "test" };
            var testModel = new CarInsuranceModel();
            Assert.ThrowsAsync<Exception>(() => service.SendRequestToAllServices(testUrl, testModel));
        }

        [Fact]
        public void SendRequestToOfferAsync_ShouldReturnModel_WhenExecute()
        {
            var testUrl = new List<string>() { "t" };
            var testModel = new CarInsuranceModel();
            var result = service.SendRequestToAllServices(testUrl, testModel).Result;
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<CompanyOfferModel>>(result);
        }
    }
}
