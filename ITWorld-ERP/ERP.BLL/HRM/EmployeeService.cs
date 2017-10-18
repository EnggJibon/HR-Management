using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure.Contract;
using ERP.Utilities.Infrastructure;

namespace ERP.BLL.HRM
{
    public partial interface IEmployeeService : IBaseService<EmployeeModel, Employee>
    {
        List<EmployeeModel> GetAllEmployees();
        List<EmployeeModel> GetEmployees(long categoryId);
        EmployeeModel GetEmployeeDetails(long employeeId);
        EmployeeModel GetEmployeeInformation(long? employeeId, string employeeCode);
        bool IsApprover(long employeeId);
    }

    public class EmployeeService : BaseService<EmployeeModel, Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPersonalInformationService _personalInformationService;
        private readonly IAddressService _addressService;

        public EmployeeService(IEmployeeRepository employeeRepository, IPersonalInformationService personalInformationService, IAddressService addressService)
            : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _personalInformationService = personalInformationService;
            _addressService = addressService;
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            var employeeList = _employeeRepository.GetAllEmployees();
            return Mapper.Map<List<EmployeeModel>>(employeeList);
        }

        public List<EmployeeModel> GetEmployees(long categoryId)
        {
            var employeeList = _employeeRepository.GetEmployees(categoryId);
            return Mapper.Map<List<EmployeeModel>>(employeeList);
        } 

        public EmployeeModel GetEmployeeDetails(long employeeId)
        {
            var employeeDetails = GetById(employeeId);
            
            if (employeeDetails != null)
            {
                employeeDetails.PersonalInformation = _personalInformationService.GetAll().SingleOrDefault(p => p.EmployeeId == employeeDetails.Id);

                if (employeeDetails.PersonalInformation != null)
                {
                    employeeDetails.PersonalInformation.Address = _addressService.GetAll().SingleOrDefault(a => a.PersonId == employeeDetails.PersonalInformation.Id);
                }
            }
            return employeeDetails;
        }

        public EmployeeModel GetEmployeeInformation(long? employeeId, string employeeCode)
        {
            var employee = _employeeRepository.GetEmployeeInformation(employeeId, employeeCode);
            return Mapper.Map<EmployeeModel>(employee);
        }

        public bool IsApprover(long employeeId)
        {
            return _employeeRepository.IsApprover(employeeId);
        }
    }
}
