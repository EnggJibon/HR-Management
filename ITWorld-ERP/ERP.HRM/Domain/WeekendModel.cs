using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class WeekendModel : BaseDomainModel<WeekendModel>
    {
        public long WeekendCategoryId { get; set; }
        public string WeekendCategoryName { get; set; }
        public string Day { get; set; }
        public bool IsHalfDay { get; set; }
    }
}
