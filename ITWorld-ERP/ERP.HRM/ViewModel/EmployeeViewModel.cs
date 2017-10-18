using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeModel Employee { get; set; }

        public SelectList EmployeeCategoryList { get; set; }
        public SelectList DepartmentList { get; set; }
        public SelectList DesignationList { get; set; }
        public SelectList RosterList { get; set; }
        public SelectList SupervisorList { get; set; }
        public SelectList ApproverList { get; set; }

        public SelectList TitleList { get; set; }
        public SelectList GenderList { get; set; }
        public SelectList MaritalStatusList { get; set; }
    }
}
