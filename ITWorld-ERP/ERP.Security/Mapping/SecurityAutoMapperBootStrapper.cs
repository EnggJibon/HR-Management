using AutoMapper;

namespace ERP.Security.Mapping
{
    public class SecurityAutoMapperBootStrapper : Profile
    {
        public static void Initialize()
        {
            Mapper.AddProfile(new DomainToDatabase());
            Mapper.AddProfile(new DatabaseToDomain());
        }
    }
}
