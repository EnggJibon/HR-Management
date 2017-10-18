using ERP.DAL.HRM;
using ERP.DAL.HRM.Repository;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.HRM
{


    public partial interface IEmployeeEducationalQualificationService : IBaseService<EmployeeEducationalQualificationModel, EmployeeEducationalQualification>
    {
    }



    public class EmployeeEducationalQualificationService : BaseService<EmployeeEducationalQualificationModel, EmployeeEducationalQualification>, IEmployeeEducationalQualificationService
    {

        private readonly IEmployeeEducationalQualificationRepository _employeeEducationalQualificationRepository;
        public EmployeeEducationalQualificationService(IEmployeeEducationalQualificationRepository employeeEducationalQualificationRepository)
            : base(employeeEducationalQualificationRepository)
        {


            _employeeEducationalQualificationRepository = employeeEducationalQualificationRepository;
        
        
        }



    }
}
