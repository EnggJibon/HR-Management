using AutoMapper;

namespace ERP.HRM.Mapping
{
    public class HRMAutoMapperBootStrapper : Profile
    {
        public static void Initialize()
        {
            Mapper.AddProfile(new DomainToDatabase());
            Mapper.AddProfile(new DatabaseToDomain());
        }
    }
}
