using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
    public class EmployeeLeaveInformationViewModel
    {
        public EmployeeLeaveInformationModel EmployeeLeaveInformation { get; set; }

        public SelectList AnnualHolidayCategoryList { get; set; }
        public SelectList WeekendCategoryList { get; set; }
        public SelectList LeavePolicyList { get; set; }
    }
}
