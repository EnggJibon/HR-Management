using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class LeaveApprovalStatusModel : BaseDomainModel<LeaveApprovalStatusModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
