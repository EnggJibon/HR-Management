using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeLeaveInformationModel : BaseDomainModel<EmployeeLeaveInformationModel>
    {
        public long EmployeeId { get; set; }
        public long AnnualHolidayCategoryId { get; set; }
        public long WeekendCategoryId { get; set; }
        public long LeavePolicyId { get; set; }

        public LeavePolicyModel LeavePolicy { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
