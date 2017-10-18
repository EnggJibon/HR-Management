using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class AdditionalScreenPermissionViewModel
    {
        public IEnumerable<AdditionalScreenPermissionModel> AdditionalScreenPermissions { get; set; }
        public AdditionalScreenPermissionModel AdditionalScreenPermission { get; set; }
        public SelectList ScreenList { get; set; }
        public SelectList UserList { get; set; }
        public SelectList ModuleList { get; set; }
    }
}
