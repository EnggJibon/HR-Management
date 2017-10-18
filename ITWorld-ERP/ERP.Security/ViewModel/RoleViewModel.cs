using System.Collections.Generic;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class RoleViewModel
    {
        public IEnumerable<RoleModel> Roles { get; set; }
        public RoleModel Role { get; set; }
    }
}
