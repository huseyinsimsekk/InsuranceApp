using InsuranceApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Data.Configurations
{
    public class CarInsuranceConfiguration : IEntityTypeConfiguration<CarInsurance>
    {
        public void Configure(EntityTypeBuilder<CarInsurance> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.TCKN).IsRequired();
            builder.Property(m => m.LicenceCode).IsRequired();
            builder.Property(m => m.LicencePlate).IsRequired();
            builder.Property(m => m.LicenceSerialNumber).IsRequired();
        }
    }
}
