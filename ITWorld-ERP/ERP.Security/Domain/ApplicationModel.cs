using ERP.Utilities.Infrastructure;

namespace ERP.Security.Domain
{
    public class ApplicationModel : BaseDomainModel<ApplicationModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
