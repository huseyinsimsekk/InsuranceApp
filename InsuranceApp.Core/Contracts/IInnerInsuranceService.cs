using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using System.Collections.Generic;

namespace InsuranceApp.Core.Contracts
{
    public interface IInnerInsuranceService
    {
        bool HasRecord(string licencePlate, string tckn);
        CarInsurance GetCarInsuranceByLicencePlateAndTCKNAsync(string licencePlate, string tckn);
        void AddCarInsurance(CarInsurance carInsurance);
        void SaveCompanyOffer(CompanyOffer offer);
        IEnumerable<CompanyOffer> GetCompanyOffersByLicencePlate(string licencePlate);
        CarInsurance GetInsuranceByLicencePlate(string licencePlate);
    }
}
