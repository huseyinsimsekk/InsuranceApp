using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Service.Services
{
    public class OuterInsuranceService : IOuterInsuranceService
    {
        public async Task<CompanyOfferModel> SendRequestToOfferAsync(string url, CarInsuranceModel model)
        {
            if (string.IsNullOrEmpty(url) || model is null) throw new ArgumentNullException();
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
                    return vmodel;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Outer Insurance Service Error. Detail: {ex.Message}");
            }
        }
    }
}
