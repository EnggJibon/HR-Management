using AutoMapper;
using ERP.DAL.Security;
using ERP.Security.Domain;

namespace ERP.Security.Mapping
{
    public class DatabaseToDomain : Profile
    {
        protected override void Configure()
        {
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<AccessLog, AccessLogModel>();
            CreateMap<AdditionalOperationPermission, AdditionalOperationPermissionModel>();
            CreateMap<AdditionalScreenPermission, AdditionalScreenPermissionModel>();
            CreateMap<ApplicationPolicy, ApplicationPolicyModel>();
            CreateMap<Application, ApplicationModel>();
            CreateMap<Menu, MenuModel>();
            CreateMap<Module, ModuleModel>();
            CreateMap<Role, RoleModel>();
            CreateMap<RoleWiseScreenPermission, RoleWiseScreenPermissionModel>();
            CreateMap<RoleWiseOperationPermission, RoleWiseOperationPermissionModel>();
            CreateMap<Screen, ScreenModel>();
            CreateMap<ScreenOperation, ScreenOperationModel>();
            CreateMap<UserInformation, UserInformationModel>();
            CreateMap<UserType, UserTypeModel>();

            CreateMap<USP_GetAdditionalOperationPermissionList_Result, AdditionalOperationPermissionModel>();
            CreateMap<USP_GetAdditionalScreenPermissionList_Result, AdditionalScreenPermissionModel>();
            CreateMap<USP_GetRoleWiseScreenPermissionList_Result, RoleWiseScreenPermissionModel>();
            CreateMap<USP_GetRoleWiseOperationPermissionList_Result, RoleWiseOperationPermissionModel>();
            CreateMap<USP_GetScreenList_Result, ScreenModel>();
            CreateMap<USP_GetScreenOperationList_Result, ScreenOperationModel>();
            CreateMap<USP_GetUsers_Result, UserInformationModel>();
            CreateMap<USP_GetUserDetails_Result, UserInformationModel>();
        }
    }
}
