using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface IEmployeeLeaveRequestRepository : IBaseRepository<EmployeeLeaveRequest>
    {
        List<EmployeeLeaveRequest> GetEmployeeLeaveRequests(long approverId);
        void UpdateApprovalStatus(EmployeeLeaveRequest employeeLeaveRequest);
    }

    public class EmployeeLeaveRequestRepository : BaseRepository<EmployeeLeaveRequest, ERP_HRM>, IEmployeeLeaveRequestRepository
    {
        private readonly ERP_HRM _dbContextHRM;

        public EmployeeLeaveRequestRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _dbContextHRM = new ERP_HRM();
        }

        public override IEnumerable<EmployeeLeaveRequest> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }

        public void UpdateApprovalStatus(EmployeeLeaveRequest employeeLeaveRequest)
        {
            const string command = "UPDATE [HRM].EmployeeLeaveRequest SET ApprovalStatusId = {0}, ApprovalDate = {1} WHERE Id = {2} AND EmployeeId = {3}";
            _dbContextHRM.Database.ExecuteSqlCommand(command, employeeLeaveRequest.ApprovalStatusId, employeeLeaveRequest.ApprovalDate, employeeLeaveRequest.Id, employeeLeaveRequest.EmployeeId);
            _dbContextHRM.SaveChanges();
        }

        public List<EmployeeLeaveRequest> GetEmployeeLeaveRequests(long approverId)
        {
            var listLeaveRequests = (from elr in _dbContextHRM.EmployeeLeaveRequests
                                     join e in _dbContextHRM.Employees on elr.EmployeeId equals e.Id
                                     where e.SupervisorId == approverId || e.ApproverId == approverId
                                     select elr).ToList();

            return listLeaveRequests;
        }
    }
}
