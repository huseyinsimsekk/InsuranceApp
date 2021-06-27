using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.UnitTest.FakeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace InsuranceApp.UnitTest
{
    public class InnerInsuranceServiceTest
    {
        private InnerInsuranceFakeService service;
        public InnerInsuranceServiceTest()
        {
            service = new InnerInsuranceFakeService();
        }

        [Fact]
        public void AddCarInsurance_ShouldReturnArgumenNullReferenceException_WhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => service.AddCarInsurance(null));
        }

        [Fact]
        public void AddCarInsurance_ShouldAddElement_WhenArgumentIsNotNull()
        {
            var carInsurance = new CarInsurance();
            service.AddCarInsurance(carInsurance);
            Assert.Equal(4, service.carInsurances.Count);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("test", "test")]
        public void GetCarInsuranceByLicencePlateAndTCKNAsync_ShouldNull_WhenIsNotFound(string licencePlate, string tckn)
        {
            var result = service.GetCarInsuranceByLicencePlateAndTCKNAsync(licencePlate, tckn);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("06nn06", "11111111111")]
        [InlineData("06nn07", "11111111112")]
        public void GetCarInsuranceByLicencePlateAndTCKNAsync_ShouldReturnCarInsurance_WhenIsFound(string licencePlate, string tckn)
        {
            var result = service.GetCarInsuranceByLicencePlateAndTCKNAsync(licencePlate, tckn);
            Assert.NotNull(result);
            Assert.IsType<CarInsurance>(result);
        }

        [Fact]
        public void GetCompanyOffersByLicencePlate_ShouldEmpty_WhenIsNotFound()
        {
            var result = service.GetCompanyOffersByLicencePlate("notfoundplate");
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetCompanyOffersByLicencePlate_ShouldReturnValue_WhenIsFound()
        {
            var result = service.GetCompanyOffersByLicencePlate("06nn03");
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void GetInsuranceByLicencePlate_ShouldEmpty_WhenIsNotFound()
        {
            var result = service.GetInsuranceByLicencePlate("notfoundplate");
            Assert.Null(result);
        }

        [Fact]
        public void GetInsuranceByLicencePlate_ShouldReturnValue_WhenIsFound()
        {
            var result = service.GetInsuranceByLicencePlate("06nn06");
            Assert.NotNull(result);
        }

        [Fact]
        public void HasRecord_ShouldReturnFalse_WhenIsNotFound()
        {
            var result = service.HasRecord("notfound", "notfound");
            Assert.False(result);
        }

        [Fact]
        public void HasRecord_ShouldReturnTrue_WhenIsFound()
        {
            var result = service.HasRecord("06nn06", "11111111111");
            Assert.True(result);
        }

        [Fact]
        public void SaveCompanyOffer_ShouldReturnArgumentNullReferenceException_WhenNullModelIsSend()
        {
            Assert.Throws<ArgumentNullException>(() => service.SaveCompanyOffer(null));
        }

        [Fact]
        public void SaveCompanyOffer_ShouldAddElement_WhenNotNullModelIsSend()
        {
            service.SaveCompanyOffer(new CompanyOffer());
            Assert.Equal(4, service.companyOffers.Count());
        }

    }
}
