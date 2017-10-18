using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{


    public partial interface IEmployeeHealthInformationService : IBaseService<EmployeeHealthInformationModel, EmployeeHealthInformation>
    {
    }



    public class EmployeeHealthInformationService : BaseService<EmployeeHealthInformationModel, EmployeeHealthInformation>, IEmployeeHealthInformationService
    {
        private readonly IEmployeeHealthInformationRepository _employeeHealthInformationRepository;

        public EmployeeHealthInformationService(IEmployeeHealthInformationRepository employeeHealthInformationRepository)
            : base(employeeHealthInformationRepository)
        {
            _employeeHealthInformationRepository = employeeHealthInformationRepository;
        }


    }
}
