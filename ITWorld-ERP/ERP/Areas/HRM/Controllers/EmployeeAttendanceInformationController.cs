using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class EmployeeAttendanceInformationController : Controller
    {
        private readonly IEmployeeAttendanceInformationService _employeeAttendanceInformationService;
        private readonly IEmployeeService _employeeService;

        public EmployeeAttendanceInformationController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (karnel != null)
            {
                _employeeAttendanceInformationService = karnel.GetService(typeof(EmployeeAttendanceInformationService)) as EmployeeAttendanceInformationService;
                _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult Index()
        {
            var employeeAttendanceInformationView = new EmployeeAttendanceInformationViewModel()
            {
                EmployeeAttendanceMonthList = new SelectList(_employeeAttendanceInformationService.GetMonthList(), "Value", "Text"),
                EmployeeAttendanceYearList = new SelectList(_employeeAttendanceInformationService.GetYearList(), "Value", "Text")
            };
            return View(employeeAttendanceInformationView);
        }

        public JsonResult GetByEmployeeCode(string employeeCode)
        {
            var employeeInfo = _employeeService.GetEmployeeInformation(null, employeeCode);

            if (employeeInfo != null)
            {
                return new JsonResult { Data = new EmployeeAttendanceInformationModel { Employee = employeeInfo } };
            }
            return new JsonResult { Data = null };
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId, string date, long? month, long? year)
        { 
            if (month == 0)
            {
                month = null;
            }

            if (year == 0)
            {
                year = null;
            }
            var employeeAttendanceInformationList = _employeeAttendanceInformationService.GetEmployeeAttendanceInformationListWithDate(employeeId,date, month, year);
            return Json(employeeAttendanceInformationList.ToDataSourceResult(request));
        }
    }
}