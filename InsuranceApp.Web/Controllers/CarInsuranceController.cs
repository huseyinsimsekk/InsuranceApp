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
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApp.Web.Controllers
{
    public class CarInsuranceController : Controller
    {
        private readonly IInnerInsuranceService _innerInsuranceService;
        private readonly IOuterInsuranceService _outerInsuranceService;
        private readonly IMapper _mapper;

        private readonly List<string> CompanyUrls;
        public CarInsuranceController(IMapper mapper, IInnerInsuranceService innerInsuranceService, IOuterInsuranceService outerInsuranceService)
        {
            _innerInsuranceService = innerInsuranceService;
            _outerInsuranceService = outerInsuranceService;
            _mapper = mapper;
            CompanyUrls = new List<string>(){
                        ConfigHelper.CompanyUrlSetting(Constants.InsuranceCompanyAUrl),
                        ConfigHelper.CompanyUrlSetting(Constants.InsuranceCompanyBUrl),
                        ConfigHelper.CompanyUrlSetting(Constants.InsuranceCompanyCUrl),
                    };
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
                if (!_innerInsuranceService.HasRecord(model.LicencePlate, model.TCKN))
                {
                    var entity = _mapper.Map<CarInsurance>(model);
                    _innerInsuranceService.AddCarInsurance(entity);
                }
                var offerList = await _outerInsuranceService.SendRequestToAllServices(CompanyUrls, model);
                if (offerList != null && offerList.Count() > 0)
                    TempData["CurrentOfferList"] = JsonConvert.SerializeObject(offerList);
                return RedirectToAction("Index", "CompanyOffer");
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
    }
}
