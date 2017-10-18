using System;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeLeaveRequestModel : BaseDomainModel<EmployeeLeaveRequestModel>
    {
        public long EmployeeId { get; set; }
        public DateTime RequestDate { get; set; }
        public long LeaveTypeId { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public byte DaysCount { get; set; }
        public string Purpose { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsAdjustment { get; set; }
        public DateTime? AdjustmentDate { get; set; }
        public long ApprovalStatusId { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public string ApprovalStatusName { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
