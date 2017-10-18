using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class RoleWiseScreenPermissionViewModel
    {
        public IEnumerable<RoleWiseScreenPermissionModel> RoleWiseScreenPermissions { get; set; }
        public RoleWiseScreenPermissionModel RoleWiseScreenPermission { get; set; }
        public SelectList ScreenList { get; set; }
        public SelectList RoleList { get; set; }
        public SelectList ModuleList { get; set; }
    }
}
