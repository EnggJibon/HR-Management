using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class RoleWiseScreenPermissionModel : BaseDomainModel<RoleWiseScreenPermissionModel>
    {
        public long RoleId { get; set; }
        public long ScreenId { get; set; }
        public string AccessRight { get; set; }

        public string CanRead { get; set; }
        public string CanCreate { get; set; }
        public string CanUpdate { get; set; }
        public string CanDelete { get; set; }
        public string RoleName { get; set; }
        public long ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenTitle { get; set; }
        public string UserName { get; set; }

        public RoleModel Role { get; set; }
        public ScreenModel Screen { get; set; }
    }
}
