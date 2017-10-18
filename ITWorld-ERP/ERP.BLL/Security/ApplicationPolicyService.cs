using ERP.DAL.Security;
using ERP.DAL.Security.Repository;
using ERP.Security.Domain;
using ERP.Utilities.Infrastructure;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.BLL.Security
{
    public partial interface IApplicationPolicyService : IBaseService<ApplicationPolicyModel, ApplicationPolicy>
    {
    }

    public class ApplicationPolicyService : BaseService<ApplicationPolicyModel, ApplicationPolicy>, IApplicationPolicyService
    {
        private readonly IApplicationPolicyRepository _applicationPolicyRepository;

        public ApplicationPolicyService(IApplicationPolicyRepository applicationPolicyRepository)
            : base(applicationPolicyRepository)
        {
            _applicationPolicyRepository = applicationPolicyRepository;
        }
    }
}
