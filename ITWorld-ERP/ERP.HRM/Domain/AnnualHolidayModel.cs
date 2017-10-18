using System;
using ERP.Utilities.Infrastructure;


namespace ERP.HRM.Domain
{
    public class AnnualHolidayModel:BaseDomainModel<AnnualHolidayModel>
    {

        public long AnnualHolidayCategoryId { get; set; }
        public string AnnualHolidayCategoryTitle { get; set; }
        public DateTime? Date { get; set; }
        public string Day { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

    }
}
