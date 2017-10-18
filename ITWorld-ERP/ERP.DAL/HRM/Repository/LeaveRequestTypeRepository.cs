using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface ILeaveRequestTypeRepository : IBaseRepository<LeaveRequestType>
    {

    }
    public class LeaveRequestTypeRepository : BaseRepository<LeaveRequestType, ERP_HRM>, ILeaveRequestTypeRepository
    {
        public LeaveRequestTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
