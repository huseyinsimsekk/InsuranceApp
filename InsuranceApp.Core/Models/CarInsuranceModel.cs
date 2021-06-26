using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceApp.Core.Models
{
    public class CarInsuranceModel
    {
        [Required(ErrorMessage = "TC Kimlik Numarası Boş Olamaz")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 Rakam İçermesi Zorunludur")]
        public string TCKN { get; set; }

        [Required(ErrorMessage = "Plaka Boş Olamaz")]
        [MinLength(2, ErrorMessage = "Lütfen Araç Plaka No Giriniz.")]
        [MaxLength(10)]
        public string LicencePlate { get; set; }
        
        [Required(ErrorMessage = "Ruhsat Kodu Boş Olamaz")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Ruhsat Kodu 2 Karakter Uzunluğunda Olmalıdır")]
        public string LicenceCode { get; set; }
        
        [Required(ErrorMessage = "Ruhsat Numarası Boş Olamaz")]
        public string LicenceSerialNumber { get; set; }
    }
}
