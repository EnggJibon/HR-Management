using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IUserTypeRepository : IBaseRepository<UserType>
    {

    }

    public class UserTypeRepository : BaseRepository<UserType, ERP_Security>, IUserTypeRepository
    {
        public UserTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
