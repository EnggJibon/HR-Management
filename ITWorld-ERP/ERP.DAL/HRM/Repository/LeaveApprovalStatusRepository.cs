using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface ILeaveApprovalStatusRepository : IBaseRepository<LeaveApprovalStatu>
    {

    }

    public class LeaveApprovalStatusRepository : BaseRepository<LeaveApprovalStatu, ERP_HRM>, ILeaveApprovalStatusRepository
    {
        public LeaveApprovalStatusRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }


    }
}
