using InsuranceApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Data.Seeds
{
    public class CarInsuranceSeed : IEntityTypeConfiguration<CarInsurance>
    {
        public void Configure(EntityTypeBuilder<CarInsurance> builder)
        {
            builder.HasData(new CarInsurance()
            {
                Id = 1,
                LicencePlate = "06NNE67",
                LicenceCode = "AA",
                LicenceSerialNumber = "111111",
                TCKN="11111111111"
            },
            new CarInsurance()
            {
                Id = 2,
                LicencePlate = "06BHY724",
                LicenceCode = "AB",
                LicenceSerialNumber = "111112",
                TCKN = "11111111112"
            });
        }
    }
}
