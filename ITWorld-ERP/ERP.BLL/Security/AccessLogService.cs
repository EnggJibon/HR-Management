using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IAccessLogService : IBaseService<AccessLogModel, AccessLog>
    {
    }

    public class AccessLogService : BaseService<AccessLogModel, AccessLog>, IAccessLogService
    {
        private readonly IAccessLogRepository _accessLogRepository;

        public AccessLogService(IAccessLogRepository accessLogRepository)
            : base(accessLogRepository)
        {
            _accessLogRepository = accessLogRepository;
        }
    }
}
