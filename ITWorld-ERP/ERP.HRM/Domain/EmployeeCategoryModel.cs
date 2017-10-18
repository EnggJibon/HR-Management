using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeCategoryModel : BaseDomainModel<EmployeeCategoryModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
