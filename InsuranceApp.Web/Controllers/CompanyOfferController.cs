using AutoMapper;
using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using InsuranceApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Web.Controllers
{
    public class CompanyOfferController : Controller
    {
        private readonly IInnerInsuranceService _innerInsuranceService;
        private readonly IMapper _mapper;
        public CompanyOfferController(IInnerInsuranceService innerInsuranceService, IMapper mapper)
        {
            _innerInsuranceService = innerInsuranceService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            if (TempData["CurrentOfferList"] is string modelJson)
            {
                var model = JsonConvert.DeserializeObject<IEnumerable<CompanyOfferModel>>(modelJson);
                return View(model);
            }
            TempData["Message"] = "Tekliflere Ulaşamadık. Lütfen Daha Sonra Tekrar Deneyiniz";
            return View();
        }

        public IActionResult List(string licencePlate)
        {
            if (licencePlate is null) return View();
            var offerList = _innerInsuranceService.GetCompanyOffersByLicencePlate(licencePlate);

            var model = _mapper.Map<IEnumerable<CompanyOfferModel>>(offerList);
            return View(model);
        }

    }
}
