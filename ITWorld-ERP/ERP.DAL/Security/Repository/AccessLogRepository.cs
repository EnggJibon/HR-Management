using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.DAL.Security.Repository
{
    public partial interface IAccessLogRepository : IBaseRepository<AccessLog>
    {

    }

    public class AccessLogRepository : BaseRepository<AccessLog, ERP_Security>, IAccessLogRepository
    {
        public AccessLogRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
