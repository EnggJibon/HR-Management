using System.Collections.Generic;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class ApplicationPolicyViewModel
    {
        public IEnumerable<ApplicationPolicyModel> ApplicationPolicies { get; set; }
        public ApplicationPolicyModel ApplicationPolicy { get; set; }
    }
}
