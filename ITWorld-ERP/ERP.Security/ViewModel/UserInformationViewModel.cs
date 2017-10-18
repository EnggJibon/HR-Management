using System.Collections.Generic;
using System.Web.Mvc;
using ERP.HRM.Domain;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class UserInformationViewModel
    {
        public IEnumerable<UserInformationModel> UserInformationList { get; set; }
        public UserInformationModel UserInformation { get; set; }
        public SelectList UserTypeList { get; set; }
        public SelectList RoleList { get; set; }
        public SelectList EmployeeCategoryList { get; set; }
        public SelectList EmployeeList { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
