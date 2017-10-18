using System.Collections.Generic;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
    public class LeaveRequestTypeViewModel
    {
        public IEnumerable<LeaveRequestTypeModel> LeaveRequestTypeList { get; set; }
        public LeaveRequestTypeModel LeaveRequestType { get; set; }
    }
}
