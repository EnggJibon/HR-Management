using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IEmployeeEmploymentHistoryRepository : IBaseRepository<EmployeeEmploymentHistory>
    {

    }

    public class EmployeeEmploymentHistoryRepository : BaseRepository<EmployeeEmploymentHistory, ERP_HRM>, IEmployeeEmploymentHistoryRepository
    {

        public EmployeeEmploymentHistoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<EmployeeEmploymentHistory> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
