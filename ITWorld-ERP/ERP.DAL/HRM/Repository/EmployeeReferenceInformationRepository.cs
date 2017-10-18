using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{

    public partial interface IEmployeeReferenceInformationRepository : IBaseRepository<EmployeeReferenceInformation>
    {

    }


    public class EmployeeReferenceInformationRepository : BaseRepository<EmployeeReferenceInformation, ERP_HRM>, IEmployeeReferenceInformationRepository
    {


        public EmployeeReferenceInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }


        public override IEnumerable<EmployeeReferenceInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
