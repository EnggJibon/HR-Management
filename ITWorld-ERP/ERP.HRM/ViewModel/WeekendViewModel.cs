using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
     public class WeekendViewModel
    {
        public WeekendModel Weekend { get; set; }

        public SelectList WeekendHolidayDayList { get; set; }
        public SelectList WeekendCategoryList { get; set; }
    }
}
