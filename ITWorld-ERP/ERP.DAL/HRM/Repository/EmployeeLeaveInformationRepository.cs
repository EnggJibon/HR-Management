using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IEmployeeLeaveInformationRepository : IBaseRepository<EmployeeLeaveInformation>
    {
        EmployeeLeaveInformation GetLeaveInformation(long employeId);
        List<USP_GetEmployeeLeaveBalanceInformation_Result> GetEmployeeLeaveBalanceInformation(long employeId, string employeeCode);
    }

    public class EmployeeLeaveInformationRepository : BaseRepository<EmployeeLeaveInformation, ERP_HRM>, IEmployeeLeaveInformationRepository
    {
        private readonly ERP_HRM _dbContextHRM;

        public EmployeeLeaveInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _dbContextHRM = new ERP_HRM();
        }

        public EmployeeLeaveInformation GetLeaveInformation(long employeId)
        {
            return _dbContextHRM.EmployeeLeaveInformations.FirstOrDefault(l => l.EmployeeId==employeId);
        }

        public List<USP_GetEmployeeLeaveBalanceInformation_Result> GetEmployeeLeaveBalanceInformation(long employeId, string employeeCode)
        {
            var query = new StringBuilder();
            query.Append("EXEC [HRM].[USP_GetEmployeeLeaveBalanceInformation] ");
            query.Append("'" + employeId + "',");
            query.Append("'" + employeeCode + "'");

            string finalQuery = query.ToString().Contains("''")
                ? query.ToString().Replace("''", "NULL")
                : query.ToString();

            return _dbContextHRM.Database.SqlQuery<USP_GetEmployeeLeaveBalanceInformation_Result>(finalQuery).ToList();
        }

        public override IEnumerable<EmployeeLeaveInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
