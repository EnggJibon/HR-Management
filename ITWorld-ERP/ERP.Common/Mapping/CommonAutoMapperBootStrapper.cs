using AutoMapper;

namespace ERP.Common.Mapping
{
    public class CommonAutoMapperBootStrapper : Profile
    {
        public static void Initialize()
        {
            Mapper.AddProfile(new DomainToDatabase());
            Mapper.AddProfile(new DatabaseToDomain());
        }
    }
}
