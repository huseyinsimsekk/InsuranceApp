using AutoMapper;
using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using InsuranceApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace InsuranceApp.UnitTest
{
    public class CarInsuranceControllerTest
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IInnerInsuranceService> innerServiceMock;
        private readonly Mock<IOuterInsuranceService> outterServiceMock;
        private readonly CarInsuranceController carInsuranceController;

        private List<CarInsuranceModel> carInsurances;
        public CarInsuranceControllerTest()
        {
            mapperMock = new Mock<IMapper>();
            innerServiceMock = new Mock<IInnerInsuranceService>();
            outterServiceMock = new Mock<IOuterInsuranceService>();
            carInsuranceController = new CarInsuranceController(mapperMock.Object, innerServiceMock.Object, outterServiceMock.Object);
            carInsurances = new List<CarInsuranceModel>(){
                new CarInsuranceModel(){ TCKN="11111111111", LicencePlate="06ab06", LicenceCode="aa", LicenceSerialNumber="1111"  },
                new CarInsuranceModel(){ TCKN="11111111112", LicencePlate="06ac06", LicenceCode="ab", LicenceSerialNumber="1112"  },
                new CarInsuranceModel(){ TCKN="11111111113", LicencePlate="06ad06", LicenceCode="ac", LicenceSerialNumber="1131"  }
            };
        }
        [Fact]
        public void Create_ShouldReturnView_WhenExecute()
        {
            var result = carInsuranceController.Create();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_ShouldReturnView_WhenInvalidModelState()
        {
            carInsuranceController.ModelState.AddModelError("TCKN", "is required");
            var result = await carInsuranceController.Create(carInsurances.First());
            var viewResult = Assert.IsType<ViewResult>(result);
            
            Assert.IsType<CarInsuranceModel>(viewResult.Model);
        }

        [Fact]
        public async void Create_ShouldRedirectToCompanyOfferIndex_WhenValidModelState()
        {
            var companyOfferModels = new List<CompanyOfferModel>();
            outterServiceMock.Setup(m => m.SendRequestToAllServices(It.IsAny<List<string>>(), It.IsAny<CarInsuranceModel>())).ReturnsAsync(companyOfferModels);
            var result = await carInsuranceController.Create(carInsurances.First());
            var redirectAction = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("CompanyOffer", redirectAction.ControllerName);
            Assert.Equal("Index", redirectAction.ActionName);
        }
        [Fact]
        public void GetCarInsuranceModel_ShouldReturnJsonNullData_WhenNotFoundObject()
        {
            CarInsurance carInsurance = null;
            innerServiceMock.Setup(m => m.GetCarInsuranceByLicencePlateAndTCKNAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(carInsurance);
            var result = carInsuranceController.GetCarInsuranceModel("test", "test");
            
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Null(jsonResult.Value);
        }
        [Theory]
        [InlineData("06ab06", "11111111111")]
        public void GetCarInsuranceModel_ShouldReturnJsonData_WhenFoundObject(string licencePlate, string tckn)
        {
            var selected = carInsurances.FirstOrDefault(m => m.LicencePlate == licencePlate && m.TCKN == tckn);
            CarInsurance carInsurance = new CarInsurance() { Id = 1, CreatedDate = DateTime.Now, TCKN = selected.TCKN, LicencePlate = selected.LicencePlate, LicenceCode = selected.LicenceCode, LicenceSerialNumber = selected.LicenceSerialNumber };
            
            innerServiceMock.Setup(m => m.GetCarInsuranceByLicencePlateAndTCKNAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(carInsurance);
            mapperMock.Setup(m => m.Map<CarInsuranceModel>(carInsurance)).Returns(selected);
            var result = carInsuranceController.GetCarInsuranceModel(licencePlate, tckn);
           
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.NotNull(jsonResult.Value);
        }

    }
}
