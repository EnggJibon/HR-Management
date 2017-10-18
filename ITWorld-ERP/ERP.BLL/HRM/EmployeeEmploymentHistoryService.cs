using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{

    public partial interface IEmployeeEmploymentHistoryService : IBaseService<EmployeeEmploymentHistoryModel, EmployeeEmploymentHistory>
    {
    }


    public class EmployeeEmploymentHistoryService : BaseService<EmployeeEmploymentHistoryModel, EmployeeEmploymentHistory>, IEmployeeEmploymentHistoryService
    {

        private readonly IEmployeeEmploymentHistoryRepository _employeeEmploymentHistoryRepository;

        public EmployeeEmploymentHistoryService(IEmployeeEmploymentHistoryRepository employeeEmploymentHistoryRepository)
            : base(employeeEmploymentHistoryRepository)
        {
            _employeeEmploymentHistoryRepository = employeeEmploymentHistoryRepository;
        }



    }
}
