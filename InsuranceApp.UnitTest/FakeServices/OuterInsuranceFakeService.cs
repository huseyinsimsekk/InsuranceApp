using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.UnitTest.FakeServices
{
    public class OuterInsuranceFakeService : IOuterInsuranceService
    {
        public async Task<CompanyOfferModel> SendRequestToOfferAsync(string companyApiUrl, CarInsuranceModel model)
        {
            if (string.IsNullOrEmpty(companyApiUrl) || model is null) throw new ArgumentNullException();
            if (companyApiUrl == "test")
            {
                try
                {
                    var test = JsonConvert.DeserializeObject<CompanyOfferModel>("test");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Outer Insurance Service Error. Detail: {ex.Message}");
                }

            }
            var vmodel = new CompanyOfferModel();
            return vmodel;
        }
    }
}
