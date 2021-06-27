using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApp.Core.Contracts
{
    public interface IOuterInsuranceService
    {
        Task<IEnumerable<CompanyOfferModel>> SendRequestToAllServices(List<string> companiesUrls, CarInsuranceModel model);

    }
}
