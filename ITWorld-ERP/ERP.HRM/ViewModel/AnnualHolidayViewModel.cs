using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
     public class AnnualHolidayViewModel
    {
         public AnnualHolidayModel AnnualHoliday { get; set; }
         public SelectList AnnualHolidayCategoryList { get; set; }
         public SelectList AnnualHolidayTypeList { get; set; }
         public SelectList AnnualHolidayDayList { get; set; }
    }
}
