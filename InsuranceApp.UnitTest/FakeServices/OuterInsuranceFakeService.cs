using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.UnitTest.FakeServices
{
    public class OuterInsuranceFakeService : IOuterInsuranceService
    {
        private readonly ILogger<OuterInsuranceFakeService> _logger;
        public OuterInsuranceFakeService(ILogger<OuterInsuranceFakeService> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<CompanyOfferModel>> SendRequestToAllServices(List<string> companiesUrls, CarInsuranceModel model)
        {
            if (companiesUrls is null || model is null) throw new ArgumentNullException();
            foreach (var companyApiUrl in companiesUrls)
            {
                if (companyApiUrl == "test")
                {
                    try
                    {
                        var test = JsonConvert.DeserializeObject<CompanyOfferModel>("test");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Outer Insurance Service Error. Detail: {ex.Message}");
                    }

                }
            }
            return new List<CompanyOfferModel>();
        }
    }
}
