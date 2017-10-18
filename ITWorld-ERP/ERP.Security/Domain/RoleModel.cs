using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class RoleModel : BaseDomainModel<RoleModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
