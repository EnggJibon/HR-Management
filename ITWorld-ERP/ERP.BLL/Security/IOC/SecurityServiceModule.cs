using ERP.DAL.Security.Repository;
using Ninject.Modules;

namespace ERP.BLL.Security.IOC 
{
    public partial class SecurityServiceModule : NinjectModule
	{
		public override void Load()
        {
            Bind<IAccessLogService>().To<AccessLogService>();
            Bind<IAdditionalOperationPermissionService>().To<AdditionalOperationPermissionService>();
            Bind<IAdditionalOperationPermissionService>().To<AdditionalOperationPermissionService>();
            Bind<IApplicationPolicyService>().To<ApplicationPolicyService>();
            Bind<IApplicationService>().To<ApplicationService>();
            Bind<IMenuService>().To<MenuService>();
            Bind<IModuleService>().To<ModuleService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IRoleWiseScreenPermissionService>().To<RoleWiseScreenPermissionService>();
            Bind<IRoleWiseOperationPermissionService>().To<RoleWiseOperationPermissionService>();
            Bind<IScreenService>().To<ScreenService>();
            Bind<IScreenOperationService>().To<ScreenOperationService>();
            Bind<IUserInformationService>().To<UserInformationService>();
            Bind<IUserTypeService>().To<UserTypeService>();
		}
	}
}

