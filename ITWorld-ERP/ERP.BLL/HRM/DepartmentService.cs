using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IDepartmentService : IBaseService<DepartmentModel, Department>
    {
    }

    public class DepartmentService : BaseService<DepartmentModel, Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
            : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
