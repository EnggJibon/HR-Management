using System.Collections.Generic;
using AutoMapper;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IEmployeeLeaveRequestService : IBaseService<EmployeeLeaveRequestModel, EmployeeLeaveRequest>
    {
        List<EmployeeLeaveRequestModel> GetEmployeeLeaveRequests(long approverId);
        void UpdateApprovalStatus(EmployeeLeaveRequestModel employeeLeaveRequest);
    }

    public class EmployeeLeaveRequestService : BaseService<EmployeeLeaveRequestModel, EmployeeLeaveRequest>, IEmployeeLeaveRequestService
    {

        private readonly IEmployeeLeaveRequestRepository _employeeLeaveRequestRepository;
        public EmployeeLeaveRequestService(IEmployeeLeaveRequestRepository leaveRequestRepository)
            : base(leaveRequestRepository)
        {
            _employeeLeaveRequestRepository = leaveRequestRepository;
        }

        public List<EmployeeLeaveRequestModel> GetEmployeeLeaveRequests(long approverId)
        {
            var leaveRequests = _employeeLeaveRequestRepository.GetEmployeeLeaveRequests(approverId);
            return Mapper.Map<List<EmployeeLeaveRequestModel>>(leaveRequests);
        }

        public void UpdateApprovalStatus(EmployeeLeaveRequestModel employeeLeaveRequest)
        {
            _employeeLeaveRequestRepository.UpdateApprovalStatus(Mapper.Map<EmployeeLeaveRequest>(employeeLeaveRequest));
        }
    }
}
