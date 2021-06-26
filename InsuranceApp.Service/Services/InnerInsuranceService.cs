using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Service.Services
{
    public class InnerInsuranceService : IInnerInsuranceService
    {
        public readonly IUnitOfWork _unitOfWork;
        public InnerInsuranceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CarInsurance GetCarInsuranceByLicencePlateAndTCKNAsync(string licencePlate, string tckn)
        {
            return _unitOfWork.GetEntity<CarInsurance>().FirstOrDefault(m => m.LicencePlate == licencePlate && m.TCKN == tckn);
        }

        public void AddCarInsurance(CarInsurance carInsurance)
        {
            if (carInsurance == null) throw new ArgumentNullException();
            carInsurance.EffectedDate = DateTime.Now;
            _unitOfWork.GetEntity<CarInsurance>().Add(carInsurance);
            _unitOfWork.Commit();
        }

        public bool HasRecord(string licencePlate, string tckn)
        {
            return _unitOfWork.GetEntity<CarInsurance>().FirstOrDefault(m => m.LicencePlate == licencePlate && m.TCKN == tckn) != null;
        }

        public void SaveCompanyOffer(CompanyOffer offer)
        {
            if (offer is null) throw new ArgumentNullException();
            _unitOfWork.GetEntity<CompanyOffer>().Add(offer);
            _unitOfWork.Commit();
        }

        public IEnumerable<CompanyOffer> GetCompanyOffersByLicencePlate(string licencePlate)
        {
            return _unitOfWork.GetEntity<CompanyOffer>().Where(m => m.LicencePlate == licencePlate);
        }

        public CarInsurance GetInsuranceByLicencePlate(string licencePlate)
        {
            return _unitOfWork.GetEntity<CarInsurance>().FirstOrDefault(m => m.LicencePlate == licencePlate);
        }
    }
}
