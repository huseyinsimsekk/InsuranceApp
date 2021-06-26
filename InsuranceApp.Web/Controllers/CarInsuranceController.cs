using AutoMapper;
using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using InsuranceApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApp.Web.Controllers
{
    public class CarInsuranceController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IInnerInsuranceService _innerInsuranceService;
        private readonly IOuterInsuranceService _outerInsuranceService;
        private readonly IMapper _mapper;
        private readonly ILogger<CarInsuranceController> _logger;
        public CarInsuranceController(IConfiguration configuration,
                                     IMapper mapper,
                                     ILogger<CarInsuranceController> logger,
                                     IInnerInsuranceService innerInsuranceService,
                                     IOuterInsuranceService outerInsuranceService)
        {
            _configuration = configuration;
            _innerInsuranceService = innerInsuranceService;
            _outerInsuranceService = outerInsuranceService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarInsuranceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!_innerInsuranceService.HasRecord(model.LicencePlate, model.TCKN))
                    {
                        var entity = _mapper.Map<CarInsurance>(model);
                        _innerInsuranceService.AddCarInsurance(entity);
                    }
                    var list = await SendRequestToAllServices(model);
                    TempData["CurrentOfferList"] = JsonConvert.SerializeObject(_mapper.Map<IEnumerable<CompanyOfferModel>>(list));
                    return RedirectToAction("Index", "CompanyOffer");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error. Detail:{ex.Message}");
                    ViewData["Message"] = "İşlemizin Şuan Gerçekleştirilemedi. Lütfen Daha Sonra Tekrar Deneyiniz!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public JsonResult GetCarInsuranceModel(string tckn, string licencePlate)
        {
            var entity = _innerInsuranceService.GetCarInsuranceByLicencePlateAndTCKNAsync(licencePlate, tckn);
            if (entity is null) return Json(null);
            var model = _mapper.Map<CarInsuranceModel>(entity);
            return Json(model);
        }
        private async Task<List<CompanyOffer>> SendRequestToAllServices(CarInsuranceModel model)
        {
            var offerList = new List<CompanyOffer>();
            // Burada tek tek servislere istek atıp cevap almak yerine bir message queue yapısı ile kuyruğa bir teklif talebi gönderilir.
            // Kuyruğu dinleyen servisler mesajı alıp tekliflerini gönderir. Bu şekilde yapmak için süre kısıtlı olduğu için deneyemedim.
            await GetCompanyOffer(Constants.InsuranceCompanyAUrl, model, offerList);
            await GetCompanyOffer(Constants.InsuranceCompanyBUrl, model, offerList);
            await GetCompanyOffer(Constants.InsuranceCompanyCUrl, model, offerList);

            return offerList;
        }
        private async Task GetCompanyOffer(string companyUrlSection, CarInsuranceModel model, List<CompanyOffer> offerList)
        {
            try
            {
                string companyApiUrl = _configuration.GetSection(companyUrlSection).Value;
                var vmodel = await _outerInsuranceService.SendRequestToOfferAsync(companyApiUrl, model);
                var entity = _mapper.Map<CompanyOffer>(vmodel);
                entity.EffectedDate = DateTime.Now;
                _innerInsuranceService.SaveCompanyOffer(entity);
                offerList.Add(entity);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Service Error: Detail{ex.Message}");
            }
        }
    }
}
