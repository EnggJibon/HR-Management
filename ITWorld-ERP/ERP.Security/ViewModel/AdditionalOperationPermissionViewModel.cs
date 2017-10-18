using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class AdditionalOperationPermissionViewModel
    {
        public IEnumerable<AdditionalOperationPermissionModel> AdditionalOperationPermissions { get; set; }
        public AdditionalOperationPermissionModel AdditionalOperationPermission { get; set; }
        public SelectList ScreenOperationList { get; set; }
        public SelectList UserList { get; set; }
    }
}
