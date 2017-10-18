using System.Collections.Generic;
using AutoMapper;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{
    public partial interface IEmployeeLeaveInformationService : IBaseService<EmployeeLeaveInformationModel, EmployeeLeaveInformation>
    {
        EmployeeLeaveInformationModel GetLeaveInformation(long employeId);
        List<EmployeeLeaveBalanceInformation> GetEmployeeLeaveBalanceInformation(long employeeId);
    }

    public class EmployeeLeaveInformationService : BaseService<EmployeeLeaveInformationModel, EmployeeLeaveInformation>, IEmployeeLeaveInformationService
    {
        private readonly IEmployeeLeaveInformationRepository _employeeLeaveInformationRepository;

        public EmployeeLeaveInformationService(IEmployeeLeaveInformationRepository employeeLeaveInformationRepository)
            : base(employeeLeaveInformationRepository)
        {
            _employeeLeaveInformationRepository = employeeLeaveInformationRepository;
        }

        public EmployeeLeaveInformationModel GetLeaveInformation(long employeId)
        {
            var leaveInfo = _employeeLeaveInformationRepository.GetLeaveInformation(employeId);
            return Mapper.Map<EmployeeLeaveInformationModel>(leaveInfo);
        }

        public List<EmployeeLeaveBalanceInformation> GetEmployeeLeaveBalanceInformation(long employeeId)
        {
            var employeeLeaveInformation = _employeeLeaveInformationRepository.GetEmployeeLeaveBalanceInformation(employeeId, null);
            return Mapper.Map<List<EmployeeLeaveBalanceInformation>>(employeeLeaveInformation);
        }
    }
}
