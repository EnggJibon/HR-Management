using System.Collections.Generic;
using System.Linq;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.HRM.Repository
{
    public partial interface IPersonalInformationRepository : IBaseRepository<PersonalInformation>
    {

    }

    public class PersonalInformationRepository : BaseRepository<PersonalInformation, ERP_HRM>,
        IPersonalInformationRepository
    {
        public PersonalInformationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override IEnumerable<PersonalInformation> GetAll()
        {
            return base.GetAll().Where(w => !w.IsDeleted);
        }
    }
}
