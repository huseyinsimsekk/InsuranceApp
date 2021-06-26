using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using System.Threading.Tasks;

namespace InsuranceApp.Core.Contracts
{
    public interface IOuterInsuranceService
    {
        Task<CompanyOfferModel> SendRequestToOfferAsync(string companyApiUrl, CarInsuranceModel model);
    }
}
