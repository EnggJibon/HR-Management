using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class AnnualHolidayCategoryModel : BaseDomainModel<AnnualHolidayCategoryModel>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
    }
}
