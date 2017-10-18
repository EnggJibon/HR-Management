namespace ERP.HRM.Domain
{
    public class EmployeeLeaveBalanceInformation
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public long LeavePolicyId { get; set; }
        public string LeavePolicyName { get; set; }
        public long LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
        public int? EnjoyedDays { get; set; }
        public int? RemainingDays { get; set; }
    }
}
