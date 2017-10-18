using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class DepartmentModel : BaseDomainModel<DepartmentModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
