using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IRoleRepository : IBaseRepository<Role>
    {

    }

    public class RoleRepository : BaseRepository<Role, ERP_Security>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
