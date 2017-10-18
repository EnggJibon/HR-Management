using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SecurityAdministration.BLL.ViewModels
{
    public class UserInRoleVM
    {
        public IEnumerable<UserInRoleView> UserInRoles { get; set; }
        public UserInRoleView UserInRole { get; set; }

        public SelectList UserList { get; set; }
        public SelectList RoleList { get; set; }
    }

    public class UserInRoleView
    { 
        public int UserID { get; set; }
        public short RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime SetOn { get; set; }
        public int SetBy { get; set; }

        public string UserFullName { get; set; }
        public string RoleName { get; set; }
        public string DesignationName { get; set; }
    }
}