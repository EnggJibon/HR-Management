using AutoMapper;
using ERP.DAL.Security;
using ERP.Security.Domain;

namespace ERP.Security.Mapping
{
    public class DomainToDatabase:Profile
    {
        protected override void Configure()
        {
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<AccessLogModel, AccessLog>();
            CreateMap<AdditionalOperationPermissionModel, AdditionalOperationPermission>();
            CreateMap<AdditionalScreenPermissionModel, AdditionalScreenPermission>();
            CreateMap<ApplicationPolicyModel, ApplicationPolicy>();
            CreateMap<ApplicationModel, Application>();
            CreateMap<MenuModel, Menu>();
            CreateMap<ModuleModel, Module>();
            CreateMap<RoleModel, Role>();
            CreateMap<RoleWiseScreenPermissionModel, RoleWiseScreenPermission>();
            CreateMap<RoleWiseOperationPermissionModel, RoleWiseOperationPermission>();
            CreateMap<ScreenModel, Screen>();
            CreateMap<ScreenOperationModel, ScreenOperation>();
            CreateMap<UserInformationModel, UserInformation>();
            CreateMap<UserTypeModel, UserType>();
        }
    }
}
