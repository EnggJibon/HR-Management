using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IApplicationRepository : IBaseRepository<Application>
    {

    }

    public class ApplicationRepository : BaseRepository<Application, ERP_Security>, IApplicationRepository
    {
        public ApplicationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
