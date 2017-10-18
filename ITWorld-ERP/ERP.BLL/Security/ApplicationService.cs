using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IApplicationService : IBaseService<ApplicationModel, Application>
    {
    }

    public class ApplicationService : BaseService<ApplicationModel, Application>, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
            : base(applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
    }
}
