using AutoMapper;
using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using InsuranceApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InsuranceApp.UnitTest.Tests
{
    public class CompanyOfferControllerTest
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IInnerInsuranceService> innerServiceMock;
        private readonly CompanyOfferController companyOfferController;
        private readonly Mock<ITempDataDictionary> tempDataMock;

        private readonly List<CompanyOffer> companyOffers;
        private readonly List<CompanyOfferModel> companyOffersModel;
        public CompanyOfferControllerTest()
        {
            mapperMock = new Mock<IMapper>();
            innerServiceMock = new Mock<IInnerInsuranceService>();
            tempDataMock = new Mock<ITempDataDictionary>();
            companyOfferController = new CompanyOfferController(innerServiceMock.Object, mapperMock.Object);
            companyOfferController.TempData = tempDataMock.Object;

            companyOffers = new List<CompanyOffer>(){
                new CompanyOffer(){Id=1, Name="test1", LogoUrl="logo1", LicencePlate="06ab06", OfferDescription="description1", Fee=1000, CreatedDate=DateTime.Now },
                new CompanyOffer(){Id=2, Name="test2", LogoUrl="logo2", LicencePlate="06ab07", OfferDescription="description2", Fee=2000, CreatedDate=DateTime.Now },
                new CompanyOffer(){Id=3, Name="test3", LogoUrl="logo3", LicencePlate="06ab06", OfferDescription="description3", Fee=3000, CreatedDate=DateTime.Now }
            };
            companyOffersModel = new List<CompanyOfferModel>()
            {
                new CompanyOfferModel(){ Name="test1", LogoUrl="logo1", LicencePlate="06ab06", OfferDescription="description1", Fee=1000 },
                new CompanyOfferModel(){ Name="test2", LogoUrl="logo2", LicencePlate="06ab07", OfferDescription="description2", Fee=2000},
                new CompanyOfferModel(){ Name="test3", LogoUrl="logo3", LicencePlate="06ab06", OfferDescription="description3", Fee=3000}
            };
        }
        [Fact]
        public void Index_ShouldReturnView_WhenExecute()
        {
            var result = companyOfferController.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_ShouldReturnView_WhenParameterNull()
        {
            var result = companyOfferController.List(null);
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData("06ab06")]
        public void List_ShouldReturnView_WhenExecute(string licencePlate)
        {
            var selected = companyOffers.Where(m => m.LicencePlate == licencePlate);
            var mapped = companyOffersModel.Where(m => m.LicencePlate == licencePlate);
            innerServiceMock.Setup(m => m.GetCompanyOffersByLicencePlate(It.IsAny<string>())).Returns(selected);
            mapperMock.Setup(x => x.Map<IEnumerable<CompanyOfferModel>>(selected)).Returns(mapped);
            var result = companyOfferController.List(licencePlate);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(mapped, viewResult.Model);
        }
    }
}
