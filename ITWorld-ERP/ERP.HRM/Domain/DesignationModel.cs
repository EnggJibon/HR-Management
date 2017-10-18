using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class DesignationModel : BaseDomainModel<DesignationModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
