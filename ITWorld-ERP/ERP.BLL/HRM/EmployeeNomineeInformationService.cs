using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{

    public partial interface IEmployeeNomineeInformationService : IBaseService<EmployeeNomineeInformationModel, EmployeeNomineeInformation>
    {
    }

    public class EmployeeNomineeInformationService: BaseService<EmployeeNomineeInformationModel, EmployeeNomineeInformation>, IEmployeeNomineeInformationService
    {
        private readonly IEmployeeNomineeInformationRepository _employeeNomineeInformationRepository;

        public EmployeeNomineeInformationService(IEmployeeNomineeInformationRepository employeeNomineeInformationRepository)
            : base(employeeNomineeInformationRepository)
        {
            _employeeNomineeInformationRepository = employeeNomineeInformationRepository;
        }

    }
}
