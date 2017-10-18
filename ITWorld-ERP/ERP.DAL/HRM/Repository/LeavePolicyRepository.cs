using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface ILeavePolicyRepository : IBaseRepository<LeavePolicy>
    {

    }

    public class LeavePolicyRepository : BaseRepository<LeavePolicy, ERP_HRM>, ILeavePolicyRepository
    {
        public LeavePolicyRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<LeavePolicy> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
