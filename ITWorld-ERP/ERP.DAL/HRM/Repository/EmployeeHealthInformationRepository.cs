using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface IEmployeeHealthInformationRepository : IBaseRepository<EmployeeHealthInformation>
    {

    }


    public class EmployeeHealthInformationRepository : BaseRepository<EmployeeHealthInformation, ERP_HRM>, IEmployeeHealthInformationRepository
    {

        public EmployeeHealthInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<EmployeeHealthInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }

    }
}
