using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{

    public partial interface ILeaveRequestTypeService : IBaseService<LeaveRequestTypeModel, LeaveRequestType>
    {
    }

    public class LeaveRequestTypeService : BaseService<LeaveRequestTypeModel, LeaveRequestType>, ILeaveRequestTypeService
    {

        private readonly ILeaveRequestTypeRepository _leaveRequestTypeRepository;

        public LeaveRequestTypeService(ILeaveRequestTypeRepository leaveRequestTypeRepository)
            : base(leaveRequestTypeRepository)
        {
            _leaveRequestTypeRepository = leaveRequestTypeRepository;
        }

    }
}
