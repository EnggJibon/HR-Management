using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IRosterInformationRepository : IBaseRepository<RosterInformation>
    {

    }

    public class RosterInformationRepository : BaseRepository<RosterInformation, ERP_HRM>, IRosterInformationRepository
    {
        public RosterInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<RosterInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
