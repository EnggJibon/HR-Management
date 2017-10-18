using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class ApplicationPolicyModel : BaseDomainModel<ApplicationPolicyModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ApplicationId { get; set; }

        public ApplicationModel Application { get; set; }
    }
}
