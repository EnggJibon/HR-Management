using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface ILeaveApprovalStatusService : IBaseService<LeaveApprovalStatusModel, LeaveApprovalStatu>
    {
    }

    public class LeaveApprovalStatusService : BaseService<LeaveApprovalStatusModel, LeaveApprovalStatu>, ILeaveApprovalStatusService
    {
        private readonly ILeaveApprovalStatusRepository _leaveApprovalStatusRepository;

        public LeaveApprovalStatusService(ILeaveApprovalStatusRepository leaveApprovalStatusRepository)
            : base(leaveApprovalStatusRepository)
        {
            _leaveApprovalStatusRepository = leaveApprovalStatusRepository;
        }
    }
}
