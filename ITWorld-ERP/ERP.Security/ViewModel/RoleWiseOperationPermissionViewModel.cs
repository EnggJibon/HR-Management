using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class RoleWiseOperationPermissionViewModel
    {
        public IEnumerable<RoleWiseOperationPermissionModel> RoleWiseOperationPermissions { get; set; }
        public RoleWiseOperationPermissionModel RoleWiseOperationPermission { get; set; }
        public SelectList ScreenOperationList { get; set; }
        public SelectList RoleList { get; set; }
    }
}
