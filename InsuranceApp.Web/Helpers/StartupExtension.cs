using InsuranceApp.Core.Contracts;
using InsuranceApp.Data;
using InsuranceApp.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceApp.Web.Helpers
{
    public static class StartupExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IInnerInsuranceService, InnerInsuranceService>();
            services.AddScoped<IOuterInsuranceService, OuterInsuranceService>();
        }
    }
}
