using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface ILeaveTypeRepository : IBaseRepository<LeaveType>
    {

    }

    public class LeaveTypeRepository : BaseRepository<LeaveType, ERP_HRM>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
