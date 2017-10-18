using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface ILeaveTypeService : IBaseService<LeaveTypeModel, LeaveType>
    {
    }

    public class LeaveTypeService : BaseService<LeaveTypeModel, LeaveType>, ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
            : base(leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
    }
}
