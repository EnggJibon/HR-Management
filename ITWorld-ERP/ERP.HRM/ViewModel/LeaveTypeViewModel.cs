using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
    public class LeaveTypeViewModel
    {
        public LeaveTypeModel LeaveType { get; set; }

        public SelectList LeavePolicyList { get; set; }
    }
}
