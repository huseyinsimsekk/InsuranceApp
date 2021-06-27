using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Service.Services
{
    public class OuterInsuranceService : IOuterInsuranceService
    {
        private readonly ILogger<OuterInsuranceService> _logger;
        public OuterInsuranceService(ILogger<OuterInsuranceService> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<CompanyOfferModel>> SendRequestToAllServices(List<string> companiesUrls, CarInsuranceModel model)
        {
            if (companiesUrls is null || model is null) throw new ArgumentNullException();
            var offerList = new List<CompanyOfferModel>();
            foreach (var url in companiesUrls)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var jsonModel = JsonConvert.SerializeObject(model);
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(url),
                            Content = new StringContent(jsonModel, Encoding.UTF8, "application/json")
                        };

                        var response = client.SendAsync(request).ConfigureAwait(false);

                        var responseInfo = response.GetAwaiter().GetResult();
                        var resonseModel = await responseInfo.Content.ReadAsStringAsync();
                        var vmodel = JsonConvert.DeserializeObject<CompanyOfferModel>(resonseModel);
                        offerList.Add(vmodel);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Outer Insurance Service Error. Detail: {ex.Message}");
                }
            }
            return offerList;
        }
    }
}
