using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IApplicationPolicyRepository : IBaseRepository<ApplicationPolicy>
    {

    }

    public class ApplicationPolicyRepository : BaseRepository<ApplicationPolicy, ERP_Security>, IApplicationPolicyRepository
    {
        public ApplicationPolicyRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
