using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class WeekendCategoryModel : BaseDomainModel<WeekendCategoryModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
