using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
    public class EmployeeLeaveRequestViewModel
    {
        public EmployeeLeaveRequestModel EmployeeLeaveRequest { get; set; }

        public SelectList LeaveTypeList { get; set; }
        public SelectList LeaveApprovalStatusList { get; set; }
    }
}
