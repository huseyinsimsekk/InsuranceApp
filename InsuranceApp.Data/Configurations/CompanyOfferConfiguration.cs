using InsuranceApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Data.Configurations
{
    public class CompanyOfferConfiguration : IEntityTypeConfiguration<CompanyOffer>
    {
        public void Configure(EntityTypeBuilder<CompanyOffer> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.LicencePlate).IsRequired();
            builder.Property(m => m.LogoUrl).IsRequired();
            builder.Property(m => m.OfferDescription).IsRequired();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Fee).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
