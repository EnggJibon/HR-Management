using AutoMapper;
using ERP.DAL.Common;
using ERP.Common.Domain;

namespace ERP.Common.Mapping
{
    public class DomainToDatabase:Profile
    {
        protected override void Configure()
        {
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CountryModel, Country>();
        }
    }
}
