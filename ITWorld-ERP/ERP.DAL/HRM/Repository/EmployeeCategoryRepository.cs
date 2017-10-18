using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IEmployeeCategoryRepository : IBaseRepository<EmployeeCategory>
    {

    }

    public class EmployeeCategoryRepository : BaseRepository<EmployeeCategory, ERP_HRM>, IEmployeeCategoryRepository
    {
        public EmployeeCategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<EmployeeCategory> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
