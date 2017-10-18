using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IEmployeeCategoryService : IBaseService<EmployeeCategoryModel, EmployeeCategory>
    {
    }

    public class EmployeeCategoryService : BaseService<EmployeeCategoryModel, EmployeeCategory>, IEmployeeCategoryService
    {
        private readonly IEmployeeCategoryRepository _employeeCategoryRepository;

        public EmployeeCategoryService(IEmployeeCategoryRepository employeeCategoryRepository)
            : base(employeeCategoryRepository)
        {
            _employeeCategoryRepository = employeeCategoryRepository;
        }
    }
}
