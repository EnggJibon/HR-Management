using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface IEmployeeEducationalQualificationRepository : IBaseRepository<EmployeeEducationalQualification>
    {

    }

    public class EmployeeEducationalQualificationRepository : BaseRepository<EmployeeEducationalQualification, ERP_HRM>, IEmployeeEducationalQualificationRepository
    {

        public EmployeeEducationalQualificationRepository(IUnitOfWork unitOfWork): base(unitOfWork)
        { 
        
        }


        public override IEnumerable<EmployeeEducationalQualification> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }


    }
}
