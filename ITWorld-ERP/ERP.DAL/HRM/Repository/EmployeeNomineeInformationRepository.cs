using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface IEmployeeNomineeInformationRepository : IBaseRepository<EmployeeNomineeInformation>
    {

    }

    public class EmployeeNomineeInformationRepository : BaseRepository<EmployeeNomineeInformation, ERP_HRM>, IEmployeeNomineeInformationRepository
    {

        public EmployeeNomineeInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<EmployeeNomineeInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }

    }
}
