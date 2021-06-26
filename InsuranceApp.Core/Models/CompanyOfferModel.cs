using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceApp.Core.Models
{
    public class CompanyOfferModel
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string OfferDescription { get; set; }
        public decimal Fee { get; set; }
        public string LicencePlate { get; set; }
    }
}
