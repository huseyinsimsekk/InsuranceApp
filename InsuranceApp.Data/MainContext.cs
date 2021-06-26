using InsuranceApp.Core.Entities;
using InsuranceApp.Data.Configurations;
using InsuranceApp.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApp.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
        public DbSet<CarInsurance> CarInsurances { get; set; }
        public DbSet<CompanyOffer> CompanyOffers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarInsuranceConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyOfferConfiguration());
            // Seed Data Configuration
            modelBuilder.ApplyConfiguration(new CarInsuranceSeed());
        }
    }
}
