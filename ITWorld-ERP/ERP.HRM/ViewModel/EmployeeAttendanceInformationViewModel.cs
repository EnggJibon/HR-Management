using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.HRM.Domain;
using ERP.Utilities.Infrastructure;
using System.Web.Mvc;
using ERP.HRM.Domain;

namespace ERP.HRM.ViewModel
{
    public class EmployeeAttendanceInformationViewModel
    {

        public EmployeeAttendanceInformationModel EmployeeAttendanceInformation { get; set; }

        public IEnumerable<EmployeeAttendanceInformationModel> EmployeeAttendanceInformations { get; set; }

        public SelectList EmployeeAttendanceMonthList { get; set; }
        public SelectList EmployeeAttendanceYearList { get; set; }



    }
}
