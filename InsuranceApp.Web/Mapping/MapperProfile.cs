using AutoMapper;
using InsuranceApp.Core.Entities;
using InsuranceApp.Core.Models;
using InsuranceApp.Web.Models;

namespace InsuranceApp.Web.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarInsuranceModel, CarInsurance>().ReverseMap();
            CreateMap<CompanyOfferModel, CompanyOffer>().ReverseMap();
        }
    }
}
