using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Core.Entities
{
    public class CompanyOffer : BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string OfferDescription { get; set; }
        public decimal Fee { get; set; }
        public string LicencePlate { get; set; }
    }
}
