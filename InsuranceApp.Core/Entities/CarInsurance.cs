using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Core.Entities
{
    public class CarInsurance : BaseEntity
    {
        public string TCKN { get; set; }
        public string LicencePlate { get; set; }
        public string LicenceCode { get; set; }
        public string LicenceSerialNumber { get; set; }
    }
}
