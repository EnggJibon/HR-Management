using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class LeaveTypeModel : BaseDomainModel<LeaveTypeModel>
    {
        public long LeavePolicyId { get; set; }
        public string LeavePolicyPolicyName { get; set; }
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
        public string Description { get; set; }
        public int EnjoyedDays { get; set; }
        public int RemainingDays { get; set; }

        //public LeavePolicyModel LeavePolicy { get; set; }
    }
}
