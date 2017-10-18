using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IDepartmentRepository : IBaseRepository<Department>
    {

    }

    public class DepartmentRepository : BaseRepository<Department, ERP_HRM>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork): base(unitOfWork)
        {

        }

        public override IEnumerable<Department> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}

