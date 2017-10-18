using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IEmployeeRepository : IBaseRepository<Employee>
    {
        List<USP_GetEmployees_Result> GetAllEmployees();
        List<Employee> GetEmployees(long categoryId);
        bool IsApprover(long employeeId);

        USP_GetEmployeeDetails_Result GetEmployeeInformation(long? employeeId, string employeeCode);
    }

    public class EmployeeRepository : BaseRepository<Employee, ERP_HRM>, IEmployeeRepository
    {
        private readonly ERP_HRM _dbContextHRM;

        public EmployeeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _dbContextHRM = new ERP_HRM();
        }

        public List<USP_GetEmployees_Result> GetAllEmployees()
        {
            return _dbContextHRM.Database.SqlQuery<USP_GetEmployees_Result>("EXEC [HRM].[USP_GetEmployees]").ToList();
        }

        public List<Employee> GetEmployees(long categoryId)
        {
            return _dbContextHRM.Employees.Where(w => !w.IsDeleted && w.CategoryId == categoryId).ToList();
        }

        public USP_GetEmployeeDetails_Result GetEmployeeInformation(long? employeeId, string employeeCode)
        {
            var query = new StringBuilder();
            query.Append("EXEC [HRM].[USP_GetEmployeeDetails] ");
            query.Append("'" + employeeId + "', '" + employeeCode + "'");

            string finalQuery = query.ToString().Contains("''")
                ? query.ToString().Replace("''", "NULL")
                : query.ToString();

            return _dbContextHRM.Database.SqlQuery<USP_GetEmployeeDetails_Result>(finalQuery).FirstOrDefault();
        }

        public bool IsApprover(long employeeId)
        {
            return _dbContextHRM.Employees.Any(e => e.SupervisorId == employeeId);
        }

        public override IEnumerable<Employee> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
