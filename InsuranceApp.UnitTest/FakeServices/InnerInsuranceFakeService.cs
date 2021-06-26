using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsuranceApp.UnitTest.FakeServices
{
    public class InnerInsuranceFakeService : IInnerInsuranceService
    {
        public readonly List<CompanyOffer> companyOffers;
        public readonly List<CarInsurance> carInsurances;
        public InnerInsuranceFakeService()
        {
            companyOffers = new List<CompanyOffer>(){
                new CompanyOffer(){ Id=1, Name="test1", Fee=1000, CreatedDate=DateTime.Now, LogoUrl="logo1", LicencePlate="06nn06", OfferDescription="description"},
                new CompanyOffer(){ Id=2, Name="test2", Fee=2000, CreatedDate=DateTime.Now, LogoUrl="logo2", LicencePlate="06nn03", OfferDescription="description2"},
                new CompanyOffer(){ Id=3, Name="test1", Fee=3000, CreatedDate=DateTime.Now, LogoUrl="logo3", LicencePlate="06nn03", OfferDescription="description3"}
            };

            carInsurances = new List<CarInsurance>(){
                new CarInsurance(){ Id=1, LicencePlate="06nn06", CreatedDate= DateTime.Now, LicenceCode="AA", TCKN="11111111111", LicenceSerialNumber="1212"},
                new CarInsurance(){ Id=2, LicencePlate="06nn07", CreatedDate= DateTime.Now, LicenceCode="AB", TCKN="11111111112", LicenceSerialNumber="1214"},
                new CarInsurance(){ Id=3, LicencePlate="06nn08", CreatedDate= DateTime.Now, LicenceCode="AC", TCKN="11111111113", LicenceSerialNumber="1216"}
            };
        }
        public void AddCarInsurance(CarInsurance carInsurance)
        {
            if (carInsurance == null) throw new ArgumentNullException();
            carInsurances.Add(carInsurance);
        }

        public CarInsurance GetCarInsuranceByLicencePlateAndTCKNAsync(string licencePlate, string tckn)
        {
            return carInsurances.FirstOrDefault(m => m.LicencePlate == licencePlate && m.TCKN == tckn);
        }

        public IEnumerable<CompanyOffer> GetCompanyOffersByLicencePlate(string licencePlate)
        {
            return companyOffers.Where(m => m.LicencePlate == licencePlate);
        }

        public CarInsurance GetInsuranceByLicencePlate(string licencePlate)
        {
            return carInsurances.FirstOrDefault(m => m.LicencePlate == licencePlate);
        }

        public bool HasRecord(string licencePlate, string tckn)
        {
            return carInsurances.FirstOrDefault(m => m.LicencePlate == licencePlate && m.TCKN == tckn) != null;
        }

        public void SaveCompanyOffer(CompanyOffer offer)
        {
            if (offer is null) throw new ArgumentNullException();
            companyOffers.Add(offer);
        }
    }
}
