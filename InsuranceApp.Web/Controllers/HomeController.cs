using InsuranceApp.Core.Contracts;
using InsuranceApp.Core.Entities;
using InsuranceApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInnerInsuranceService _innerInsuranceService;
        public HomeController(ILogger<HomeController> logger, IInnerInsuranceService innerInsuranceService)
        {
            _logger = logger;
            _innerInsuranceService = innerInsuranceService;
        }

        public IActionResult Index()
        {
            var x = _innerInsuranceService.GetCarInsuranceByLicencePlateAndTCKNAsync("06NNE67", "11111111111");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
