using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class UserTypeModel : BaseDomainModel<UserTypeModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
